using Microsoft.AspNetCore.Mvc;
using CharacterSheetManager.Models;
using System.Diagnostics;

namespace CharacterSheetManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddCharacter()
        {
            return View();
        }

        public IActionResult ManageCharacters()
        {
            return View();
        }

        public IActionResult ManageItems()
        {
            return View();
        }

        public IActionResult ManageSpells()
        {
            return View();
        }

        public IActionResult Profile()
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