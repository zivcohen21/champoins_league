using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Champions_league.Models;
using MySql.Data.MySqlClient;

namespace Champions_league.Controllers
{
    public class AvgGoalsPerTeam
    {
        public double AvgGoals { get; set; }

        public AvgGoalsPerTeam(double avgGoals)
        {
            AvgGoals = avgGoals;
        }
    }

    public class AvgGoalsPerTeamController : ApiController
    {
        // GET api/avggoalsperteam/5
        public AvgGoalsPerTeam Get(int id)
        {
            AvgGoalsPerTeam answer = null;

            MySqlConnection conn = WebApiConfig.conn();
            MySqlCommand query = conn.CreateCommand();

            query.CommandText = "SELECT AVG(a.GoalsPerDate) as AvgGoals from(SELECT SUM(GoalCount) as GoalsPerDate, TrainingTimestamp as dates FROM player_statistics where PlayerId in (select UserId FROM team_player where TeamId = " + id +") GROUP BY TrainingTimestamp) as a";

            try
            {
                conn.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw;
            }

            MySqlDataReader fetch_query = query.ExecuteReader();

            if(fetch_query.Read())
            {                 
                answer = new AvgGoalsPerTeam(fetch_query.GetDouble("AvgGoals"));
            }
            else { } //Todo- handle not existing player
            fetch_query.Close();
            conn.Close();

            return answer;
        }
    }
}