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
using System.Text.RegularExpressions;
using System.Threading;
using F23.StringSimilarity;

namespace AlgotrageScraper
{
    public class Scraper
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Scraper starting");
            HtmlNode.ElementsFlags.Remove("form");

            //SetupSites();

            var sites = new SitesManager().GetAll();

            while (true)
            {
                foreach (var site in sites)
                {
                    Console.WriteLine("\nParsing site {0}: {1}", site.Id, site.Name);
                    LoadAndParse(site);
                }

                Console.WriteLine("\nSleeping until next go...\n");
                Thread.Sleep(1000*60);
            }
        }

        private static void SetupSites()
        {
            var manager = new SitesManager();

            #region Intertops - Next Games
            var IntertopsNG = new Site()
            {
                Url = "https://sports.intertops.eu/en/Bets/Sport/12",
                Name = "Intertops - Next Games",
                Image = "",
                ScrapingInfo = new ScrapingInfo()
                {
                    GameListExpression = "//ul[contains(@class,'nextbets')]/li[not(@class)]",
                    DateExpression = ".//div/div[contains(@class, 'event-date')]/span",
                    DateAttribute = "title",
                    DateFormat = "M/d/yyyy'<br/>'h:mm tt",
                    TimeExpression = ".//div/div[contains(@class, 'event-date')]/span",
                    TimeAttribute = "title",
                    TimeFormat = "M/d/yyyy'<br/>'h:mm tt",
                    HomeTeamNameExpression = ".//div/div[contains(@class,'market-options')]/div/div[1]/div",
                    HomeTeamAttribute = "title",
                    AwayTeamNameExpression = ".//div/div[contains(@class,'market-options')]/div/div[3]/div",
                    AwayTeamAttribute = "title",
                    HomeRatioExpression = ".//div/div[contains(@class,'market-options')]/div/div[1]/div/a/span[2]",
                    HomeRatioAttribute = "",
                    RatioXExpression = ".//div/div[contains(@class,'market-options')]/div/div[2]/div/a/span[2]",
                    RatioXAttribute = "",
                    AwayRatioExpression = ".//div/div[contains(@class,'market-options')]/div/div[3]/div/a/span[2]",
                    AwayRatioAttribute = "",
                }
            };
            #endregion
            manager.Add(IntertopsNG);

            #region Intertops - Top Bets
            var IntertopsTB = new Site()
            {
                Url = "https://sports.intertops.eu/en/Bets/Sport/12",
                Name = "Intertops - Top Bets",
                Image = "",
                ScrapingInfo = new ScrapingInfo()
                {
                    GameListExpression = "//ul[contains(@class,'topbets')]/li",
                    DateExpression = ".//div/div[contains(@class, 'event-date')]/span",
                    DateAttribute = "title",
                    DateFormat = "M/d/yyyy'<br/>'h:mm tt",
                    TimeExpression = ".//div/div[contains(@class, 'event-date')]/span",
                    TimeAttribute = "title",
                    TimeFormat = "M/d/yyyy'<br/>'h:mm tt",
                    HomeTeamNameExpression = ".//div/div[contains(@class,'market-options')]/div/div[1]/div",
                    HomeTeamAttribute = "title",
                    AwayTeamNameExpression = ".//div/div[contains(@class,'market-options')]/div/div[3]/div",
                    AwayTeamAttribute = "title",
                    HomeRatioExpression = ".//div/div[contains(@class,'market-options')]/div/div[1]/div/a/span[2]",
                    HomeRatioAttribute = "",
                    RatioXExpression = ".//div/div[contains(@class,'market-options')]/div/div[2]/div/a/span[2]",
                    RatioXAttribute = "",
                    AwayRatioExpression = ".//div/div[contains(@class,'market-options')]/div/div[3]/div/a/span[2]",
                    AwayRatioAttribute = "",
                }
            };
            #endregion
            //manager.Add(IntertopsTB);

            #region WilliamHill
            var WilliamHill = new Site()
            {
                Url = "http://sports.williamhill.com/bet/en-gb/betting/y/5/Football.html",
                Name = "WilliamHill",
                Image = "",
                ScrapingInfo = new ScrapingInfo()
                {
                    GameListExpression = "//tr[contains(@class,'rowOdd') and not(td[1]/a)]",
                    DateExpression = ".//td[1]/span",
                    DateAttribute = "",
                    DateFormat = "dd MMM",
                    TimeExpression = ".//td[2]/span",
                    TimeAttribute = "",
                    TimeFormat = "HH:mm 'UK'",
                    HomeTeamNameExpression = ".//td[3]/a/span",
                    HomeTeamAttribute = "",
                    AwayTeamNameExpression = ".//td[3]/a/span",
                    AwayTeamAttribute = "",
                    HomeRatioExpression = ".//td[5]/div/div",
                    HomeRatioAttribute = "",
                    RatioXExpression = ".//td[6]/div/div",
                    RatioXAttribute = "",
                    AwayRatioExpression = ".//td[7]/div/div",
                    AwayRatioAttribute = "",
                }
            };
            #endregion
            manager.Add(WilliamHill);
            
            #region BWin
            var BWin = new Site()
            {
                Url = "https://sports.bwin.com/en/sports#sportId=4",
                Name = "BWin",
                Image = "",
                ScrapingInfo = new ScrapingInfo()
                {
                    GameListExpression = "//div[@id='markets']/div[1]/div/div/div/div/div/div[contains(@class,'marketboard-event-group__item--sub-group')]/div/div[contains(@class,'marketboard-event-group__item--event')]",
                    DateExpression = "//div[@id='markets']/div[1]/div/div/h2/span",
                    DateAttribute = "",
                    DateFormat = "dddd - M/d/yyyy",
                    TimeExpression = ".//div/div/div[1]",
                    TimeAttribute = "",
                    TimeFormat = "h:mm tt",
                    HomeTeamNameExpression = ".//div/div/div[3]/table/tbody/tr/td[1]/form/button/div[1]",
                    HomeTeamAttribute = "",
                    AwayTeamNameExpression = ".//div/div/div[3]/table/tbody/tr/td[5]/form/button/div[1]",
                    AwayTeamAttribute = "",
                    HomeRatioExpression = ".//div/div/div[3]/table/tbody/tr/td[1]/form/button/div[2]",
                    HomeRatioAttribute = "",
                    RatioXExpression = ".//div/div/div[3]/table/tbody/tr/td[3]/form/button/div[2]",
                    RatioXAttribute = "",
                    AwayRatioExpression = ".//div/div/div[3]/table/tbody/tr/td[5]/form/button/div[2]",
                    AwayRatioAttribute = ""
                }
            };
            #endregion
            manager.Add(BWin);

            #region MockSite1
            var MockSite = new Site()
            {
                Url = "http://lior-kedem.com/dor_test/MockSite1.html",
                Name = "Intertops - Next Games",
                Image = "",
                ScrapingInfo = new ScrapingInfo()
                {
                    GameListExpression = "//ul/li",
                    DateExpression = ".//div[1]",
                    DateAttribute = "",
                    DateFormat = "dd/MM/yy HH:mm",
                    TimeExpression = ".//div[1]",
                    TimeAttribute = "",
                    TimeFormat = "dd/MM/yy HH:mm",
                    HomeTeamNameExpression = ".//div[2]/span[1]",
                    HomeTeamAttribute = "",
                    AwayTeamNameExpression = ".//div[4]/span[1]",
                    AwayTeamAttribute = "",
                    HomeRatioExpression = ".//div[2]/span[2]",
                    HomeRatioAttribute = "",
                    RatioXExpression = ".//div[3]/span[2]",
                    RatioXAttribute = "",
                    AwayRatioExpression = ".//div[4]/span[2]",
                    AwayRatioAttribute = "",
                }
            };
            #endregion
            manager.Add(MockSite);
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
                        break;
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
                var datetime = GetDateTime(game, info);
                var teams = ExtractTeams(game, info);
                var team1 = teams[0].Trim();
                var team2 = teams[1].Trim();
                var r1 = ParseOdd(ReadFromNode(game, info.HomeRatioExpression, info.HomeRatioAttribute));
                var rX = ParseOdd(ReadFromNode(game, info.RatioXExpression, info.RatioXAttribute));
                var r2 = ParseOdd(ReadFromNode(game, info.AwayRatioExpression, info.AwayRatioAttribute));
                AddGame(siteId, datetime, team1, team2, r1, rX, r2);
                //Console.WriteLine("{0}: {1} vs {2}\nHome: {3}\tDraw: {4}\tAway: {5}\n\n", datetime, team1, team2, r1, rX, r2);
            }

            return true;
        }

        private static string[] ExtractTeams(HtmlNode game, ScrapingInfo info)
        {
            var separators = new string[] { " vs. ", " vs ", " v " };
            string[] teams = null;
            if (info.HomeTeamNameExpression!= info.AwayTeamNameExpression ||
                info.HomeTeamAttribute != info.AwayTeamAttribute)
            {
                teams = new string[2];
                teams[0] = ReadFromNode(game, info.HomeTeamNameExpression, info.HomeTeamAttribute);
                teams[1] = ReadFromNode(game, info.AwayTeamNameExpression, info.AwayTeamAttribute);
            }
            else
            {
                var teamsOneLine = ReadFromNode(game, info.HomeTeamNameExpression, info.HomeTeamAttribute).Replace("&nbsp;", " ");
                foreach (var sep in separators)
                {
                    if (teamsOneLine.Contains(sep))
                    {
                        teams = teamsOneLine.Split(new[] { sep }, StringSplitOptions.RemoveEmptyEntries);
                        break;
                    }
                }
            }

            if (teams != null)
                return teams;
            else
                throw new FormatException();
        }

        private static DateTime GetDateTime(HtmlNode game, ScrapingInfo info)
        {
            var date = ParseDateTime(ReadFromNode(game, info.DateExpression, info.DateAttribute), info.DateFormat);
            var time = ParseDateTime(ReadFromNode(game, info.TimeExpression, info.TimeAttribute), info.TimeFormat);
            return new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, time.Second);
        }

        private static DateTime ParseDateTime(string dateStr, string format)
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
            else if (oddStr == "EVS")
                return 2;

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

            return val.Trim();
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
            var game = manager.GetByTeamsAndDate(team1, team2, date) ?? manager.GetByTeamsAndDate(team2, team1, date);

            if (game == null)
            {
                // Check if game exists but one of the team names is different
                game = manager.GetByHomeTeamAndDate(team1, date);
                if (game != null)
                    SetPossibleNameIfNeeded(game.AwayTeamId, team2);
                else
                {
                    game = manager.GetByAwayTeamAndDate(team2, date);
                    if (game != null)
                        SetPossibleNameIfNeeded(game.HomeTeamId, team1);
                    else
                    {
                        game = new Game()
                        {
                            HomeTeamId = team1.Id,
                            AwayTeamId = team2.Id,
                            Date = date,
                            GameSiteRatios = new List<GameSiteRatio>()
                        };
                        manager.Add(game);
                    }
                }
            }

            return game;
        }

        private static void SetPossibleNameIfNeeded(int teamId, Team team)
        {
            var manager = new TeamsManager();
            var existingTeam = manager.GetById(teamId);
            if (JaroWinklerWrapper.AreSimilar(existingTeam.DisplayName.ToLower(), team.DisplayName.ToLower()))
            {
                manager.AddPossibleName(new TeamPossibleName()
                {
                    TeamId = teamId,
                    PossibleName = team.DisplayName,
                });
                //existingTeam.PossibleNames.Add();
                //manager.Update(existingTeam);
                manager.Remove(team);
            }
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
