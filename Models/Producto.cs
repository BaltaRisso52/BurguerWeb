public class Producto
{
    private int idProducto;
    private string nombre;
    private string descripcion;
    private string img;
    private decimal? precioBase;
    private List<VarianteProducto>? varianteProductos;
    private bool visible;
    private string publicId;
    private int categoriaId;

    public int IdProducto { get => idProducto; set => idProducto = value; }
    public string Nombre { get => nombre; set => nombre = value; }
    public string Descripcion { get => descripcion; set => descripcion = value; }
    public string Img { get => img; set => img = value; }
    public bool Visible { get => visible; set => visible = value; }
    public string PublicId { get => publicId; set => publicId = value; }
    public int CategoriaId { get => categoriaId; set => categoriaId = value; }
    public List<VarianteProducto>? VariantesProducto { get => varianteProductos; set => varianteProductos = value; }
    public decimal? PrecioBase { get => precioBase; set => precioBase = value; }
}

public class VarianteProducto
{
    public int Id { get; set; }
    public string Nombre { get; set; }         // Ej: "Doble", "Triple"
    public decimal Precio { get; set; }

    public int ProductoId { get; set; }
}