public interface IProductoRepository
{
    List<Producto> obtenerProductoPorCategoria(int id);
    List<Producto> ListarProductos();
    Producto obtenerProductoPorId(int id);
}