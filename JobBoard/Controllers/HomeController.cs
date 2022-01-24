using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using JobBoard.Models;
using JobBoard.ViewModels.OutputModel;
using JobBoard.Handlers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using JobBoard.ViewModels.InputModel;

namespace JobBoard.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        RegionReader _regionReader = new RegionReader();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            SearchInPutModel response = new SearchInPutModel
            {
              //  Regions = new SelectList(await _regionReader.GetRegionsAsync()),
            };
            return View(response);
        }

        [Route("about")]
        public IActionResult About()
        {
            return View();
        }

        [Route("contact-us")]
        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
