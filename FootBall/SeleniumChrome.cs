using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using System.Threading;
using System.Configuration;

namespace FootBall
{

    class SeleniumChrome
    {
        WebDriverWait wait;
        IWebDriver driver = new ChromeDriver();
        FootBallTeams footBallTeams = new FootBallTeams();
        List<FootBallTeams> ListFootBallTeams = new List<FootBallTeams>();
        WriteFile writeFile = new WriteFile();
        log log = new log();
        public void LoadData()
        {
            int DataYeasCount = Convert.ToInt16(ConfigurationManager.AppSettings["DataYeasCount"] ?? "3")-1;
            int StartYear = DateTime.Now.Year - DataYeasCount;
            //多抓下一年看有沒有值，顯示資料那段會篩掉
            int EndYear = DateTime.Now.Year + 1;
            try
            {
                //目前年份不管檔案有沒有存在都會去更新
                foreach (string name in Enum.GetNames(typeof(CountryEnum)))
                {
                    ReSearch(name);
                    for (int i = StartYear; i <= EndYear; i++)
                    {
                        //判斷是不是最新一期和最新二期，最新一期必須更新，最新二期不管是否最新也一併更新
                        if (i != EndYear && i!= EndYear-1)
                        {
                            //舊資料檔案只要存在就不做處理
                            if (!writeFile.IsExistData(name, i))
                            {
                                //進入網站檢查elemenet在不在，不在跳過
                                if (IsExistYears(i))
                                {
                                    ListFootBallTeams.Clear();
                                    GetData(i);
                                    //寫入txt
                                    writeFile.WriteData(name, i, ListFootBallTeams);
                                }
                                else
                                    continue;
                            }
                        }
                        else
                        {
                            //進入網站檢查elemenet在不在，不在跳過
                            if (IsExistYears(i))
                            {
                                ListFootBallTeams.Clear();
                                GetData(i);
                                //目前年份寫入txt
                                writeFile.UpdateData(name, i, ListFootBallTeams);
                            }
                            else
                                continue;
                        }
                    }
                }

            }
            catch (Exception ex)
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
                string GoogleUrl = ConfigurationManager.AppSettings["GoogleUrl"].ToString() ?? "http://google.com";
                driver.Navigate().GoToUrl(GoogleUrl);
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                GetSearchBar = wait.Until(ExpectedConditions.ElementToBeClickable(By.Name("q")));
                GetSearchBar.SendKeys(country.ToString());
                SearchSubmit = wait.Until(ExpectedConditions.ElementToBeClickable(By.Name("btnK")));
                SearchSubmit.Click();
                //按下戰績
                RecordSubmit = wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath($"//*[@id='sports-app']/div/div[2]/div/div/div/ol/li[3]")));
                RecordSubmit.Click();
            }
            catch (Exception ex)
            {
                log.WriteLog("ReSearch，重新搜尋錯誤。" + ex.Message);
            }
        }
        //切換不同年份資料
        public bool IsExistYears(int year)
        {
            IWebElement DowpdownYears;
            string tmpNextYear = ((year).ToString()).Substring(2, 2);
            string yearString = (year - 1).ToString() + "-" + tmpNextYear;
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
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    log.WriteLog("SearchData錯誤 : " + ex.Message);
                    return false;
                }
            }
            return false;
        }
        //取資料並存入list
        public void GetData(int year)
        {
            int TeamsCount = default;
            int dynamicIndex = 2;
            int Years, Level, TotalGames, WinGames, LoseGames, TieGames, WinBall, LoseBall;
            string TeamName;
            string SubtractBall;
            string XPath = ConfigurationManager.AppSettings["XPath"] ?? "//*[@id='liveresults-sports-immersive__league-fullpage']/div/div[2]/div[2]/div/div/div/div[3]/div/div/div/div[2]/div/div/div/div/div/div/div[2]/div";
            int Element = Convert.ToInt16(ConfigurationManager.AppSettings["Element"] ?? "2");
            try
            {
                //*[@id="liveresults-sports-immersive__league-fullpage"]/div/div[2]/div[2]/div/div/div/div[3]/div/div/div/div[2]/div/div/div/div/div/div/div[2]/div/div[2]/div/table/tbody/tr[1]
                wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(XPath + "/div[" + dynamicIndex + "]/div/table/tbody/tr[1]")));
                //TeamsCount = driver.FindElements(By.XPath($"//*[@id='liveresults-sports-immersive__league-fullpage']/div/div[2]/div[2]/div/div/div/div[3]/div/div/div/div[2]/div/div/div/div/div/div[" + dynamicIndex + "]/ div/table/tbody/tr")).Count;
                TeamsCount = driver.FindElements(By.XPath(XPath + "/div[" + dynamicIndex + "]/div/table/tbody/tr")).Count;
                if (TeamsCount == 0)
                {
                    dynamicIndex++;
                    TeamsCount = driver.FindElements(By.XPath($"//*[@id='liveresults-sports-immersive__league-fullpage']/div/div[2]/div[2]/div/div/div/div[3]/div/div/div/div[2]/div/div/div/div/div/div[" + dynamicIndex + "]/ div/table/tbody/tr")).Count;
                }
                for (int i = Element; i < TeamsCount + 1; i++)
                {
                    Years = year;
                    Level = Convert.ToInt32(driver.FindElement(By.XPath(XPath + "/div[" + dynamicIndex + "]/div/table/tbody/tr[" + i + "]/td[2]/div[2]")).Text);
                    TeamName = driver.FindElement(By.XPath(XPath + "/div[" + dynamicIndex + "]/div/table/tbody/tr[" + i + "]/td[3]/div/div/span")).Text;
                    TotalGames = Convert.ToInt32(driver.FindElement(By.XPath(XPath + "/div[" + dynamicIndex + "]/div/table/tbody/tr[" + i + "]/td[4]")).Text);
                    WinGames = Convert.ToInt32(driver.FindElement(By.XPath(XPath + "/div[" + dynamicIndex + "]/div/table/tbody/tr[" + i + "]/td[5]")).Text);
                    TieGames = Convert.ToInt32(driver.FindElement(By.XPath(XPath + "/div[" + dynamicIndex + "]/div/table/tbody/tr[" + i + "]/td[6]")).Text);
                    LoseGames = Convert.ToInt32(driver.FindElement(By.XPath(XPath + "/div[" + dynamicIndex + "]/div/table/tbody/tr[" + i + "]/td[7]")).Text);
                    WinBall = Convert.ToInt32(driver.FindElement(By.XPath(XPath + "/div[" + dynamicIndex + "]/div/table/tbody/tr[" + i + "]/td[8]")).Text);
                    LoseBall = Convert.ToInt32(driver.FindElement(By.XPath(XPath + "/div[" + dynamicIndex + "]/div/table/tbody/tr[" + i + "]/td[9]")).Text);
                    SubtractBall = driver.FindElement(By.XPath(XPath + "/div[" + dynamicIndex + "]/div/table/tbody/tr[" + i + "]/td[10]")).Text.ToString();
                    ListFootBallTeams.Add(new FootBallTeams { Years = Years, Level = Level, TeamName = TeamName, TotalGames = TotalGames, WinGames = WinGames, TieGames = TieGames, LoseGames = LoseGames, WinBall = WinBall, LoseBall = LoseBall, SubtractBall = SubtractBall });
                }
            }
            catch (Exception ex)
            {
                log.WriteLog("GetData錯誤 : " + ex.Message);
            }
        }
    }
}
