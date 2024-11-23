using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestionConsultorio.Clases
{
    public class Clientes : Personas,IPersona
    {
        public int CodCliente { get; set; }
        public string? Apellidos { get; set; }

        public List<Clientes> listaClientes = new List<Clientes>();
    }
}
