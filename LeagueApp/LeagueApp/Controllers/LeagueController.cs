using LeagueApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace LeagueApp.Controllers
{
    public class LeagueController : Controller
    {
        private readonly ITeamsService teamsService;
        private readonly IMatchesService matchesService;

        public LeagueController(ITeamsService teamsService, IMatchesService matchesService)
        {
            this.teamsService = teamsService;
            this.matchesService = matchesService;
        }
        public IActionResult Index()
        {
            var table = teamsService.GetAll();
            return View(table);
        }
    }
}
