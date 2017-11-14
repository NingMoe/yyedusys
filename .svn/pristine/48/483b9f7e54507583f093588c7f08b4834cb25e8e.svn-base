using DapperExtensions;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YY.Edu.Sys.Api.Services
{
    public class VenueNoticeService
    {
        private static readonly ILog logs = Comm.Helper.LogHelper.GetInstance();

        public static bool Create(Sys.Models.VenueNotice venueNoticeInfo)
        {

            try
            {
                venueNoticeInfo.State = 1;
                var result = Comm.Helper.DapperHelper.Instance.Insert(venueNoticeInfo);
                return result > 0;
            }
            catch (Exception ex)
            {
                logs.Error("通知添加失败", ex);
                return false;
            }

        }
    }
}