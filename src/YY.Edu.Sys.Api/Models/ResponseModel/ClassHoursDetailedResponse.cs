using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YY.Edu.Sys.Models;

namespace YY.Edu.Sys.Api.Models.ResponseModel
{
    public class ClassHoursDetailedResponse : ClassHoursDetailed
    {
        //场馆名字
        public string VenueName { get; set; }
        /// <summary>
        /// 教练名字
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// 学生名字
        /// </summary>
        public string StudentFullName { get; set; }
    }
}