//using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Configuration;
using System.Linq;
using System.Threading;

namespace FootBall
{
   
    public partial class Form1 : Form
    {
        log log = new log();
        public const int LevelCount= 20;
        List<FootBallTeams> ListFootBallTeams = new List<FootBallTeams>();
        FootBallTeams footBallTeams2019 = new FootBallTeams();
        //HtmlWeb webClient = new HtmlWeb();
        ReadTxtFile readTxtFile = new ReadTxtFile();
        int endYear = 2021;
        public Form1()
        {
            InitializeComponent();
        }
      

        private void button1_Click(object sender, EventArgs e)
        {
            double AvgWinBall = default;
            double AvgLoseBall = default;
            int RealWinGameCount = default;
            double ClearAvg = default;
            double WinRate = default;
            //取得下拉選單值進行搜尋
            int? cbYearValue = null;
            string cbTeamValue = null;
            string cbTeam2Value = null;
            ReloadListView();
            if (cbYears.SelectedIndex!=0)
                cbYearValue =Convert.ToInt32(cbYears.SelectedItem);
            if (cbTeam.SelectedIndex != 0)
                cbTeamValue = cbTeam.SelectedItem.ToString();
            if (cbTeam.SelectedIndex != 0)
                cbTeam2Value = cbTeam2.SelectedItem.ToString();
            
            if (ListFootBallTeams.Count == 0)
            {
                MessageBox.Show("找不到資料請重新操作");
                return;
            }

            var query = (from lfb in ListFootBallTeams
                         where (cbYearValue==null || lfb.Years== cbYearValue) && ((cbTeamValue == null || lfb.TeamName == cbTeamValue)||(cbTeam2Value == null || lfb.TeamName == cbTeam2Value))
                         orderby lfb.TeamName, lfb.Years
                         select lfb).ToList();
           
            foreach (var q in query)
            {
                AvgWinBall = Math.Round(Convert.ToDouble(q.WinBall) /Convert.ToDouble(q.TotalGames),2);
                AvgLoseBall = Math.Round(Convert.ToDouble(q.LoseBall) / Convert.ToDouble(q.TotalGames), 2);
                RealWinGameCount = q.WinGames - (q.TieGames + q.LoseGames);
                ClearAvg = Math.Round(Convert.ToDouble(q.SubtractBall) / Convert.ToDouble(q.TotalGames),2);
                WinRate = Math.Round(Convert.ToDouble(q.WinGames) /Convert.ToDouble(q.TotalGames),2)*100;
                lvShow.Items.Add(new ListViewItem(new string[] { q.Years.ToString(), q.Level.ToString(), q.TeamName,q.TotalGames.ToString(),q.WinGames.ToString(),q.LoseGames.ToString(),q.TieGames.ToString(), RealWinGameCount.ToString(), q.WinBall.ToString(),q.LoseBall.ToString(),q.SubtractBall.ToString(),AvgWinBall.ToString(),AvgLoseBall.ToString(), ClearAvg.ToString(), WinRate.ToString()+"%"
                    , q.Years.ToString() }));
            }

            //抓出所有不重複隊伍
            var DistinctTeams = (from m in query
                                 orderby m.TeamName, m.Years
                                 select new
                                 {
                                     m.TeamName
                                 }
               ).Distinct().ToList();

            string TeamName = default;
            double AvgLv = default;
            int TeamTotalGames = default;
            
            int TeamWinGames = default;
            int TeamTieGames = default;
            int TeamLoseGames = default;
            int TeamWinBall = default;
            int TeamLoseBall = default;
            int TeamClerWin = default;
            double AvgTeamWinBall = default;
            double AvgTeamLoseBall = default;
            int RealTeamWinGameCount = default;
            double TeamClearAvg = default;
            double TeamWinRate = default;

            //把不重複隊伍進行groupby計算
            foreach (var d in DistinctTeams)
            {
                TeamName = d.TeamName;
                AvgLv = ListFootBallTeams.Where(x => x.TeamName == d.TeamName).Average(y => y.Level);
                AvgLv = Math.Round(AvgLv,2);
                TeamTotalGames= ListFootBallTeams.Where(x => x.TeamName == d.TeamName).Sum(y => y.TotalGames);
                TeamWinGames = ListFootBallTeams.Where(x => x.TeamName == d.TeamName).Sum(y => y.WinGames);
                TeamTieGames = ListFootBallTeams.Where(x => x.TeamName == d.TeamName).Sum(y => y.TieGames);
                TeamLoseGames = ListFootBallTeams.Where(x => x.TeamName == d.TeamName).Sum(y => y.LoseGames);
                TeamWinBall = ListFootBallTeams.Where(x => x.TeamName == d.TeamName).Sum(y => y.WinBall);
                TeamLoseBall = ListFootBallTeams.Where(x => x.TeamName == d.TeamName).Sum(y => y.LoseBall);
                TeamClerWin= ListFootBallTeams.Where(x => x.TeamName == d.TeamName).Sum(y =>Convert.ToInt32(y.SubtractBall));
                AvgTeamWinBall = Math.Round(Convert.ToDouble(TeamWinBall) / Convert.ToDouble(TeamTotalGames), 2);
                AvgTeamLoseBall = Math.Round(Convert.ToDouble(TeamLoseBall) / Convert.ToDouble(TeamTotalGames), 2);
                RealTeamWinGameCount = TeamWinGames - (TeamTieGames+ TeamLoseGames);
                TeamClearAvg =Math.Round(Convert.ToDouble(TeamClerWin) / Convert.ToDouble(TeamTotalGames),2);
                TeamWinRate= Math.Round(Convert.ToDouble(TeamWinGames) / Convert.ToDouble(TeamTotalGames), 2)*100;
                lvStatistics.Items.Add(new ListViewItem(new string[] { TeamName, AvgLv.ToString()
                    , TeamTotalGames.ToString(),TeamWinGames.ToString(),TeamLoseGames.ToString(),TeamTieGames.ToString(),RealTeamWinGameCount.ToString(),
                TeamWinBall.ToString(),TeamLoseBall.ToString(),TeamClerWin.ToString(),AvgTeamWinBall.ToString(),AvgTeamLoseBall.ToString(),TeamClearAvg.ToString(),TeamWinRate.ToString()+"%"}));
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ReloadListView();
            
            foreach (var item in Enum.GetValues(typeof(CountryEnum)))
            {
                cbCountry.Items.Add(item);
            }

        }
        //private void LoadUrl(int year,int CountryId)
        //{
        //   HtmlAgilityPack.HtmlDocument doc = default;
        //   ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //    int tableIndex = 0;
        //    switch (CountryId)
        //    {
        //        case 0:
        //            if(year==2019 || year == 2020)                   
        //                tableIndex = 5;                    
        //            else if(year==2021)
        //                tableIndex = 7;
        //            doc = webClient.Load(ConfigurationManager.AppSettings["PremierLeague"+ year]);
        //            break;
        //        case 1:
        //            if (year == 2019)
        //            tableIndex = 6;
        //            else if (year == 2020)
        //                tableIndex = 5;
        //            doc = webClient.Load(ConfigurationManager.AppSettings["Italian" + year]);
        //            break;
        //        case 2:
        //            if (year == 2019)
        //                tableIndex = 7;
        //            else if (year == 2020)
        //                tableIndex = 6;
        //            doc = webClient.Load(ConfigurationManager.AppSettings["Spain" + year]);
        //            break;
        //        case 3:
        //                tableIndex = 6;
        //            doc = webClient.Load(ConfigurationManager.AppSettings["Germany" + year]);
        //            break;
        //        case 4:
        //            if (year == 2019)
        //                tableIndex = 6;
        //            else if (year == 2020)
        //                tableIndex = 5;
        //            doc = webClient.Load(ConfigurationManager.AppSettings["France" + year]);
        //            break;
        //    }
        //    GetGameData(doc, year, tableIndex);
        //}

        
        //private void GetGameData(HtmlAgilityPack.HtmlDocument doc,int year,int tableIndex)
        //{

        //    int Years, Level, TotalGames, WinGames, LoseGames, TieGames, WinBall, LoseBall;
        //    string TeamName;
        //    string SubtractBall;
        //    int TeamCounts = doc.DocumentNode.SelectNodes($"//*[@id='mw-content-text']/div[1]/table[{tableIndex}]/tbody/tr").Count-1;
        //    //int getXpathIndex2021 =0;
        //    //取2021位置不同
        //    //if (year==2021)
        //    //    getXpathIndex2021 = 2;
        //    for (int i = 2; i < TeamCounts+2; i++)
        //    {
        //        Years = year;
        //        //*[@id="mw-content-text"]/div[1]/table[5]/tbody/tr[2]   
        //                                                               //*[@id="mw-content-text"]/div[1]/table[7]/tbody/tr[2]
        //        Level = Convert.ToInt32(doc.DocumentNode.SelectNodes($"//*[@id='mw-content-text']/div[1]/table[{tableIndex}]/tbody/tr[{i}]/td")[0].InnerHtml);
                                                                      
        //        TeamName = doc.DocumentNode.SelectNodes($"//*[@id='mw-content-text']/div[1]/table[{tableIndex}]/tbody/tr[{i}]/th")[0].InnerText;
        //        TeamName = TeamName.Replace("(C)", "").Replace("(R)", "").Replace("(O)", "").Replace("\n","").Trim().Replace("RB萊比錫", "RB莱比锡");
        //        TotalGames = Convert.ToInt32(doc.DocumentNode.SelectNodes($"//*[@id='mw-content-text']/div[1]/table[{tableIndex}]/tbody/tr[{i}]/td")[1].InnerHtml);
        //        WinGames = Convert.ToInt32(doc.DocumentNode.SelectNodes($"//*[@id='mw-content-text']/div[1]/table[{tableIndex}]/tbody/tr[{i}]/td")[2].InnerHtml);
        //        TieGames = Convert.ToInt32(doc.DocumentNode.SelectNodes($"//*[@id='mw-content-text']/div[1]/table[{tableIndex}]/tbody/tr[{i}]/td")[3].InnerHtml);
        //        LoseGames = Convert.ToInt32(doc.DocumentNode.SelectNodes($"//*[@id='mw-content-text']/div[1]/table[{tableIndex}]/tbody/tr[{i}]/td")[4].InnerHtml);
        //        WinBall = Convert.ToInt32(doc.DocumentNode.SelectNodes($"//*[@id='mw-content-text']/div[1]/table[{tableIndex}]/tbody/tr[{i}]/td")[5].InnerHtml);
        //        LoseBall = Convert.ToInt32(doc.DocumentNode.SelectNodes($"//*[@id='mw-content-text']/div[1]/table[{tableIndex}]/tbody/tr[{i}]/td")[6].InnerHtml);
        //        SubtractBall = (doc.DocumentNode.SelectNodes($"//*[@id='mw-content-text']/div[1]/table[{tableIndex}]/tbody/tr[{i}]/td")[7].InnerHtml).Replace("&#8722;","-").Trim();
        //        ListFootBallTeams.Add(new FootBallTeams { Years=Years, Level = Level, TeamName = TeamName, TotalGames = TotalGames, WinGames = WinGames, TieGames = TieGames, LoseGames = LoseGames, WinBall = WinBall, LoseBall = LoseBall, SubtractBall = SubtractBall });
        //    }
        //}
        private void ReloadListView()
        {

            lvShow.Clear();
            lvStatistics.Clear();

            lvShow.View = View.Details;
            lvShow.GridLines = true;
            lvShow.FullRowSelect = true;
            lvShow.Columns.Add("年份", 80);
            lvShow.Columns.Add("排名", 70);
            lvShow.Columns.Add("隊名", 120);
            lvShow.Columns.Add("總場", 70);
            lvShow.Columns.Add("贏局", 70);
            lvShow.Columns.Add("輸局", 70);
            lvShow.Columns.Add("和局", 70);
            lvShow.Columns.Add("勝-(和+負)", 110);
            lvShow.Columns.Add("進球", 70);
            lvShow.Columns.Add("失球", 70);
            lvShow.Columns.Add("淨勝", 70);
            lvShow.Columns.Add("平均得分", 120);
            lvShow.Columns.Add("平均失分", 120);
            lvShow.Columns.Add("平均淨勝", 100);
            lvShow.Columns.Add("勝率", 70);
            lvShow.Columns.Add("年份", 80);

            lvStatistics.View = View.Details;
            lvStatistics.GridLines = true;
            lvStatistics.FullRowSelect = true;
            lvStatistics.Columns.Add("隊名", 120);
            lvStatistics.Columns.Add("平均名次", 80);
            lvStatistics.Columns.Add("總場", 50);
            lvStatistics.Columns.Add("贏局", 50);
            lvStatistics.Columns.Add("輸局", 50);
            lvStatistics.Columns.Add("和局", 50);
            lvStatistics.Columns.Add("勝-(和+負)", 90);
            lvStatistics.Columns.Add("進球", 50);
            lvStatistics.Columns.Add("失球", 50);
            lvStatistics.Columns.Add("淨勝", 50);
            lvStatistics.Columns.Add("平均得分", 100);
            lvStatistics.Columns.Add("平均失分", 100);
            lvStatistics.Columns.Add("平均淨勝", 100);
            lvStatistics.Columns.Add("勝率", 50);
        }

        private void cbCountry_SelectedIndexChanged(object sender, EventArgs e)
        {

            cbYears.Items.Clear();
            cbTeam.Items.Clear();
            cbTeam2.Items.Clear();
            ListFootBallTeams.Clear();
            ReloadListView();

            log.WriteLog("匯入資料開始...");
            string Country = ((CountryEnum)cbCountry.SelectedIndex).ToString();
            //txt匯入資料
            ListFootBallTeams = readTxtFile.ReadFile(Country);
            log.WriteLog("匯入資料完成...");
            if (ListFootBallTeams.Count != 0)
            {
                //int CountryId = cbCountry.SelectedIndex;
                //if (cbCountry.SelectedIndex == 0)
                //    endYear = 2022;
                //else
                //    endYear = 2021;

                //for (int beginYear = 2019; beginYear < endYear; beginYear++)
                //    LoadUrl(beginYear, CountryId);

                var DistinctYears = (from m in ListFootBallTeams
                                     orderby m.Years
                                     select new
                                     {
                                         m.Years
                                     }
               ).Distinct().ToList();

                var DistinctTeams = (from m in ListFootBallTeams
                                     orderby m.TeamName, m.Years
                                     select new
                                     {
                                         m.TeamName
                                     }
                  ).Distinct().ToList();

                cbYears.Items.Add("全部");
                cbTeam.Items.Add("全部");
                cbTeam2.Items.Add("無");
                foreach (var y in DistinctYears)
                    cbYears.Items.Add(y.Years);
                foreach (var t in DistinctTeams)
                {
                    cbTeam.Items.Add(t.TeamName);
                    cbTeam2.Items.Add(t.TeamName);
                }
                cbYears.SelectedIndex = 0;
                cbTeam.SelectedIndex = 0;
                cbTeam2.SelectedIndex = 0;
                lbSelect.Text = "";
            }
            else
            {
                log.WriteLog("匯入資料失敗");
                MessageBox.Show("匯入失敗，請按更新後在重新選擇國家");
            }
        }

        private void lbSelect_Click(object sender, EventArgs e)
        {

        }

        private void lvShow_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            log.WriteLog("爬蟲開始...");
            try
            {
                SeleniumChrome seleniumChrome = new SeleniumChrome();
                seleniumChrome.LoadData();
                log.WriteLog("爬蟲完成!");
                MessageBox.Show("更新完成");
            }
            catch(Exception ex)
            {
                log.WriteLog("爬蟲失敗，原因 : "+ex.Message);
                MessageBox.Show("更新失敗，原因 : " + ex.Message);
            }
        }

        private void btnExcel_Click(object sender, EventArgs e)
        {
            ExportExcel exportExcel = new ExportExcel();
            log.WriteLog("匯出Excel");
            try
            {
                exportExcel.ExportDataExcel();
                log.WriteLog("匯出Excel成功");
                MessageBox.Show("匯出Excel成功，位置 : "+ System.Windows.Forms.Application.StartupPath);
            }
            catch(Exception ex)
            {
                log.WriteLog("匯出Excel失敗，原因 : "+ex.Message);
            }
        }
    }
}
