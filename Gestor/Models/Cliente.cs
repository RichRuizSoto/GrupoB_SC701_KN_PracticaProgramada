using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Cliente
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [Display(Name = "Apellido")]
        public string Apellido { get; set; } = string.Empty;

        [Required(ErrorMessage = "La identificación es obligatoria.")]
        [Display(Name = "Identificación")]
        public string Identificacion { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Ingrese un correo electrónico válido.")]
        [Display(Name = "Correo")]
        public string Correo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La direccion es es obligatoria.")]
        [Display(Name = "Dirección")]
        public string Direccion { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria.")]
        [Display(Name = "Fecha de nacimiento")]
        public DateOnly fechaNacimiento { get; set; }

        public List<Telefono> Telefonos { get; set; } = new List<Telefono>();
    }
}