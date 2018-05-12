using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;

namespace Champions_league.Controllers
{
    public class AvgBallTouchesPerPlayerController : ApiController
    {
        public class AvgBallTouchesPerPlayer
        {
            public double AvgBallTouches { get; set; }

            public AvgBallTouchesPerPlayer(double avgBallTouches)
            {
                AvgBallTouches = avgBallTouches;
            }
        }

        // GET api/avgballtouchesperplayer/5
        public AvgBallTouchesPerPlayer Get(int id)
        {
            AvgBallTouchesPerPlayer answer = null;

            MySqlConnection conn = WebApiConfig.conn();
            MySqlCommand query = conn.CreateCommand();

            query.CommandText = "SELECT AVG(BallTouches) as AvgBallTouches FROM player_statistics where PlayerId = " + id;

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
                answer = new AvgBallTouchesPerPlayer(fetch_query.GetDouble("AvgBallTouches"));
            }
            else { } //Todo- handle not existing player
            fetch_query.Close();
            conn.Close();

            return answer;
        }
    }
}