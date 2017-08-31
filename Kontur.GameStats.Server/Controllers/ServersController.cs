using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using Newtonsoft.Json;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Kontur.GameStats.Server.Controllers
{
    public class ServersController : ApiController
    {

        public string Get_info()
        {        
            List<Models.All_Info>  servers_info = new List<Models.All_Info>();
            foreach (var e in WorkSQL.Select_Endpoint())
            {
                {
                    List<string> gameModes = WorkSQL.Select_gameModes(e);
                    int i = 0;
                    string[] endpoint_gameModes = new string[gameModes.Count];
                    foreach (var a in gameModes)
                    {
                        endpoint_gameModes[i] = a;
                        i++;

                    }
                    servers_info.Add(new Models.All_Info() { endpoint = e, name = WorkSQL.Select_name(e)[0], gameModes = endpoint_gameModes });
                }
            }
            return JsonConvert.SerializeObject(servers_info);
        }

        [HttpGet, ActionName("info")]
        public string Get_endpoint(string endpoint)
        {
            bool EndpointExist = false;
            EndpointExist = Other_methods.The_existence_check(EndpointExist,endpoint);
            if (EndpointExist)
            {
                Models.Info info1 = new Models.Info();
                List<string> gameModes = WorkSQL.Select_gameModes(endpoint);
                int i = 0;
                info1.gameModes = new string[gameModes.Count];
                foreach (var a in gameModes)
                {
                    info1.gameModes[i] = a;
                    i++;
                }
                info1.name = WorkSQL.Select_name(endpoint)[0];
                string serilized = JsonConvert.SerializeObject(info1);
                return serilized;
            }
            else
            {
                return "404 Not Found";
            }
        }

        [HttpGet, ActionName("matches")]
        public string Get_matches(string endpoint, string timestamp)
        {
            bool endpoint_exist = false;

            endpoint_exist = Other_methods.The_existence_check(endpoint_exist, endpoint, timestamp);
            if (endpoint_exist)
            {
                Models.Matches matches1 = new Models.Matches();
                string[] string_parametrs = new string[2];               
                matches1.map = WorkSQL.Select_matche_string_parametrs(endpoint, timestamp)[0][0];
                matches1.gameMode = WorkSQL.Select_matche_string_parametrs(endpoint, timestamp)[0][1];
                matches1.fragLimit = WorkSQL.Select_matche_int_parametrs(endpoint, timestamp)[0][1];
                matches1.timeLimit = WorkSQL.Select_matche_int_parametrs(endpoint, timestamp)[0][0];
                matches1.timeElapsed = WorkSQL.Select_matche_double_parametr(endpoint, timestamp)[0];
                matches1.scoreboard = new List<Models.class_scoreboard>();
                foreach (var a in WorkSQL.Select_scoreboard_name(endpoint, timestamp))
                {
                    matches1.scoreboard.Add(new Models.class_scoreboard() { name = a, frags = WorkSQL.Select_scoreboard_int_parametrs(endpoint, timestamp, a)[0][0], kills = WorkSQL.Select_scoreboard_int_parametrs(endpoint, timestamp, a)[0][1], deaths = WorkSQL.Select_scoreboard_int_parametrs(endpoint, timestamp, a)[0][2] });
                }
                
                string serilized = JsonConvert.SerializeObject(matches1);
                return serilized;
            }
            else
                return "404 Not Found";
        }

        [HttpPut, ActionName("info")]
        public void Put_info([ModelBinder] Models.Info putInfo, string endpoint)
        {

            HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.OK);
            bool EndpointExist = false;
            EndpointExist = Other_methods.The_existence_check(EndpointExist, endpoint);
            if (EndpointExist)
            {
                WorkSQL.UPDATE_info(endpoint, putInfo.name);
                WorkSQL.DELETE_gameModes(endpoint);
                int i = 0;
                while (i < putInfo.gameModes.Length)
                {
                    WorkSQL.Add_advertise_gameModes(endpoint, putInfo.gameModes[i]);
                    i++;
                }
            }
            else
            {
                WorkSQL.Add_advertise_info(endpoint, putInfo.name);
                int j = 0;
                while (j < putInfo.gameModes.Length)
                {
                    WorkSQL.Add_advertise_gameModes(endpoint, putInfo.gameModes[j]);
                    j++;
                }
            }
        }

        [HttpPut, ActionName("matches")]
        public HttpResponseMessage Put_matches([FromBody]JToken jsonbody, string endpoint, string timestamp)
        {

            Models.Matches putMatches = JsonConvert.DeserializeObject<Models.Matches>(jsonbody.ToString());
            HttpResponseMessage message = Request.CreateResponse(HttpStatusCode.OK);
            bool endpoint_exist = false;
            endpoint_exist = Other_methods.The_existence_check(endpoint_exist, endpoint);
            if (!endpoint_exist)
            {
                string output = "400 Not Found";
                StringContent content = new StringContent(output, Encoding.UTF8, "application/json");
                message = Request.CreateResponse(HttpStatusCode.NotFound);
                message.Content = content;
            }
            else
            {
                string output = "Match add";
            StringContent content = new StringContent(output, Encoding.UTF8, "application/json");
            message = Request.CreateResponse(HttpStatusCode.OK);
            message.Content = content;
            WorkSQL.Add_match(endpoint, timestamp, putMatches.map, putMatches.gameMode, putMatches.fragLimit, putMatches.timeLimit, putMatches.timeElapsed);
            int g = 0;
            while (g < putMatches.scoreboard.Count)
            {
                WorkSQL.Add_scoreboard(endpoint, timestamp, putMatches.scoreboard[g].name, putMatches.scoreboard[g].frags, putMatches.scoreboard[g].kills, putMatches.scoreboard[g].deaths);
                g++;
            }
        }
            return message;
        }


        [HttpGet, ActionName("stats")]
        public string Get_stat(string endpoint)
        {
            Models.Stats_match stat = new Models.Stats_match();
            stat.totalMatchesPlayed = WorkSQL.Select_timestamp_Matches(endpoint).Count;
            stat.maximumMatchesPerDay = 33;
            stat.averageMatchesPerDay = 24.456240;
            stat.maximumPopulation = 32;
            stat.averagePopulation = 20.450000;
        string serilized = JsonConvert.SerializeObject(stat);
            return serilized;
            
        }
    }
}
