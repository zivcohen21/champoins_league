using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;

namespace Champions_league.Controllers
{
    public class MaxMotionScorePerPlayerController : ApiController
    {
        public class MaxMotionScorePerPlayer
        {
            public double MaxMotionScore { get; set; }

            public MaxMotionScorePerPlayer(double maxMotionScore)
            {
                MaxMotionScore = maxMotionScore;
            }
        }

        // GET api/maxmotionscoreperPlayer/5
        public MaxMotionScorePerPlayer Get(int id)
        {
            MaxMotionScorePerPlayer answer = null;

            MySqlConnection conn = WebApiConfig.conn();
            MySqlCommand query = conn.CreateCommand();

            query.CommandText = "SELECT MAX(MotionScore) as MaxMotionScore FROM player_statistics where PlayerId = " + id;

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
                answer = new MaxMotionScorePerPlayer(fetch_query.GetDouble("MaxMotionScore"));
            }
            else { } //Todo- handle not existing player
            fetch_query.Close();
            conn.Close();

            return answer;
        }
    }
}