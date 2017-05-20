﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgotrageDAL.Entities
{
    public class Team
    {
        public int TeamId { get; set; }
        public string DisplayName { get; set; }

        public virtual List<TeamPossibleName> PossibleNames { get; set; }
    }
}
