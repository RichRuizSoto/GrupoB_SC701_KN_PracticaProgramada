using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gestor.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ClienteController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://localhost:7217/api/cliente");
            if (response.IsSuccessStatusCode)
            {
                var clientes = await response.Content.ReadFromJsonAsync<List<API.Models.Cliente>>();
                return View(clientes);
            }
            else
            {
                return View(new List<API.Models.Cliente>());
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Cliente cleinte)
        {
            var httpClient = _httpClientFactory.CreateClient();
            await httpClient.PostAsJsonAsync("https://localhost:7217/api/cliente", cleinte);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"https://localhost:7217/api/cliente/{id}");
            if (response.IsSuccessStatusCode)
            {
                var cliente = await response.Content.ReadFromJsonAsync<API.Models.Cliente>();
                return View(cliente);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Cliente cliente)
        {
            var httpClient = _httpClientFactory.CreateClient();
            await httpClient.PutAsJsonAsync($"https://localhost:7217/api/cliente/{id}", cliente);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            await httpClient.DeleteAsync($"https://localhost:7217/api/cliente/{id}");
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"https://localhost:7217/api/cliente/{id}");
            if (response.IsSuccessStatusCode)
            {
                var cliente = await response.Content.ReadFromJsonAsync<API.Models.Cliente>();
                return View(cliente);
            }
            else
            {
                return NotFound();
            }
        }






        }
}
