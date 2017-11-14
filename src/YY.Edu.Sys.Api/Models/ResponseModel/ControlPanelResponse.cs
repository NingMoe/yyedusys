﻿using System;
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

        /// <summary>
        /// 场馆老师总量
        /// </summary>
        public int VenueSumCoachCount { get; set; }

        /// <summary>
        /// 本月新增学生
        /// </summary>
        public int ThisMonthNewStudentCount { get; set; }

        /// <summary>
        /// 本月流失学生
        /// </summary>
        public int ThisMonthLossStudentCount { get; set; }

        /// <summary>
        /// 本月新增收入
        /// </summary>
        public decimal ThisMonthNewInMoney { get; set; }

        /// <summary>
        /// 本月应付工资
        /// </summary>
        public decimal ThisMonthNewOutMoney { get; set; }
        //历史收入、历史支出；

        /// <summary>
        /// 今日结课
        /// </summary>
        public int TodayClassOverStudentCount { get; set; }

    }
}