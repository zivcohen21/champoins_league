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
    public class TrainingDatesController : ApiController
    {

        public class TrainingDates
        {
            public long Date {get; set;}

            public TrainingDates(long date)
            {
                Date = date;
            }
        }


    // GET api/<controller>
    public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/trainingdates/5
        public List<TrainingDates> Get(int id)
        {
            List<TrainingDates> answer = new List<TrainingDates>();

            MySqlConnection conn = WebApiConfig.conn();
            MySqlCommand query = conn.CreateCommand();
            
            query.CommandText = "SELECT distinct TrainingTimestamp FROM player_statistics where PlayerId in (select UserId FROM team_player where TeamId = " + id + ") order by TrainingTimestamp;";

            try
            {
                conn.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw;
            }

            MySqlDataReader fetch_query = query.ExecuteReader();

            while (fetch_query.Read())
            {
                TrainingDates trainingDates = new TrainingDates(fetch_query.GetInt64("TrainingTimestamp"));
                answer.Add(trainingDates);
            }
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

