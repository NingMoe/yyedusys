using System;
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
        /// 学生姓名
        /// </summary>
        public string StudentFullName { get; set; }

        /// <summary>
        /// 课时次数
        /// </summary>
        public int ClassNumber { get; set; }

        
    }
}