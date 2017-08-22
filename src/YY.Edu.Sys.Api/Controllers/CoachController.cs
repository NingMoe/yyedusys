using Dapper;
using DapperExtensions;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using YY.Edu.Sys.Comm.Helper;


namespace YY.Edu.Sys.Api.Controllers
{
    [RoutePrefix("api/Coach")]
    public class CoachController : ApiController
    {
        private static readonly ILog logs = Comm.Helper.LogHelper.GetInstance();

        // GET: api/Student
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        #region 基础信息
        [HttpGet]
        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            //查询
            var query = Comm.Helper.DapperHelper.Instance.Query<YY.Edu.Sys.Models.Coach>("select * from Coach where CoachID=@id", new { Id = 1 });
            return Ok(query);
            //return Json(query.ToString());
        }


        [HttpPost]
        public IHttpActionResult Create(YY.Edu.Sys.Models.Coach coach)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            try
            {

                var result = Comm.Helper.DapperHelper.Instance.Insert(coach);

                if (result > 0)
                {
                    return Ok(Comm.ResponseModel.ResponseModelBase.Success());
                }
                else
                {
                    return Content(HttpStatusCode.OK, Comm.ResponseModel.ResponseModelBase.GetRes(Comm.ResponseModel.ResponseModelErrorEnum.SystemError));
                }

            }
            catch (Exception ex)
            {
                logs.Error("教练信息添加失败", ex);
                return BadRequest();
            }
        }


        [HttpPost]
        public IHttpActionResult Edit(YY.Edu.Sys.Models.Coach coach)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                //Models.VenueInfo venueInfo = Comm.Helper.DapperHelper.Instance.Get<Models.VenueInfo>(cityId);

                var result = Comm.Helper.DapperHelper.Instance.Update(coach);

                if (result)
                {
                    return Ok(Comm.ResponseModel.ResponseModelBase.Success());
                }
                else
                {
                    return Content(HttpStatusCode.OK, Comm.ResponseModel.ResponseModelBase.GetRes(Comm.ResponseModel.ResponseModelErrorEnum.SystemError));
                }
            }
            catch (Exception ex)
            {
                logs.Error("场馆信息编辑失败", ex);
                return BadRequest();
            }

        }



        #endregion


        #region //教师风采

        [HttpGet]
        // GET api/<controller>
        public IHttpActionResult GetCoachingPresence()
        {
            try
            {

                if (!ModelState.IsValid)
                    return BadRequest();

                System.Web.HttpContextBase context = (System.Web.HttpContextBase)Request.Properties["MS_HttpContext"];//获取传统context
                System.Web.HttpRequestBase request = context.Request;//定义传统request对象
                string UserName = Comm.Helper.ParamHelper<string>.GetParam(request["UserName"], "");
                string ParentMobile = Comm.Helper.ParamHelper<string>.GetParam(request["ParentMobile"], "");
                string FullName = Comm.Helper.ParamHelper<string>.GetParam(request["FullName"], "");
                string ParentFullName = Comm.Helper.ParamHelper<string>.GetParam(request["ParentFullName"], "");
                int start = Comm.Helper.ParamHelper<int>.GetParam(request["start"], 0);
                start += 1;//adminlte 加载的datatable起始页为0
                int length = Comm.Helper.ParamHelper<int>.GetParam(request["length"], 0);
                int venueId = Comm.Helper.ParamHelper<int>.GetParam(request["venueId"], 0);

                if (venueId <= 0 || start < 0 || length <= 0)
                    return BadRequest();

                PageCriteria criteria = new PageCriteria();
                criteria.Condition = "1=1";

                criteria.Condition += string.Format(" and VenueID = {0} and FCState=1 ", venueId);

               

                criteria.CurrentPage = start;
                criteria.Fields = "*";
                criteria.PageSize = length;
                criteria.TableName = "CoachingPresence";
                criteria.PrimaryKey = "FCID";

                var r = Comm.Helper.DapperHelper.GetPageData<YY.Edu.Sys.Models.CoachingPresence>(criteria);

                return Ok(new Comm.ResponseModel.ResponseModel4Page<YY.Edu.Sys.Models.CoachingPresence>()
                {
                    data = r.Items,
                    recordsFiltered = r.TotalNum,
                    recordsTotal = r.TotalNum,
                });

            }
            catch (Exception ex)
            {
                logs.Error("教师风采", ex);
                return BadRequest();
            }
        }

        [HttpPost]
        public IHttpActionResult AddCoachingPresence(YY.Edu.Sys.Models.CoachingPresence cp)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {

                var result = Comm.Helper.DapperHelper.Instance.Insert(cp);

                if (result > 0)
                {
                    return Ok(Comm.ResponseModel.ResponseModelBase.Success());
                }
                else
                {
                    return Content(HttpStatusCode.OK, Comm.ResponseModel.ResponseModelBase.GetRes(Comm.ResponseModel.ResponseModelErrorEnum.SystemError));
                }

            }
            catch (Exception ex)
            {
                logs.Error("风采信息添加失败", ex);
                return BadRequest();
            }
        }

        [HttpPost]
        public IHttpActionResult DelCoachingPresence(int ID, int State)
        {
            string sql = "update CoachingPresence set FCState=@FCState where FCID=@FCID";

            if (!ModelState.IsValid)
                return BadRequest();

            //单条添加
            var result = Comm.Helper.DapperHelper.Instance.Execute(sql.ToString(),
                                   new { FCState = State, FCID = ID });

            if (result > 0)
            {
                return Ok(new { error = false, code = "1000", message = "操作成功" });
            }
            else
            {
                return Content(HttpStatusCode.OK, new { error = true, code = "1001", message = "操作失败,请重新操作" });
            }
        }

        #endregion


        #region 课程

        /// <summary>
        /// 取的教练下的所有课程
        /// </summary>
        /// <param name="CoachID"></param>
        /// <returns></returns>
        public IHttpActionResult GetCoachCurriculum(int CoachID)
        {
            //
            //select T.*,v.VenueName,c.CampusName from  TeachingSchedule t with(nolock) inner join Venue v with(nolock) on t.VenueID=v.VenueID             left join Campus c with(nolock) on t.CampusID = t.CampusID  where PKID in(select PKID from Curriculum c with(nolock) where CoachID = 0 ) and CoachID = 0
            try
            {

                if (!ModelState.IsValid)
                    return BadRequest();

                System.Web.HttpContextBase context = (System.Web.HttpContextBase)Request.Properties["MS_HttpContext"];//获取传统context
                System.Web.HttpRequestBase request = context.Request;//定义传统request对象
                string UserName = Comm.Helper.ParamHelper<string>.GetParam(request["UserName"], "");
                string ParentMobile = Comm.Helper.ParamHelper<string>.GetParam(request["ParentMobile"], "");
                string FullName = Comm.Helper.ParamHelper<string>.GetParam(request["FullName"], "");
                string ParentFullName = Comm.Helper.ParamHelper<string>.GetParam(request["ParentFullName"], "");
                int start = Comm.Helper.ParamHelper<int>.GetParam(request["start"], 0);
                start += 1;//adminlte 加载的datatable起始页为0
                int length = Comm.Helper.ParamHelper<int>.GetParam(request["length"], 0);
                int venueId = Comm.Helper.ParamHelper<int>.GetParam(request["venueId"], 0);

                if (venueId <= 0 || start < 0 || length <= 0)
                    return BadRequest();

                PageCriteria criteria = new PageCriteria();
                criteria.Condition = "1=1";

                criteria.Condition += string.Format(" and t.CoachID = {0} ", CoachID);

              //  select T.*,v.VenueName,c.CampusName from  TeachingSchedule t with(nolock) inner join Venue v with(nolock) on t.VenueID = v.VenueID             left join Campus c with(nolock) on t.CampusID = t.CampusID  where PKID in(select PKID from Curriculum c with(nolock) where CoachID = 0 ) and CoachID = 0

                criteria.CurrentPage = start;
                criteria.Fields = "*";
                criteria.PageSize = length;
                criteria.TableName = " TeachingSchedule t with(nolock) inner join Venue v with(nolock) on t.VenueID = v.VenueID             left join Campus c with(nolock) on t.CampusID = t.CampusID  where PKID in(select PKID from Curriculum c with(nolock) where CoachID = '"+CoachID+"' )";
                criteria.PrimaryKey = "PKID";

                var r = Comm.Helper.DapperHelper.GetPageData<YY.Edu.Sys.Models.TeachingSchedule>(criteria);

                return Ok(new Comm.ResponseModel.ResponseModel4Page<YY.Edu.Sys.Models.TeachingSchedule>()
                {
                    data = r.Items,
                    recordsFiltered = r.TotalNum,
                    recordsTotal = r.TotalNum,
                });

            }
            catch (Exception ex)
            {
                logs.Error("取的教练下的所有课程", ex);
                return BadRequest();
            }
        }


        /// <summary>
        /// 取的课程下预约的学生
        /// </summary>
        /// <param name="pkid"></param>
        /// <returns></returns>
        public IHttpActionResult GetCurriculumStudentByPKID(int pkid)
        {

            string sql = "select c.CurriculumID,s.FullName,s.Mobile,s.ParentFullName,s.ParentMobile from Curriculum c with(nolock) inner join Student s with(nolock) on c.StudentID=s.StudentID  where PKID=@PKID ";
            var query = Comm.Helper.DapperHelper.Instance.Query<YY.Edu.Sys.Models.CurriculumStudentQuery>(sql, new { PKID = pkid });
            return Ok(query);        
        }

        /// <summary>
        ///课程表 学生进行签到（由教练发起）
        /// </summary>
        /// <param name="State"></param>
        /// <param name="StudentID"></param>
        /// <param name="pkid"></param>
        /// <returns></returns>
        public IHttpActionResult UpdateCurriculumState(int State, string StudentIDs, int pkid)
        {
            string sql = "update Curriculum set State=@State,ModifyTime=GETDATE() where PKID=@PKID and StudentID in(@StudentID) ";
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                //Models.VenueInfo venueInfo = Comm.Helper.DapperHelper.Instance.Get<Models.VenueInfo>(cityId);

                var result = Comm.Helper.DapperHelper.Instance.Execute(sql,new { State=State, PKID=pkid, StudentID = StudentIDs });

                if (result>0)
                {
                    return Ok(Comm.ResponseModel.ResponseModelBase.Success());
                }
                else
                {
                    return Content(HttpStatusCode.OK, Comm.ResponseModel.ResponseModelBase.GetRes(Comm.ResponseModel.ResponseModelErrorEnum.SystemError));
                }
            }
            catch (Exception ex)
            {
                logs.Error("学生进行签到失败", ex);
                return BadRequest();
            }
        }



        // 薪酬列表、明细


        [HttpGet]
        // GET api/<controller>
        public IHttpActionResult GetCoachWages(int CoachID, int VenueID)
        {
            //传参 http://www.cnblogs.com/landeanfen/p/5337072.html
            //dynamic

            //返回值 http://www.cnblogs.com/landeanfen/p/5501487.html
            //1 return Json<obj>;
            //2 return Ok<obj>;
            //3 return NotFound<obj>;
            //4 return Content<HttpStatusCode.OK,obj>;
            //5 return BadRequest(); 返回400错误


            try
            {

                if (!ModelState.IsValid)
                    return BadRequest();

                System.Web.HttpContextBase context = (System.Web.HttpContextBase)Request.Properties["MS_HttpContext"];//获取传统context
                System.Web.HttpRequestBase request = context.Request;//定义传统request对象
                string UserName = Comm.Helper.ParamHelper<string>.GetParam(request["UserName"], "");
                string ParentMobile = Comm.Helper.ParamHelper<string>.GetParam(request["ParentMobile"], "");
                string FullName = Comm.Helper.ParamHelper<string>.GetParam(request["FullName"], "");
                string ParentFullName = Comm.Helper.ParamHelper<string>.GetParam(request["ParentFullName"], "");
                int start = Comm.Helper.ParamHelper<int>.GetParam(request["start"], 0);
                start += 1;//adminlte 加载的datatable起始页为0
                int length = Comm.Helper.ParamHelper<int>.GetParam(request["length"], 0);
                int venueId = Comm.Helper.ParamHelper<int>.GetParam(request["venueId"], 0);

                if (venueId <= 0 || start < 0 || length <= 0)
                    return BadRequest();

                PageCriteria criteria = new PageCriteria();
                criteria.Condition = "1=1";

                criteria.Condition += string.Format(" and CoachID={0} and VenueID={1} ", CoachID,VenueID);

                //  select T.*,v.VenueName,c.CampusName from  TeachingSchedule t with(nolock) inner join Venue v with(nolock) on t.VenueID = v.VenueID             left join Campus c with(nolock) on t.CampusID = t.CampusID  where PKID in(select PKID from Curriculum c with(nolock) where CoachID = 0 ) and CoachID = 0

                criteria.CurrentPage = start;
                criteria.Fields = "c.*,v.VenueName";
                criteria.PageSize = length;
                criteria.TableName = " CoachWages c with(nolock) inner join Venue v with(nolock)";
                criteria.PrimaryKey = "WagesID";

                var r = Comm.Helper.DapperHelper.GetPageData<YY.Edu.Sys.Models.CoachWages>(criteria);

                return Ok(new Comm.ResponseModel.ResponseModel4Page<YY.Edu.Sys.Models.CoachWages>()
                {
                    data = r.Items,
                    recordsFiltered = r.TotalNum,
                    recordsTotal = r.TotalNum,
                });

            }
            catch (Exception ex)
            {
                logs.Error("取的教练下的所有课程", ex);
                return BadRequest();
            }          
           
        }


        [HttpGet]
        // GET api/<controller>
        public IHttpActionResult GetCoachWagesByWagesID(int WagesID)
        {
            //传参 http://www.cnblogs.com/landeanfen/p/5337072.html
            //dynamic

            //返回值 http://www.cnblogs.com/landeanfen/p/5501487.html
            //1 return Json<obj>;
            //2 return Ok<obj>;
            //3 return NotFound<obj>;
            //4 return Content<HttpStatusCode.OK,obj>;
            //5 return BadRequest(); 返回400错误

            string sql = "select T.*,v.VenueName,c.CampusName,s.CoachPrice from CoachWages_sub s with(nolock) inner join TeachingSchedule t with(nolock) on s.PKID=t.PKID inner join Venue v with(nolock) on t.VenueID = v.VenueID left join Campus c with(nolock) on t.CampusID = t.CampusID where  s.WagesID=@WagesID ";
            var query = Comm.Helper.DapperHelper.Instance.Query<YY.Edu.Sys.Models.TeachingSchedule>(sql, new { WagesID = WagesID });
            return Ok(query);
        }

        #endregion



    }
}
