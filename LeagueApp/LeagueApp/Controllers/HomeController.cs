using LeagueApp.Models;
using LeagueApp.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LeagueApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITeamsService teamsService;

        public HomeController(ITeamsService teamsService)
        {
            this.teamsService = teamsService;
        }

        public IActionResult Index()
        {
           // var table = teamsService.GetAll();
            return View();
        }
        public IActionResult Match()
        {
            teamsService.CreateMatchesLeague();
            return RedirectToAction(nameof(Index));
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}