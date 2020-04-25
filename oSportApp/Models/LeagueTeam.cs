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

        [ForeignKey("OwnerLeague")]
        public int OwnerLeagueId { get; set; }
        public OwnerLeague OwnerLeague { get; set; }

        [ForeignKey("CoachTeam")]
        public int CoachTeamId { get; set; }
        public CoachTeam CoachTeam { get; set; }

        public bool Approved { get; set; }
    }
}
