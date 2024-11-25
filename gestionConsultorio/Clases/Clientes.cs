using System.Data;
using gestionBD;
using gestionConsultorio.Metodos;

namespace gestionConsultorio.Clases
{
    public class Clientes : Personas
    {
        [ClavePrimaria]
        public int CodCliente { get; set; }
        public string? Apellidos { get; set; }

        private List<Clientes> listaClientes = new List<Clientes>();

        public List<Clientes> ObtenerClientes()
        {
            //Metodo para obtener la lista de Clientes
            return listaClientes;
        }

        public bool AgregarCliente(Clientes cliente)
        {
            //Metodo para agregar un Cliente
            if(!listaClientes.Any(u => u.CodCliente == cliente.CodCliente))
            {
                listaClientes.Add(cliente);
                return true;
            }
            else
            {
                throw new ArgumentException("No se puede añadir el cliente. Ya existe uno con ese codigo");
            }
        }

        public bool ActualizarCliente(Clientes cliente)
        {
            // Método para actualizar los datos de un cliente en la lista
            var existeCliente = listaClientes.FirstOrDefault(u => u.CodCliente == cliente.CodCliente);

            if(existeCliente != null)
            {
                listaClientes[listaClientes.IndexOf(existeCliente)] = cliente;
                return true;
            }
            else
            {
                throw new ArgumentException("No se ha actualizado el cliente.");
            }

        }

        public void EliminarCliente(int codCliente)
        {
            // Método para eliminar un cliente
            var existeCliente = listaClientes.FirstOrDefault(u => u.CodCliente == codCliente);
            if(existeCliente != null)
            {
                listaClientes.Remove(existeCliente);
            }
            else
            {
                throw new KeyNotFoundException($"No se encontró ningún Cliente con CodCliente = {codCliente}");
            }
        }

        public List<Clientes> CargarClientesBC()
        {
            //Metodo para cargar los clientes desde la base de datos
            List<Clientes> listaClientes = new List<Clientes>();

            try
            {
                using(var conexion = new conectar(ConfiguracionBD.RutaBD))
                {
                    conexion.crearConexion();
                    string sql = "SELECT * FROM Clientes";
                    DataTable tablaClientes = (conexion.consultaSQL(sql));
                    foreach(DataRow fila in tablaClientes.Rows)
                    {
                        Clientes cliente = new Clientes
                        {
                            CodCliente = Convert.ToInt32(fila["codCliente"]),
                            NIF = fila["nif"]?.ToString(),
                            Nombre = fila["nombre"]?.ToString(),
                            Apellidos = fila["apellidos"]?.ToString(),
                            Direccion = fila["direccion"]?.ToString(),
                            CodPostal = fila["codPostal"]?.ToString(),
                            Poblacion = fila["poblacion"]?.ToString(),
                            Provincia = fila["provincia"]?.ToString(),
                            Telefono = fila["telefono"]?.ToString(),
                            Email = fila["email"]?.ToString(),
                            Activo = fila["activo"] != DBNull.Value && Convert.ToBoolean(fila["activo"])
                        };

                        listaClientes.Add(cliente);
                    }
                }
            }

            catch(Exception ex)
            {
                Console.WriteLine("Error al obtener la lista de Clientes: " + ex.Message);
            }

            return listaClientes;

        }
    }
}
