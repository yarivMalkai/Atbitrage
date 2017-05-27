using AlgotrageDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgotrageDAL.EntityManagers
{
    public class TeamsManager : AbstractEnttyManager<Team>
    {
        public Team GetByPossibleName(string name)
        {
            var teams = GetAll();

            return teams.FirstOrDefault(t => t.PossibleNames.ConvertAll(x => x.PossibleName).Contains(name));
        }
    }
}
