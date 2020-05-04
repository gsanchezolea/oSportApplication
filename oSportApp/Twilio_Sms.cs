using oSportApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace oSportApp
{
    public class Twilio_Sms
    {
        public void SendSms(Match match) //takes in the id of the league team home/away teams
        {     
            List<string> phoneNumbers = new List<string>();

            var refereePhone = "+1" + match.Referee.PhoneNumber;
            var homeCoachPhone = "+1" + match.HomeTeam.CoachTeam.Coach.PhoneNumber;
            var awayCoachPhone = "+1" + match.AwayTeam.CoachTeam.Coach.PhoneNumber;

            phoneNumbers.Add(refereePhone);
            phoneNumbers.Add(homeCoachPhone);
            phoneNumbers.Add(awayCoachPhone);

            var accountSid = Twilio_Data.Account_Sid;
            var authToken = Twilio_Data.Auth_Token;

            var from = Twilio_Data.Phone_Number;

            var body = "oSport - League Notification : " +
                match.HomeTeam.CoachTeam.Team.Name + " vs. " +
                match.AwayTeam.CoachTeam.Team.Name + ", has been sheduled for " +
                match.Date.Date + " @ " +
                match.Field.Name + "," + 
                match.Field.StreetAddress + ", " +
                match.Field.City + ", " + 
                match.Field.State + " " +
                match.Field.ZipCode + "."; 

            TwilioClient.Init(accountSid, authToken);

            foreach (var to in phoneNumbers)
            {
                var message = MessageResource.Create(
                    from: from,
                    to: to,
                    body: body
                    );
                Console.WriteLine(message.Sid);
            }


        }

    }
}
