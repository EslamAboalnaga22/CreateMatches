using LeagueApp.Models;
using LeagueApp.ViewModel;

namespace LeagueApp.Services
{
    public interface ITeamsService
    {
        public List<Team> GetAll();
        public Team? GetById(int id);
        public Task Add(AddTeamsViewModel model);
        public Task<Team?> Edit(EditTeamViewModel viewModel);
        public bool Delete(int id);
        public void CreateMatchesLeague();

    }
}
