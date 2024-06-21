using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeagueApp.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; } =string.Empty;
        [DefaultValue(null)]
        public int Points { get; set; }
        [DefaultValue(null)]
        public int Played { get; set; }
        [DefaultValue(null)]
        public int Won { get; set; }
        [DefaultValue(null)]
        public int Drawn { get; set; }
        [DefaultValue(null)]
        public int Lost { get; set; }
        [DefaultValue(null)]     
        public int GF { get; set; }
        [DefaultValue(null)]
        public int GA { get; set; }

        [DefaultValue(null)]
        public int GD { get; set; }
        public string ImageUrl { get; set; } = string.Empty;

        public ICollection<Matches> MatchesOne { get; set; } = new List<Matches>();
        public ICollection<Matches> MatchesTwo { get; set; } = new List<Matches>();

    }

}

