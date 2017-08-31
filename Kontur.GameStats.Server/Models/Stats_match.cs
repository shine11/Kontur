using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kontur.GameStats.Server.Models
{
    class Stats_match
    {
        public int totalMatchesPlayed { get; set; }
        public int maximumMatchesPerDay { get; set; }
        public double averageMatchesPerDay { get; set; }
        public int maximumPopulation { get; set; }
        public double averagePopulation { get; set; }

    }
}
