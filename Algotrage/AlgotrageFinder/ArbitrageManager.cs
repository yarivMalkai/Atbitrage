using AlgotrageDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AlgotrageFinder
{
    public class ArbitrageManager
    {
        private List<Team> teams;
        private List<Game> games;
        private List<Site> sites;

        private Dictionary<int, Arbitrage> activeArbitrages;

        public ArbitrageManager()
        {
            teams = new List<Team>();
            games = new List<Game>();
            sites = new List<Site>();

            activeArbitrages = new Dictionary<int, Arbitrage>();
        }



        public void findArbitrage()
        {
            foreach (var game in games)
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

        public void addSite(string name, string url, string image = null)
        {
            if (sites.FirstOrDefault(x => x.Name == name && x.Url == url) == null)
                return;

            Site site = new Site();
            site.Name = name;
            site.Url = url;
            site.Image = image;

            sites.Add(site);
        }

        public void removeSite(string url)
        {
            var site = sites.FirstOrDefault(x => x.Url == url);

            if (site != null)
                sites.Remove(site);
        }

        public void addTeam(string name)
        {
            Team team = teams.FirstOrDefault(x => x.DisplayName == name || x.PossibleNames.FirstOrDefault(possibleName => possibleName.PossibleName == name) != null);

            if (team == null)
            {
                team = new Team();
                team.DisplayName = name;

                teams.Add(team);
            }
            else
            {
                TeamPossibleName anotherName = new TeamPossibleName();
                anotherName.PossibleName = name;
                anotherName.TeamId = team.Id;

                team.PossibleNames.Add(anotherName);
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
            arbitrage.ProfitPervent = (1 / probability) - 1;

            arbitrage.IsActive = true;
            arbitrage.FindTime = DateTime.Now;

            activeArbitrages.Add(game.Id, arbitrage);
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
            arbitrage.ExpireTime = DateTime.Now;

            activeArbitrages.Remove(arbitrage.GameId);
        }
    }
}
