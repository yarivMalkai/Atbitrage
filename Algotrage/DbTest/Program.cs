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
                var team1 = new Team
                {
                    DisplayName = "First"
                };

                var team2 = new Team
                {
                    DisplayName = "Second"
                };

                db.Teams.Add(team1);
                db.Teams.Add(team2);

                var game = new Game
                {
                    AwayTeam = team2,
                    HomeTeam = team1,
                    Date = DateTime.Now,
                    
                };

                db.Games.Add(game);

                Site s1 = new Site()
                {
                    Image = "bla.jpg",
                    Url = "http://bla.com",
                    Name = "bla"
                };

                Site s2 = new Site()
                {
                    Image = "bla2.jpg",
                    Url = "http://bla2.com",
                    Name = "bla2"
                };

                Site s3 = new Site()
                {
                    Image = "bla3.jpg",
                    Url = "http://bla3.com",
                    Name = "bla3"
                };

                db.Sites.Add(s1);
                db.Sites.Add(s2);
                db.Sites.Add(s3);

                Arbitrage arb = new Arbitrage()
                {
                    Game = game,
                    AwayRatio = 3.1,
                    HomeRatio = 3.1,
                    DrawRatio = 3.1,
                    AwayRatioSite = s1,
                    HomeRatioSite = s2,
                    DrawRatioSite = s3,
                    FindTime = DateTime.Now,
                    IsActive = true,
                    ProfitPercent = 0.3,
                    HomeBetPercent = 0.33,
                    AwayBetPercent = 0.33,
                    DrawBetPercent = 0.34,
                    
                };

                db.Arbitrages.Add(arb);

                db.SaveChanges();



                //var games = new GamesManager().GetAll();
            }
        }
    }
}
