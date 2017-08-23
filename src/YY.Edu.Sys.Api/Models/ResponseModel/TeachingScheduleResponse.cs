using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YY.Edu.Sys.Models;

namespace YY.Edu.Sys.Api.Models.Response
{
    public class TeachingScheduleResponse : TeachingSchedule
    {
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
    }
}