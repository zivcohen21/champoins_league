using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Champions_league.Models
{
    public class Player
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public int ShirtNumber { get; set; }
        public string DominantLeg { get; set; }

        public Player(int id, string firstName, string lastName, string phoneNumber, int shirtNumber, string dominantLeg)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            ShirtNumber = shirtNumber;
            DominantLeg = dominantLeg;
        }
    }

}