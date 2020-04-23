using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace oSportApp.Models
{
    public class CoachTeam
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Coach")]
        public int CoachId { get; set; }
        public Coach Coach { get; set; }

        [ForeignKey("Team")]
        public int TeamId { get; set; }
        public Team Team { get; set; }

    }
}
