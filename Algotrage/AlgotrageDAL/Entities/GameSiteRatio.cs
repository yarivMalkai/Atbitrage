using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgotrageDAL.Entities
{
    public class GameSiteRatio
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int SiteId { get; set; }
        public double HomeRatio { get; set; }
        public double DrawRatio { get; set; }
        public double AwayRatio { get; set; }
    }
}
