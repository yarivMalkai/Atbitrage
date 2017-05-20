using AlgotrageDAL.Context;
using AlgotrageDAL.Entities;
using AlgotrageDAL.EntityManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new AlgotrageContext())
            {
                //var team1 = new Team
                //{
                //    DisplayName = "First"
                //};

                //var team2 = new Team
                //{
                //    DisplayName = "Second"
                //};

                //db.Teams.Add(team1);
                //db.Teams.Add(team2);

                //var game = new Game
                //{
                //    AwayTeam = team2,
                //    HomeTeam = team1,
                //    Date = DateTime.Now
                //};

                //db.Games.Add(game);

                //db.SaveChanges();

                var games = new GamesManager().GetAll();
            }
        }
    }
}
