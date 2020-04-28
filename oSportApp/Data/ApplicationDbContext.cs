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
                    Id = 1,
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
            builder.Entity<Position>()
                .HasData(
                new Position
                {
                    Id = 1,
                    Name = "Goalkeeper", 
                    Abbreviation = "GK",
                },
                new Position
                {
                    Id = 2,
                    Name = "Right Fullback",
                    Abbreviation = "RB",
                },
                new Position
                {
                    Id = 3,
                    Name = "Left Fullback",
                    Abbreviation = "LB",
                },
                new Position
                {
                    Id = 4,
                    Name = "Center Back",
                    Abbreviation = "CB",
                },
                new Position
                {
                    Id = 5,
                    Name = "Sweeper", 
                    Abbreviation = "SW",
                },
                new Position
                {
                    Id = 6,
                    Name = "Defending/Holding Midfielder",
                    Abbreviation = "DM",
                },
                new Position
                {
                    Id = 7,
                    Name = "Right Midfielder/Winger",
                    Abbreviation = "RM",
                },
                new Position
                {
                    Id = 8,
                    Name = "Central/Box-to-Box Midfielder",
                    Abbreviation = "CM",
                },
                new Position
                {
                    Id = 9,
                    Name = "Striker", 
                    Abbreviation = "S",
                },
                new Position
                {
                    Id = 10,
                    Name = "Attacking Midfielder/Playmaker", 
                    Abbreviation = "AM",
                },
                new Position
                {
                    Id = 11,
                    Name = "Left Midfielder/Wingers",
                    Abbreviation = "LM",
                },
                new Position
                {
                    Id = 12, 
                    Name = "Center Forward",
                    Abbreviation = "CF",
                },
                new Position
                {
                    Id = 13,
                    Name = "Second Striker",
                    Abbreviation = "SS",
                },
                new Position
                {
                    Id = 14,
                    Name = "Left Wingback", 
                    Abbreviation = "LWB",
                },
                new Position
                {
                    Id = 15, 
                    Name = "Right Wingback", 
                    Abbreviation = "RWB",
                }
                );

        }

        public DbSet<Owner> Owners { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<OwnerLeague> OwnerLeagues { get; set; }
        public DbSet<Referee> Referees { get; set; }
        public DbSet<Coach> Coaches { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<CoachTeam> CoachTeams { get; set; }
        public DbSet<TeamPlayer> TeamPlayers { get; set; }
        public DbSet<Field> Fields { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<LeagueTeam> LeagueTeams { get; set; }
        public DbSet<MatchDay> MatchDays { get; set; }
        public DbSet<Match> Matches { get; set; }
    }
}
