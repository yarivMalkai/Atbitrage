using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgotrageScraper
{
    public class SiteScrapingInfo
    {
        public string Url { get; set; }
        public string GameListExpression { get; set; }
        public string Team1NameExpression { get; set; }
        public string Team2NameExpression { get; set; }
        public string Ratio1Expression { get; set; }
        public string RatioXExpression { get; set; }
        public string Ratio2Expression { get; set; }

        public SiteScrapingInfo(string url, string gamelistExp, string team1Exp, string team2Exp, string ratio1Exp, string ratioXExp, string ratio2Exp)
        {
            Url = url;
            GameListExpression = gamelistExp;
            Team1NameExpression = team1Exp;
            Team2NameExpression = team2Exp;
            Ratio1Expression = ratio1Exp;
            RatioXExpression = ratioXExp;
            Ratio2Expression = ratio2Exp;
        }
    }
}
