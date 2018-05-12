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
    public class PlayersController : ApiController
    {
        string table = "player";

        // GET api/players
        public List<Player> Get()
        {
            List<Player> answer = new List<Player>();

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
                Player player = new Player(fetch_query.GetInt32("Id"), fetch_query.GetString("FirstName"), fetch_query.GetString("LastName"), fetch_query.GetString("PhoneNumber"), fetch_query.GetInt32("ShirtNumber"), fetch_query.GetString("DominantLeg"));
                answer.Add(player);
            }
            fetch_query.Close();           
            conn.Close();

            return answer;
        }

        // GET api/players/5
        public Player Get(int id)
        {
            Player answer = null;
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
            if(fetch_query.Read())
            {
                answer = new Player(fetch_query.GetInt32("Id"), fetch_query.GetString("FirstName"), fetch_query.GetString("LastName"), fetch_query.GetString("PhoneNumber"), fetch_query.GetInt32("ShirtNumber"), fetch_query.GetString("DominantLeg"));
            }   
            else { } //Todo- handle not existing player
          
            fetch_query.Close();
            conn.Close();

            return answer;
        }

        // POST api/players
        public void Post([FromBody]Player player)
        {
            MySqlConnection conn = WebApiConfig.conn();
            MySqlCommand query = conn.CreateCommand();
            string mySqlInsert = "INSERT INTO " + table + " (Id, FirstName, LastName, PhoneNumber, ShirtNumber, DominantLeg) VALUES (" + player.Id + ",'" + player.FirstName + "','" + player.LastName + "','" + player.PhoneNumber + "'," + player.ShirtNumber + ",'" + player.DominantLeg + "')";
           
            try
            {
                conn.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw;
            }

            query.CommandText = mySqlInsert;
            query.ExecuteNonQuery();           
           
            conn.Close();
        }

        // PUT api/players/5
        public void Put(int id, [FromBody]Player player)
        {
            MySqlConnection conn = WebApiConfig.conn();
            MySqlCommand query = conn.CreateCommand();
            string mySqlUpdate = "UPDATE " + table + " SET FirstName='" + player.FirstName + "', LastName='" + player.LastName + "', PhoneNumber='" + player.PhoneNumber + "', ShirtNumber=" + player.ShirtNumber + ", DominantLeg='" + player.DominantLeg + "' WHERE Id=" + id;

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

        // DELETE api/players/5
        public void Delete(int id)
        {
            MySqlConnection conn = WebApiConfig.conn();
            MySqlCommand query = conn.CreateCommand();
            string mySqlDelete = "DELETE FROM " + table + " WHERE Id='" + id + "'";
            try
            {
                conn.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                throw;
            }

            query.CommandText = mySqlDelete;
            query.ExecuteNonQuery();

            conn.Close();
        }
    }
}
