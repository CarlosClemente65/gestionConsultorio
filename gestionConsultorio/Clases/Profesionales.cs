using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestionConsultorio.Clases
{
    public class Profesionales:Personas,IPersona
    {
        public string? Apellido { get; set; }
        public int CodProfesional { get; set; }
        public string? Especialidad { get; set; }
        public List<Profesionales> ListaProfesionales { get; set; }

        public List<Profesionales> obtenerProfesionales()
        {
            return ListaProfesionales;
        }

    }
}
