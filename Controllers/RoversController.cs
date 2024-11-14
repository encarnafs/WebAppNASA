using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WebAppNASA.Models;

namespace WebAppNASA.Controllers
{
    public class RoversController : Controller
    {
        private readonly HttpClient _httpClient;
        private RoversViewModel _rovers;


        public RoversController(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://api.nasa.gov/");

            _rovers = new RoversViewModel();
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _httpClient.GetAsync("mars-photos/api/v1/rovers/curiosity/photos?sol=1000&page=2&api_key=DEMO_KEY");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();

                    if (content != null)
                    {
                        JObject objeto = JObject.Parse(content);
                        _rovers.Id = objeto["photos"][0]["id"].ToString();
                        _rovers.Img_src = objeto["photos"][0]["img_src"].ToString();
                        _rovers.Earth_date = objeto["photos"][0]["earth_date"].ToString();

                        return View("Index", _rovers);
                    }
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
