using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace oSportApp.Models
{
    public class League
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Sport")]
        public int SportId { get; set; }
        public Sport Sport { get; set; }

        [Required]
        [Display(Name = "League Name")]
        public string Name { get; set; }

        [Required]
        public int Capacity { get; set; }
    }
}
