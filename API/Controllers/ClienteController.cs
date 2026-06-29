using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {

        private static List<Cliente> clientes = new()
        {
            new Cliente
            {
                Id = 1,
                Nombre = "Richard",
                Apellido = "Ruiz",
                Identificacion = "1234567890",
                Correo = "rruiz@gmail.com",
                Direccion = "Heredia",
                fechaNacimiento = new DateOnly(2002, 5, 15),
                Telefonos = new List<Telefono>
                {
                    new Telefono
                    {
                        Id = 1,
                        Numero = "8888-8888",
                        Tipo = "Móvil"
                    },
                    new Telefono
                    {
                        Id = 2,
                        Numero = "2222-2222",
                        Tipo = "Casa"
                    }
                }
            },
            new Cliente
            {
                Id = 2,
                Nombre = "María",
                Apellido = "Gómez",
                Identificacion = "0987654321",
                Correo = "mgomez@gmail.com",
                Direccion = "Alajuela",
                fechaNacimiento= new DateOnly(1985, 10, 20),
                Telefonos = new List<Telefono>
                {
                    new Telefono
                    {
                        Id = 3,
                        Numero = "7777-7777",
                        Tipo = "Trabajo"
                    }
                }
            }
        };

        [HttpGet]
        public IActionResult GetClientes()
        {
            return Ok(clientes);
        }

        [HttpGet("{id}")]
        public IActionResult GetCliente(int id)
        {
            var cliente = clientes.FirstOrDefault(c => c.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }
            return Ok(cliente);
        }

        [HttpPost]
        public IActionResult CreateCliente(Cliente cliente)
        {

            if(EsMayorDeEdad(cliente.fechaNacimiento) == false)
            {
                return BadRequest("El cliente debe ser mayor de edad.");
            }

            if (identficacionRepetida(cliente.Identificacion))
            {
                return BadRequest("La identificación ya está en uso por otro cliente.");
            }

            cliente.Id = clientes.Max(c => c.Id) + 1;
            clientes.Add(cliente);
            return CreatedAtAction("" , cliente);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCliente(int id, Cliente cliente)
        {
            var existingCliente = clientes.FirstOrDefault(c => c.Id == id);

            if (existingCliente == null)
            {
                return NotFound();
            }

            if(identficacionRepetida(cliente.Identificacion) && existingCliente.Identificacion != cliente.Identificacion)
            {
                return BadRequest("La identificación ya está en uso por otro cliente.");
            }

            if (EsMayorDeEdad(cliente.fechaNacimiento) == false)
            {
                return BadRequest("El cliente debe ser mayor de edad.");
            }

            existingCliente.Nombre = cliente.Nombre;
            existingCliente.Apellido = cliente.Apellido;
            existingCliente.Identificacion = cliente.Identificacion;
            existingCliente.Correo = cliente.Correo;
            existingCliente.Direccion = cliente.Direccion;
            existingCliente.fechaNacimiento = cliente.fechaNacimiento;

            existingCliente.Telefonos = cliente.Telefonos ?? new List<Telefono>();


            return Ok(existingCliente);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCliente(int id)
        {
            var cliente = clientes.FirstOrDefault(c => c.Id == id);
            if (cliente == null)
            {
                return NotFound();
            }
            clientes.Remove(cliente);
            return Ok();
        }

        public static bool identficacionRepetida(string identificacion)
        {
            return clientes.Any(c => c.Identificacion == identificacion);
        }

        public static bool EsMayorDeEdad(DateOnly fechaNacimiento)
        {
            var hoy = DateOnly.FromDateTime(DateTime.Today);
            var edad = hoy.Year - fechaNacimiento.Year;

            if (fechaNacimiento > hoy.AddYears(-edad))
                edad--;

            return edad >= 18;
        }

        public static bool clienteExiste(int id)
        {
            if (clientes.FirstOrDefault(c => c.Id == id) == null)
            {
                return false;
            }
            return true;
        }

    }

}
