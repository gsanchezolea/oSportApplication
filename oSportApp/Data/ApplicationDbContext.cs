using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using oSportApp.Models;

namespace oSportApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>()
            .HasData(
            new IdentityRole
            {
                Name = "Owner",
                NormalizedName = "OWNER",
            },
            new IdentityRole
            {
                Name = "Coach",
                NormalizedName = "COACH",
            },
            new IdentityRole
            {
                Name = "Referee",
                NormalizedName = "REFEREE",
            },
            new IdentityRole
            {
                Name = "Player",
                NormalizedName = "PLAYER",
            }
            );
            builder.Entity<Sport>()
                .HasData(
                new Sport
                {
                    Id  = 1,
                    Name = "Soccer",
                },
                new Sport
                {
                    Id = 2,
                    Name = "Football",
                },
                new Sport
                {
                    Id = 3,
                    Name = "Basketball",
                }
                );
        }

        public DbSet<Owner> Owners { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<OwnerLeague> OwnerLeagues { get; set; }
        public DbSet<LeagueReferee> LeagueReferees { get; set; }
        public DbSet<Referee> Referees { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<CoachTeam> CoachTeams { get; set; }
        public DbSet<TeamPlayer> TeamPlayers { get; set; }
        public DbSet<LeagueTeam> LeagueTeams { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Match> Matches { get; set; }
        public DbSet<LeagueMatch> LeagueMatches { get; set; }
    }
}
