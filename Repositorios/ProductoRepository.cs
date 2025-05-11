using Npgsql;

public class ProductoRepository : IProductoRepository
{

    private readonly string _ConnectionString;

    public ProductoRepository(string connectionString)
    {
        _ConnectionString = connectionString;
    }

    public List<Producto> ListarProductos()
    {

        var productos = new List<Producto>();
        var productosMap = new Dictionary<int, Producto>();


        string consulta = @"SELECT * FROM producto p LEFT JOIN variante v ON v.producto_id = p.id;";

        using (var connection = new NpgsqlConnection(_ConnectionString))
        {
            connection.Open();

            var command = new NpgsqlCommand(consulta, connection);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int productoId = Convert.ToInt32(reader["p.id"]);

                    if (!productosMap.ContainsKey(productoId))
                    {
                        var producto = new Producto
                        {
                            IdProducto = productoId,
                            Nombre = reader["p.nombre"].ToString(),
                            Descripcion = reader["p.descripcion"].ToString(),
                            Img = reader["p.img"].ToString(),
                            PrecioBase = reader["p.precio_base"] == DBNull.Value ? null : Convert.ToDecimal(reader["p.precio_base"]),
                            Visible = Convert.ToBoolean(reader["p.visible"]),
                            PublicId = reader["p.public_id"].ToString(),
                            CategoriaId = Convert.ToInt32(reader["p.categoria_id"]),
                            VariantesProducto = new List<VarianteProducto>()
                        };

                        productosMap[productoId] = producto;
                        productos.Add(producto);
                    }

                    if (reader["v.id"] != DBNull.Value)
                    {
                        var variante = new VarianteProducto
                        {
                            Id = Convert.ToInt32(reader["v.id"]),
                            Nombre = reader["v.nombre"].ToString(),
                            Precio = Convert.ToDecimal(reader["v.precio"]),
                            ProductoId = productoId
                        };

                        productosMap[productoId].VariantesProducto.Add(variante);
                    }
                }
            }

            connection.Close();
        }

        return productos;
    }

    public List<Producto> obtenerProductoPorCategoria(int id)
    {

        var productos = new List<Producto>();
        var productosMap = new Dictionary<int, Producto>();


        string consulta = @"SELECT p.id AS producto_id,
    p.nombre AS producto_nombre,
    p.descripcion AS producto_descripcion,
    p.precio_base,
    p.visible,
    p.img,
    p.public_id,
    p.categoria_id,
    v.id AS variante_id,
    v.nombre AS variante_nombre,
    v.precio AS variante_precio FROM producto p LEFT JOIN variante v ON v.producto_id = p.id WHERE p.categoria_id = @id;";

        using (var connection = new NpgsqlConnection(_ConnectionString))
        {
            connection.Open();

            var command = new NpgsqlCommand(consulta, connection);

            command.Parameters.AddWithValue("@id", id);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int productoId = Convert.ToInt32(reader["producto_id"]);

                    if (!productosMap.ContainsKey(productoId))
                    {
                        var producto = new Producto
                        {
                            IdProducto = productoId,
                            Nombre = reader["producto_nombre"].ToString(),
                            Descripcion = reader["producto_descripcion"].ToString(),
                            Img = reader["img"].ToString(),
                            PrecioBase = reader["precio_base"] == DBNull.Value ? null : Convert.ToDecimal(reader["precio_base"]),
                            Visible = Convert.ToBoolean(reader["visible"]),
                            PublicId = reader["public_id"].ToString(),
                            CategoriaId = Convert.ToInt32(reader["categoria_id"]),
                            VariantesProducto = new List<VarianteProducto>()
                        };

                        productosMap[productoId] = producto;
                        productos.Add(producto);
                    }

                    if (reader["variante_id"] != DBNull.Value)
                    {
                        var variante = new VarianteProducto
                        {
                            Id = Convert.ToInt32(reader["variante_id"]),
                            Nombre = reader["variante_nombre"].ToString(),
                            Precio = Convert.ToDecimal(reader["variante_precio"]),
                            ProductoId = productoId
                        };

                        productosMap[productoId].VariantesProducto.Add(variante);
                    }
                }
            }

            connection.Close();
        }

        return productos;
    }

    public Producto obtenerProductoPorId(int id)
    {

        var producto = new Producto();
        var productosMap = new Dictionary<int, Producto>();


        string consulta = @"SELECT p.id AS producto_id,
    p.nombre AS producto_nombre,
    p.descripcion AS producto_descripcion,
    p.precio_base,
    p.visible,
    p.img,
    p.public_id,
    p.categoria_id,
    v.id AS variante_id,
    v.nombre AS variante_nombre,
    v.precio AS variante_precio FROM producto p LEFT JOIN variante v ON v.producto_id = p.id WHERE p.id = @id;";

        using (var connection = new NpgsqlConnection(_ConnectionString))
        {
            connection.Open();

            var command = new NpgsqlCommand(consulta, connection);

            command.Parameters.AddWithValue("@id", id);

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int productoId = Convert.ToInt32(reader["producto_id"]);

                    if (!productosMap.ContainsKey(productoId))
                    {
                        producto = new Producto
                        {
                            IdProducto = productoId,
                            Nombre = reader["producto_nombre"].ToString(),
                            Descripcion = reader["producto_descripcion"].ToString(),
                            Img = reader["img"].ToString(),
                            PrecioBase = reader["precio_base"] == DBNull.Value ? null : Convert.ToDecimal(reader["precio_base"]),
                            Visible = Convert.ToBoolean(reader["visible"]),
                            PublicId = reader["public_id"].ToString(),
                            CategoriaId = Convert.ToInt32(reader["categoria_id"]),
                            VariantesProducto = new List<VarianteProducto>()
                        };

                        productosMap[productoId] = producto;
                    }

                    if (reader["variante_id"] != DBNull.Value)
                    {
                        var variante = new VarianteProducto
                        {
                            Id = Convert.ToInt32(reader["variante_id"]),
                            Nombre = reader["variante_nombre"].ToString(),
                            Precio = Convert.ToDecimal(reader["variante_precio"]),
                            ProductoId = productoId
                        };

                        productosMap[productoId].VariantesProducto.Add(variante);
                    }
                }
            }

            connection.Close();
        }

        return producto;
    }

}