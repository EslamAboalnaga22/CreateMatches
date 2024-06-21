using LeagueApp.Data;
using LeagueApp.Models;
using LeagueApp.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Drawing;

namespace LeagueApp.Services
{
    public class MatchesService : IMatchesService
    {
        private readonly AppDbContext context;

        public MatchesService(AppDbContext context)
        {
            this.context = context;
        }

        public List<Matches> GetAll()
        {
            return context.Matches
                .Include(x => x.One)
                .Include(x => x.Two)
                .ToList();
        }
        public Matches? GetById(int id)
        {
            return context.Matches.SingleOrDefault(t => t.Id == id);
        }

        public Matches? CreateResult(AddResultViewModel model)
        {
            var match = context.Matches.SingleOrDefault(x =>x.Id == model.Id);

            var teamOne = context.Teams.SingleOrDefault(x => x.Id == match.TeamOne);
            var teamTwo = context.Teams.SingleOrDefault(x => x.Id == match.TeamTwo);

            match.GoalsTeamOne = model.GoalsTeamOne;
            match.GoalsTeamTwo = model.GoalsTeamTwo;
            //Points
            teamOne.Played += 1;
            teamOne.GF += model.GoalsTeamOne;
            teamOne.GA += model.GoalsTeamTwo;
            teamOne.GD = teamOne.GF - teamOne.GA;

            teamTwo.Played += 1;
            teamTwo.GF += model.GoalsTeamTwo;
            teamTwo.GA += model.GoalsTeamOne;
            teamTwo.GD = teamTwo.GF - teamTwo.GA;

            if (model.GoalsTeamOne > model.GoalsTeamTwo)
            {
                teamOne.Points += 3;
                teamOne.Won += 1;
               
                teamTwo.Lost += 1;
            }
            else if (model.GoalsTeamOne == model.GoalsTeamTwo)
            {
                teamOne.Points += 1;
                teamOne.Drawn += 1;

                teamTwo.Points += 1;
                teamTwo.Drawn += 1;
            }
            else if (model.GoalsTeamOne < model.GoalsTeamTwo)
            {
                teamOne.Lost += 1;

                teamTwo.Points += 3;
                teamTwo.Won += 1;
            }
            context.SaveChanges();
            return match;
        }

        public Matches? UpdateResult(EditResultViewModel model)
        {
            var match = context.Matches.SingleOrDefault(x => x.Id == model.Id);

            var teamOne = context.Teams.SingleOrDefault(x => x.Id == match.TeamOne);
            var teamTwo = context.Teams.SingleOrDefault(x => x.Id == match.TeamTwo);

            // here update table --> from old results
            teamOne.GF -= (int) match.GoalsTeamOne;
            teamOne.GA -= (int) match.GoalsTeamTwo;
            teamOne.GD = teamOne.GF - teamOne.GA;

            teamTwo.GF -= (int) match.GoalsTeamTwo;
            teamTwo.GA -= (int) match.GoalsTeamOne;
            teamTwo.GD = teamTwo.GF - teamTwo.GA;

            if (match.GoalsTeamOne > match.GoalsTeamTwo)
            {
                teamOne.Points -= 3;
                teamOne.Won -= 1;

                teamTwo.Lost -= 1;
            }
            else if (match.GoalsTeamOne == match.GoalsTeamTwo)
            {
                teamOne.Points -= 1;
                teamOne.Drawn -= 1;

                teamTwo.Points -= 1;
                teamTwo.Drawn -= 1;
            }
            else if (match.GoalsTeamOne < match.GoalsTeamTwo)
            {
                teamOne.Lost -= 1;

                teamTwo.Points -= 3;
                teamTwo.Won -= 1;
            }

            // here update table --> after new result
            match.GoalsTeamOne = model.GoalsTeamOne;
            match.GoalsTeamTwo = model.GoalsTeamTwo;

            teamOne.GF += model.GoalsTeamOne;
            teamOne.GA += model.GoalsTeamTwo;
            teamOne.GD = teamOne.GF - teamOne.GA;

            teamTwo.GF += model.GoalsTeamTwo;
            teamTwo.GA += model.GoalsTeamOne;
            teamTwo.GD = teamTwo.GF - teamTwo.GA;

            if (model.GoalsTeamOne > model.GoalsTeamTwo)
            {
                teamOne.Points += 3;
                teamOne.Won += 1;

                teamTwo.Lost += 1;
            }
            else if (model.GoalsTeamOne == model.GoalsTeamTwo)
            {
                teamOne.Points += 1;
                teamOne.Drawn += 1;

                teamTwo.Points += 1;
                teamTwo.Drawn += 1;
            }
            else if (model.GoalsTeamOne < model.GoalsTeamTwo)
            {
                teamOne.Lost += 1;

                teamTwo.Points += 3;
                teamTwo.Won += 1;
            }

            context.SaveChanges();
            return match;
        }

    }
}
