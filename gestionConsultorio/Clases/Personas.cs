using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace gestionConsultorio.Clases
{
    public class Personas
    {
        protected string? NIF { get; set; }
        protected string? Nombre { get; set; }
        protected string? Direccion { get; set; }
        protected string? CodPostal { get; set; }
        protected string? Poblacion { get; set; }
        protected string? Provincia { get; set; }
        protected string? Telefono { get; set; }
        protected string? Email { get; set; }
        protected bool Activo { get; set; }


        public void ActualizarDatos(string _nif, string _nombre, string _direccion, string _codPostal, string _poblacion, string _provincia, string _telefono, string _email, bool _activo = true)
        {
            // Método para actualizar los datos
            NIF = _nif;
            Nombre = _nombre;
            Direccion = _direccion;
            CodPostal = _codPostal;
            Poblacion = _poblacion;
            Provincia = _provincia;
            Telefono = _telefono;
            Email = _email;
            Activo = _activo;
        }
    }
}
