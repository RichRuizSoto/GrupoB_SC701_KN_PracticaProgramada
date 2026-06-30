using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class CitaLavado
    {
        public int Id { get; set; }

        [Required]
        public int ClienteId { get; set; }

        [Required]
        public int VehiculoId { get; set; }

        [Required]
        public DateTime FechaCita { get; set; }

        [Required]
        public string Estado { get; set; } = "Ingresada";
    }
}
