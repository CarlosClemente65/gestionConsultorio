using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gestionConsultorio.Metodos;

namespace gestionConsultorio.Clases
{
    public class Proveedores:Personas , IPersona
    {
        [ClavePrimaria]
        public int CodProveedor { get; set; }
        public string? Apellidos { get; set; }

        public List<Proveedores> listaProveedores = new List<Proveedores>();
    }
}
