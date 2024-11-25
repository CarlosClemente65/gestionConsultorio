using gestionBD;
using System.Data.SQLite;
using System.Reflection;


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
        //Instanciacion metodo conexion
        static string dbPath = ConfiguracionBD.RutaBD;
        conectar conexion = new conectar(dbPath);

        public void ChequeoBD()
        {
            //Chequear si existe el la base de datos
            conectar conexionBD = new conectar(dbPath);
            bool resultado = conexionBD.chequeoBD();
            if(resultado)
            {
                //En el caso de que no exista la base de datos, se lanza la creacion de las tablas
                CrearTablaClientes();
                CrearTablaProfesionales();
            }
        }

        public void CrearTablaClientes()
        {
            //Crea la tabla de Clientes
            string sql = @"CREATE TABLE Clientes
                            (codCliente INTEGER PRIMARY KEY AUTOINCREMENT, 
                            nif VARCHAR(12) NOT NULL,
                            nombre TEXT NOT NULL,
                            apellidos TEXT NOT NULL,
                            direccion TEXT, 
                            codPostal VARCHAR(5), 
                            poblacion TEXT, 
                            provincia TEXT,
                            telefono VARCHAR(20), 
                            email TEXT,
                            activo BOOL DEFAULT 1);
                            ";
            CrearTablas(sql, "clientes");
        }

        public void CrearTablaUsuarios()
        {
            //Crea la tabla de usuarios
            string sql = @"CREATE TABLE Usuarios
                            (codUsuario INTEGER PRIMARY KEY AUTOINCREMENT,
                            nif VARCHAR(12) NOT NULL,
                            nombre TEXT NOT NULL,
                            apellidos TEXT NOT NULL,
                            direccion TEXT, 
                            codPostal VARCHAR(5), 
                            poblacion TEXT, 
                            provincia TEXT,
                            telefono VARCHAR(20), 
                            email TEXT,
                            passwordHash TEXT NOT NULL,
                            passwordSalt TEXTO NOT NULL,
                            activo BOOL DEFAULT 1);
                            ";
            CrearTablas(sql, "clientes");
        }

        public void CrearTablaProfesionales()
        {
            throw new NotImplementedException();
        }

        public void CrearTablaProveedores()
        {
            //Pendiente de implementacion
            throw new NotImplementedException();
        }

        public void CrearTablas(string sql, string tabla)
        {
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

        public bool GestionRegistrosBD<T>(T objeto, string operacion)
        {
            bool resultado = false;
            //Metodo para insertar, eliminar o modificar registros en la base de datos
            try
            {
                // Obtener el nombre de la tabla y las propiedades del objeto
                string nombreTabla = typeof(T).Name;
                List<string> columnas = new List<string>();
                Dictionary<string, object> parametros = new Dictionary<string, object>();

                // Obtener las propiedades públicas del objeto
                foreach(var propiedad in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    string nombreColumna = propiedad.Name;
                    object valorColumna = propiedad.GetValue(objeto);

                    columnas.Add(nombreColumna);
                    parametros.Add(nombreColumna, valorColumna);
                }

                string sql = string.Empty;

                //Obtiene el nombre del atributo 'ClavePrimaria' que deben tener todas las clases
                var propiedadClavePrimaria = typeof(T).GetProperties().FirstOrDefault(p => p.GetCustomAttribute<ClavePrimariaAttribute>() != null);
                string campoClave = propiedadClavePrimaria.Name;

                switch(operacion)
                {
                    case "insertar":
                        // Construir la sentencia SQL de inserción
                        string columnasSQL = string.Join(", ", columnas);
                        string valoresSQL = string.Join(", ", columnas.ConvertAll(c => "@" + c));
                        sql = $"INSERT INTO {nombreTabla} ({columnasSQL}) VALUES ({valoresSQL})";
                        break;

                    case "eliminar":
                        sql = $"DELETE FROM {nombreTabla} WHERE {campoClave} = @{campoClave}";
                        break;

                    case "modificar":
                        string camposBD = string.Join(", ", columnas.Select(c => $"{c} = @{c}"));
                        campoClave = "";
                        sql = $"UPDATE {nombreTabla} SET {camposBD} WHERE {campoClave} = @{campoClave}";
                        break;
                }

                // Llamar al método operacionSQL pasando la sentencia SQL y los parámetros
                resultado = conexion.operacionSQL(sql, parametros);
            }

            catch(Exception ex)
            {
                throw new Exception ($"Se ha producido un error al actualizar la base de datos {ex}");
            }

            return resultado;
        }
    }
}
