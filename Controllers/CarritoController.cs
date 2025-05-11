using Microsoft.AspNetCore.Mvc;

public class CarritoController : Controller
{
    private readonly IProductoRepository productoRepository;

    public CarritoController(IProductoRepository productoRepository)
    {
        this.productoRepository = productoRepository;
    }

    public ActionResult Index()
    {
        try
        {
            var carrito = HttpContext.Session.GetObject<Carrito>("Carrito") ?? new Carrito();
            
            return View(carrito);

        }
        catch (Exception ex)
        {
            return StatusCode(500, "Ocurrió un error inesperado en el servidor.");
        }
    }

    [HttpPost]
    public IActionResult Agregar([FromBody] ProductoCarritoDTO datos)
    {

        try
        {
            var producto = productoRepository.obtenerProductoPorId(datos.ProductoId);
            if (producto is null)
            {
                return Json(new { ok = false });
            }

            var carrito = HttpContext.Session.GetObject<Carrito>("Carrito") ?? new Carrito();

            // Buscar la variante si hay ID
            VarianteProducto? variante = null;
            if (datos.VarianteId.HasValue)
            {
                variante = producto.VariantesProducto.FirstOrDefault(v => v.Id == datos.VarianteId.Value);
                if (variante == null)
                {
                    return Json(new { ok = false, error = "Variante no encontrada" });
                }
            }

            // Usar una clave única por combinación ProductoId + VarianteId
            var item = carrito.Items.FirstOrDefault(i =>
                i.ProductoId == datos.ProductoId &&
                i.VarianteId == datos.VarianteId);

            if (item != null)
            {
                item.Cantidad++;
            }
            else
            {
                carrito.Items.Add(new CarritoItem
                {
                    ProductoId = producto.IdProducto,
                    VarianteId = variante?.Id,
                    Nombre = variante != null ? $"{producto.Nombre} ({variante.Nombre})" : producto.Nombre,
                    Precio = variante?.Precio ?? producto.PrecioBase ?? 0,
                    Cantidad = 1,
                    ImagenUrl = producto.Img
                });
            }

            HttpContext.Session.SetObject("Carrito", carrito);
            return Json(new { ok = true, cantidadTotal = carrito.Items.Sum(i => i.Cantidad) });

        }
        catch (Exception ex)
        {
            return Json(new { ok = false, mensaje = "Ocurrió un error inesperado" });
        }
    }
}