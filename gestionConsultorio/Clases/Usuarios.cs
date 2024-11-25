using System.Data;
using gestionBD;
using gestionConsultorio.Metodos;

namespace gestionConsultorio.Clases
{
    public class Usuarios : Personas
    {
        [ClavePrimaria]
        public int? CodUsuario { get; set; }
        public string? Apellidos { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }

        public List<Usuarios> listaUsuarios = new List<Usuarios>();

        ConexionBD conexionBD = new ConexionBD();

        public List<Usuarios> ObtenerUsuarios()
        {
            //Metodo para obtener la lista de Usuarios
            return listaUsuarios;
        }

        public bool AgregarUsuario(Usuarios usuario)
        {
            //Metodo para agregar un Usuario
            try
            {
                var usuarioEncontrado = listaUsuarios.Any(u => u.CodUsuario == usuario.CodUsuario);
                if(usuarioEncontrado)
                {
                    listaUsuarios.Add(usuario);
                    conexionBD.GestionRegistrosBD(usuario, "insertar");
                    return true;
                }
                else
                {
                    throw new Exception($"Codigo de usuario {usuario.CodUsuario} repetido.");
                }

            }

            catch(Exception ex)
            {
                throw new Exception($"Se ha producido un error al agregar el usuario. {ex.Message}");
            }
        }

        public bool ActualizarUsuario(Usuarios usuario)
        {
            // Método para actualizar los datos de un Usuario en la lista
            try
            {
                var usuarioEncontrado = listaUsuarios.FirstOrDefault(u => u.CodUsuario == usuario.CodUsuario);

                if(usuarioEncontrado != null)
                {
                    listaUsuarios[listaUsuarios.IndexOf(usuarioEncontrado)] = usuario;
                    conexionBD.GestionRegistrosBD(usuario, "modificar");
                    return true;
                }
                else
                {
                    throw new Exception($"No se ha podido actualizar el usuario {usuario.Nombre} {usuario.Apellidos}.");
                }

            }

            catch(Exception ex)
            {
                throw new Exception($"Se ha producido un error al actualizar el usuario {usuario.Nombre} {usuario.Apellidos}. {ex.Message}");
            }


        }

        public void EliminarUsuario(int codUsuario)
        {
            // Método para eliminar un Usuario
            Usuarios usuarioEncontrado = null;
            try
            {
                usuarioEncontrado = listaUsuarios.FirstOrDefault(u => u.CodUsuario == codUsuario);
                if(usuarioEncontrado != null)
                {
                    listaUsuarios.Remove(usuarioEncontrado);
                    conexionBD.GestionRegistrosBD(usuarioEncontrado, "eliminar");
                }
                else
                {
                    throw new Exception($"No se encontró el usuario con el codigo {codUsuario}");
                }

            }

            catch(Exception ex)
            {
                string nombre = usuarioEncontrado?.Nombre ?? "desconocido";
                string apellidos = usuarioEncontrado?.Apellidos ?? "desconocido";
                throw new Exception($"Se ha producido un error al intentar eliminar el usuario {nombre} {apellidos}. {ex.Message}");
            }
        }

        public List<Usuarios> CargarUsuariosBC()
        {
            //Metodo para cargar los Usuarios desde la base de datos
            if(listaUsuarios.Count > 0) listaUsuarios.Clear();

            try
            {
                using(var conexion = new conectar(ConfiguracionBD.RutaBD))
                {
                    conexion.crearConexion();
                    string sql = "SELECT * FROM Usuarios";
                    DataTable tablaUsuarios = conexion.consultaSQL(sql);
                    foreach(DataRow fila in tablaUsuarios.Rows)
                    {
                        Usuarios usuario = new Usuarios
                        {
                            CodUsuario = Convert.ToInt32(fila["codUsuario"]),
                            NIF = fila["nif"]?.ToString(),
                            Nombre = fila["nombre"]?.ToString(),
                            Apellidos = fila["apellidos"]?.ToString(),
                            Direccion = fila["direccion"]?.ToString(),
                            CodPostal = fila["codPostal"]?.ToString(),
                            Poblacion = fila["poblacion"]?.ToString(),
                            Provincia = fila["provincia"]?.ToString(),
                            Telefono = fila["telefono"]?.ToString(),
                            Email = fila["email"]?.ToString(),
                            PasswordHash = fila["passwordHash"].ToString(),
                            PasswordSalt = fila["passwordSalt"].ToString(),
                            Activo = Convert.ToBoolean(fila["activo"])
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


        public void AgregarUsuarioBD(Usuarios usuario)
        {
            try
            {
                using(var conexion = new conectar(ConfiguracionBD.RutaBD))
                {
                    conexion.crearConexion();

                    string sql = "INSERT INTO Usuarios " +
                                 "(nif, nombre, apellidos, direccion, codPostal, poblacion, provincia, telefono, email, passwordHash, passwordSalt, activo" +
                                 "VALUES (@nif @nombre, @apellidos, @direccion, @codPostal, @poblacion, @provincia, @telefono, @email, @passwordHash, @passwordSalt, @activo";


                }
            }

            catch(Exception ex)
            {
                Console.WriteLine("Error al obtener la lista de usuarios: " + ex.Message);
            }
        }


    }
}
