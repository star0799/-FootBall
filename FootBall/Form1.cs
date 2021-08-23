using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace FootBall
{
    public partial class Form1 : Form
    {
        public const int LevelCount= 4;
        List<FootBallTeams> ListFootBallTeams = new List<FootBallTeams>();
        FootBallTeams footBallTeams2019 = new FootBallTeams();
        HtmlWeb webClient = new HtmlWeb();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            //WebClient webClient = new WebClient();

            //HtmlNodeCollection item = doc.DocumentNode.SelectNodes($"//*[@id='main']");


            ///html/body/div[4]/div[2]/div[2]/div/div/div[1]/div[1]/table/tbody/tr[1]/td[3]/a[2]/span


            //var memoryStream = new MemoryStream(webClient.DownloadData(@"https://www.google.com/search?q=%E8%8B%B1%E8%B6%85&rlz=1C1ASUM_enTW858TW858&oq=%E8%8B%B1%E8%B6%85&aqs=chrome..69i57.1348j0j7&sourceid=chrome&ie=UTF-8#sie=lg;/g/11p44qhs93;2;/m/02_tc;st;fp;1;;"));
            //HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            //doc.Load(memoryStream, Encoding.UTF8);
            //HtmlNodeCollection item = doc.DocumentNode.SelectNodes($"//*[@id='ow165']/div[1]/g-dropdown-button/div/g-dropdown-menu-button-caption/span");
            //HtmlAgilityPack.HtmlDocument docData = new HtmlAgilityPack.HtmlDocument();
            //docData.LoadHtml(doc.DocumentNode.SelectSingleNode(@"//div[@name='printhere']")
            //                                 .InnerHtml);

            //string tenp = item[0].InnerText;
            // HtmlAgilityPack.HtmlDocument docData = new HtmlAgilityPack.HtmlDocument();
            //docData.LoadHtml(doc.DocumentNode.SelectSingleNode(@"//*[@id='main']").InnerHtml);
            //*[@id="rcnt"]

            for(int beginYear=2019; beginYear< beginYear+3;beginYear++ )
            LoadYearsUrl(beginYear);
        }
        private void LoadYearsUrl(int year)
        {
            HtmlAgilityPack.HtmlDocument doc = default;
            if (year == 2019)
            {
                 doc = webClient.Load(ConfigurationManager.AppSettings["PremierLeague2019"]);
                
            }
            else if(year == 2020)
            {
                doc = webClient.Load(ConfigurationManager.AppSettings["PremierLeague2020"]);
            }
            else if(year==2021)
            {
                doc = webClient.Load(ConfigurationManager.AppSettings["PremierLeague2021"]);
            }
            GetGameData(doc, year);
        }

        //取2021年資料錯誤
        private void GetGameData(HtmlAgilityPack.HtmlDocument doc,int year)
        {

            int Years, Level, TotalGames, WinGames, LoseGames, TieGames, WinBall, LoseBall;
            string TeamName;
            Decimal SubtractBall;
            for (int i = 2; i < LevelCount + 2; i++)
            {
                Years = year;
                Level = Convert.ToInt32(doc.DocumentNode.SelectNodes($"//*[@id='mw-content-text']/div[1]/table[5]/tbody/tr[{i}]/td")[0].InnerHtml);
                TeamName = doc.DocumentNode.SelectNodes($"//*[@id='mw-content-text']/div[1]/table[5]/tbody/tr[{i}]/th")[0].InnerText;
                TotalGames = Convert.ToInt32(doc.DocumentNode.SelectNodes($"//*[@id='mw-content-text']/div[1]/table[5]/tbody/tr[{i}]/td")[1].InnerHtml);
                WinGames = Convert.ToInt32(doc.DocumentNode.SelectNodes($"//*[@id='mw-content-text']/div[1]/table[5]/tbody/tr[{i}]/td")[2].InnerHtml);
                TieGames = Convert.ToInt32(doc.DocumentNode.SelectNodes($"//*[@id='mw-content-text']/div[1]/table[5]/tbody/tr[{i}]/td")[3].InnerHtml);
                LoseGames = Convert.ToInt32(doc.DocumentNode.SelectNodes($"//*[@id='mw-content-text']/div[1]/table[5]/tbody/tr[{i}]/td")[4].InnerHtml);
                WinBall = Convert.ToInt32(doc.DocumentNode.SelectNodes($"//*[@id='mw-content-text']/div[1]/table[5]/tbody/tr[{i}]/td")[5].InnerHtml);
                LoseBall = Convert.ToInt32(doc.DocumentNode.SelectNodes($"//*[@id='mw-content-text']/div[1]/table[5]/tbody/tr[{i}]/td")[6].InnerHtml);
                SubtractBall = Convert.ToDecimal(doc.DocumentNode.SelectNodes($"//*[@id='mw-content-text']/div[1]/table[5]/tbody/tr[{i}]/td")[7].InnerHtml);
                ListFootBallTeams.Add(new FootBallTeams { Years=Years, Level = Level, TeamName = TeamName, TotalGames = TotalGames, WinGames = WinGames, TieGames = TieGames, LoseGames = LoseGames, WinBall = WinBall, LoseBall = LoseBall, SubtractBall = SubtractBall });
            }
        }

    }
}
