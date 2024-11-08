using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using tl2_tp5_2024_miguell29.Models;

namespace tl2_tp5_2024_miguell29.Repository
{
    public class ProductoRepository : IProductoRepository
    {
        private string _stringConnection = @"Data Source=db\Tienda.db;Cache=Shared";
        public List<Producto> GetProductos()
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
    }
}