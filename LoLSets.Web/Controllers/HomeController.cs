using LoLSets.Core.Entities;
using LoLSets.Core.Interfaces;
using LoLSets.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace LoLSets.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMobafireService _mobafireService;

        public HomeController(
            IMobafireService mobafireService)
        {
            _mobafireService = mobafireService;
        }

        [HttpGet]
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("/")]
        public async Task<IActionResult> Index(GetSetViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            ItemSet itemSet = await _mobafireService.GetItemSetAsync(model.Link, model.Title);
            
            var settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            string json = JsonConvert.SerializeObject(itemSet, settings);

            var stream = new MemoryStream(Encoding.UTF8.GetBytes(json ?? ""));

            var response = File(stream, "application/octet-stream", "build.json"); // FileStreamResult
            return response;
        }
    }
}