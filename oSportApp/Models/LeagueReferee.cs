using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace oSportApp.Models
{
    public class LeagueReferee
    {
        [Key]
        public int Id { get; set; }
        
        [ForeignKey("League")]
        public int LeagueId { get; set; }
        public OwnerLeague League { get; set; }

        [ForeignKey("Referee")]
        public int RefereeId { get; set; }
        public Referee Referee { get; set; }

        public bool Approved { get; set; }

    }
}
