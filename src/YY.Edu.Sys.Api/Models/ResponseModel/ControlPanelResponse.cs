using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YY.Edu.Sys.Api.Models.ResponseModel
{
    public class ControlPanelResponse
    {

        /// <summary>
        /// 今日等待上课学生数
        /// </summary>
        public int TodayWaitForClassStudentCount { get; set; }

        /// <summary>
        /// 今日约课人数
        /// </summary>
        public int TodayBookClassStudentCount { get; set; }

        /// <summary>
        /// 今日收入
        /// </summary>
        public decimal TodayIncomeMoney { get; set; }

        /// <summary>
        /// 场馆学生数
        /// </summary>
        public int VenueSumStudentCount { get; set; }

    }
}