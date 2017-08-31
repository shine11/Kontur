using System.Collections.Generic;
using System.Web.Http;

namespace Kontur.GameStats.Server.Controllers
{
    public class PlayersController : ApiController

    {
        [HttpGet, ActionName("stats")]

        public string Get(string name)
        {

            return  "ne uspel:(";
        }
    }
}
