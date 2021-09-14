using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.IO;

namespace FootBall
{
    
    class ExportExcel
    {
        string path = Path.Combine(System.Windows.Forms.Application.StartupPath);
        IWorkbook workbook = new HSSFWorkbook();
        WriteFile writeFile = new WriteFile();
        log log = new log();
        public void ExportDataExcel()
        {
            string xlsPath = Path.Combine(path,"足球分析.xls");
            foreach (string name in Enum.GetNames(typeof(CountryEnum)))
            {
                if (IsExistTxtFile(name))
                {
                    ReadTxtFile readTxtFile = new ReadTxtFile();
                    List<FootBallTeams> ListFootBallTeams = new List<FootBallTeams>();
                    ListFootBallTeams = readTxtFile.ReadFile(name);
                    //新增分頁
                    ISheet sheet = workbook.CreateSheet(name);
                    IRow row0 = sheet.CreateRow(0);
                    sheet.CreateFreezePane(0, 1, 0, 1);
                    ICellStyle headerStyle = workbook.CreateCellStyle();
                    IFont headerfont = workbook.CreateFont();
                    headerStyle.Alignment = HorizontalAlignment.Center; //水平置中
                    headerStyle.VerticalAlignment = VerticalAlignment.Center; //垂直置中
                    headerfont.FontName = "微軟正黑體";
                    headerfont.FontHeightInPoints = 12;
                    headerfont.Boldweight = (short)FontBoldWeight.Bold;//粗體
                    headerStyle.SetFont(headerfont);
                    row0.CreateCell(0).SetCellValue("年份");
                    row0.CreateCell(1).SetCellValue("排名");
                    row0.CreateCell(2).SetCellValue("隊名");
                    row0.CreateCell(3).SetCellValue("總場");
                    row0.CreateCell(4).SetCellValue("贏局");
                    row0.CreateCell(5).SetCellValue("輸局");
                    row0.CreateCell(6).SetCellValue("和局");
                    row0.CreateCell(7).SetCellValue("勝-(和+負)");
                    row0.CreateCell(8).SetCellValue("進球");
                    row0.CreateCell(9).SetCellValue("失球");
                    row0.CreateCell(10).SetCellValue("淨勝");
                    row0.CreateCell(11).SetCellValue("平均得分");
                    row0.CreateCell(12).SetCellValue("平均失分");
                    row0.CreateCell(13).SetCellValue("平均淨勝");
                    row0.CreateCell(14).SetCellValue("勝率");
                    for (int j = 0; j < row0.Cells.Count; j++)
                        sheet.GetRow(0).GetCell(j).CellStyle = headerStyle;
                    for (int r = 1; r < ListFootBallTeams.Count; r++)
                    {                       
                        double AvgWinBall = default;
                        double AvgLoseBall = default;
                        int RealWinGameCount = default;
                        double ClearAvg = default;
                        double WinRate = default;
                        AvgWinBall = Math.Round(Convert.ToDouble(ListFootBallTeams[r].WinBall) / Convert.ToDouble(ListFootBallTeams[r].TotalGames), 2);
                        AvgLoseBall = Math.Round(Convert.ToDouble(ListFootBallTeams[r].LoseBall) / Convert.ToDouble(ListFootBallTeams[r].TotalGames), 2);
                        RealWinGameCount = ListFootBallTeams[r].WinGames - (ListFootBallTeams[r].TieGames + ListFootBallTeams[r].LoseGames);
                        ClearAvg = Math.Round(Convert.ToDouble(ListFootBallTeams[r].SubtractBall) / Convert.ToDouble(ListFootBallTeams[r].TotalGames), 2);
                        WinRate = Math.Round(Convert.ToDouble(ListFootBallTeams[r].WinGames) / Convert.ToDouble(ListFootBallTeams[r].TotalGames), 2) * 100;
                        IRow row = sheet.CreateRow(r);
                        row.CreateCell(0).SetCellValue(ListFootBallTeams[r].Years);
                        row.CreateCell(1).SetCellValue(ListFootBallTeams[r].Level);
                        row.CreateCell(2).SetCellValue(ListFootBallTeams[r].TeamName);
                        row.CreateCell(3).SetCellValue(ListFootBallTeams[r].TotalGames);
                        row.CreateCell(4).SetCellValue(ListFootBallTeams[r].WinGames);           
                        row.CreateCell(5).SetCellValue(ListFootBallTeams[r].LoseGames);
                        row.CreateCell(6).SetCellValue(ListFootBallTeams[r].TieGames);
                        row.CreateCell(7).SetCellValue(RealWinGameCount);
                        row.CreateCell(8).SetCellValue(ListFootBallTeams[r].WinBall);
                        row.CreateCell(9).SetCellValue(ListFootBallTeams[r].LoseBall);
                        row.CreateCell(10).SetCellValue(ListFootBallTeams[r].SubtractBall);
                        row.CreateCell(11).SetCellValue(AvgWinBall);
                        row.CreateCell(12).SetCellValue(AvgLoseBall);
                        row.CreateCell(13).SetCellValue(ClearAvg);
                        row.CreateCell(14).SetCellValue(WinRate);
                    }             
                }
                else
                {
                    log.WriteLog("IsExistTxtFile : "+ name+".txt不存在");
                }
            }
            //整批寫入
            using (FileStream url = File.OpenWrite(xlsPath))
            {
                workbook.Write(url);
            };
        }
        public bool IsExistTxtFile(string countryEnum)
        {            
            string DataPath = Path.Combine(path, countryEnum.ToString() + ".txt");
            if (File.Exists(DataPath))
            {
                return true;
            }
            else
            {
                return false;
            }
            }
        }     
}
