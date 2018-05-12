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
    public class PlayersByTeamController : ApiController
    {
        // GET api/playersbyteam/5
        public List<Player> Get(int id)
        {
            List<Player> answer = new List<Player>();

            MySqlConnection conn = WebApiConfig.conn();
            MySqlCommand query = conn.CreateCommand();
            query.CommandText = "SELECT * FROM player where Id in (select UserId FROM team_player where TeamId =" + id + ")";

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
    }
}