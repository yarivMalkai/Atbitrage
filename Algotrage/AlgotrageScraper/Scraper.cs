using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.IO;
using AlgotrageDAL.EntityManagers;
using AlgotrageDAL.Entities;

namespace AlgotrageScraper
{
    class Scraper
    {
        static void Main(string[] args)
        {
            //var url = "https://sports.intertops.eu/en/Bets/Sport/12";
            //var gamelistExp = "//ul[contains(@class,'nextbets')]/li[not(@class)]";
            //var dateExp = ".//div/div[contains(@class, 'event-date')]/span";
            //var dateAtt = "title";
            //var dateFormat = "M/dd/yyyy'<br/>'h:mm tt";
            //var team1Exp = ".//div/div[contains(@class,'market-options')]/div/div[1]/div";
            //var team1Att = "title";
            //var team2Exp = ".//div/div[contains(@class,'market-options')]/div/div[3]/div";
            //var team2Att = "title";
            //var ratio1Exp = ".//div/div[contains(@class,'market-options')]/div/div[1]/div/a/span[2]";
            //var ratio1Att = "";
            //var ratioXExp = ".//div/div[contains(@class,'market-options')]/div/div[2]/div/a/span[2]";
            //var ratioXAtt = "";
            //var ratio2Exp = ".//div/div[contains(@class,'market-options')]/div/div[3]/div/a/span[2]";
            //var ratio2Att = "";

            //var tmp = new Site()
            //{
            //    Url = url,
            //    Name = "Intertops",
            //    Image = "",
            //    ScrapingInfo = new ScrapingInfo()
            //    {
            //        GameListExpression = gamelistExp,
            //        DateExpression = dateExp,
            //        DateAttribute = dateAtt,
            //        DateFormat = dateFormat,
            //        HomeTeamNameExpression = team1Exp,
            //        HomeTeamAttribute = team1Att,
            //        AwayTeamNameExpression = team2Exp,
            //        AwayTeamAttribute = team2Att,
            //        HomeRatioExpression = ratio1Exp,
            //        HomeRatioAttribute = ratio1Att,
            //        RatioXExpression = ratioXExp,
            //        RatioXAttribute = ratioXAtt,
            //        AwayRatioExpression = ratio2Exp,
            //        AwayRatioAttribute = ratio2Att
            //    }
            //};
            //new SitesManager().Add(tmp);

            //var siteInfo = new SiteScrapingInfo(2, url, gamelistExp, dateExp, dateAtt, dateFormat, team1Exp, team1Att, team2Exp, team2Att, ratio1Exp, ratio1Att, ratioXExp, ratioXAtt, ratio2Exp, ratio2Att);
            //LoadAndParse(siteInfo);

            var sites = new SitesManager().GetAll();

            foreach (var site in sites)
            {
                LoadAndParse(site);
            }
        }

        private static void LoadAndParse(Site site)
        {
            using (var driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(site.Url);
                for (int i = 0; i < 100; i++)
                {
                    var html = driver.FindElementByTagName("html");
                    try
                    {
                        DateTime date;
                        string team1, team2;
                        double r1, rX, r2;
                        if (TryParsePage(html.GetAttribute("innerHTML"), site.Id, site.ScrapingInfo))
                            break;
                    }
                    catch (StaleElementReferenceException) { }
                    catch (FormatException)
                    {
                        Logger.LogCantParse(site.Id);
                    }
                }
                driver.Close();
            }
        }

        static bool TryParsePage(string html, int siteId, ScrapingInfo info)
        {
            var page = new HtmlDocument();
            page.LoadHtml(html);
            var games = page.DocumentNode.SelectNodes(info.GameListExpression);
            if (games == null) return false;
            foreach (var game in games)
            {
                var date = ParseDate(ReadFromNode(game, info.DateExpression, info.DateAttribute), info.DateFormat);
                var team1 = ReadFromNode(game, info.HomeTeamNameExpression, info.HomeTeamAttribute);
                var team2 = ReadFromNode(game, info.AwayTeamNameExpression, info.AwayTeamAttribute);
                var r1 = ParseOdd(ReadFromNode(game, info.HomeRatioExpression, info.HomeRatioAttribute));
                var rX = ParseOdd(ReadFromNode(game, info.RatioXExpression, info.RatioXAttribute));
                var r2 = ParseOdd(ReadFromNode(game, info.AwayRatioExpression, info.AwayRatioAttribute));
                AddGame(siteId, date, team1, team2, r1, rX, r2);
                Console.WriteLine("{0}: {1} vs {2}\nHome: {3}\tDraw: {4}\tAway: {5}\n\n",
                    date, team1, team2, r1, rX, r2);
            }

            return true;
        }

        private static DateTime ParseDate(string dateStr, string format)
        {
            return DateTime.ParseExact(dateStr, format, null);
        }

        private static double ParseOdd(string oddStr)
        {
            // If fraction
            if (oddStr.Contains("/"))
            {
                var parts = oddStr.Split('/');
                if (parts.Length != 2)
                    throw new FormatException();

                var prof = double.Parse(parts[0]) / double.Parse(parts[1]);
                return 1 + Math.Round(prof, 2);
            }

            return double.Parse(oddStr);
        }

        static string ReadFromNode(HtmlNode root, string exp, string att)
        {
            var node = root.SelectSingleNode(exp);
            string val;
            if (att != "")
                val = node.Attributes[att].Value;
            else
                val = node.InnerText;

            return val;
        }

        static void AddGame(int siteId, DateTime date, string team1name, string team2name, double ratio1, double ratioX, double ratio2)
        {
            var team1 = GetOrAddTeam(team1name);
            var team2 = GetOrAddTeam(team2name);

            var game = GetOrAddGame(date, team1, team2);

            UpdateOrAddRatio(game, siteId, ratio1, ratioX, ratio2);
        }        

        private static Team GetOrAddTeam(string teamName)
        {
            var manager = new TeamsManager();
            var team = manager.GetByPossibleName(teamName);
            if (team == null)
            {
                team = new Team()
                {
                    DisplayName = teamName,
                    PossibleNames = new List<TeamPossibleName>()
                };
                manager.Add(team);
            }

            return team;
        }

        private static Game GetOrAddGame(DateTime date, Team team1, Team team2)
        {
            var manager = new GamesManager();
            var game = manager.GetByTeams(team1, team2);
            if (game == null)
            {
                game = new Game()
                {
                    HomeTeam = team1,
                    AwayTeam = team2,
                    Date = date,
                    GameSiteRatios = new List<GameSiteRatio>()
                };
                manager.Add(game);
            }

            return game;
        }

        private static void UpdateOrAddRatio(Game game, int siteId, double ratio1, double ratioX, double ratio2)
        {
            var manager = new GameSiteRatiosManager();
            var siteRatios = game.GameSiteRatios;
            var siteRatio = siteRatios.FirstOrDefault(r => r.SiteId == siteId);
            if (siteRatio == null)
            {
                siteRatio = new GameSiteRatio()
                {
                    GameId = game.Id,
                    SiteId = siteId,
                    HomeRatio = ratio1,
                    DrawRatio = ratioX,
                    AwayRatio = ratio2,
                    LastUpdateTime = DateTime.Now
                };
                manager.Add(siteRatio);
            }
            else
            {
                siteRatio.HomeRatio = ratio1;
                siteRatio.DrawRatio = ratioX;
                siteRatio.AwayRatio = ratio2;
                manager.Update(siteRatio);
            }
        }
    }
}
