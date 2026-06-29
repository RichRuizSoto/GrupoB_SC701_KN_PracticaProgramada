using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Vehiculo
    {
        public int Id { get; set; }

        [Required]
        public string Matricula { get; set; } = string.Empty;

        [Required]
        public string Marca {  get; set; } = string.Empty;

        [Required]
        public string Modelo { get; set; } = string.Empty;

        [Required]
        public string Color { get; set; } = string.Empty;

        [Required]
        public int Anio {  get; set; }

        [Required]
        public int ClienteId { get; set; }

    }
}
