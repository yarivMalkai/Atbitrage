using AlgotrageDAL.Entities;
using AlgotrageDAL.EntityManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgotrageFinder
{
    public class ArbitrageManager
    {
        private Dictionary<int, Arbitrage> activeArbitrages;
        private ArbitragesDbManager arbitrageDBManager;

        public ArbitrageManager()
        {
            activeArbitrages = new Dictionary<int, Arbitrage>();

            foreach (var curr in arbitrageDBManager.GetActiveArbitrages())
            {
                activeArbitrages.Add(curr.GameId, curr);
            }
        }

        public void findArbitrage()
        {
            foreach (var game in new GamesManager().GetAll())
            {
                // Checking if we didn't find arbitrage for current game yet
                if (!activeArbitrages.ContainsKey(game.Id))
                    calcArbitrage(game);
            }
        }

        public void checkForUpdates()
        {
            foreach (var arbitrage in activeArbitrages)
            {
                var game = arbitrage.Value.Game;

                if (game.Date > DateTime.Now ||
                    arbitrage.Value.HomeRatio != game.GameSiteRatios.FirstOrDefault(x => x.GameId == game.Id && x.SiteId == arbitrage.Value.HomeRatioSiteId).SiteId ||
                    arbitrage.Value.DrawRatio != game.GameSiteRatios.FirstOrDefault(x => x.GameId == game.Id && x.SiteId == arbitrage.Value.DrawRatioSiteId).SiteId ||
                    arbitrage.Value.AwayRatio != game.GameSiteRatios.FirstOrDefault(x => x.GameId == game.Id && x.SiteId == arbitrage.Value.AwayRatioSiteId).SiteId)
                {
                    removeArbitrage(arbitrage.Value);
                }
            }
        }



        private void calcArbitrage(Game game)
        {
            var bestHomeRatio = game.GameSiteRatios.OrderByDescending(x => x.HomeRatio).ToList()[0];
            var bestDrawRatio = game.GameSiteRatios.OrderByDescending(x => x.DrawRatio).ToList()[0];
            var bestAwayRatio = game.GameSiteRatios.OrderByDescending(x => x.AwayRatio).ToList()[0];

            var probability = getProbability(bestHomeRatio.HomeRatio, bestDrawRatio.DrawRatio, bestAwayRatio.AwayRatio);

            if (probability < 1)
                createNewArbitrage(game, bestHomeRatio, bestDrawRatio, bestAwayRatio, probability);
        }

        private void createNewArbitrage(Game game, GameSiteRatio homeRatio, GameSiteRatio drawRatio, GameSiteRatio awayRatio, double probability)
        {
            Arbitrage arbitrage = new Arbitrage();
            arbitrage.GameId = game.Id;

            arbitrage.HomeRatio = homeRatio.HomeRatio;
            arbitrage.HomeRatioSiteId = homeRatio.SiteId;
            arbitrage.DrawRatio = drawRatio.DrawRatio;
            arbitrage.DrawRatioSiteId = drawRatio.SiteId;
            arbitrage.AwayRatio = awayRatio.AwayRatio;
            arbitrage.AwayRatioSiteId = awayRatio.SiteId;
            
            arbitrage.HomeBetPercent = calcBetPercent(homeRatio.HomeRatio, probability);
            arbitrage.DrawBetPercent = calcBetPercent(drawRatio.DrawRatio, probability);
            arbitrage.AwayBetPercent = calcBetPercent(awayRatio.AwayRatio, probability);
            arbitrage.ProfitPercent = (1 / probability) - 1;

            arbitrage.IsActive = true;
            arbitrage.FindTime = DateTime.Now;

            activeArbitrages.Add(game.Id, arbitrage);
            arbitrageDBManager.Add(arbitrage); // Add to DB
        }

        private double calcBetPercent(double probability, double probabilitesSum)
        {
            return 1 / probability / probabilitesSum;
        }

        private double getProbability(double homeRatio, double drawRatio, double awayRatio)
        {
            var prob1 = 1 / homeRatio;
            var prob2 = 1 / drawRatio;
            var prob3 = 1 / awayRatio;

            return (prob1 + prob2 + prob3);
        }

        private void removeArbitrage(Arbitrage arbitrage)
        {
            arbitrage.IsActive = false;
            arbitrage.ExpireTime = DateTime.Now;

            activeArbitrages.Remove(arbitrage.GameId);
            arbitrageDBManager.Update(arbitrage);
        }
    }
}
