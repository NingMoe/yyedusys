using Dapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace YY.Edu.Sys.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/ControlPanel")]
    public class ControlPanelController : ApiController
    {

        private static readonly ILog logs = Comm.Helper.LogHelper.GetInstance();

        // GET: api/ControlPanel
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        // GET: api/ControlPanel/5
        public IHttpActionResult Get(int venueID)
        {

            try
            {
                //今日等待上课人数
                var sql = @"select COUNT(PKID) from TeachingSchedule 
                where State=1 and VenueID=@venueID and CurriculumDate=@curriculumDate";
                var result = Comm.Helper.DapperHelper.Instance.Query<int>(sql, new
                {
                    venueID = venueID,
                    curriculumDate = DateTime.Now.Date
                });

                //今日约课人数
                var curriculmSql = @"SELECT COUNT([CurriculumID])  FROM [dbo].[Curriculum] 
                where VenueID=@venueID and AddTime>=@startDay and AddTime<@endDay ";
                var curriculmResult = Comm.Helper.DapperHelper.Instance.Query<int>(curriculmSql, new
                {
                    VenueID = venueID,
                    startDay = DateTime.Now.Date,
                    endDay = DateTime.Now.Date.AddDays(1).Date,
                });

                //今日收入(没有区分课程类型)
                var classHoursDetailedSql = @"SELECT COALESCE(SUM([CMoney]),0) FROM [dbo].[ClassHoursDetailed] 
                where VenueID=@venueID and AddTime>=@startDay and AddTime<@endDay ";
                var classHoursDetailedResult = Comm.Helper.DapperHelper.Instance.Query<int>(classHoursDetailedSql, new
                {
                    VenueID = venueID,
                    startDay = DateTime.Now.Date,
                    endDay = DateTime.Now.Date.AddDays(1).Date,
                });

                //场馆学生数
                var studentSql = @"SELECT COUNT([SVID]) FROM [dbo].[Student_Venue] where VenueID=@venueID ";
                var studentResult = Comm.Helper.DapperHelper.Instance.Query<int>(studentSql, new { VenueID = venueID });

                return Ok(new Comm.ResponseModel.ResponseModel4Res<Models.ResponseModel.ControlPanelResponse>()
                {
                    Info = new Models.ResponseModel.ControlPanelResponse()
                    {
                        TodayWaitForClassStudentCount = result.FirstOrDefault(),
                        TodayBookClassStudentCount = curriculmResult.FirstOrDefault(),
                        VenueSumStudentCount = studentResult.FirstOrDefault(),
                        TodayIncomeMoney = classHoursDetailedResult.FirstOrDefault(),
                    }
                });
            }
            catch (Exception ex)
            {
                logs.Error("控制面板信息获取失败", ex);
                return BadRequest();
            }

        }

        // POST: api/ControlPanel
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ControlPanel/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ControlPanel/5
        public void Delete(int id)
        {
        }
    }
}
