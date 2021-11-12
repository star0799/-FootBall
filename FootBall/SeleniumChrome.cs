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
        IWebDriver driver = new ChromeDriver();
        FootBallTeams footBallTeams = new FootBallTeams();
        List<FootBallTeams> ListFootBallTeams = new List<FootBallTeams>();
        WriteFile writeFile = new WriteFile();
        log log = new log();
        public void LoadData()
        {
            
            try
            {
                //2021不管檔案有沒有存在都會去更新
                foreach (string name in Enum.GetNames(typeof(CountryEnum)))
                {
                    ReSearch(name);
                    for (int i = 2019; i < 2022; i++)
                    {
                        if (i != 2021)
                        {
                            if (!writeFile.IsExistData(name, i))
                            {
                                SearchData(i);
                                ListFootBallTeams.Clear();
                                GetData(i);
                                //寫入txt
                                writeFile.WriteData(name, i, ListFootBallTeams);
                            }
                        }
                        else
                        {
                            //ReSearch(name);
                            SearchData(i);
                            ListFootBallTeams.Clear();
                            GetData(i);
                            //2021寫入txt
                            writeFile.UpdateData(name, i, ListFootBallTeams);
                        }
                    }
                    
                }
                
              
            }
            catch(Exception ex) 
            {
                log.WriteLog(ex.Message);
            }
            finally
            {
                driver.Quit();
            }

        }
        //重新到google頁面搜尋
        public void ReSearch(string country)
        {
            try
            {
                IWebElement GetSearchBar;
                IWebElement SearchSubmit;
                IWebElement RecordSubmit;
                driver.Navigate().GoToUrl("http://google.com");
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                GetSearchBar = wait.Until(ExpectedConditions.ElementToBeClickable(By.Name("q")));
                GetSearchBar.SendKeys(country.ToString());
                SearchSubmit = wait.Until(ExpectedConditions.ElementToBeClickable(By.Name("btnK")));
                SearchSubmit.Click();
                //按下戰績
                RecordSubmit = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath($"//*[@id='sports-app']/div/div[2]/div/div/div/ol/li[3]")));
                RecordSubmit.Click();
            }
            catch(Exception ex)
            {
                log.WriteLog("ReSearch，重新搜尋錯誤。"+ex.Message);
            }
        }
        //切換不同年份資料
        public void SearchData(int year)
        {
          
            IWebElement DowpdownYears;
            string yearString = year.ToString().Replace("2019", "2019-20").Replace("2020", "2020-21").Replace("2021","2021-22");
         
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
                    log.WriteLog("SearchData錯誤 : "+ex.Message);
                    }
                }
            

        }
        //取資料並存入list
        public void GetData(int year)
        {
            int TeamsCount = default;
            int dynamicIndex = 1;
            int Years, Level, TotalGames, WinGames, LoseGames, TieGames, WinBall, LoseBall;
            string TeamName;
            string SubtractBall;
            IWebElement TeamsData;
            //*[@id="liveresults-sports-immersive__league-fullpage"]/div/div[2]/div[2]/div/div/div/div[3]/div/div/div/div[2]/div/div/div/div/div/div[1]/div/table/tbody/tr[2]
            try
            {
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath($"//*[@id='liveresults-sports-immersive__league-fullpage']/div/div[2]/div[2]/div/div/div/div[3]/div/div/div/div[2]/div/div/div/div/div/div[" + dynamicIndex + "]/ div/table/tbody/tr")));
                TeamsCount = driver.FindElements(By.XPath($"//*[@id='liveresults-sports-immersive__league-fullpage']/div/div[2]/div[2]/div/div/div/div[3]/div/div/div/div[2]/div/div/div/div/div/div[" + dynamicIndex + "]/ div/table/tbody/tr")).Count;
                if (TeamsCount == 0)
                {
                    dynamicIndex++;
                    TeamsCount = driver.FindElements(By.XPath($"//*[@id='liveresults-sports-immersive__league-fullpage']/div/div[2]/div[2]/div/div/div/div[3]/div/div/div/div[2]/div/div/div/div/div/div[" + dynamicIndex + "]/ div/table/tbody/tr")).Count;
                }
                for (int i = 2; i < TeamsCount + 1; i++)
                {
                    Years = year;
                    Level = Convert.ToInt32(driver.FindElement(By.XPath($"//*[@id='liveresults-sports-immersive__league-fullpage']/div/div[2]/div[2]/div/div/div/div[3]/div/div/div/div[2]/div/div/div/div/div/div[" + dynamicIndex + "]/div/table/tbody/tr[" + i + "]/td[2]/div[2]")).Text);
                    TeamName = driver.FindElement(By.XPath($"//*[@id='liveresults-sports-immersive__league-fullpage']/div/div[2]/div[2]/div/div/div/div[3]/div/div/div/div[2]/div/div/div/div/div/div[" + dynamicIndex + "]/div/table/tbody/tr[" + i + "]/td[3]/div/div/span")).Text;
                    TotalGames = Convert.ToInt32(driver.FindElement(By.XPath($"//*[@id='liveresults-sports-immersive__league-fullpage']/div/div[2]/div[2]/div/div/div/div[3]/div/div/div/div[2]/div/div/div/div/div/div[" + dynamicIndex + "]/div/table/tbody/tr[" + i + "]/td[4]")).Text);
                    WinGames = Convert.ToInt32(driver.FindElement(By.XPath($"//*[@id='liveresults-sports-immersive__league-fullpage']/div/div[2]/div[2]/div/div/div/div[3]/div/div/div/div[2]/div/div/div/div/div/div[" + dynamicIndex + "]/div/table/tbody/tr[" + i + "]/td[5]")).Text);
                    TieGames = Convert.ToInt32(driver.FindElement(By.XPath($"//*[@id='liveresults-sports-immersive__league-fullpage']/div/div[2]/div[2]/div/div/div/div[3]/div/div/div/div[2]/div/div/div/div/div/div[" + dynamicIndex + "]/div/table/tbody/tr[" + i + "]/td[6]")).Text);
                    LoseGames = Convert.ToInt32(driver.FindElement(By.XPath($"//*[@id='liveresults-sports-immersive__league-fullpage']/div/div[2]/div[2]/div/div/div/div[3]/div/div/div/div[2]/div/div/div/div/div/div[" + dynamicIndex + "]/div/table/tbody/tr[" + i + "]/td[7]")).Text);
                    WinBall = Convert.ToInt32(driver.FindElement(By.XPath($"//*[@id='liveresults-sports-immersive__league-fullpage']/div/div[2]/div[2]/div/div/div/div[3]/div/div/div/div[2]/div/div/div/div/div/div[" + dynamicIndex + "]/div/table/tbody/tr[" + i + "]/td[8]")).Text);
                    LoseBall = Convert.ToInt32(driver.FindElement(By.XPath($"//*[@id='liveresults-sports-immersive__league-fullpage']/div/div[2]/div[2]/div/div/div/div[3]/div/div/div/div[2]/div/div/div/div/div/div[" + dynamicIndex + "]/div/table/tbody/tr[" + i + "]/td[9]")).Text);
                    SubtractBall = driver.FindElement(By.XPath($"//*[@id='liveresults-sports-immersive__league-fullpage']/div/div[2]/div[2]/div/div/div/div[3]/div/div/div/div[2]/div/div/div/div/div/div[" + dynamicIndex + "]/div/table/tbody/tr[" + i + "]/td[10]")).Text.ToString();

                    ListFootBallTeams.Add(new FootBallTeams { Years = Years, Level = Level, TeamName = TeamName, TotalGames = TotalGames, WinGames = WinGames, TieGames = TieGames, LoseGames = LoseGames, WinBall = WinBall, LoseBall = LoseBall, SubtractBall = SubtractBall });
                }
            }
            catch(Exception ex)
            {
                log.WriteLog("GetData錯誤 : " + ex.Message);
            }
        }


    }
}
