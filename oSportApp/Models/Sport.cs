﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace oSportApp.Models
{
    public class Sport
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Sport")]
        public string Name { get; set; }
    }
}
