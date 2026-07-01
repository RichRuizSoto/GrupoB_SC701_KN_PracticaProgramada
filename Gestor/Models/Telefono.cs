using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Telefono
    {
        public int Id { get; set; }
        [Required]
        [Range(10000000, int.MaxValue, ErrorMessage = "El número debe tener al menos 8 dígitos.")]
        public int Numero { get; set; }
        public string Tipo { get; set; } = string.Empty;

        public int ClienteId { get; set; }
        public Cliente? Cliente { get; set; }
    }
}
