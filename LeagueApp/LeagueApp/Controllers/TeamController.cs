using LeagueApp.Models;
using LeagueApp.Services;
using LeagueApp.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LeagueApp.Controllers
{
    public class TeamController : Controller
    {
        private readonly ITeamsService teamsService;

        public TeamController(ITeamsService teamsService)
        {
            this.teamsService = teamsService;
        }
        public IActionResult Index()
        {
            var Teams = teamsService.GetAll();
            return View(Teams);
        }

        [HttpGet]
        public IActionResult Create()
        {
            AddTeamsViewModel addTeamsViewModel = new AddTeamsViewModel();
            return View(addTeamsViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddTeamsViewModel model)
        {
            await teamsService.Add(model);
            return RedirectToAction(nameof(Create));
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            var team = teamsService.GetById(id);

            if (team is null) 
            {
                return NotFound();
            }

            EditTeamViewModel viewModel = new()
            {
                Id = team.Id,
                Name = team.Name,
                CurrentCover = team.ImageUrl
            };

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(EditTeamViewModel model)
        {
            await teamsService.Edit(model);
            return RedirectToAction(nameof(Index) , "League");
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            teamsService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
