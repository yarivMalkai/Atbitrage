using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgotrageScraper
{
    public static class Logger
    {
        public static void Log(string msg)
        {
            var file = File.Open("ScraperLog.txt", FileMode.Append);
            var writer = new StreamWriter(file);
            writer.WriteLine(DateTime.Now.ToString() + ": " + msg);
        }

        public static void LogNewTeamAdded(string team)
        {
            Log("New team added - " + team);
        }

        public static void LogCantParse(int siteId)
        {
            Log("Error parsing ratios or date from site " + siteId);
        }
    }
}
