namespace API.Models
{
    public class CitaLavado
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int VehiculoId { get; set; }
        public DateTime FechaCita { get; set; }
        public string Estado { get; set; } = "Ingresada";
    }
}
