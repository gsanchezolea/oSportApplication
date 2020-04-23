using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace oSportApp.Models
{
    public class LeagueTeam
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("League")]
        public int LeagueId { get; set; }
        public OwnerLeague League { get; set; }

        [ForeignKey("Team")]
        public int TeamId { get; set; }
        public CoachTeam Team { get; set; }
    }
}
