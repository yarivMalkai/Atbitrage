using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgotrageDAL.Entities
{
    public class TeamPossibleName
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public string PossibleName { get; set; }
    }
}
