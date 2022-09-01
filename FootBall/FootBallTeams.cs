using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FootBall
{
    class FootBallTeams
    {
        /// <summary>
        /// 年份
        /// </summary>
        public int Years { get; set; }
        /// <summary>
        /// 排名
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// 隊伍名稱
        /// </summary>
        public string TeamName { get; set; }
        /// <summary>
        /// 總比賽場次
        /// </summary>       
        public int TotalGames { get; set; }
        /// <summary>
        /// 贏場次
        /// </summary>
        public int WinGames { get; set; }
        /// <summary>
        /// 輸場次
        /// </summary>
        public int LoseGames { get; set; }
        /// <summary>
        /// 和局場次
        /// </summary>
        public int TieGames { get; set; }
        /// <summary>
        /// 進球數
        /// </summary>
        public int WinBall { get; set; }
        /// <summary>
        /// 失球數
        /// </summary>
        public int LoseBall { get; set; }
        /// <summary>
        /// 淨勝(進球數-失球數)
        /// </summary>
        public string SubtractBall { get; set; }
    }
}
