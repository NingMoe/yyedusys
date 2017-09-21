using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YY.Edu.Sys.Api.Services
{
    public class ClassHoursNumberService
    {
        private static readonly log4net.ILog logs = Comm.Helper.LogHelper.GetInstance();


        public static bool IsExist(int venueID, int studentID, int coachID)
        {

            if (venueID <= 0 || studentID <= 0 || coachID <= 0)
                throw new Comm.YYException.YYException("参数不能为空");

            var sql = "select count(CHNID) from ClassHoursNumber where StudentID=@studentID and CoachID=@coachID and VenueID=@venueID";

            var count = Comm.Helper.DapperHelper.Instance.Query<int>(sql, new
            {
                studentID = studentID,
                coachID = coachID,
                venueID = venueID,
            });

            return (count.FirstOrDefault() > 0);

        }

    }
}