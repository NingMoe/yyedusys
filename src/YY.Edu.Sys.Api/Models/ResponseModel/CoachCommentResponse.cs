using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YY.Edu.Sys.Models;

namespace YY.Edu.Sys.Api.Models.ResponseModel
{
    public class CoachCommentResponse: CoachComment
    {
        /// <summary>
        /// 学生姓名
        /// </summary>
        public string StudentFullName { get; set; }
    }
}