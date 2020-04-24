using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace oSportApp.Models
{
    public class Position
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Playing Position")]
        public string Name { get; set; }

        public string Abbreviation { get; set; }
    }
}
