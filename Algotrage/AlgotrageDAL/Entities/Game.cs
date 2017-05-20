using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgotrageDAL.Entities
{
    public class Game : BaseEntity
    {
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public DateTime Date { get; set; }

        [ForeignKey("HomeTeamId")]
        public virtual Team HomeTeam { get; set; }
        [ForeignKey("AwayTeamId")]
        public virtual Team AwayTeam { get; set; }
        public virtual List<GameSiteRatio> GameSiteRatios { get; set; }
    }
}
