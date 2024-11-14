using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.MSIdentity.Shared;
using Newtonsoft.Json;
using NuGet.Protocol;
using WebAppNASA.Models;
using static System.Net.WebRequestMethods;

namespace WebAppNASA.Controllers
{
    public class ApodController : Controller
    {
        private readonly HttpClient _httpClient;

        public ApodController(IHttpClientFactory httpClientFactory)
        { 
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://api.nasa.gov/");
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _httpClient.GetAsync("planetary/apod?api_key=DEMO_KEY");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var _apod = JsonConvert.DeserializeObject<ApodViewModel>(content);

                    return View("Index", _apod);
                }
            }
            catch (Exception)
            {
                throw;
            }
            return View();
        }
    }
}
