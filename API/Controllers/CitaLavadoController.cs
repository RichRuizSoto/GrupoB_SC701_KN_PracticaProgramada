using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitaLavadoController : ControllerBase
    {
        private static List<CitaLavado> citas = new()
        {
            new CitaLavado
            {
                Id = 1,
                ClienteId = 1,
                VehiculoId = 1,
                FechaCita = DateTime.Today.AddDays(1).AddHours(9),
                Estado = "Ingresada"
            },
            new CitaLavado
            {
                Id = 2,
                ClienteId = 2,
                VehiculoId = 3,
                FechaCita = DateTime.Today.AddDays(2).AddHours(11),
                Estado = "Concluida"
            }
        };

        private static readonly string[] EstadosPermitidos =
        {
            "Ingresada",
            "Cancelada",
            "Concluida"
        };

        [HttpGet]
        public IActionResult GetCitas()
        {
            return Ok(citas);
        }

        [HttpGet("{id}")]
        public IActionResult GetCita(int id)
        {
            var cita = citas.FirstOrDefault(c => c.Id == id);
            if (cita == null)
            {
                return NotFound();
            }

            return Ok(cita);
        }

        [HttpPost]
        public IActionResult CreateCita(CitaLavado cita)
        {
            var error = ValidarCita(cita);
            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            cita.Id = citas.Any() ? citas.Max(c => c.Id) + 1 : 1;
            cita.Estado = "Ingresada";
            citas.Add(cita);

            return CreatedAtAction("", cita);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCita(int id, CitaLavado cita)
        {
            var citaExistente = citas.FirstOrDefault(c => c.Id == id);
            if (citaExistente == null)
            {
                return NotFound();
            }

            var error = ValidarCita(cita, id);
            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            citaExistente.ClienteId = cita.ClienteId;
            citaExistente.VehiculoId = cita.VehiculoId;
            citaExistente.FechaCita = cita.FechaCita;
            citaExistente.Estado = cita.Estado;

            return Ok(citaExistente);
        }

        [HttpPut("{id}/estado/{estado}")]
        public IActionResult CambiarEstado(int id, string estado)
        {
            var cita = citas.FirstOrDefault(c => c.Id == id);
            if (cita == null)
            {
                return NotFound();
            }

            if (EstadoNoValido(estado))
            {
                return BadRequest("El estado indicado no es valido.");
            }

            cita.Estado = estado;
            return Ok(cita);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCita(int id)
        {
            var cita = citas.FirstOrDefault(c => c.Id == id);
            if (cita == null)
            {
                return NotFound();
            }

            citas.Remove(cita);
            return Ok();
        }

        private static string ValidarCita(CitaLavado cita, int? citaId = null)
        {
            if (ClienteController.clienteExiste(cita.ClienteId) == false)
            {
                return "El cliente indicado no existe.";
            }

            if (VehiculoController.vehiculoExiste(cita.VehiculoId) == false)
            {
                return "El vehiculo indicado no existe.";
            }

            if (VehiculoController.vehiculoPerteneceACliente(cita.VehiculoId, cita.ClienteId) == false)
            {
                return "El vehiculo seleccionado no pertenece al cliente indicado.";
            }

            if (cita.FechaCita == default)
            {
                return "Debe indicar la fecha de la cita.";
            }

            if (EstadoNoValido(cita.Estado))
            {
                return "El estado indicado no es valido.";
            }

            var citaRepetida = citas.Any(c =>
                c.Id != citaId &&
                c.VehiculoId == cita.VehiculoId &&
                c.FechaCita == cita.FechaCita &&
                c.Estado != "Cancelada");

            if (citaRepetida)
            {
                return "El vehiculo ya tiene una cita registrada para esa fecha y hora.";
            }

            return string.Empty;
        }

        private static bool EstadoNoValido(string estado)
        {
            return EstadosPermitidos.Contains(estado) == false;
        }
    }
}
