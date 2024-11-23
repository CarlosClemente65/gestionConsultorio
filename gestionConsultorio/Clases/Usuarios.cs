using System.Data;
using gestionBD;
using gestionConsultorio.Metodos;

namespace gestionConsultorio.Clases
{
    public class Usuarios : Personas
    {
        public int CodUsuario { get; set; }
        public string? Apellidos { get; set; }

        private List<Usuarios> listaUsuarios = new List<Usuarios>();

        public List<Usuarios> ObtenerUsuarios()
        {
            //Metodo para obtener la lista de usuarios
            return listaUsuarios;
        }

        public bool AgregarUsuario(Usuarios usuario)
        {
            //Metodo para agregar un usuario
            if(!listaUsuarios.Any(u => u.CodUsuario == usuario.CodUsuario))
            {
                listaUsuarios.Add(usuario);
                return true;
            }
            else
            {
                throw new ArgumentException("No se puede añadir el uuario. Ya existe uno con ese codigo");
            }
        }

        public bool ActualizarUsuario(Usuarios usuario)
        {
            // Método para actualizar los datos de un usuario en la lista
            var existeUsuario = listaUsuarios.FirstOrDefault(u => u.CodUsuario == usuario.CodUsuario);

            if(existeUsuario != null)
            {
                listaUsuarios[listaUsuarios.IndexOf(existeUsuario)] = usuario;
                return true;
            }
            else
            {
                throw new ArgumentException("No se ha actualizado el usuario.");
            }

        }

        public void EliminarUsuario(int codUsuario)
        {
            // Método para eliminar un usuario
            var existeUsuario = listaUsuarios.FirstOrDefault(u => u.CodUsuario == codUsuario);
            if(existeUsuario != null)
            {
                listaUsuarios.Remove(existeUsuario);
            }
            else
            {
                throw new KeyNotFoundException($"No se encontró ningún usuario con CodUsuario = {codUsuario}");
            }
        }

        public List<Usuarios> CargarUsuariosBC()
        {
            //Metodo para cargar los usuarios desde la base de datos
            List<Usuarios> listaUsuarios = new List<Usuarios>();

            try
            {
                using(var conexion = new conectar(ConfiguracionBD.RutaBD))
                {
                    conexion.crearConexion();
                    string sql = "SELECT codUsuario, nif, nombre, apellidos, direccion, codPostal, poblacion, provincia, telefono, email, activo FROM usuarios";
                    DataTable tablaUsuarios = (conexion.consultaSQL(sql));
                    foreach(DataRow fila in tablaUsuarios.Rows)
                    {
                        Usuarios usuario = new Usuarios
                        {
                            CodUsuario = fila["codUsuario"] != DBNull.Value ? Convert.ToInt32(fila["codUsuario"]) : 0,
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

                        listaUsuarios.Add(usuario);
                    }
                }
            }

            catch(Exception ex)
            {
                Console.WriteLine("Error al obtener la lista de usuarios: " + ex.Message);
            }

            return listaUsuarios;

        }
    }
}
