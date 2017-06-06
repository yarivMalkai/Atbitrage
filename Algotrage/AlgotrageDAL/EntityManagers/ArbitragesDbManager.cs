using AlgotrageDAL.Context;
using AlgotrageDAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgotrageDAL.EntityManagers
{
    public class ArbitragesDbManager : AbstractEnttyManager<Arbitrage>
    {
        public AlgotrageContext context { get; set; }

        public ArbitragesDbManager()
        {
            context = new AlgotrageContext();
        }

        public List<Arbitrage> GetActiveArbitrages()
        {
            return context.Arbitrages.Where(x => x.IsActive).ToList();

        }

        public override void Update(Arbitrage t)
        {
            var entry = context.Entry(t);
            entry.State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
