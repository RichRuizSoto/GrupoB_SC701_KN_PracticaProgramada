using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public string Apellido { get; set; } = string.Empty;

        [Required]
        public string Identificacion { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Correo { get; set; } = string.Empty;

        public string Direccion { get; set; } = string.Empty;

        [Required]
        public DateOnly fechaNacimiento { get; set; }
        
        public ICollection<Telefono> Telefonos { get; set; } = new List<Telefono>();
    
    }

}

