using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.IO;

namespace Kontur.GameStats.Server
{
    public class WorkSQL
    {

        public static bool TableExists(string tableName)
        {
            string connStr = "Data Source=mydatabase.sdf;";
            SqlCeEngine engine = new SqlCeEngine(connStr);
            if (!File.Exists("mydatabase.sdf"))
                engine.CreateDatabase();
            SqlCeConnection conn = new SqlCeConnection(connStr);
            conn.Open();
            using (var command = new SqlCeCommand())
            {
                command.Connection = conn;
                var sql = string.Format(
                        "SELECT COUNT(*) FROM information_schema.tables WHERE table_name = '{0}'",
                         tableName);
                command.CommandText = sql;
                var count = Convert.ToInt32(command.ExecuteScalar());
                conn.Close();
                return (count > 0);
            }
        }


        public static void Create_table_info()
        {
            string connStr = "Data Source=mydatabase.sdf;";
            List<string> line = new List<string>();

            SqlCeConnection conn = new SqlCeConnection(connStr);
            conn.Open();
            SqlCeCommand cmd = conn.CreateCommand();
            cmd.CommandText = "CREATE TABLE info("
                + "ID INT IDENTITY NOT NULL PRIMARY KEY,"
                + "endpoint NVarChar(200), "
                + "name ntext);";
            cmd.ExecuteNonQuery();
            conn.Close();
        }


        public static void Create_table_gameModes()
        {
            string connStr = "Data Source=mydatabase.sdf;";
            List<string> line = new List<string>();

            SqlCeConnection conn = new SqlCeConnection(connStr);
            conn.Open();
            SqlCeCommand cmd = conn.CreateCommand();
            cmd.CommandText = "CREATE TABLE gameModes("
                + "ID INT IDENTITY NOT NULL PRIMARY KEY,"
                + "endpoint NVarChar(200), "
                + "gameModes NVarChar(200));";
            cmd.ExecuteNonQuery();
            conn.Close();
        }


        public static void Create_table_matches()
        {
            string connStr = "Data Source=mydatabase.sdf;";

            SqlCeConnection conn = new SqlCeConnection(connStr);
            conn.Open();
            SqlCeCommand cmd = conn.CreateCommand();
            cmd.CommandText = "CREATE TABLE matches("
                + "ID INT IDENTITY NOT NULL PRIMARY KEY,"
                + "endpoint NVarChar(200), "
                + "timestamp NVarChar(200),"
                + "map ntext,"
                + "gameMode ntext,"
                + "fragLimit int,"
                + "timeLimit int,"
                + "timeElapsed Float);";
            cmd.ExecuteNonQuery();
            conn.Close();
        }


        public static void Create_table_scoreboard()
        {
            string connStr = "Data Source=mydatabase.sdf;";
            List<string> line = new List<string>();

            SqlCeConnection conn = new SqlCeConnection(connStr);
            conn.Open();
            SqlCeCommand cmd = conn.CreateCommand();
            cmd.CommandText = "CREATE TABLE scoreboard("
                + "ID INT IDENTITY NOT NULL PRIMARY KEY,"
                + "endpoint NVarChar(200), "
                + "timestamp NVarChar(200),"
                + "name NVarChar(200),"
                + "frags int,"
                + "kills int,"
                + "deaths int);";
            cmd.ExecuteNonQuery();
            conn.Close();
        }


        public static void Add_advertise_info(string endpoint, string name)
        {
            string connStr = "Data Source=mydatabase.sdf;";
            SqlCeConnection conn = new SqlCeConnection(connStr);
            conn.Open();
            SqlCeCommand cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO info(endpoint, name)"
                + "VALUES (@endpoint, @name);";
            cmd.Parameters.Add(new SqlCeParameter("@endpoint", endpoint));
            cmd.Parameters.Add(new SqlCeParameter("@name", name));
            cmd.ExecuteNonQuery();
            conn.Close();
        }


        public static void Add_advertise_gameModes(string endpoint, string gameModes)
        {
            string connStr = "Data Source=mydatabase.sdf;";
            SqlCeConnection conn = new SqlCeConnection(connStr);
            conn.Open();
            SqlCeCommand cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO gameModes(endpoint, gameModes)"
            + "VALUES (@endpoint, @name);";
            cmd.Parameters.Add(new SqlCeParameter("@name", gameModes));
            cmd.Parameters.Add(new SqlCeParameter("@endpoint", endpoint));
            cmd.ExecuteNonQuery();
            conn.Close();
        }


