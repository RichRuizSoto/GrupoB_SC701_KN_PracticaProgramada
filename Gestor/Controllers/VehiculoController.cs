using API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace Web.Controllers
{
    public class VehiculoController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public VehiculoController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient();

            var vehiculos = await httpClient.GetFromJsonAsync<List<Vehiculo>>(
                "https://localhost:7217/api/vehiculo"
            ) ?? new List<Vehiculo>();

            await CargarClientesAsync();

            return View(vehiculos);
        }

        public async Task<IActionResult> Details(int id)
        {
            var httpClient = _httpClientFactory.CreateClient();

            var vehiculo = await httpClient.GetFromJsonAsync<Vehiculo>(
                $"https://localhost:7217/api/vehiculo/{id}"
            );

            if (vehiculo == null)
            {
                return NotFound();
            }

            await CargarClientesAsync();

            return View(vehiculo);
        }

        public async Task<IActionResult> Create()
        {
            await CargarClientesAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Vehiculo vehiculo)
        {
            if (!ModelState.IsValid)
            {
                await CargarClientesAsync();
                return View(vehiculo);
            }

            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.PostAsJsonAsync(
                "https://localhost:7217/api/vehiculo",
                vehiculo
            );

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, error);
                await CargarClientesAsync();
                return View(vehiculo);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var httpClient = _httpClientFactory.CreateClient();

            var vehiculo = await httpClient.GetFromJsonAsync<Vehiculo>(
                $"https://localhost:7217/api/vehiculo/{id}"
            );

            if (vehiculo == null)
            {
                return NotFound();
            }

            await CargarClientesAsync();

            return View(vehiculo);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Vehiculo vehiculo)
        {
            if (!ModelState.IsValid)
            {
                await CargarClientesAsync();
                return View(vehiculo);
            }

            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.PutAsJsonAsync(
                $"https://localhost:7217/api/vehiculo/{vehiculo.Id}",
                vehiculo
            );

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError(string.Empty, error);
                await CargarClientesAsync();
                return View(vehiculo);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var httpClient = _httpClientFactory.CreateClient();

            var vehiculo = await httpClient.GetFromJsonAsync<Vehiculo>(
                $"https://localhost:7217/api/vehiculo/{id}"
            );

            if (vehiculo == null)
            {
                return NotFound();
            }

            await CargarClientesAsync();

            return View(vehiculo);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.DeleteAsync(
                $"https://localhost:7217/api/vehiculo/{id}"
            );

            if (!response.IsSuccessStatusCode)
            {
                ModelState.AddModelError(string.Empty, "No se pudo eliminar el vehículo.");
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task CargarClientesAsync()
        {
            var httpClient = _httpClientFactory.CreateClient();

            var clientes = await httpClient.GetFromJsonAsync<List<Cliente>>(
                "https://localhost:7217/api/cliente"
            ) ?? new List<Cliente>();

            ViewBag.ClientesPorId = clientes.ToDictionary(
                c => c.Id,
                c => $"{c.Nombre} {c.Apellido}"
            );

            ViewBag.Clientes = clientes;
        }
    }
}