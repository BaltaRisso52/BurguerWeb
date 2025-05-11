using Microsoft.AspNetCore.Mvc;

public class ProductoController : Controller
{
    private readonly IProductoRepository productoRepository;

    public ProductoController(IProductoRepository productoRepository)
    {
        this.productoRepository = productoRepository;
    }

    [HttpGet]
    public ActionResult Index(int pagina = 1, int tamanoPagina = 9){
        try
        {
            bool EsAdmin = false;

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("User")))
            {
                ViewData["EsAdmin"] = true;
                EsAdmin = true;
            }

            int totalProductos = productoRepository.ListarProductos().Count;
            var productos = productoRepository.ListarProductos()
                .OrderBy(p => p.Nombre) // Ordenar por nombre, puedes cambiarlo
                .Skip((pagina - 1) * tamanoPagina)
                .Take(tamanoPagina)
                .ToList();

            if (!EsAdmin)
            {
                productos = productos.Where(p => p.Visible).ToList();
            }

            ViewData["PaginaActual"] = pagina;
            ViewData["TotalPaginas"] = (int)Math.Ceiling(totalProductos / (double)tamanoPagina);

            return View(productos);

        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocurrió un error inesperado en el servidor. " + ex.ToString());
        }
    }

    [HttpGet]
    public ActionResult PorCategoria(int id,int pagina = 1, int tamanoPagina = 9){
        try
        {
            bool EsAdmin = false;

            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("User")))
            {
                ViewData["EsAdmin"] = true;
                EsAdmin = true;
            }

            
            var productos = productoRepository.obtenerProductoPorCategoria(id)
                .OrderBy(p => p.Nombre) // Ordenar por nombre, puedes cambiarlo
                .Skip((pagina - 1) * tamanoPagina)
                .Take(tamanoPagina)
                .ToList();

            if (!EsAdmin)
            {
                productos = productos.Where(p => p.Visible).ToList();
            }

            ViewData["PaginaActual"] = pagina;
            ViewData["TotalPaginas"] = (int)Math.Ceiling(productos.Count / (double)tamanoPagina);

            return View("Index", productos);

        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocurrió un error inesperado en el servidor. " + ex.ToString());
        }

    }
}