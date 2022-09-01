using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootBall
{
    class ReadTxtFile
    {
        string path = Path.Combine(System.Windows.Forms.Application.StartupPath);
        log log = new log();
        //從txt讀檔轉成list
        public List<FootBallTeams> ReadFile(string countryEnum)
        {
            List<FootBallTeams> ListFootBallTeams = new List<FootBallTeams>();
            string TxtPath = Path.Combine(path, countryEnum.ToString() + ".txt");
            string line = "";
            int takeCount = Convert.ToInt16(ConfigurationManager.AppSettings["DataYeasCount"].ToString() ?? "3");
            string[] Data = default;
            try
            {
                if (File.Exists(TxtPath))
                {
                    using (StreamReader sr = new StreamReader(TxtPath))
                    {
                        while ((line = sr.ReadLine()) != null)
                        {
                            Data = line.Split(',');
                            ListFootBallTeams.Add(new FootBallTeams { Years = Convert.ToInt16(Data[0]), Level = Convert.ToInt16(Data[1]), TeamName = Data[2], TotalGames = Convert.ToInt16(Data[3]), WinGames = Convert.ToInt16(Data[4]), TieGames = Convert.ToInt16(Data[5]), LoseGames = Convert.ToInt16(Data[6]), WinBall = Convert.ToInt16(Data[7]), LoseBall = Convert.ToInt16(Data[8]), SubtractBall = Data[9] });
                        }
                    }
                }
                else
                {
                    log.WriteLog("找不到" + TxtPath);
                    return ListFootBallTeams;
                }
            }
            catch (Exception ex)
            {
                log.WriteLog("ReadFileToList錯誤:" + ex.Message);
            }
            //取出所有資料從最近的年份
            List<int> yearsFilter = ListFootBallTeams.OrderByDescending(x => x.Years).Select(x => x.Years).Distinct().Take(takeCount).ToList();
            ListFootBallTeams = ListFootBallTeams.Where(r => yearsFilter.Contains(r.Years)).ToList();
            return ListFootBallTeams;
        }
    }
}
