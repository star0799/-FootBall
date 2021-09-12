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

        public FootBallTeams LoadData(int year,CountryEnum country)
        {
            string yearString = year.ToString().Replace("2019", "2019-20").Replace("2020","2020-21");
            IWebElement GetSearchBar;
            IWebElement SearchSubmit;
            IWebElement RecordSubmit;
            IWebElement DowpdownYears;
            try
            {
                driver.Navigate().GoToUrl("http://google.com");
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                GetSearchBar = wait.Until(ExpectedConditions.ElementToBeClickable(By.Name("q")));
                GetSearchBar.SendKeys(country.ToString());
                SearchSubmit = wait.Until(ExpectedConditions.ElementToBeClickable(By.Name("btnK")));
                SearchSubmit.Click();
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
                        catch { }
                    }
                }
                Thread.Sleep(2000);
            }
            catch { }
            finally
            {
                driver.Quit();
            }

            return footBallTeams;
        }


    }
}
