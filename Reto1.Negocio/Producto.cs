using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Reto1.DALC;
using Oracle.ManagedDataAccess.Client;
using System.Runtime.Remoting.Messaging;

namespace Reto1.Negocio
{
    public class Producto
    {
        public decimal Id { get; set; }
        public String nombre { get; set; }

        public String descripcion { get; set; }

        public decimal precio { get; set; }

        Reto1Entities db = new Reto1Entities();

        public List<Producto> ReadAll() {
            return this.db.PRODUCTOS.Select(p => new Producto() {
                Id = p.ID_PRODUCTO,
                nombre = p.NOMBRE,
                descripcion = p.DESCRIPCION,
                precio = p.PRECIO
            }).ToList();
        }

        public bool Save()
        {
            using (OracleConnection connection = new OracleConnection(connectionString))
            {
                try
                {
                    // Abrir conexión
                    connection.Open();

                    // Query SQL para insertar
                    string query = @"INSERT INTO PRODUCTOS (ID_PRODUCTO, NOMBRE, DESCRIPCION, PRECIO) 
                                     VALUES (:ID_PRODUCTO, :NOMBRE, :DESCRIPCION, :PRECIO)";

                    // Crear el comando
                    using (OracleCommand command = new OracleCommand(query, connection))
                    {
                        // Agregar parámetros
                        command.Parameters.Add(new OracleParameter("ID_PRODUCTO", this.Id));
                        command.Parameters.Add(new OracleParameter("NOMBRE", this.nombre));
                        command.Parameters.Add(new OracleParameter("DESCRIPCION", this.descripcion));

                        // Manejar valores nulos para PRECIO
                        if (precio.HasValue)
                        {
                            command.Parameters.Add(new OracleParameter("PRECIO", this.precio));
                        }
                        else
                        {
                            command.Parameters.Add(new OracleParameter("PRECIO", DBNull.Value));
                        }

                        // Ejecutar el comando
                        int rowsAffected = command.ExecuteNonQuery();
                        Console.WriteLine($"Se han insertado {rowsAffected} filas.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al insertar el producto: {ex.Message}");
                }
            }
        }

        public Producto Find(int id)
        {
            return this.db.PRODUCTOS.Select(p => new Producto()
            {
                Id = p.ID_PRODUCTO,
                nombre = p.NOMBRE,
                descripcion = p.DESCRIPCION,
                precio = p.PRECIO
            }).Where(p => p.Id == id).FirstOrDefault();
        }

        public bool Update()
        {
            try{
                db.SP_UPDATE(this.id, this.nombre, this.descripcion.descripcion, this.precio);
            } catch(Exception){
                return false;
            } 
        }

        public bool Delete(int id)
        {
            try
            {
                db.SP_DELETE(id);
                return true;
            } catch (Exception ex) {
                return false;
            } 
        }

    }
}
