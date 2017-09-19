using Dapper;
using DapperExtensions;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using YY.Edu.Sys.Comm.Helper;

namespace YY.Edu.Sys.Api.Controllers
{

    [Authorize]
    public class CoachWagesSubController : ApiController
    {

        private static readonly ILog logs = Comm.Helper.LogHelper.GetInstance();

        /// <summary>
        /// 查询工资详情
        /// </summary>
        /// <param name="venueID"></param>
        /// <param name="wagesID"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetClassOverTeachingSches(int venueID, int wagesID)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            try
            {

                var sql = @"SELECT [WagesSubID],c.[WagesID],c.[CurriculumID],[CoachPrice],c.[PKID],c.[AddTime] ,c2.CoachFullName,c3.StudentFullName
FROM [dbo].[CoachWages_Sub] as c join CoachWages as c2 on c.WagesID=c2.WagesID join Curriculum as c3 on c.CurriculumID=c3.CurriculumID
where c.[WagesID]=@wagesID and c2.VenueID=@venueID";

                var result = DapperHelper.Instance.Query<Models.ResponseModel.CoachWagesSubResponse>(sql, new
                {
                    wagesID = wagesID,
                    venueID = venueID
                });

                return Ok(new Comm.ResponseModel.ResponseModel4Page<Models.ResponseModel.CoachWagesSubResponse>()
                {
                    recordsFiltered = result.Count(),
                    recordsTotal = result.Count(),
                    data = result.ToList()
                });
            }
            catch (Exception ex)
            {
                logs.Error("查询工资详情异常", ex);
                return BadRequest();
            }
        }

    }
}
