using API.Models;

namespace Gestor.Models
{
    public class CitaLavadoIndexViewModel
    {
        public List<CitaLavado> Citas { get; set; } = new();
        public List<Cliente> Clientes { get; set; } = new();
        public List<Vehiculo> Vehiculos { get; set; } = new();
        public string[] Estados { get; set; } =
        {
            "Ingresada",
            "Cancelada",
            "Concluida"
        };
    }
}
