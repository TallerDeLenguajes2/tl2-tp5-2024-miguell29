using Microsoft.Data.Sqlite;
using tl2_tp5_2024_miguell29.Models;

namespace tl2_tp5_2024_miguell29.Repository
{
    public class ProductoRepository : IProductoRepository
    {
        private string _stringConnection = @"Data Source=db\Tienda.db;Cache=Shared";

        

        public void NewProduct(Producto producto)
        {
            string query = @"INSERT INTO Productos (Descripcion, Precios)
                            VALUES (@descripcion, @precio)";
            using (SqliteConnection connection = new SqliteConnection(_stringConnection))
            {
                SqliteCommand command = new SqliteCommand(query,connection);
                command.Parameters.AddWithValue("@descripcion",producto.Descripcion);
                command.Parameters.AddWithValue("@precio",producto.Precio);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void UpdateProduct(int id, Producto producto)
        {
            string query = @"UPDATE Poductos SET
                            Descripcion = @descripcion,
                            Precio = @precio
                            WHERE idProducto = @id";
            using (SqliteConnection connection = new SqliteConnection(_stringConnection))
            {
                SqliteCommand command = new SqliteCommand(query,connection);
                command.Parameters.AddWithValue("@descripcion",producto.Descripcion);
                command.Parameters.AddWithValue("@precio",producto.Precio);
                command.Parameters.AddWithValue("@id",id);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }
        public List<Producto> GetProducts()
        {
            var list = new List<Producto>();
            string query = "SELECT idProducto, Descripcion, Precio FROM Productos";
            using (SqliteConnection connection = new SqliteConnection(_stringConnection))
            {
                SqliteCommand command = new SqliteCommand(query,connection);
                connection.Open();
                using (SqliteDataReader reader = command.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        var producto = new Producto(){
                            Id = Convert.ToInt32(reader["idProducto"]),
                            Descripcion = reader["Descripcion"].ToString(),
                            Precio = Convert.ToInt32(reader["Precio"])
                        };
                        list.Add(producto);
                    }
                }
                connection.Close();  
            }
            
            return list;
        }

        public Producto GetProductById(int id)
        {
            Producto producto = new Producto();
            string query = @"SELECT idProducto, Descipcion, Precio 
                            FROM Productos 
                            WHERE idProducto = @id";
            using (SqliteConnection connection = new SqliteConnection(_stringConnection))
            {
                SqliteCommand command = new SqliteCommand(query,connection);
                command.Parameters.AddWithValue("@id", id);
                SqliteDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    producto.Id = Convert.ToInt32(reader["idProducto"]);
                    producto.Descripcion = reader["Descripcion"].ToString();
                    producto.Precio = Convert.ToInt32(reader["Precio"]);
                }
                connection.Close();
            }
            return producto;
        }
        public void DeleteProduct(int id)
        {
            string query = @"DELETE FROM Productos WHERE idProducto = @id";
            using (SqliteConnection connection = new SqliteConnection(_stringConnection))
            {
                SqliteCommand command = new SqliteCommand(query, connection);
                command.Parameters.AddWithValue("@id",id);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

    }
}