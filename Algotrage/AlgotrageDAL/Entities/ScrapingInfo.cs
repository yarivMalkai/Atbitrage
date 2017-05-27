using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgotrageDAL.Entities
{
    public class ScrapingInfo
    {
        public string GameListExpression { get; set; }
        public string DateExpression { get; set; }
        public string DateAttribute { get; set; }
        public string DateFormat { get; set; }
        public string HomeTeamNameExpression { get; set; }
        public string HomeTeamAttribute { get; set; }
        public string AwayTeamNameExpression { get; set; }
        public string AwayTeamAttribute { get; set; }
        public string HomeRatioExpression { get; set; }
        public string HomeRatioAttribute { get; set; }
        public string RatioXExpression { get; set; }
        public string RatioXAttribute { get; set; }
        public string AwayRatioExpression { get; set; }
        public string AwayRatioAttribute { get; set; }
    }
}
