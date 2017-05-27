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

        public Game GetByTeams(Team team1, Team team2)
        {
            var games = GetAll();
            return games.FirstOrDefault(g => g.HomeTeamId == team1.Id && g.AwayTeamId == team2.Id);
        }
    }
}
