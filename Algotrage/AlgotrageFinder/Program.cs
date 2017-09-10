using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AlgotrageDAL.Entities;
using System.Threading;
using AlgotrageDAL.EntityManagers;

namespace AlgotrageFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            GamesManager gameMan = new GamesManager();
            var game = gameMan.GetAll()[0];

            SitesManager siteMan = new SitesManager();
            var sites = siteMan.GetAll();
            var site1 = sites[0];
            var site2 = sites[1];
            var site3 = sites[2];

            GameSiteRatio g1 = new GameSiteRatio();
            g1.AwayRatio = 10;
            g1.DrawRatio = 3;
            g1.HomeRatio = 3;
            g1.GameId = game.Id;
            g1.SiteId = site1.Id;
            g1.LastUpdateTime = DateTime.Now;

            GameSiteRatio g2 = new GameSiteRatio();
            g2.AwayRatio = 3;
            g2.DrawRatio = 10;
            g2.HomeRatio = 3;
            g2.GameId = game.Id;
            g2.SiteId = site2.Id;
            g2.LastUpdateTime = DateTime.Now;

            GameSiteRatio g3 = new GameSiteRatio();
            g3.AwayRatio = 3;
            g3.DrawRatio = 3;
            g3.HomeRatio = 10;
            g3.GameId = game.Id;
            g3.SiteId = site3.Id;
            g3.LastUpdateTime = DateTime.Now;

            GameSiteRatiosManager ratiosMan = new GameSiteRatiosManager();
            ratiosMan.Add(g1);
            ratiosMan.Add(g2);
            ratiosMan.Add(g3);
            */
            while (true)
            {
                ArbitrageManager man = new ArbitrageManager();

                man.checkForUpdates();
                man.findArbitrage();

                Thread.Sleep(1000);
            }
        }
    }
}
