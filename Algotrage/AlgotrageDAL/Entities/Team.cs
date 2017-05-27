using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgotrageDAL.Entities
{
    public class Team : BaseEntity
    {
        public string DisplayName { get; set; }

        [JsonIgnore]
        public virtual List<TeamPossibleName> PossibleNames { get; set; }
    }
}
