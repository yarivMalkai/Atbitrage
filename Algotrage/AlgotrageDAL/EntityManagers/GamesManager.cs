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
            List<Game> ts = null;
            using (var db = new AlgotrageContext())
            {
                ts = db.Games.Include(x => x.GameSiteRatios).OrderBy(x => x.Date).ToList();
            }

            return ts;
        }

        public List<Game> GetActiveGames()
        {
            return GetAll().Where(x => (x.Date > DateTime.Now)).ToList();
        }

        public Game GetByTeamsAndDate(Team team1, Team team2, DateTime date)
        {
            var games = GetAll();
            return games.FirstOrDefault(g => g.HomeTeamId == team1.Id && g.AwayTeamId == team2.Id && SameDate(g.Date, date));
        }

        public Game GetByHomeTeamAndDate(Team team, DateTime date)
        {
            var games = GetAll();
            return games.FirstOrDefault(g => g.HomeTeamId == team.Id && SameDate(g.Date, date));
        }

        public Game GetByAwayTeamAndDate(Team team, DateTime date)
        {
            var games = GetAll();
            return games.FirstOrDefault(g => g.AwayTeamId == team.Id && SameDate(g.Date, date));
        }

        private bool SameDate(DateTime date1, DateTime date2)
        {
            return date1.Year == date2.Year && date1.Month == date2.Month && date1.Day == date2.Day;
        }
    }
}
