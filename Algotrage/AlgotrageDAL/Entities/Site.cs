﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgotrageDAL.Entities
{
    public class Site : BaseEntity
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Image { get; set; }

        public virtual List<GameSiteRatio> GameRatios { get; set; }
    }
}
