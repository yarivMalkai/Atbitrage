using AlgotrageDAL.Context;
using AlgotrageDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgotrageDAL.EntityManagers
{
    public class ArbitragesDbManager : AbstractEnttyManager<Arbitrage>
    {
        public List<Arbitrage> GetActiveArbitrages()
        {
            using (var db = new AlgotrageContext())
            {
                return db.Arbitrages.Where(x => x.IsActive).ToList();
            }
        }
    }
}
