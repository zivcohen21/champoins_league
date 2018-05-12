using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;

namespace Champions_league.Controllers
{
    public class AvgScorePerPlayerController : ApiController
    {
        public class AvgScorePerPlayer
        {
            public double AvgScore { get; set; }

            public AvgScorePerPlayer(double avgScore)
            {
                AvgScore = avgScore;
            }
        }

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/avgscoreperplayer/5
        public AvgScorePerPlayer Get(int id)
        {
            AvgScorePerPlayer answer = null;

            MySqlConnection conn = WebApiConfig.conn();
            MySqlCommand query = conn.CreateCommand();

            query.CommandText = "SELECT AVG(GoalCount) as AvgScore FROM player_statistics where PlayerId = "+ id;

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
                answer = new AvgScorePerPlayer(fetch_query.GetDouble("AvgScore"));
            }
            else { } //Todo- handle not existing player
            fetch_query.Close();
            conn.Close();

            return answer;
        }


        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}