using LeagueApp.Models;
using LeagueApp.ViewModel;

namespace LeagueApp.Services
{
    public interface IMatchesService
    {
        public List<Matches> GetAll();
        public Matches? GetById(int id);
        public Matches? CreateResult(AddResultViewModel model);
        public Matches? UpdateResult(EditResultViewModel model);
    }
}
