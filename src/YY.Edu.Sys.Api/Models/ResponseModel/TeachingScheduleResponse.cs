﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YY.Edu.Sys.Models;

namespace YY.Edu.Sys.Api.Models.ResponseModel
{
    public class TeachingScheduleResponse : TeachingSchedule
    {
        public int CurriculumID { get; set; }
        /// <summary>
        /// 卖场名称
        /// </summary>
        public string VenueNam { get; set; }
        /// <summary>
        /// 校区名称
        /// </summary>
        public string CampusName { get; set; }
        /// <summary>
        ///课时状态
        /// </summary>
        public string KSstate { get; set; }
        /// <summary>
        /// 场馆名称
        /// </summary>
        public string VenueName { get; set; }
        /// <summary>
        /// 教练姓名
        /// </summary>
        public string CoachFullName { get; set; }
        /// <summary>
        /// 教练头像
        /// </summary>
        public string HeadUrl { get; set; }
        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentFullName { get; set; }


        /// <summary>
        /// 学生ID
        /// </summary>
        public string StudentID{ get; set; }

      
        /// <summary>
        /// 课时次数
        /// </summary>
        public int ClassNumber { get; set; }



        /// <summary>
        /// 总课时次数
        /// </summary>
        public int ZClassNumber { get; set; }

        /// <summary>
        /// 教案内容
        /// </summary>
        public string PlanContent { get; set; }

        /// <summary>
        /// 课程总结
        /// </summary>
        public string SContent { get; set; }
        /// <summary>
        /// 请假人数
        /// </summary>
        public int sqjcount { get; set; }

        /// <summary>
        /// 预约人数
        /// </summary>
        public int Sucount { get; set; }


        public string CampusLatitude { get; set; }
       public string CampusLongitude { get; set; }
        /// <summary>
        /// 排课开始日期
        /// </summary>

        public DateTime CurriculumBeginDate { get; set; }
        /// <summary>
        /// 排课结束日期
        /// </summary>
        public DateTime CurriculumEndDate { get; set; }
        /// <summary>
        /// 总课时
        /// </summary>
        public int TotalClassHour { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        public int StarLevel { get; set; }


        public int TSMID { get; set; }

        /// <summary>
        /// 距离
        /// </summary>
        public Double Distance { get; set; }

        public int CanTry { get; set; }
        /// <summary>
        /// 试课的状态
        /// </summary>
        public int TryoutState { get; set; }
    }
}