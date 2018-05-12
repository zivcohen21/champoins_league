using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;

namespace Champions_league.Controllers
{
    public class UpdateTeamScoreController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/updateteamscore/5
        public void Get(int id)
        {
            MySqlConnection conn = WebApiConfig.conn();
            MySqlCommand query = conn.CreateCommand();
            string mySqlUpdate = "update team SET LeagueScore = (SELECT AVG(a.AccuratePassesPerPlayer) as AvgAccuratePasses from" +
                "(SELECT PlayerId, SUM(AccuratePasses) as AccuratePassesPerPlayer FROM player_statistics where PlayerId in" + 
                "(select UserId FROM team_player where TeamId =" + id + ") GROUP BY PlayerId) as a) WHERE Id = " + id;

            try
            {
                conn.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw;
            }

            query.CommandText = mySqlUpdate;
            query.ExecuteNonQuery();

            conn.Close();
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