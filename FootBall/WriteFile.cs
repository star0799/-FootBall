using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootBall
{
    class WriteFile
    {
        public bool IsExistData(string countryEnum, int year)
        {
            bool result = true;
            string line = "";
            string path = Path.Combine(System.Windows.Forms.Application.StartupPath, countryEnum.ToString(), ".txt");
            if (File.Exists(path))
            {
                    StreamReader sr = new StreamReader(path);
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
            }
            else
            {
                result = false;
            }
           
            return result;
        }
        public void WriteData(string countryEnum, int year,List<FootBallTeams> data)
        {
            string path = Path.Combine(System.Windows.Forms.Application.StartupPath, countryEnum.ToString());
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            using (StreamWriter sw = new StreamWriter(Path.Combine(path, ".txt"), true))
            {             
                sw.WriteLine(FormatDataFile(data).ToList());
            }
        }
        public void UpdateData(string countryEnum, int year, List<FootBallTeams> data)
        {
            string path = Path.Combine(System.Windows.Forms.Application.StartupPath, countryEnum.ToString(), ".txt");

            if (!File.Exists(path))
            {
                StreamWriter sw = new StreamWriter(path, true);
            }
            List<string> lines = new List<string>(File.ReadAllLines(path));
            //先刪除
            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].Contains("2021"))
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
            File.WriteAllLines(path, lines.ToArray());

        }
        public List<string> FormatDataFile(List<FootBallTeams> data)
        {
            List<string> result = new List<string>();
            foreach (var d in data)
            {
                result.Add(d.Years.ToString()+"," + d.TotalGames.ToString()+","+d.WinGames.ToString()+","+d.TieGames.ToString()+"," + d.LoseGames.ToString() + "," + d.WinBall.ToString() + "," + d.LoseBall.ToString() + "," + d.SubtractBall.ToString());
            }
            return result;
        }
    }
}
