namespace gestionConsultorio.Clases
{
    public interface IPersona
    {
        string? NIF { get; set; }
        string? Nombre { get; set; }
        string? Direccion { get; set; }
        string? CodPostal { get; set; }
        string? Poblacion { get; set; }
        string? Provincia { get; set; }
        string? Telefono { get; set; }
        string? Email { get; set; }
        bool Activo { get; set; }

    }

    public class Personas : IPersona
    {
        public string? NIF { get; set; }
        public string? Nombre { get; set; }
        public string? Direccion { get; set; }
        public string? CodPostal { get; set; }
        public string? Poblacion { get; set; }
        public string? Provincia { get; set; }
        public string? Telefono { get; set; }
        public string? Email { get; set; }
        public bool Activo { get; set; }
    }
}
