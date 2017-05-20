using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgotrageDAL.Entities
{
    public class TeamPossibleName : BaseEntity
    {
        public int TeamId { get; set; }
        public string PossibleName { get; set; }
    }
}
