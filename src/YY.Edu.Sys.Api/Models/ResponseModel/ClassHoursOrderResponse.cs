using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YY.Edu.Sys.Models;

namespace YY.Edu.Sys.Api.Models.ResponseModel
{
    public class ClassHoursOrderResponse:ClassHoursOrder
    {
        public string CoachFullName { get; set; }
    }
}