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

namespace AlgotrageScraper
{
    class Scraper
    {
        static void Main(string[] args)
        {
            //var url = "https://sports.intertops.eu/en";
            //var gamelistExp = "//ul[contains(@class,'nextgames')]/li";
            //var team1Exp = ".//div/div[contains(@class,'event-title')]";
            //var team2Exp = ".//div/div[contains(@class,'event-title')]";
            //var ratio1Exp = ".//div/div[contains(@class,'market-options')]/div/div[1]/div/a/span[2]";
            //var ratioXExp = ".//div/div[contains(@class,'market-options')]/div/div[2]/div/a/span[2]";
            //var ratio2Exp = ".//div/div[contains(@class,'market-options')]/div/div[3]/div/a/span[2]";

            var siteInfoList = GetAllSitesInfo();

            foreach (var siteInfo in siteInfoList)
            {
                LoadAndParse(siteInfo);
            }

            Console.Read();
        }

        private static List<SiteScrapingInfo> GetAllSitesInfo()
        {
            var list = new List<SiteScrapingInfo>();
            string[] csvText = File.ReadAllLines("ScrapingInfo.csv");

            foreach (string line in csvText)
            {
                var currInfo = line.Split(',');
                list.Add(new SiteScrapingInfo(currInfo[0], currInfo[1], currInfo[2], currInfo[3], currInfo[4], currInfo[5], currInfo[6]));
            }

            return list;
        }

        private static void LoadAndParse(SiteScrapingInfo info)
        {
            using (var driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(info.Url);
                for (int i = 0; i < 100; i++)
                {
                    var html = driver.FindElementByTagName("html");
                    try
                    {
                        if (ParsePage(html.GetAttribute("innerHTML"), info.GameListExpression, info.Team1NameExpression, info.Team2NameExpression, info.Ratio1Expression, info.RatioXExpression, info.Ratio2Expression))
                            break;
                    }
                    catch (StaleElementReferenceException e) { }
                }
            }
        }

        static bool ParsePage(string html, string gamelistExp, string team1Exp, string team2Exp, string ratio1Exp, string ratioXExp, string ratio2Exp)
        {
            var page = new HtmlDocument();
            page.LoadHtml(html);
            var games = page.DocumentNode.SelectNodes(gamelistExp);
            if (games == null) return false;
            foreach (var game in games)
            {
                var team1 = game.SelectSingleNode(team1Exp);
                var team2 = game.SelectSingleNode(team2Exp);
                var r1 = game.SelectSingleNode(ratio1Exp);
                var rX = game.SelectSingleNode(ratioXExp);
                var r2 = game.SelectSingleNode(ratio2Exp);
            }

            return true;
        }
    }
}
