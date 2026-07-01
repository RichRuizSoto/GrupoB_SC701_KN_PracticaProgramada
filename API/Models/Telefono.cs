using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Telefono
    {
        
        public int Id { get; set; }

        [Required]
        public int Numero { get; set; }

        [Required]
        public string Tipo { get; set; } = string.Empty;

        [Required]
        public int ClienteId { get; set; }

        public Cliente? Cliente { get; set; }
    }
}
