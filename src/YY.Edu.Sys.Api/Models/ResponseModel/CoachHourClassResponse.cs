using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YY.Edu.Sys.Models;

namespace YY.Edu.Sys.Api.Models.ResponseModel
{
    public class CoachHourClassResponse:Coach
    {
        /// <summary>
        /// 课时数
        /// </summary>
        public int hNumber { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Price { get; set; }
    }
}