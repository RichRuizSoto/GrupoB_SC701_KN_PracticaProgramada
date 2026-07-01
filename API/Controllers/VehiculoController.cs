using API.Models;
using Microsoft.AspNetCore.Mvc;
using API.Controllers;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VehiculoController : Controller
    {
        private static List<Vehiculo> vehiculos = new()
        {
            new Vehiculo
            {
                Id = 1,
                Matricula = "123456",
                Marca = "Toyota",
                Modelo = "Corolla",
                Color = "Rojo",
                Anio = 2010,
                ClienteId = 1,
            },
            new Vehiculo
            {
                Id = 2,
                Matricula = "654321",
                Marca = "Toyota",
                Modelo = "Corolla",
                Color = "Azul",
                Anio = 2020,
                ClienteId = 1,
            },
            new Vehiculo
            {
                Id = 3,
                Matricula = "223344",
                Marca = "Suzuki",
                Modelo = "Terios",
                Color = "Blanco",
                Anio = 2023,
                ClienteId = 2,
            }
        };

        [HttpGet]
        public IActionResult GetVehiculos()
        {
            return Ok(vehiculos);
        }

        [HttpGet("{id}")]
        public IActionResult GetVehiculo(int id)
        {
            var vehiculo = vehiculos.FirstOrDefault(v => v.Id == id);
            if (vehiculo == null)
            {
                return NotFound();
            }
            return Ok(vehiculo);
        }

        [HttpGet("Cliente/{clienteId}")]
        public IActionResult GetVehiculosCliente(int clienteId)
        {
            var vehiculosCliente = vehiculos.FindAll(v => v.ClienteId == clienteId);
            if(vehiculosCliente == null) 
            { 
                return NotFound(); 
            }
            return Ok(vehiculosCliente);
        }

        [HttpPost]
        public IActionResult CreateVehiculo(Vehiculo vehiculo)
        {
            if (matriculaRepetida(vehiculo.Matricula))
            {
                return BadRequest("La matricula ya esta en uso en otro vehiculo.");
            }

            if (ClienteController.clienteExiste(vehiculo.ClienteId) == false)
            { 
                return BadRequest("El cliente indicado no existe.");
            }

            vehiculo.Id = vehiculos.Max(v => v.Id) + 1;
            vehiculos.Add(vehiculo);
            return CreatedAtAction("", vehiculo);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateVehiculo(int id, Vehiculo vehiculo)
        {
            var vehiculoExistente = vehiculos.FirstOrDefault(v => v.Id == id);

            if (vehiculoExistente == null)
            {
                return NotFound();
            }

            if (vehiculos.Any(v => v.Matricula == vehiculo.Matricula && v.Id != id))
            {
                return BadRequest("La matricula ya esta en uso en otro vehiculo.");
            }

            if (ClienteController.clienteExiste(vehiculo.ClienteId) == false)
            {
                return BadRequest("El cliente indicado no existe.");
            }

            vehiculoExistente.Matricula = vehiculo.Matricula;
            vehiculoExistente.Marca = vehiculo.Marca;
            vehiculoExistente.Modelo = vehiculo.Modelo;
            vehiculoExistente.Color = vehiculo.Color;
            vehiculoExistente.Anio = vehiculo.Anio;
            vehiculoExistente.ClienteId = vehiculo.ClienteId;

            return Ok(vehiculoExistente);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteVehiculo(int id)
        {
            var vehiculo = vehiculos.FirstOrDefault(c => c.Id == id);

            if (vehiculo == null)
            {
                return NotFound();
            }

            if (CitaLavadoController.VehiculoTieneCita(id))
            {
                return BadRequest("El vehículo tiene una cita.");
            }

            vehiculos.Remove(vehiculo);
            return Ok();
        }

        public static bool matriculaRepetida(string matricula)
        {
            return vehiculos.Any(c => c.Matricula == matricula);
        }

        public static bool vehiculoExiste(int id)
        {
            if (vehiculos.FirstOrDefault(v => v.Id == id) == null)
            {
                return false;
            }
            return true;
        }

        public static bool vehiculoPerteneceACliente(int vehiculoId, int clienteId)
        {
            return vehiculos.Any(v => v.Id == vehiculoId && v.ClienteId == clienteId);
        }

    }
}
