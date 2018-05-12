using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Champions_league.Models
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ClubName { get; set; }
        public string MainCoachName { get; set; }
        public string LeagueScore { get; set; }

        public Team(int id, string name, string clubName, string mainCoachName, string leagueScore)
        {
            Id = id;
            Name = name;
            ClubName = clubName;
            MainCoachName = mainCoachName;
            LeagueScore = leagueScore;
        }
    }
}