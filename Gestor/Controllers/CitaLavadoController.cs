using API.Models;
using Gestor.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gestor.Controllers
{
    public class CitaLavadoController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string ApiUrl = "https://localhost:7217/api";

        public CitaLavadoController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient();

            var modelo = new CitaLavadoIndexViewModel
            {
                Citas = await ObtenerLista<CitaLavado>(httpClient, $"{ApiUrl}/CitaLavado"),
                Clientes = await ObtenerLista<Cliente>(httpClient, $"{ApiUrl}/Cliente"),
                Vehiculos = await ObtenerLista<Vehiculo>(httpClient, $"{ApiUrl}/Vehiculo")
            };

            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CitaLavado cita)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.PostAsJsonAsync($"{ApiUrl}/CitaLavado", cita);

            await GuardarMensaje(response, "La cita fue registrada correctamente.");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CitaLavado cita)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.PutAsJsonAsync($"{ApiUrl}/CitaLavado/{id}", cita);

            await GuardarMensaje(response, "La cita fue actualizada correctamente.");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> CambiarEstado(int id, string estado)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.PutAsync($"{ApiUrl}/CitaLavado/{id}/estado/{estado}", null);

            await GuardarMensaje(response, "El estado de la cita fue actualizado correctamente.");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.DeleteAsync($"{ApiUrl}/CitaLavado/{id}");

            await GuardarMensaje(response, "La cita fue eliminada correctamente.");
            return RedirectToAction("Index");
        }

        private static async Task<List<T>> ObtenerLista<T>(HttpClient httpClient, string url)
        {
            var response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<T>>() ?? new List<T>();
            }

            return new List<T>();
        }

        private async Task GuardarMensaje(HttpResponseMessage response, string mensajeExito)
        {
            if (response.IsSuccessStatusCode)
            {
                TempData["Success"] = mensajeExito;
                return;
            }

            TempData["Error"] = await response.Content.ReadAsStringAsync();
        }
    }
}
