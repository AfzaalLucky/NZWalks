using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using NzWalksUi.Models;
using NzWalksUi.Models.DTOs;

namespace NzWalksUi.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {
            // Get All Regions from API
            var response = new List<RegionDto>();
            try
            {
                var client = _httpClientFactory.CreateClient();
                var httResponseMessage = await client.GetAsync("http://localhost:8081/api/regions/");
                httResponseMessage.EnsureSuccessStatusCode();

                //var stringResponseBody = await httResponseMessage.Content.ReadAsStringAsync();
                //var jsonResponseAsync = await httResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>();
                //ViewBag.Response = jsonResponseAsync;

                response.AddRange(await httResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>());
            }
            catch (Exception)
            {
                throw;
            }
            return View(response);
        }

        [HttpGet]
        public IActionResult Add()
        {
	        return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRegionViewModel model)
        {
	        var client = _httpClientFactory.CreateClient();
	        var httpRequestMessage = new HttpRequestMessage()
	        {
                Method = HttpMethod.Post,
                RequestUri = new Uri("http://localhost:8081/api/regions/"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
	        };

	        var httpResponseMessage = await client.SendAsync(httpRequestMessage);
	        httpResponseMessage.EnsureSuccessStatusCode();
            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();
            if (response is not null)
            {
	            return RedirectToAction("Index", "Regions");
            }

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
	        var client = _httpClientFactory.CreateClient();
	        var response = await client.GetFromJsonAsync<RegionDto>($"http://localhost:8081/api/regions/{id.ToString()}");
	        if (response is not null)
	        {
		        return View(response);
	        }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RegionDto requestDto)
        {
	        var client = _httpClientFactory.CreateClient();
	        
	        var request = new HttpRequestMessage()
	        {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"http://localhost:8081/api/regions/{requestDto.Id.ToString()}"),
                Content = new StringContent(JsonSerializer.Serialize(requestDto), Encoding.UTF8, "application/json")
			};
	        
	        var responseMessage = await client.SendAsync(request);
	        responseMessage.EnsureSuccessStatusCode();
            
	        var response = await responseMessage.Content.ReadFromJsonAsync<RegionDto>();
            if (response is not null)
            {
	            return RedirectToAction("Index", "Regions");
            }

            return View();
		}

        [HttpPost]
        public async Task<IActionResult> Delete(RegionDto requestDto)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();

                var httpResponseMessage = await client.DeleteAsync($"http://localhost:8081/api/regions/{requestDto.Id.ToString()}");
                httpResponseMessage.EnsureSuccessStatusCode();

                return RedirectToAction("Index", "Regions");
			}
            catch (Exception)
            {
                throw;
            }
        }
    }
}
