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

            existingCliente.Nombre = cliente.Nombre;
            existingCliente.Apellido = cliente.Apellido;
            existingCliente.Identificacion = cliente.Identificacion;
            existingCliente.Correo = cliente.Correo;
            existingCliente.Direccion = cliente.Direccion;


            if (cliente.Telefonos != null && cliente.Telefonos.Any())
            {
                existingCliente.Telefonos = cliente.Telefonos;
            }


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



    }
}
