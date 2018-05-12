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
    public class TeamsController : ApiController
    {
        string table = "team";
        // GET api/teams
        public List<Team> Get()
        {
            List<Team> answer = new List<Team>();

            MySqlConnection conn = WebApiConfig.conn();
            MySqlCommand query = conn.CreateCommand();
            query.CommandText = "SELECT * from " + table;

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
                Team team = new Team(fetch_query.GetInt32("Id"), fetch_query.GetString("Name"), fetch_query.GetString("ClubName"), fetch_query.GetString("MainCoachName"), fetch_query.GetString("LeagueScore"));
                answer.Add(team);
            }
            fetch_query.Close();
            conn.Close();

            return answer;
        }

        // GET api/teams/5
        public Team Get(int id)
        {
            Team answer = null;
            MySqlConnection conn = WebApiConfig.conn();
            MySqlCommand query = conn.CreateCommand();
            query.CommandText = "SELECT * from " + table + " where Id = " + id;

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
                answer = new Team(fetch_query.GetInt32("Id"), fetch_query.GetString("Name"), fetch_query.GetString("ClubName"), fetch_query.GetString("MainCoachName"), fetch_query.GetString("LeagueScore"));
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