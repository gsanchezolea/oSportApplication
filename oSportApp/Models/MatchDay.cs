using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace oSportApp.Models
{
    public class MatchDay
    {
        [Key]
        public int Id { get; set; }

        public int Day { get; set; }

    }
}
