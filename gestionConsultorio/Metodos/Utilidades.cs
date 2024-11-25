using System.Security.Cryptography;

namespace gestionConsultorio.Metodos
{
    public class Utilidades
    {
        public class PasswordHasher
        {
            public static string CrearPassword(string password, out string salt)
            {
                //Metodo para crear un password seguro

                // Generar un salt aleatorio para añadirlo al password
                salt = GenerateSalt();

                // Convertir el salt de Base64 a bytes
                byte[] saltBytes = Convert.FromBase64String(salt);

                // Derivar la clave usando PBKDF2
                byte[] hash;
                using(var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000, HashAlgorithmName.SHA256))
                {
                    hash = pbkdf2.GetBytes(32); // Generar un hash de 256 bits (32 bytes)
                }

                return Convert.ToBase64String(hash); // Retornar el hash como cadena Base64
            }

            private static string GenerateSalt()
            {
                //Metodo para generar un dato aleatorio
                byte[] saltBytes = new byte[16]; // Longitud del salt (16 bytes)
                RandomNumberGenerator.Fill(saltBytes); // Genera un numero aleatorio
                return Convert.ToBase64String(saltBytes);
            }

            public static bool VerificarPassword(string password, string hashAlmacenado, string saltAlmacenado)
            {
                //Metodo para verificar el password introducido
                byte[] saltBytes = Convert.FromBase64String(saltAlmacenado);

                // Derivar el hash con el salt almacenado
                byte[] computedHash;
                using(var pbkdf2 = new Rfc2898DeriveBytes(password, saltBytes, 10000, HashAlgorithmName.SHA256))
                {
                    computedHash = pbkdf2.GetBytes(32);
                }

                // Comparar hash calculado con el almacenado
                return Convert.ToBase64String(computedHash) == hashAlmacenado;
            }
        }
    }
    public class ClavePrimariaAttribute : Attribute
    {
    }
}
