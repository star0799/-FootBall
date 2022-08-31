using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootBall
{
    class WriteFile
    {
        string path = Path.Combine(System.Windows.Forms.Application.StartupPath);
        //判斷檔案和檔案內容是否存在
        public bool IsExistData(string countryEnum, int year)
        {
            bool result = true;
            string line = "";
            string DataPath = Path.Combine(path, countryEnum.ToString()+ ".txt");
            if (File.Exists(DataPath))
            {
                    StreamReader sr = new StreamReader(DataPath);
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Contains(year.ToString()))
                        {
                            result = true;
                           
                            break;
                        }
                        else
                            result = false;
                    }
                sr.Close();
            }
            else
            {
                result = false;
            }
           
            return result;
        }
        //前兩年專用寫入檔案完就不會在使用
        public void WriteData(string countryEnum, int year,List<FootBallTeams> data)
        {
            data = FilterYears(data);
            using (StreamWriter sw = new StreamWriter(Path.Combine(path, countryEnum + ".txt"), true))
            {             
                foreach(var d in FormatDataFile(data))
                sw.WriteLine(d);
                sw.Close();
            }
        }
        //今年專用不管檔案存不存在都會去寫入
        public void UpdateData(string countryEnum, int year, List<FootBallTeams> data)
        {
            //取得目前年份
            string NowYear = DateTime.Now.Year.ToString();
            string DataPath = Path.Combine(path, countryEnum.ToString()+".txt");
            data = FilterYears(data);
            if (!File.Exists(DataPath))
            {
                StreamWriter sw = new StreamWriter(DataPath, true);
            }
            List<string> lines = new List<string>(File.ReadAllLines(DataPath));
            
            //先刪除
            for (int i = 0; i < lines.Count; i++)
            {
                //判斷是今年先把檔資料刪除
                if (lines[i].Contains(NowYear))
                {
                    lines.RemoveAt(i);                   
                    i--;
                }
            }
            //在新增
            foreach (var d in FormatDataFile(data))
            {
                lines.Add(d);
            }
            //寫入檔案
            File.WriteAllLines(DataPath, lines.ToArray());
            
        }
        public List<string> FormatDataFile(List<FootBallTeams> data)
        {
            //把隊伍結果串起來存入txt
            List<string> result = new List<string>();
            foreach (var d in data)
            {
                result.Add(d.Years.ToString()+","+d.Level.ToString()+","+d.TeamName.ToString()+"," + d.TotalGames.ToString()+","+d.WinGames.ToString()+","+d.TieGames.ToString()+"," + d.LoseGames.ToString() + "," + d.WinBall.ToString() + "," + d.LoseBall.ToString() + "," + d.SubtractBall.ToString());
            }
            return result;
        }
        //移除多餘的年份
        public List<FootBallTeams> FilterYears(List<FootBallTeams> data)
        {           
            int DataYeas = Convert.ToInt16(ConfigurationManager.AppSettings["DataYeas"]);
            int MaxYears = data.Select(x => x.Years).Distinct().Count();
            if (MaxYears > DataYeas)
            {
                int deleteYears = data.Select(x => x.Years).Min();
                data.RemoveAll(x => x.Years == deleteYears);
            }
            return data;
        }
    }
}
