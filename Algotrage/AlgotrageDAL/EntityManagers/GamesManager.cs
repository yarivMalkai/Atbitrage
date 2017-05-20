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
    public class GamesManager : AbstractEnttyManager<Game>
    {
        public override List<Game> GetAll()
        {
            return base.GetAll().OrderBy(x => x.Date).ToList();
        }
    }
}
