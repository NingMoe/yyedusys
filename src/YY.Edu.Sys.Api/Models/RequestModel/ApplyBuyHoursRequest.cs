﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YY.Edu.Sys.Api.Models.RequestModel
{
    public class ApplyBuyHoursRequest : Sys.Models.ApplyBuyHours
    {

        public string StartDate { get; set; }


        public string EndDate { get; set; }

    }
}