using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace YY.Edu.Sys.Api.Controllers
{
    public class ControlPanelController : ApiController
    {
        // GET: api/ControlPanel
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        // GET: api/ControlPanel/5
        public IHttpActionResult Get(int venueID)
        {

            //当日上课数
            var sql = "select COUNT(PKID) from TeachingSchedule where State=1 and VenueID=@venueID and CurriculumDate=@curriculumDate";
            var result = Comm.Helper.DapperHelper.Instance.Query<int>(sql, new
            {
                venueID = venueID,
                curriculumDate = DateTime.Now.Date
            });

            //本月新增学生数


            //本月新增教练数


            return Ok(new Comm.ResponseModel.ResponseModel4Res<Models.ResponseModel.ControlPanelResponse>()
            {
                Info = new Models.ResponseModel.ControlPanelResponse()
                {
                    TodayWaitForClassStudentCount = result.FirstOrDefault()
                }
            });
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