        public static void Add_match(string endpoint, string timestamp, string map, string gameMode, int fragLimit, int timeLimit, double timeElapsed)
        {
            string connStr = "Data Source=mydatabase.sdf;";
            SqlCeConnection conn = new SqlCeConnection(connStr);
            conn.Open();
            SqlCeCommand cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO matches(endpoint, timestamp, map, gameMode, fragLimit, timeLimit, timeElapsed)"
                + "VALUES (@endpoint, @timestamp,@map, @gameMode, @fragLimit, @timeLimit, @timeElapsed);";
            cmd.Parameters.Add(new SqlCeParameter("@endpoint", endpoint));
            cmd.Parameters.Add(new SqlCeParameter("@timestamp", timestamp));
            cmd.Parameters.Add(new SqlCeParameter("@map", map));
            cmd.Parameters.Add(new SqlCeParameter("@gameMode", gameMode));
            cmd.Parameters.Add(new SqlCeParameter("@fragLimit", fragLimit));
            cmd.Parameters.Add(new SqlCeParameter("@timeLimit", timeLimit));
            cmd.Parameters.Add(new SqlCeParameter("@timeElapsed", timeElapsed));
            cmd.ExecuteNonQuery();
            conn.Close();
        }


        public static void Add_scoreboard(string endpoint, string timestamp, string name, int frags, int kills, int deaths)
        {
            string connStr = "Data Source=mydatabase.sdf;";
            SqlCeConnection conn = new SqlCeConnection(connStr);
            conn.Open();
            SqlCeCommand cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO scoreboard(endpoint, timestamp, name, frags, kills, deaths)"
                + "VALUES (@endpoint, @timestamp, @name, @frags, @kills, @deaths);";
            cmd.Parameters.Add(new SqlCeParameter("@endpoint", endpoint));
            cmd.Parameters.Add(new SqlCeParameter("@timestamp", timestamp));
            cmd.Parameters.Add(new SqlCeParameter("@name", name));
            cmd.Parameters.Add(new SqlCeParameter("@frags", frags));
            cmd.Parameters.Add(new SqlCeParameter("@kills", kills));
            cmd.Parameters.Add(new SqlCeParameter("@deaths", deaths));
            cmd.ExecuteNonQuery();
            conn.Close();
        }


        public static void UPDATE_info(string endpoint, string name)
        {

            string connStr = "Data Source=mydatabase.sdf;";
            SqlCeConnection conn = new SqlCeConnection(connStr);
            conn.Open();
            SqlCeCommand cmd = conn.CreateCommand();

            cmd.CommandText = "UPDATE info set name=@name where endpoint=@endpoint;";

            cmd.Parameters.Add(new SqlCeParameter("@endpoint", endpoint));
            cmd.Parameters.Add(new SqlCeParameter("@name", name));
            cmd.ExecuteNonQuery();
            conn.Close();
        }


        public static void DELETE_gameModes(string endpoint)
        {

            string connStr = "Data Source=mydatabase.sdf;";
            SqlCeConnection conn = new SqlCeConnection(connStr);
            conn.Open();
            SqlCeCommand cmd = conn.CreateCommand();
            cmd.CommandText = "DELETE  from gameModes where endpoint=@endpoint;";
            cmd.Parameters.Add(new SqlCeParameter("@endpoint", endpoint));
            cmd.ExecuteNonQuery();
            conn.Close();
        }


        public static List<string> Select_Endpoint()
        {
            string connStr = "Data Source = mydatabase.sdf;";
            List<string> line = new List<string>();
            SqlCeConnection conn = new SqlCeConnection(connStr);
            conn.Open();
            SqlCeCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM info";
            SqlCeDataReader w = cmd.ExecuteReader();
            int i = 0;
            while (w.Read())
            {
                line.Add(w["endpoint"].ToString());
                i++;
            }
            conn.Close();
            return line;
        }


        public static List<string[]> Select_Endpoint_timestamp()
        {
            string connStr = "Data Source = mydatabase.sdf;";
            List<string[]> line = new List<string[]>();

            SqlCeConnection conn = new SqlCeConnection(connStr);
            conn.Open();
            SqlCeCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM matches ";
            SqlCeDataReader w = cmd.ExecuteReader();
            int i = 0;
            while (w.Read())
            {
                line.Add(new string[2] { w["endpoint"].ToString(), w["timestamp"].ToString() });
                i++;
            }
            conn.Close();

            return line;
        }


        public static List<string[]> Select_matche_string_parametrs(string endpoint, string timestamp)
        {
            string connStr = "Data Source = mydatabase.sdf;";
            List<string[]> line = new List<string[]>();

            SqlCeConnection conn = new SqlCeConnection(connStr);
            conn.Open();
            SqlCeCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM matches WHERE endpoint=@endpoint AND @timestamp=timestamp";
            cmd.Parameters.Add(new SqlCeParameter("@endpoint", endpoint));
            cmd.Parameters.Add(new SqlCeParameter("@timestamp", timestamp));
            SqlCeDataReader w = cmd.ExecuteReader();
            int i = 0;
            while (w.Read())
            {
                line.Add(new string[2] { w["map"].ToString(), w["gameMode"].ToString() });
                i++;
            }
            conn.Close();
            return line;
        }


