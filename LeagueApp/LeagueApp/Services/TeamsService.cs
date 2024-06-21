using LeagueApp.Data;
using LeagueApp.Models;
using LeagueApp.Settings;
using LeagueApp.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeagueApp.Services
{
    public class TeamsService : ITeamsService
    {
        private readonly AppDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly string imagesPath;

        public TeamsService(AppDbContext context
                    , IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
            imagesPath = $"{webHostEnvironment.WebRootPath}{FileSettings.ImagePath}";
        }

        public List<Team> GetAll()
        {
            return context.Teams
                .OrderByDescending(x => x.Points)
                .ThenByDescending(x => x.GD)
                .ToList();
        }
        public Team? GetById(int id)
        {
            return context.Teams.SingleOrDefault(t => t.Id == id);
        }


        public async Task Add(AddTeamsViewModel model)
        {
            var coverName = await SaveCover(model.Cover);

            Team team = new()
            {
                Id = model.Id,
                Name = model.Name,
                ImageUrl = coverName
            };
            context.Teams.Add(team);
            context.SaveChanges();
            
        }

        public async Task<Team?> Edit(EditTeamViewModel model)
        {
            var team = context.Teams.SingleOrDefault(t => t.Id == model.Id);

            if (team is null)
            {
                return null;
            }

            var hasNewCover = model.Cover is not null;
            var oldCover = team.ImageUrl;

            team.Name = model.Name;
            
            if (hasNewCover)
            {
                team.ImageUrl = await SaveCover(model.Cover);
            }

            var affectedRows = context.SaveChanges();

            if(affectedRows > 0) 
            {
                if (hasNewCover)
                {
                    var cover = Path.Combine(imagesPath, oldCover);
                    File.Delete(cover);
                }
                return team;
            }
            else
            {
                var cover = Path.Combine(imagesPath, team.ImageUrl);
                File.Delete(cover);
                return null;
            }        
        }

        public bool Delete(int id)
        {
            var isDeleted = false;

            var team = GetById(id);

            if (team is null)
                return isDeleted;

            context.Teams.Remove(team);

            var effectedRows = context.SaveChanges();

            if (effectedRows > 0)
            {
                isDeleted = true;
                var cover = Path.Combine(imagesPath,team.ImageUrl);
                File.Delete(cover);
            }
            return isDeleted;
        }

        public void CreateMatchesLeague()
        {
            var teams = context.Teams.Select(x => x.Id).ToList();

            Random rnd = new Random();
            teams = teams.Select(i => new { value = i, rank = rnd.Next(teams.Count()) }).OrderBy(n => n.rank).Select(n => n.value).ToList();

            int count = teams.Count() / 2;

            var split1 = teams.GetRange(0, count);
            var split2 = teams.GetRange(count, count);

            for (int i = 0; i < teams.Count() - 1; i++)
            {
                //Console.WriteLine($"Week {i + 1}");

                for (int j = 0; j < split1.Count(); j++)
                {
                    //Console.WriteLine($"{split1[j]} VS {split2[j]}");
                    Matches match = new()
                    {
                        TeamOne = split1[j],
                        TeamTwo = split2[j]
                    };
                    context.Matches.Add(match);
                    context.SaveChanges();
                }

                split1.Insert(1, split2[0]);
                split2.RemoveAt(0);
                split2.Insert(split2.Count(), split1[split1.Count() - 1]);
                split1.RemoveAt(split1.Count() - 1);
            }
        }

        private async Task<string> SaveCover(IFormFile cover)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";

            var path = Path.Combine(imagesPath, coverName) ; 

            using var stream = File.Create(path);
            await cover.CopyToAsync(stream);

            return coverName;
        }

        
    }
}
