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
    public class AvgBallTouchesPerTeamController : ApiController
    {
        public class AvgBallTouchesPerTeam
        {
            public double AvgBallTouches { get; set; }

            public AvgBallTouchesPerTeam(double avgBallTouches)
            {
                AvgBallTouches = avgBallTouches;
            }
        }

        // GET api/avgballtouchesperteam/5
        public AvgBallTouchesPerTeam Get(int id)
        {
            AvgBallTouchesPerTeam answer = null;

            MySqlConnection conn = WebApiConfig.conn();
            MySqlCommand query = conn.CreateCommand();

            query.CommandText = "SELECT AVG(a.BallTouchesPerPlayer) as AvgBallTouches from(SELECT PlayerId, SUM(BallTouches) as BallTouchesPerPlayer FROM player_statistics where PlayerId in (select UserId FROM team_player where TeamId = " + id + ") GROUP BY PlayerId) as a";

            try
            {
                conn.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw;
            }

            MySqlDataReader fetch_query = query.ExecuteReader();

            if (fetch_query.Read())
            {
                answer = new AvgBallTouchesPerTeam(fetch_query.GetDouble("AvgBallTouches"));
            }
            else { } //Todo- handle not existing player
            fetch_query.Close();
            conn.Close();

            return answer;
        }
    }
}