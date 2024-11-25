using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gestionConsultorio.Metodos;

namespace gestionConsultorio.Clases
{
    public class Profesionales:Personas,IPersona
    {
        [ClavePrimaria]
        public int CodProfesional { get; set; }
        public string? Apellido { get; set; }
        public string? Especialidad { get; set; }
        public List<Profesionales> ListaProfesionales { get; set; }

        public List<Profesionales> obtenerProfesionales()
        {
            return ListaProfesionales;
        }

    }
}