        public static List<int[]> Select_matche_int_parametrs(string endpoint, string timestamp)
        {
            string connStr = "Data Source = mydatabase.sdf;";
            List<int[]> line = new List<int[]>();

            SqlCeConnection conn = new SqlCeConnection(connStr);
            conn.Open();
            SqlCeCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM matches WHERE endpoint=@endpoint AND @timestamp=timestamp";
            cmd.Parameters.Add(new SqlCeParameter("@endpoint", endpoint));
            cmd.Parameters.Add(new SqlCeParameter("@timestamp", timestamp));
            SqlCeDataReader w = cmd.ExecuteReader();
            int i = 0;
            while (w.Read())
            {
                line.Add(new int[2] { Convert.ToInt32(w["fragLimit"].ToString()), Convert.ToInt32( w["timeLimit"].ToString()) });
                i++;
            }
            conn.Close();
            return line;
        }


        public static List<double> Select_matche_double_parametr(string endpoint, string timestamp)
        {
            string connStr = "Data Source = mydatabase.sdf;";
            List<double> line = new List<double>();

            SqlCeConnection conn = new SqlCeConnection(connStr);
            conn.Open();
            SqlCeCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM matches WHERE endpoint=@endpoint AND @timestamp=timestamp";
            cmd.Parameters.Add(new SqlCeParameter("@endpoint", endpoint));
            cmd.Parameters.Add(new SqlCeParameter("@timestamp", timestamp));
            SqlCeDataReader w = cmd.ExecuteReader();
            int i = 0;
            while (w.Read())
            {
                line.Add(Convert.ToDouble(w["timeElapsed"].ToString() ));
                i++;
            }
            conn.Close();

            return line;
        }


        public static List<string> Select_scoreboard_name(string endpoint, string timestamp)
        {
            string connStr = "Data Source = mydatabase.sdf;";
            List<string> line = new List<string>();

            SqlCeConnection conn = new SqlCeConnection(connStr);
            conn.Open();
            SqlCeCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM scoreboard WHERE endpoint=@endpoint AND @timestamp=timestamp";
            cmd.Parameters.Add(new SqlCeParameter("@endpoint", endpoint));
            cmd.Parameters.Add(new SqlCeParameter("@timestamp", timestamp));
            SqlCeDataReader w = cmd.ExecuteReader();
            int i = 0;
            while (w.Read())
            {
                line.Add(w["name"].ToString());
                i++;
            }
            conn.Close();
            return line;
        }


        public static List<int[]> Select_scoreboard_int_parametrs(string endpoint, string timestamp, string name)
        {
            string connStr = "Data Source = mydatabase.sdf;";
            List<int[]> line = new List<int[]>();

            SqlCeConnection conn = new SqlCeConnection(connStr);
            conn.Open();
            SqlCeCommand cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM scoreboard WHERE endpoint=@endpoint AND @timestamp=timestamp AND @name=name";
            cmd.Parameters.Add(new SqlCeParameter("@endpoint", endpoint));
            cmd.Parameters.Add(new SqlCeParameter("@timestamp", timestamp));
            cmd.Parameters.Add(new SqlCeParameter("@name", name));
            SqlCeDataReader w = cmd.ExecuteReader();
            int i = 0;
            while (w.Read())
            {
                line.Add(new int[3] { Convert.ToInt32(w["frags"].ToString()), Convert.ToInt32(w["kills"].ToString()), Convert.ToInt32(w["deaths"].ToString()) });
                i++;
            }
            conn.Close();

            return line;
        }


        public static List<string> Select_gameModes(string endpoint)
        {
            string connStr = "Data Source = mydatabase.sdf;";
            List<string> line = new List<string>();

            SqlCeConnection conn = new SqlCeConnection(connStr);
            conn.Open();
            SqlCeCommand cmd = conn.CreateCommand();
            cmd.CommandText = string.Format("SELECT * FROM gameModes WHERE endpoint = '{0}'", endpoint);
            SqlCeDataReader w = cmd.ExecuteReader();
            int i = 0;
            while (w.Read())
            {
                line.Add(w["gameModes"].ToString());
                i++;
            }
            conn.Close();
            return line;
        }


        public static List<string> Select_name(string endpoint)
        {
            string connStr = "Data Source = mydatabase.sdf;";
            List<string> line = new List<string>();

            SqlCeConnection conn = new SqlCeConnection(connStr);
            conn.Open();
            SqlCeCommand cmd = conn.CreateCommand();
            cmd.CommandText = string.Format("SELECT * FROM info WHERE endpoint = '{0}'", endpoint);
            SqlCeDataReader w = cmd.ExecuteReader();
            int i = 0;
            while (w.Read())
            {
                line.Add(w["name"].ToString());
                i++;
            }
            conn.Close();
            return line;
        }
        public static List<string> Select_timestamp_Matches(string endpoint)
        {
            string connStr = "Data Source = mydatabase.sdf;";
            List<string> line = new List<string>();

            SqlCeConnection conn = new SqlCeConnection(connStr);
            conn.Open();
            SqlCeCommand cmd = conn.CreateCommand();
            cmd.CommandText = string.Format("SELECT * FROM matches WHERE endpoint = '{0}'", endpoint);
            SqlCeDataReader w = cmd.ExecuteReader();
            int i = 0;
            while (w.Read())
            {
                line.Add(w["timestamp"].ToString());
                i++;
            }
            conn.Close();
            return line;
        }
    }
}
