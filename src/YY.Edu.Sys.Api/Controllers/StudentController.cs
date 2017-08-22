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
using System.Text;

namespace YY.Edu.Sys.Api.Controllers
{
    [RoutePrefix("api/Student")]
    public class StudentController : ApiController
    {

        private static readonly ILog logs = Comm.Helper.LogHelper.GetInstance();

        // GET: api/Student
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Student/5
        public string Get(int id)
        {
            return "value";
        }


        [HttpPost]
        public IHttpActionResult Create(YY.Edu.Sys.Models.Student student)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            try
            {

                var result = Comm.Helper.DapperHelper.Instance.Insert(student);

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
                logs.Error("学生信息添加失败", ex);
                return BadRequest();
            }
        }


        [HttpPost]
        public IHttpActionResult Edit(YY.Edu.Sys.Models.Student student)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                //Models.VenueInfo venueInfo = Comm.Helper.DapperHelper.Instance.Get<Models.VenueInfo>(cityId);

                var result = Comm.Helper.DapperHelper.Instance.Update(student);

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


        [HttpGet]
        public IHttpActionResult Page4Venue()
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

                criteria.Condition += string.Format(" and v.VenueID = {0}", venueId);

                if (!string.IsNullOrEmpty(UserName))
                    criteria.Condition += string.Format(" and s.UserName = '{0}'", UserName);
                if (!string.IsNullOrEmpty(ParentMobile))
                    criteria.Condition += string.Format(" and s.ParentMobile = '{0}'", ParentMobile);
                if (!string.IsNullOrEmpty(FullName))
                    criteria.Condition += string.Format(" and s.FullName like '%{0}%'", FullName);
                if (!string.IsNullOrEmpty(ParentFullName))
                    criteria.Condition += string.Format(" and s.ParentFullName like '%{0}%'", ParentFullName);

                criteria.CurrentPage = start;
                criteria.Fields = "s.StudentID,s.UserName,s.[Address],s.HeadUrl,s.Mobile,s.FullName,s.ParentFullName,s.ParentMobile,v.VenueName,v.LinkMan,v.LinkManMobile";
                criteria.PageSize = length;
                criteria.TableName = "Student as s join Student_Venue as sv on s.StudentID=sv.StudentID join Venue as v on sv.VenueID=v.VenueID";
                criteria.PrimaryKey = "s.StudentID";

                var r = Comm.Helper.DapperHelper.GetPageData<Models.QueryModel.StudentQuery>(criteria);

