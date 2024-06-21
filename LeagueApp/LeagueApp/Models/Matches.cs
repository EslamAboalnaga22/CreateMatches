using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeagueApp.Models
{
    public class Matches
    {
        public int Id { get; set; }
        public int? GoalsTeamOne { get; set; }
        public int? GoalsTeamTwo { get; set; }

        public int TeamOne { get; set; } 
        public Team? One { get; set; }

        public int TeamTwo { get; set; }
        public Team? Two { get; set; }

    }
}
