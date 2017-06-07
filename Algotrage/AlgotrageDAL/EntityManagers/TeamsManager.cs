using AlgotrageDAL.Context;
using AlgotrageDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace AlgotrageDAL.EntityManagers
{
    public class TeamsManager : AbstractEnttyManager<Team>
    {
        public override Team GetById(int id)
        {
            using (var db = new AlgotrageContext())
            {
                return db.Teams.Include(t => t.PossibleNames).FirstOrDefault(x => x.Id == id);
            }
        }

        public Team GetByPossibleName(string name)
        {
            using (var db = new AlgotrageContext())
            {
                var teams = db.Teams.Include(x => x.PossibleNames).ToList();
                return teams.FirstOrDefault(t => t.DisplayName == name || t.PossibleNames.ConvertAll(x => x.PossibleName).Contains(name));
            }
        }

        public void AddPossibleName(TeamPossibleName teamPossibleName)
        {
            using (var db = new AlgotrageContext())
            {
                var entry = db.Entry(teamPossibleName);
                entry.State = EntityState.Added;
                db.SaveChanges();
            }
        }
    }
}
