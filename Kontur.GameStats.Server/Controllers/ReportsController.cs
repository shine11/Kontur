using System.Collections.Generic;
using System.Web.Http;

namespace Kontur.GameStats.Server.Controllers
{
    public class ReportsController : ApiController

    {
        [HttpGet, ActionName("recent-matches")]

        public string Get1()
        {
            return "ne uspel:(";
        }
        public string Get2(int id)
        {
            return "ne uspel:(";
        }
        [HttpGet, ActionName("best-players")]

        public string Get3()
        {

            return "ne uspel:(";
        }
        public string Get4(int id)
        {

            return "ne uspel:(";
        }
    }
}
