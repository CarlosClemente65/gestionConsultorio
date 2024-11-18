using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gestionBD;
using System.Data.SQLite;


namespace gestionConsultorio.Metodos
{
    public class ConfiguracionBD
    {
        //Establece la configuracion de la base de datos (ruta y nombre)
        public static string RutaBD { get; set; }

        static ConfiguracionBD()
        {
            string ruta = @".\datos\";
            string nombre = "gestionConsultorio.db";
            RutaBD = Path.Combine(ruta, nombre);
        }
    }

    public class ConexionBD
    {

        //Asignacion de variables
        string dbPath = ConfiguracionBD.RutaBD;

        public void ChequeoBD()
        {
            //Chequear si existe el la base de datos
            conectar conexionBD = new conectar(dbPath);
            bool resultado = conexionBD.chequeoBD();
            if(resultado)
            {
                //En el caso de que no exista la base de datos, se lanza la creacion de las tablas
                CrearTablaUsuarios();
                CrearTablaProfesionales();
            }
        }


        private void CrearTablaUsuarios()
        {
            //Crea la tabla de usuarios
            string sql = @"CREATE TABLE usuarios
                            (codUsuario INTEGER PRIMARY KEY AUTOINCREMENT, 
                            nif VARCHAR(12) NOT NULL,
                            nombre VARCHAR(50) NOT NULL,
                            apellidos VARCHAR (50) NOT NULL,
                            direccion VARCHAR(100), 
                            codPostal VARCHAR(5), 
                            poblacion VARCHAR (50), 
                            provincia VARCHAR(50),
                            telefono VARCHAR(20) NOT NULL, 
                            email VARCHAR(50),
                            activo BOOL DEFAULT 1);
                            ";
            CrearTablas(sql, "clientes");
        }

        private void CrearTablaProfesionales()
        {
            throw new NotImplementedException();
        }


        public void CrearTablas(string sql, string tabla)
        {
            conectar conexion = new conectar(dbPath);
            try
            {
                SQLiteCommand cmd = new SQLiteCommand(sql, conexion.crearConexion());
                cmd.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                MessageBox.Show($"Error en la creacion de la tabla de {tabla} " + ex.Message);
            }
            finally
            {
                conexion.cerrarConexion();
            }
        }
    }
}
