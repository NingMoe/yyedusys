using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using YY.Edu.Sys.Api.Models.ResponseModel;
using YY.Edu.Sys.Comm.Helper;

namespace YY.Edu.Sys.Api.Controllers
{
    //[Authorize]
    [RoutePrefix("api/WxNotice")]
    public class WxNoticeController : BaseController
    {

        private static readonly ILog logs = Comm.Helper.LogHelper.GetInstance();

        /// <summary>
        /// 学生待上课程查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult WaitClass4Student()
        {

            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                Comm.RequestModel.RequestModelBase<Sys.Models.Student> oData = Newtonsoft.Json.JsonConvert.DeserializeObject<Comm.RequestModel.RequestModelBase<Sys.Models.Student>>(query);

                if (oData.SearchCondition.StudentID <= 0 || oData.PageIndex < 0 || oData.PageSize <= 0)
                    return BadRequest();

                PageCriteria criteria = new PageCriteria();

                criteria.Condition += string.Format(" and t.CurriculumDate= '{0}'", DateTime.Now.ToString("yyyy-MM-dd"));
                criteria.Condition += " and cu.State =0 ";
                criteria.Condition += string.Format(" and t.CurriculumBeginTime>= '{0}'", DateTime.Now.AddHours(3).ToString("HH:mm"));

                criteria.CurrentPage = oData.PageIndex;
                criteria.Fields = "t.*,c.CampusName,s.OpenID ";
                criteria.PageSize = oData.PageSize;
                criteria.TableName = "TeachingSchedule t with(nolock) inner join Venue v with(nolock) on t.VenueID = v.VenueID left join Campus c with(nolock) on t.CampusID = c.CampusID inner join Curriculum cu with(nolock) on t.PKID = cu.PKID inner join Student as s on cu.StudentID=s.StudentID ";
                criteria.PrimaryKey = "t.PKID";
                criteria.Sort = "CurriculumDate,CurriculumBeginTime,t.PKID";

                var r = Comm.Helper.DapperHelper.GetPageData<TeachingScheduleResponse>(criteria);

                return Ok(new Comm.ResponseModel.ResponseModel4Page<TeachingScheduleResponse>()
                {
                    data = r.Items,
                    recordsFiltered = r.TotalNum,
                    recordsTotal = r.TotalNum,
                });

            }
            catch (Exception ex)
            {
                logs.Error("学生待上课程查询失败", ex);
                return BadRequest();
            }

        }

        /// <summary>
        /// 老师待上课程查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult WaitClass4Coach()
        {

            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                Comm.RequestModel.RequestModelBase<Sys.Models.Student> oData = Newtonsoft.Json.JsonConvert.DeserializeObject<Comm.RequestModel.RequestModelBase<Sys.Models.Student>>(query);

                if (oData.SearchCondition.StudentID <= 0 || oData.PageIndex < 0 || oData.PageSize <= 0)
                    return BadRequest();

                PageCriteria criteria = new PageCriteria();

                criteria.Condition += string.Format(" and t.CurriculumDate= '{0}'", DateTime.Now.ToString("yyyy-MM-dd"));
                criteria.Condition += " and cu.State =0 ";
                criteria.Condition += string.Format(" and t.CurriculumBeginTime>= '{0}'", DateTime.Now.AddHours(3).ToString("HH:mm"));

                criteria.CurrentPage = oData.PageIndex;
                criteria.Fields = "t.*,c.CampusName,ca.OpenID ";
                criteria.PageSize = oData.PageSize;
                criteria.TableName = "TeachingSchedule t with(nolock) inner join Venue v with(nolock) on t.VenueID = v.VenueID left join Campus c with(nolock) on t.CampusID = c.CampusID inner join Coach as ca on t.CoachID=ca.CoachID ";
                criteria.PrimaryKey = "t.PKID";
                criteria.Sort = "CurriculumDate,CurriculumBeginTime,t.PKID";

                var r = Comm.Helper.DapperHelper.GetPageData<TeachingScheduleResponse>(criteria);

                return Ok(new Comm.ResponseModel.ResponseModel4Page<TeachingScheduleResponse>()
                {
                    data = r.Items,
                    recordsFiltered = r.TotalNum,
                    recordsTotal = r.TotalNum,
                });

            }
            catch (Exception ex)
            {
                logs.Error("老师待上课程查询失败", ex);
                return BadRequest();
            }

        }



    }
}
