using System.Collections.Generic;

namespace Kontur.GameStats.Server.Models
{
    public class class_scoreboard
    {
        public string name { get; set; }
        public int frags { get; set; }
        public int kills { get; set; }
        public int deaths { get; set; }

    }
    public class Matches
    {
        public string map { get; set; }
        public string gameMode { get; set; }
        public int fragLimit { get; set; }
        public int timeLimit { get; set; }
        public double timeElapsed { get; set; }
        public List<class_scoreboard> scoreboard { get; set; }
    }
}
