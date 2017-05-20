using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgotrageDAL.Entities
{
    public class Arbitrage
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public double HomeRatio { get; set; }
        public int HomeRatioSiteId { get; set; }
        public double DrawRatio { get; set; }
        public int DrawRatioSiteId { get; set; }
        public double AwayRatio { get; set; }
        public int AwayRatioSiteId { get; set; }


        public double HomeBetPercent { get; set; }
        public double DrawBetPercent { get; set; }
        public double AwayBetPercent { get; set; }
        public double ProfitPervent { get; set; }

        public bool IsActive { get; set; }
        public DateTime FindTime { get; set; }
        public DateTime ExpireTime { get; set; }

        // Virtuals
        [ForeignKey("HomeRatioSiteId")]
        public virtual Site HomeRatioSite { get; set; }
        [ForeignKey("DrawRatioSiteId")]
        public virtual Site DrawRatioSite { get; set; }
        [ForeignKey("AwayRatioSiteId")]
        public virtual Site AwayRatioSite { get; set; }
        public virtual Game Game { get; set; }
    }
}
