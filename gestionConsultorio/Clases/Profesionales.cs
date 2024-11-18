using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestionConsultorio.Clases
{
    public class Profesionales
    {
        private string? Apellido { get; set; }
        private int CodProfesional { get; set; }
        private string? Especialidad { get; set; }
        private List<Profesionales> ListaProfesionales { get; set; }

        public List<Profesionales> obtenerProfesionales()
        {
            return ListaProfesionales;
        }

    }
}
