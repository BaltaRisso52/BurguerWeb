using Npgsql;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly string _ConnectionString;

    public CategoriaRepository(string connectionString)
    {
        _ConnectionString = connectionString;
    }

    public void crearCategoria(Categoria categoria){
        
        string consulta = @"INSERT INTO categoria (nombre) VALUES (@nombre);";

        using (var connection = new NpgsqlConnection(_ConnectionString))
        {
            connection.Open();

            var command = new NpgsqlCommand(consulta, connection);

            command.Parameters.AddWithValue("@nombre", categoria.Nombre);

            command.ExecuteNonQuery();

            connection.Close();
        }
    }

    public List<Categoria> ListarCategorias(){

        List<Categoria> categorias = new();

        string consulta = @"SELECT * FROM categoria";

        using (var connection = new NpgsqlConnection(_ConnectionString))
        {
            connection.Open();

            var command = new NpgsqlCommand(consulta, connection);

            using (var reader = command.ExecuteReader())
            {

                while (reader.Read())
                {
                    Categoria categoria = new();

                    categoria.Id = Convert.ToInt32(reader["id"]);
                    categoria.Nombre = reader["nombre"].ToString();

                    categorias.Add(categoria);
                }
            }

            connection.Close();
        }

        return categorias;
    }
}