using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Drawing;
using System.Net.Http;
using WebAppNASA.Models;
using static System.Net.WebRequestMethods;

namespace WebAppNASA.Controllers
{
    public class EpicController : Controller
    {
        private readonly HttpClient _httpClientList;
        private readonly HttpClient _httpClient;
        private readonly EpicViewModel _epicModel;

        public EpicController(IHttpClientFactory httpClientFactory)
        {
            _httpClientList = httpClientFactory.CreateClient();
            _httpClientList.BaseAddress = new Uri("https://api.nasa.gov/");

            _httpClient = httpClientFactory.CreateClient();
            _httpClient.BaseAddress = new Uri("https://api.nasa.gov/");

            _epicModel = new EpicViewModel();
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var response = await _httpClientList.GetAsync("EPIC/api/natural?api_key=DEMO_KEY");
                
                if (response.IsSuccessStatusCode)
                {
                    //Read and Deserialize object for call at the action that return list of Epic
                    var content = await response.Content.ReadAsStringAsync();
                    var _epicList = JsonConvert.DeserializeObject<List<EpicViewModel>>(content);

                    if (_epicList != null)
                    {
                        _epicModel.Identifier = _epicList[0].Identifier;
                        _epicModel.Caption = _epicList[0].Caption;
                        _epicModel.Image = _epicList[0].Image;
                        _epicModel.Date = _epicList[0].Date;

                        var dateFormat = _epicList[0].Date.ToString("yyyy/MM/dd");

                        //Read and Deserialize object for call at the action that return one image (the firs image of list). The route is a string made up of several parameters from the previous query
                        _epicModel.Path = "https://api.nasa.gov/EPIC/archive/natural/" + dateFormat + "/png/" + _epicList[0].Image.ToString() + ".png?api_key=DEMO_KEY";

                        return View("Index", _epicModel);
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
