using LeagueApp.Models;
using LeagueApp.Services;
using LeagueApp.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace LeagueApp.Controllers
{
    public class MatchesController : Controller
    {
        private readonly IMatchesService matchesService;

        public MatchesController(IMatchesService matchesService)
        {
            this.matchesService = matchesService;
        }

        public IActionResult Index()
        {
            var matches = matchesService.GetAll();
            return View(matches);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateResult()
        {
            AddResultViewModel viewModel = new();

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateResult(AddResultViewModel model)
        {
            matchesService.CreateResult(model);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateResult(int id)
        {
            var match = matchesService.GetById(id);

            if (match is null)
                return NotFound();

            EditResultViewModel viewModel = new()
            {
                Id = id,
                GoalsTeamOne = (int) match.GoalsTeamOne,
                GoalsTeamTwo = (int) match.GoalsTeamTwo,
            };

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult UpdateResult(EditResultViewModel model)
        {
            matchesService.UpdateResult(model);
            return RedirectToAction(nameof(Index));
        }

    }
}
