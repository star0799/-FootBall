using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Linq;

namespace FootBall
{
    public partial class Form1 : Form
    {
        public const int LevelCount= 4;
        List<FootBallTeams> ListFootBallTeams = new List<FootBallTeams>();
        FootBallTeams footBallTeams2019 = new FootBallTeams();
        HtmlWeb webClient = new HtmlWeb();
        int endYear = 2022;
        public Form1()
        {
            InitializeComponent();
        }
      

        private void button1_Click(object sender, EventArgs e)
        {


            //var query = (from lfb in ListFootBallTeams
            //             orderby lfb.TeamName, lfb.Years
            //             select lfb.Years.ToString().PadRight(10, ' ') + lfb.Level.ToString().PadRight(10, ' ') + lfb.TeamName + string.Empty.PadRight(12 - Encoding.Default.GetByteCount(lfb.TeamName)) + lfb.TotalGames.ToString().PadRight(10, ' ') + lfb.WinGames.ToString().PadRight(10, ' ') + lfb.LoseGames.ToString().PadRight(10, ' ') + lfb.TieGames.ToString().PadRight(10, ' ') + lfb.WinBall.ToString().PadRight(10, ' ') + lfb.LoseBall.ToString().PadRight(10, ' ') + lfb.SubtractBall + "      ");
            var query = (from lfb in ListFootBallTeams
                         orderby lfb.TeamName, lfb.Years
                         select lfb).ToList();
           

            foreach (var q in query)
            {
                lvShow.Items.Add(new ListViewItem(new string[] { q.Years.ToString(), q.Level.ToString(), q.TeamName,q.TotalGames.ToString(),q.WinGames.ToString(),q.LoseGames.ToString(),q.TieGames.ToString(),q.WinBall.ToString(),q.LoseBall.ToString(),q.SubtractBall.ToString() }));
            }

            var DistinctTeams = (from m in ListFootBallTeams
                                 select new
                                   {
                                      m.TeamName                                      
                                   }
               ).Distinct().ToList();
            
            foreach (var d in DistinctTeams)
            {
                var TeamCalculate = ListFootBallTeams.Where(x => x.TeamName == d.TeamName).Sum(y=>y.WinBall);

            }
            var a = "";
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

            //年分 排名       隊名 總場  贏局 輸局  和局 贏球  輸球 球差

            lvShow.View = View.Details;
            lvShow.GridLines = true;
            //lvShow.LabelEdit = false;
            lvShow.FullRowSelect = true;
            lvShow.Columns.Add("年份", 100);
            lvShow.Columns.Add("排名", 100);
            lvShow.Columns.Add("隊名", 100);
            lvShow.Columns.Add("總場", 100);
            lvShow.Columns.Add("贏局", 100);
            lvShow.Columns.Add("輸局", 100);
            lvShow.Columns.Add("和局", 100);
            lvShow.Columns.Add("贏球", 100);
            lvShow.Columns.Add("輸球", 100);
            lvShow.Columns.Add("球差", 100);
            lvShow.Columns.Add("平均每場得分", 100);
            lvShow.Columns.Add("平均每場失分", 100);
            //for (int i = 0; i < 10; i++)
            //{
            //    var item = new ListViewItem($"No.{i}");
            //    item.SubItems.Add($"文字{i}");
            //    lvShow.Items.Add(item);
            //}


            for (int beginYear = 2019; beginYear < endYear; beginYear++)
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

        
        private void GetGameData(HtmlAgilityPack.HtmlDocument doc,int year)
        {

            int Years, Level, TotalGames, WinGames, LoseGames, TieGames, WinBall, LoseBall;
            string TeamName;
            Decimal SubtractBall;           
            int getXpathIndex2021 =0;
            //取2021位置不同
            if (year==2021)
                getXpathIndex2021 = 2;
            for (int i = 2; i < LevelCount + 2; i++)
            {
                Years = year;
                                                                       //2021//*[@id="mw-content-text"]/div[1]/table[7]/tbody/tr[2]
                Level = Convert.ToInt32(doc.DocumentNode.SelectNodes($"//*[@id='mw-content-text']/div[1]/table[{5+ getXpathIndex2021}]/tbody/tr[{i}]/td")[0].InnerHtml);
                                                          //2021//*[@id="mw-content-text"]/div[1]/table[7]/tbody/tr[2]/th
                TeamName = doc.DocumentNode.SelectNodes($"//*[@id='mw-content-text']/div[1]/table[{5 + getXpathIndex2021}]/tbody/tr[{i}]/th")[0].InnerText;
                TeamName = TeamName.Replace("(C)", "").Replace("\n","").Trim();
                TotalGames = Convert.ToInt32(doc.DocumentNode.SelectNodes($"//*[@id='mw-content-text']/div[1]/table[{5 + getXpathIndex2021}]/tbody/tr[{i}]/td")[1].InnerHtml);
                WinGames = Convert.ToInt32(doc.DocumentNode.SelectNodes($"//*[@id='mw-content-text']/div[1]/table[{5 + getXpathIndex2021}]/tbody/tr[{i}]/td")[2].InnerHtml);
                TieGames = Convert.ToInt32(doc.DocumentNode.SelectNodes($"//*[@id='mw-content-text']/div[1]/table[{5 + getXpathIndex2021}]/tbody/tr[{i}]/td")[3].InnerHtml);
                LoseGames = Convert.ToInt32(doc.DocumentNode.SelectNodes($"//*[@id='mw-content-text']/div[1]/table[{5 + getXpathIndex2021}]/tbody/tr[{i}]/td")[4].InnerHtml);
                WinBall = Convert.ToInt32(doc.DocumentNode.SelectNodes($"//*[@id='mw-content-text']/div[1]/table[{5 + getXpathIndex2021}]/tbody/tr[{i}]/td")[5].InnerHtml);
                LoseBall = Convert.ToInt32(doc.DocumentNode.SelectNodes($"//*[@id='mw-content-text']/div[1]/table[{5 + getXpathIndex2021}]/tbody/tr[{i}]/td")[6].InnerHtml);
                SubtractBall = Convert.ToDecimal(doc.DocumentNode.SelectNodes($"//*[@id='mw-content-text']/div[1]/table[{5 + getXpathIndex2021}]/tbody/tr[{i}]/td")[7].InnerHtml);
                ListFootBallTeams.Add(new FootBallTeams { Years=Years, Level = Level, TeamName = TeamName, TotalGames = TotalGames, WinGames = WinGames, TieGames = TieGames, LoseGames = LoseGames, WinBall = WinBall, LoseBall = LoseBall, SubtractBall = SubtractBall });
            }
        }

    }
}
