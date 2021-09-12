using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace FootBall
{
   
    class SeleniumChrome
    {
        WebDriverWait wait;
        IWebElement iwe;
       static IWebDriver driver = new ChromeDriver();
        FootBallTeams footBallTeams = new FootBallTeams();
        List<FootBallTeams> ListFootBallTeams = new List<FootBallTeams>();
        log log = new log();
        public FootBallTeams LoadData(int year,CountryEnum country)
        {
            
            try
            {
                ReSearch(country.ToString());
                SearchData(year);
                GetData();
            }
            catch(Exception ex) 
            {
                log.WriteLog(ex.Message);
            }
            finally
            {
                driver.Quit();
            }

            return footBallTeams;
        }

        public void ReSearch(string country)
        {
            try
            {
                IWebElement GetSearchBar;
                IWebElement SearchSubmit;
                driver.Navigate().GoToUrl("http://google.com");
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                GetSearchBar = wait.Until(ExpectedConditions.ElementToBeClickable(By.Name("q")));
                GetSearchBar.SendKeys(country.ToString());
                SearchSubmit = wait.Until(ExpectedConditions.ElementToBeClickable(By.Name("btnK")));
                SearchSubmit.Click();
            }
            catch(Exception ex)
            {
                log.WriteLog("ReSearch，重新搜尋錯誤。"+ex.Message);
            }
        }
        public void SearchData(int year)
        {
            IWebElement RecordSubmit;
            IWebElement DowpdownYears;
            string yearString = year.ToString().Replace("2019", "2019-20").Replace("2020", "2020-21");
            //按下戰績
            RecordSubmit = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath($"//*[@id='sports-app']/div/div[2]/div/div/div/ol/li[3]")));
            RecordSubmit.Click();

            if (year != 2021)
            {
                //按下下拉選單
                DowpdownYears = wait.Until(ExpectedConditions.ElementToBeClickable(By.TagName("g-dropdown-button")));
                DowpdownYears.Click();
                //找出所有g-menu-item
                var itemCount = driver.FindElements(By.TagName("g-menu-item"));
                foreach (var i in itemCount)
                {
                    try
                    {
                        //在g-menu-item的div裡找出文字並按下
                        string DowpdownItem = i.FindElement(By.TagName("div")).Text;
                        if (DowpdownItem == yearString)
                        {
                            i.Click();
                            break;
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }

        }

        public void GetData()
        {
            int TeamsCount = default;
            int dynamicIndex = 1;
            int Years, Level, TotalGames, WinGames, LoseGames, TieGames, WinBall, LoseBall;
            string TeamName;
            string SubtractBall;
            IWebElement TeamsData;
            string result = default;
            //*[@id="liveresults-sports-immersive__league-fullpage"]/div/div[2]/div[2]/div/div/div/div[3]/div/div/div/div[2]/div/div/div/div/div/div[1]/div/table/tbody/tr[2]

            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath($"//*[@id='liveresults-sports-immersive__league-fullpage']/div/div[2]/div[2]/div/div/div/div[3]/div/div/div/div[2]/div/div/div/div/div/div[" + dynamicIndex + "]/ div/table/tbody/tr")));
            TeamsCount = driver.FindElements(By.XPath($"//*[@id='liveresults-sports-immersive__league-fullpage']/div/div[2]/div[2]/div/div/div/div[3]/div/div/div/div[2]/div/div/div/div/div/div["+dynamicIndex+"]/ div/table/tbody/tr")).Count;
            if (TeamsCount == 0)
            {
                dynamicIndex++;
                TeamsCount = driver.FindElements(By.XPath($"//*[@id='liveresults-sports-immersive__league-fullpage']/div/div[2]/div[2]/div/div/div/div[3]/div/div/div/div[2]/div/div/div/div/div/div[" + dynamicIndex + "]/ div/table/tbody/tr")).Count;
            }
            for(int i = 2; i < TeamsCount+1; i++)
            {                                                       
                Level =Convert.ToInt32(driver.FindElement(By.XPath($"//*[@id='liveresults-sports-immersive__league-fullpage']/div/div[2]/div[2]/div/div/div/div[3]/div/div/div/div[2]/div/div/div/div/div/div[" + dynamicIndex + "]/div/table/tbody/tr["+i+"]/td[2]/div[2]")).Text);
                TeamName =            driver.FindElement(By.XPath($"//*[@id='liveresults-sports-immersive__league-fullpage']/div/div[2]/div[2]/div/div/div/div[3]/div/div/div/div[2]/div/div/div/div/div/div[" + dynamicIndex + "]/div/table/tbody/tr["+i+"]/td[3]/div/div/span")).Text;
                //*[@id="liveresults-sports-immersive__league-fullpage"]/div/div[2]/div[2]/div/div/div/div[3]/div/div/div/div[2]/div/div/div/div/div/div[2]/div/table/tbody/tr[2]/td[4]
                //*[@id="liveresults-sports-immersive__league-fullpage"]/div/div[2]/div[2]/div/div/div/div[3]/div/div/div/div[2]/div/div/div/div/div/div[2]/div/table/tbody/tr[2]/td[5]
                //*[@id="liveresults-sports-immersive__league-fullpage"]/div/div[2]/div[2]/div/div/div/div[3]/div/div/div/div[2]/div/div/div/div/div/div[2]/div/table/tbody/tr[2]/td[6]
                //*[@id="liveresults-sports-immersive__league-fullpage"]/div/div[2]/div[2]/div/div/div/div[3]/div/div/div/div[2]/div/div/div/div/div/div[2]/div/table/tbody/tr[2]/td[10]
                ListFootBallTeams.Add(new FootBallTeams { Level = Level, TeamName = TeamName });
            }
        }


    }
}