                return Ok(new Comm.ResponseModel.ResponseModel4Page<Models.QueryModel.StudentQuery>()
                {
                    data = r.Items,
                    recordsFiltered = r.TotalNum,
                    recordsTotal = r.TotalNum,
                });

            }
            catch (Exception ex)
            {
                logs.Error("学生查询失败", ex);
                return BadRequest();
            }
        }

        // POST: api/Student
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Student/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Student/5
        public void Delete(int id)
        {

        }




        #region 我的成长
        #region //

        [HttpGet]
        // GET api/<controller>
        public IHttpActionResult GetStudentGrowth()
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
                int StudentID = Comm.Helper.ParamHelper<int>.GetParam(request["studentID"], 0);

                if (StudentID <= 0 || start < 0 || length <= 0)
                    return BadRequest();

                PageCriteria criteria = new PageCriteria();              

                criteria.Condition += string.Format("  StudentID= {0}", StudentID);

             
                criteria.CurrentPage = start;
                criteria.Fields = "*";
                criteria.PageSize = length;
                criteria.TableName = "StudentGrowth";
                criteria.PrimaryKey = "GrowthID";

                var r = Comm.Helper.DapperHelper.GetPageData<YY.Edu.Sys.Models.StudentGrowth>(criteria);

                return Ok(new Comm.ResponseModel.ResponseModel4Page<YY.Edu.Sys.Models.StudentGrowth>()
                {
                    data = r.Items,
                    recordsFiltered = r.TotalNum,
                    recordsTotal = r.TotalNum,
                });

            }
            catch (Exception ex)
            {
                logs.Error("学生成长查询失败", ex);
                return BadRequest();
            }         
        }

        [HttpPost]
        public IHttpActionResult AddStudentGrowth(YY.Edu.Sys.Models.StudentGrowth cp)
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
                logs.Error("学生成长信息添加失败", ex);
                return BadRequest();
            }
        }

        [HttpPost]
        public IHttpActionResult DelStudentGrowth(int ID, int State)
        {
            string sql = "update StudentGrowth set FCState=@FCState where GrowthID=@GrowthID";

            if (!ModelState.IsValid)
                return BadRequest();

            //单条添加
            var result = Comm.Helper.DapperHelper.Instance.Execute(sql.ToString(),
                                   new { FCState = State, GrowthID = ID });

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

        #endregion


        #region 课程

        /// <summary>
        /// 获取学生的所有课时
        /// </summary>
        /// <param name="StudentID"></param>
        /// <returns></returns>
        public IHttpActionResult GetStudentCurriculum()
        {
            //           select T.*,v.VenueName,c.CampusName,cu.State,cu.CoachID from  TeachingSchedule t with(nolock)
            //inner join Venue v with(nolock) on t.VenueID = v.VenueID
            //left join Campus c with(nolock) on t.CampusID = t.CampusID
            //inner join Curriculum cu with(nolock) on t.PKID = cu.PKID
            //where cu.StudentID = @StudentID

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
                int StudentID = Comm.Helper.ParamHelper<int>.GetParam(request["studentID"], 0);

                if (StudentID <= 0 || start < 0 || length <= 0)
                    return BadRequest();

                PageCriteria criteria = new PageCriteria();

                criteria.Condition += string.Format("cu.StudentID= {0}", StudentID);


                criteria.CurrentPage = start;
                criteria.Fields = "T.*,v.VenueName,c.CampusName,'KSstate'=cu.State";
                criteria.PageSize = length;
                criteria.TableName = "TeachingSchedule t with(nolock) inner join Venue v with(nolock) on t.VenueID = v.VenueID left join Campus c with(nolock) on t.CampusID = t.CampusID inner join Curriculum cu with(nolock) on t.PKID = cu.PKID";
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
                logs.Error("学生成长查询失败", ex);
                return BadRequest();
            }
            return null;
        }


        //购买课时
        public IHttpActionResult BuyCurriculum(int StudentID,int CoachID,int number)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("declare @id int;");
            sql.Append(" select @id = CHNID from ClassHoursNumber with(nolock) where [StudentID] = @StudentID and CoachID = '"+CoachID+"' ");
            sql.Append(" if (@id > 0)   begin ");
            sql.Append("  update ClassHoursNumber set ClassNumber = ClassNumber +"+number+" where CHNID = @id ");
            sql.Append(" end else begin ");
            sql.Append(" insert into ClassHoursNumber(StudentID, CoachID, ClassNumber) values('"+StudentID+"', '"+CoachID+"', '"+number+"')");
            sql.Append(" end ");          

            if (!ModelState.IsValid)
                return BadRequest();

            try
            {

                var result = Comm.Helper.DapperHelper.Instance.Execute(sql.ToString());

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
                logs.Error("学生购买课时", ex);
                return BadRequest();
            }

           
        }
        /// <summary>
        /// 取的学生现有课时次数明细
        /// </summary>
        /// <param name="StudentID"></param>
        /// <returns></returns>
        public IHttpActionResult GetClassHoursNumberByStudentID(int StudentID)
        {
            var query = Comm.Helper.DapperHelper.Instance.Query<YY.Edu.Sys.Models.ClassHoursNumber>("select CHNID, ClassNumber, 'CoachFullName' = c.FullName from ClassHoursNumber n with(nolock) inner join Coach c with(nolock) on n.CoachID = c.CoachID order by CHNID desc ");

            //链表直接写sql传参

            return Ok(query);
        }

        /// <summary>
        /// 取的购买课时次数交费明细
        /// </summary>
        /// <param name="StudentID"></param>
        /// <returns></returns>
        public IHttpActionResult GetBuyBuyCurriculumDetail(int StudentID)
        {
            var query = Comm.Helper.DapperHelper.Instance.Query<YY.Edu.Sys.Models.ClassHoursOrder>("select o.*,'CoachFullName'=c.FullName from  ClassHoursOrder o with(nolock) inner join Coach c with(nolock) on o.CoachID=c.CoachID order by OrderID desc ");

            //链表直接写sql传参

            return Ok(query);
        }
        /// <summary>
        /// 预约课时
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddCurriculum(YY.Edu.Sys.Models.Curriculum c)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {

                var result = Comm.Helper.DapperHelper.Instance.Insert(c);

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
                logs.Error("预约课时失败", ex);
                return BadRequest();
            }
        }


        /// <summary>
        /// 修改预约课时
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult UpdateCurriculum(YY.Edu.Sys.Models.Curriculum c)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {

                var result = Comm.Helper.DapperHelper.Instance.Update(c);

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
                logs.Error("修改预约课时失败", ex);
                return BadRequest();
            }
        }



        /// <summary>
        /// 申请退课
        /// </summary>
        /// <param name="cp"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult AddStudentWithdrawApply(YY.Edu.Sys.Models.StudentWithdrawApply cp)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            StringBuilder sql = new StringBuilder();
            sql.Append("declare @id int;INSERT INTO[StudentWithdrawApply]([StudentID],[KSNumber],[Remark],[RefundablePrice],[RealRetreat]           values(@StudentID, @KSNumber, @Remark, @RefundablePrice, @RealRetreat)");
            sql.Append("   set @id=@@IDENTITY; ");
            List<YY.Edu.Sys.Models.StudentWithdrawApply_Sub> list = cp.sublisst;
            foreach (YY.Edu.Sys.Models.StudentWithdrawApply_Sub s in list)
            {
                sql.Append(" INSERT INTO [StudentWithdrawApply_Sub]([CoachID],[ClassNumber],[Price],[ApplyID])  values('"+s.CoachID+"', '"+s.ClassNumber+"','"+s.Price+"', @id) ");
            }

            try
            {

                var result = Comm.Helper.DapperHelper.Instance.Execute(sql.ToString(),new { StudentID=cp.StudentID, KSNumber=cp.KSNumber, Remark=cp.Remark, RefundablePrice=cp.RefundablePrice, RealRetreat =cp.RealRetreat});

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
                logs.Error("申请退课失败", ex);
                return BadRequest();
            }
        }

        /// <summary>
        /// 申请退课
        /// </summary>
        /// <param name="cp"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult UpdateStudentWithdrawApply(YY.Edu.Sys.Models.StudentWithdrawApply cp)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {

                var result = Comm.Helper.DapperHelper.Instance.Update(cp);

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
                logs.Error("修改申请退课失败", ex);
                return BadRequest();
            }
        }

        //
        #endregion
    }
}
