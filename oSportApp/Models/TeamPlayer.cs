using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace oSportApp.Models
{
    public class TeamPlayer
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("CoachTeam")]
        public int? CoachTeamId { get; set; }
        public CoachTeam CoachTeam { get; set; }

        [ForeignKey("Player")]
        public int? PlayerId { get; set; }
        public Player Player { get; set; }

        [ForeignKey("Position")]
        public int PositionId { get; set; }
        public Position Position { get; set; }

        public int Goals { get; set; }

        [Required]
        [Display(Name = "Kit #")]
        public int KitNumber { get; set; }

        public bool Approved { get; set; }
    }
}
