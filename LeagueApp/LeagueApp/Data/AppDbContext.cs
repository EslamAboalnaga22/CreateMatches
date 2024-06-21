using LeagueApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace LeagueApp.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)
        {
            
        }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Matches> Matches { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Matches>()
                .HasOne(o => o.One)
                .WithMany(m => m.MatchesOne)
                .HasForeignKey(x => x.TeamOne)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Matches>()
                .HasOne(t => t.Two)
                .WithMany(m => m.MatchesTwo)
                .HasForeignKey(x => x.TeamTwo)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
