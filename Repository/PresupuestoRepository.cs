using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using tl2_tp5_2024_miguell29.Models;

namespace tl2_tp5_2024_miguell29.Repository
{
    public class PresupuestoRepository : IPresupuestoRepository
    {
        private string _stringConnection = @"Data Source=db\Tienda.db;Cache=Shared";
    //! Originalmente este metodo debia recibir como parametro un id
    //! pero me parecio mas logico que tambien reciba el producto y la cantidad.
        public void AddProductToPresupuesto(int id,Producto producto, int cantidad)
        {
            string query = @"INSERT INTO PresupuestosDetalle (idPresupuesto, idProducto, Cantidad) 
                                            VALUES (@idPresupuesto, @idProducto, @cantidad)";
            using (SqliteConnection connection = new SqliteConnection(_stringConnection))
            {
                connection.Open();

                SqliteCommand commandDetalles = new SqliteCommand(query,connection);
                commandDetalles.Parameters.AddWithValue("@idPresupuesto",id);
                commandDetalles.Parameters.AddWithValue("@idProducto", producto.Id);
                commandDetalles.Parameters.AddWithValue("@cantidad", cantidad);
                commandDetalles.ExecuteNonQuery();
                
                connection.Close();
            }
        }

        public void CreatePresupuesto(Presupuesto presupuesto)
        {
            //* El id se genera automaticamente en la base de datos
            string queryPresupuesto = @"INSERT INTO Presupuestos (NombreDestinatario, FechaCreacion)
                                        VALUES (@nombre, @fecha)";
            using (SqliteConnection connection = new SqliteConnection(_stringConnection))
            {
                SqliteCommand command = new SqliteCommand(queryPresupuesto, connection);
                command.Parameters.AddWithValue("@nombre", presupuesto.NombreDestinatario);
                command.Parameters.AddWithValue("@fecha",DateTime.Now.ToString("yyyy-MM-dd"));
                connection.Open();
                command.ExecuteNonQuery();
                int idPresupuesto;
                using (var commandId = new SqliteCommand("SELECT last_insert_rowid()", connection))//!Obtiene el ultimo idPresupesto
                {
                    idPresupuesto = Convert.ToInt32(commandId.ExecuteScalar());
                }
                connection.Close();
                if (presupuesto.Detalle.Count() > 0)
                {
                    foreach (var detalle in presupuesto.Detalle)
                    {
                        if (detalle.Producto.Id == 0) continue;
                        AddProductToPresupuesto(idPresupuesto, detalle.Producto,detalle.Cantidad);
                    }
                }
            }
        }

        public void DeletePresupuesto(int id)
        {
            string queryPresupuesto = "DELETE FROM Presupuestos WHERE idPresupuesto = @id";
            string queryPresupuestoDetalle = "DELETE FROM PresupuestosDetalles WHERE idpresupuesto = @id";
            using (SqliteConnection connection = new SqliteConnection(_stringConnection))
            {
                SqliteCommand commandPresupuesto = new SqliteCommand(queryPresupuesto,connection);
                connection.Open();
                commandPresupuesto.Parameters.AddWithValue("@id", id);
                commandPresupuesto.ExecuteNonQuery();
                SqliteCommand commandDetalles = new SqliteCommand(queryPresupuestoDetalle, connection);
                commandDetalles.Parameters.AddWithValue("id", id);
                commandDetalles.ExecuteNonQuery();
                connection.Close();
            }
        }

        public Presupuesto GetPresupuestoById(int id)
        {
            Presupuesto presupuesto= new Presupuesto();
            presupuesto.Detalle = new List<PresupuestoDetalle>();
            string query = @"SELECT 
                                P.idPresupuesto,
                                P.NombreDestinatario,
                                PR.idProducto,
                                PR.Descripcion AS Producto,
                                PR.Precio,
                                PD.Cantidad
                            FROM 
                                Presupuestos P
                            JOIN 
                                PresupuestosDetalle PD ON P.idPresupuesto = PD.idPresupuesto
                            JOIN 
                                Productos PR ON PD.idProducto = PR.idProducto
                            WHERE 
                                P.idPresupuesto = @id;";
            using (SqliteConnection connection = new SqliteConnection(_stringConnection))
            {
                SqliteCommand command = new SqliteCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                SqliteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    if (presupuesto.IdPresupuesto == 0)
                    {
                        presupuesto.IdPresupuesto = Convert.ToInt32(reader["idPresupuesto"]);
                        presupuesto.NombreDestinatario = reader["NombreDestinatario"].ToString();
                    }
                    PresupuestoDetalle detalle = new PresupuestoDetalle();
                    detalle.Producto = new Producto(){
                        Id = Convert.ToInt32(reader["idProducto"]),
                        Precio = Convert.ToInt32(reader["Precio"]),
                        Descripcion = reader["Producto"].ToString()
                    };
                    detalle.Cantidad = Convert.ToInt32(reader["Cantidad"]);
                    presupuesto.Detalle.Add(detalle);
                }
                connection.Close();
            }
            return presupuesto;
        }


    //! GetPresupuestos consulta c/u de los idPresupuestos en BD
    //! y Utiliza el metodo GetPresupuestoById destro de un foreach para luego devolver la lista
        public List<Presupuesto> GetPresupuestos()
        {
            List<int> idPresupuestos = new List<int>();
            List<string> nombres = new List<string>();
            List<Presupuesto> presupuestos = new List<Presupuesto>();
            string query = "SELECT idPresupuesto, NombreDestinatario FROM Presupuestos";
            using (SqliteConnection connection = new SqliteConnection(_stringConnection))
            {
                SqliteCommand command = new SqliteCommand(query, connection);
                connection.Open();
                SqliteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    idPresupuestos.Add(Convert.ToInt32(reader["idPresupuesto"]));  
                    nombres.Add(reader["NombreDestinatario"].ToString());   
                };
                connection.Close();
            }
            foreach (var id in idPresupuestos)
            {
                var presupuesto = GetPresupuestoById(id);
                if (presupuesto.IdPresupuesto == 0)
                {
                    presupuesto.IdPresupuesto = id;
                    presupuesto.NombreDestinatario = nombres[id];
                }
                presupuestos.Add(presupuesto);
            }
            return presupuestos;
        }
    }
}