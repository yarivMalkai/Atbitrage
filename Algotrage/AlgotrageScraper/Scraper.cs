using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

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

            ////ParsePage(url, gamelistExp, gamelistExp, ratio1Exp, ratioXExp, ratio2Exp);

            //var url = "http://www.sportsbet.com.au/betting/soccer";
            //var gamelistExp = "//ul[contains(@class,'accordion-main')]/li[not(@class)]";
            //var team1Exp = ".//div[contains(@class,'accordion-body')]/div/div[1]/a/span[1]";
            //var team2Exp = ".//div[contains(@class,'accordion-body')]/div/div[3]/a/span[1]";
            //var ratio1Exp = ".//div[contains(@class,'accordion-body')]/div/div[1]/a/span[2]";
            //var ratioXExp = ".//div[contains(@class,'accordion-body')]/div/div[2]/a/span[2]";
            //var ratio2Exp = ".//div[contains(@class,'accordion-body')]/div/div[3]/a/span[2]";

            //LoadAndParse(url, gamelistExp, team1Exp, team2Exp, ratio1Exp, ratioXExp, ratio2Exp);

            Console.Read();
        }

        private static void LoadAndParse(string url, string gamelistExp, string team1Exp, string team2Exp, string ratio1Exp, string ratioXExp, string ratio2Exp)
        {
            using (var driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl(url);
                for (int i = 0; i < 100; i++)
                {
                    var html = driver.FindElementByTagName("html");
                    try
                    {
                        if (ParsePage(html.GetAttribute("innerHTML"), gamelistExp, team1Exp, team2Exp, ratio1Exp, ratioXExp, ratio2Exp))
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
                Console.WriteLine("{0} vs {1}: 1: {2}   X: {3}   2: 4{3}", team1.InnerText.Trim(), team2.InnerText.Trim(), r1.InnerText.Trim(), rX.InnerText.Trim(), r2.InnerText.Trim());
            }

            return true;
        }
    }
}
