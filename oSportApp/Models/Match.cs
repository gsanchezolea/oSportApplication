using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace oSportApp.Models
{
    public class Match
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("HomeTeam")]
        public int? HomeTeamId { get; set; }
        public LeagueTeam HomeTeam { get; set; }

        [ForeignKey("AwayTeam")]
        public int? AwayTeamId { get; set; }
        public LeagueTeam AwayTeam { get; set; }

        [ForeignKey("Referee")]
        public int? RefereeId { get; set; }
        public LeagueReferee Referee { get; set; }

        [ForeignKey("Field")]
        public int? FieldId { get; set; }
        public Field Field { get; set; }

        [Display(Name = "Match Day")]
        public int Day { get; set; }

        [Display(Name = "Match Date")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Date { get; set; }

        [Display(Name = "Match Time")]
        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Time { get; set; }

        [Required]
        [Display(Name = "Home Team Score")]
        public int HomeTeamScore { get; set; }

        [Required]
        [Display(Name = "Away Team Score")]
        public int AwayTeamScore { get; set; }

        [Display(Name = "Match Completed")]
        public bool Completed { get; set; }

    }
}
