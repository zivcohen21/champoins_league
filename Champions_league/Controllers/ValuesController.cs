using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MySql.Data.MySqlClient;
using System.Web.Mvc;
using Newtonsoft.Json;
using Champions_league.Models;


namespace Champions_league.Controllers
{

    public class ValuesController : ApiController
    {
        // GET api/values
        public List<Player> Get()
        {
            List<Player> answer = new List<Player>();

            MySqlConnection conn = WebApiConfig.conn();
            MySqlCommand query = conn.CreateCommand();
            query.CommandText = "SELECT * from player";

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
                Player player = new Player(fetch_query.GetInt32("Id"), fetch_query.GetString("FirstName"), fetch_query.GetString("LastName"), fetch_query.GetString("PhoneNumber"), fetch_query.GetInt32("ShirtNumber"), fetch_query.GetString("DominantLeg"));
                answer.Add(player);
            }
            fetch_query.Close();           
            conn.Close();

            return answer;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
