using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gestionBD;
using gestionConsultorio.Metodos;

namespace gestionConsultorio.Clases
{
    public class Usuarios : Personas
    {
        private string? Apellidos { get; set; }
        private int CodUsuario { get; set; }

        private List<Usuarios> listaUsuarios = new List<Usuarios>();

        public List<Usuarios> ObtenerUsuarios()
        {
            //Metodo para obtener la lista de usuarios
            return listaUsuarios;
        }

        public void AgregarUsuario(Usuarios usuario)
        {
            //Metodo para agregar un usuario
            if(!listaUsuarios.Any(u => u.CodUsuario == usuario.CodUsuario))
            {
                listaUsuarios.Add(usuario);
            }
            else
            {
                throw new ArgumentException("No se puede añadir el uuario. Ya existe uno con ese codigo");
            }

        }

        public bool ActualizarUsuario(int _codUsuario, string _nif, string _nombre, string _apellidos, string _direccion, string _codPostal, string _poblacion, string _provincia, string _telefono, string _email, bool _activo)
        {
            // Método para actualizar los datos de un usuario en la lista
            Usuarios usuario = listaUsuarios.FirstOrDefault(u => u.CodUsuario == _codUsuario);

            if(usuario != null)
            {
                usuario.ActualizarDatos(_nif, _nombre, _direccion, _codPostal, _poblacion, _provincia, _telefono, _email, _activo);
                Apellidos = _apellidos;
                return true; // Actualización exitosa
            }
            return false; // Índice inválido
        }

        public List<Usuarios> CargarUsuariosBC()
        {
            //Metodo para cargar los usuarios desde la base de datos
            List<Usuarios> listaUsuarios = new List<Usuarios>();

            try
            {
                conectar conexion = new conectar(ConfiguracionBD.RutaBD);
                conexion.crearConexion();
                string sql = "SELECT codUsuario, nif, nombre, apellidos, direccion, codPostal, poblacion, provincia, telefono, email, activo FROM usuarios";
                DataTable tablaUsuarios = (conexion.consultaSQL(sql));
                foreach(DataRow fila in tablaUsuarios.Rows)
                {
                    Usuarios usuario = new Usuarios
                    {
                        CodUsuario = Convert.ToInt32(fila["codUsuario"]),
                        NIF = fila["nif"].ToString(),
                        Nombre = fila["nombre"].ToString(),
                        Apellidos = fila["apellidos"].ToString(),
                        Direccion = fila["direccion"].ToString(),
                        CodPostal = fila["codPostal"].ToString(),
                        Poblacion = fila["poblacion"].ToString(),
                        Provincia = fila["provincia"].ToString(),
                        Telefono = fila["telefono"].ToString(),
                        Email = fila["email"].ToString(),
                        Activo = Convert.ToBoolean(fila["activo"])
                    };

                    listaUsuarios.Add(usuario);
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
