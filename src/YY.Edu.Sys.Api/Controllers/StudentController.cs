﻿using Dapper;
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
using YY.Edu.Sys.Api.Models.ResponseModel;
using System.Web;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;

namespace YY.Edu.Sys.Api.Controllers
{
    [Authorize]
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

        /// <summary>
        /// 获取我的信息
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetMe(string openId)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            if (string.IsNullOrEmpty(openId))
                return BadRequest();

            try
            {

                var sql = @" SELECT [StudentID],[UserName],[FullName],[NickName],[Mobile],[Address],[ParentFullName],[ParentMobile],[HeadUrl],[VenueID],[BirthDate],[OpenID] 
                    FROM [SportsDB].[dbo].[Student] where OpenID =@openId ";

                var result = DapperHelper.Instance.QueryFirst<Sys.Models.Student>(sql, new
                {
                    openId = openId
                });

                return Ok(new Comm.ResponseModel.ResponseModel4Res<Sys.Models.Student>()
                {
                    Info = result
                });
            }
            catch (Comm.YYException.YYException ex)
            {
                return Ok(Comm.ResponseModel.ResponseModelBase.GetRes(ex.Message));
            }
            catch (Exception ex)
            {
                logs.Error("学生取我的信息失败", ex);
                return BadRequest();
            }
        }


        /// <summary>
        /// 导入学生
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async System.Threading.Tasks.Task<IHttpActionResult> Import()
        {

            if (!ModelState.IsValid)
                return BadRequest();

            if (!Request.Content.IsMimeMultipartContent())
                return BadRequest();

            Dictionary<string, string> dic = new Dictionary<string, string>();
            string root = System.Web.HttpContext.Current.Server.MapPath("~/App_Data");//指定要将文件存入的服务器物理位置  
            var provider = new MultipartFormDataStreamProvider(root);
            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                System.Data.DataTable dt;
                try
                {
                    dt = Comm.Helper.ExcelHelper.Import(provider.FileData[0].LocalFileName);
                }
                catch (Exception ex)
                {
                    logs.Error("导入学生信息表格解析失败", ex);
                    return Ok(new Comm.ResponseModel.ResponseModel4Res<string>() { Error = false, Info = "导入学生信息失败", });
                }

                List<string> containStudents = new List<string>();

                foreach (System.Data.DataRow item in dt.Rows)
                {
                    Sys.Models.Student student = new Sys.Models.Student()
                    {
                        UserName = item["手机号"].ToString(),
                        FullName = item["姓名"].ToString(),
                        ParentMobile = item["家长手机号"].ToString(),
                        ParentFullName = item["家长姓名"].ToString(),
                        VenueID = Convert.ToInt32(provider.FormData["venueID"]),
                        Pwd = "888888",
                        Mobile = item["手机号"].ToString(),
                    };

                    try
                    {
                        Services.StudentService.Create(student);
                    }
                    catch (Comm.YYException.YYException ex)
                    {
                        return Ok(Comm.ResponseModel.ResponseModelBase.GetRes(ex.Message));
                    }
                    catch (Exception ex)
                    {
                        logs.Error("导入学生信息表格入库失败", ex);
                    }
                }

                return Ok(Comm.ResponseModel.ResponseModelBase.Success());
            }
            catch (Exception ex)
            {
                logs.Error("学生信息导入失败", ex);
                return BadRequest();
            }
            finally
            {
                System.IO.File.Delete(provider.FileData[0].LocalFileName);
            }
        }

        /// <summary>
        /// 学生注册
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Create(YY.Edu.Sys.Models.Student student)
        {

            //if (!ModelState.IsValid)
            //    return BadRequest();

            try
            {
                bool flag = Services.StudentService.Reginster(student);

                return flag ? Ok(Comm.ResponseModel.ResponseModelBase.Success()) : Ok(Comm.ResponseModel.ResponseModelBase.SysError());
            }
            catch (Comm.YYException.YYException ex)
            {
                return Ok(Comm.ResponseModel.ResponseModelBase.GetRes(ex.Message));
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

        /// <summary>
        /// 标记学生为流失状态
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        public IHttpActionResult SignLoss(dynamic student)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                if (student == null || student.StudentID <= 0)
                    return BadRequest();

                VenueContainStudent(Convert.ToInt32(student.VenueID), Convert.ToInt32(student.StudentID));

                var sql = "UPDATE Student SET State=0 WHERE StudentID=@StudentID";
                var result = DapperHelper.Instance.Execute(sql, new { StudentID = Convert.ToInt32(student.StudentID) });
                return result > 0 ? Ok(Comm.ResponseModel.ResponseModelBase.Success()) : Ok(Comm.ResponseModel.ResponseModelBase.SysError());
            }
            catch (Comm.YYException.YYException ex)
            {
                return Ok(Comm.ResponseModel.ResponseModelBase.GetRes(ex.Message));
            }
            catch (Exception ex)
            {
                logs.Error("修改学生为流失失败", ex);
                return BadRequest();
            }
        }

        [HttpGet]
        public IHttpActionResult Page4Venue(string query)
        {
            try
            {

                if (!ModelState.IsValid)
                    return BadRequest();

                Comm.RequestModel.RequestModelBase<Sys.Models.Student> oData = Newtonsoft.Json.JsonConvert.DeserializeObject<Comm.RequestModel.RequestModelBase<Sys.Models.Student>>(query);

                if (oData.SearchCondition.VenueID <= 0 || oData.PageIndex < 0 || oData.PageSize <= 0)
                    return BadRequest();

                PageCriteria criteria = new PageCriteria();
                criteria.Condition = "1=1";

                criteria.Condition += string.Format(" and v.VenueID = {0}", oData.SearchCondition.VenueID);

                if (!string.IsNullOrEmpty(oData.SearchCondition.UserName))
                    criteria.Condition += string.Format(" and s.UserName = '{0}'", oData.SearchCondition.UserName);
                if (!string.IsNullOrEmpty(oData.SearchCondition.ParentMobile))
                    criteria.Condition += string.Format(" and s.ParentMobile = '{0}'", oData.SearchCondition.ParentMobile);
                if (!string.IsNullOrEmpty(oData.SearchCondition.FullName))
                    criteria.Condition += string.Format(" and s.FullName like '%{0}%'", oData.SearchCondition.FullName);
                if (!string.IsNullOrEmpty(oData.SearchCondition.ParentFullName))
                    criteria.Condition += string.Format(" and s.ParentFullName like '%{0}%'", oData.SearchCondition.ParentFullName);

                criteria.CurrentPage = oData.PageIndex + 1;//adminlte 加载的datatable起始页为0
                criteria.Fields = "s.StudentID,s.UserName,s.[Address],s.HeadUrl,s.Mobile,s.FullName,s.ParentFullName,s.ParentMobile,s.state,sv.VenueID,v.VenueName,v.LinkMan,v.LinkManMobile";
                criteria.PageSize = oData.PageSize;
                criteria.TableName = "Student as s join Student_Venue as sv on s.StudentID=sv.StudentID join Venue as v on sv.VenueID=v.VenueID";
                criteria.PrimaryKey = "s.StudentID";


                var r = Comm.Helper.DapperHelper.GetPageData<StudentResponse>(criteria);

                return Ok(new Comm.ResponseModel.ResponseModel4Page<StudentResponse>()
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

        /// <summary>
        /// 学生是否在场馆中上课
        /// </summary>
        /// <param name="venudId"></param>
        /// <param name="studentId"></param>
        private void VenueContainStudent(int venudId, int studentId)
        {

            //查询该学校下是否有此学生
            var sql = "select count(SVID) from Student_Venue where StudentID=@StudentID and VenueID=@VenueID";
            var count = DapperHelper.Instance.Query<int>(sql, new
            {
                StudentID = studentId,
                VenueID = venudId
            });

            if (count.FirstOrDefault() == 0)
                throw new Exception("此学生没有在该场馆上课");
        }

        /// <summary>
        /// 获取场馆下的学生
        /// </summary>
        /// <param name="venueId"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetStudents4Venue(int venueId)
        {
            try
            {

                if (!ModelState.IsValid)
                    return BadRequest();

                if (venueId <= 0)
                    return BadRequest();

                var sql = @"select s.StudentID,s.UserName,s.[Address],s.HeadUrl,s.Mobile,s.FullName,s.ParentFullName,s.ParentMobile,s.state,v.VenueName,v.LinkMan,v.LinkManMobile
from Student as s join Student_Venue as sv on s.StudentID=sv.StudentID join Venue as v on sv.VenueID=v.VenueID
where v.VenueID = @VenueID and s.state=1 ";

                var result = DapperHelper.Instance.Query<StudentResponse>(sql, new { VenueID = venueId });

                return Ok(new Comm.ResponseModel.ResponseModel4Res<StudentResponse>()
                {
                    data = result.AsList(),
                });

            }
            catch (Exception ex)
            {
                logs.Error("获取学生失败", ex);
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
        public IHttpActionResult GetStudentGrowth(int VenueID, int StudentID, int FCType)
        {
            try
            {

                if (!ModelState.IsValid)
                    return BadRequest();


                string strWhere = string.Format("StudentID = {0} and FCState=1 and FCType={1} ", StudentID, FCType);
                if (VenueID > 0)
                {
                    strWhere += string.Format(" and VenueID = {0} ", VenueID);
                }



                string sql = " select top 8 * from StudentGrowth with(nolock) where " + strWhere + " order by GrowthID desc ";



                var result = DapperHelper.Instance.Query<YY.Edu.Sys.Models.StudentGrowth>(sql);
                return Ok(new Comm.ResponseModel.ResponseModel4Res<YY.Edu.Sys.Models.StudentGrowth>()
                {
                    data = result.AsList()
                });

            }
            catch (Exception ex)
            {
                logs.Error("教师风采", ex);
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
        [HttpGet]
        public IHttpActionResult GetStudentCurriculum(string query)
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

                Comm.RequestModel.RequestModelBase<Sys.Models.Student> oData = Newtonsoft.Json.JsonConvert.DeserializeObject<Comm.RequestModel.RequestModelBase<Sys.Models.Student>>(query);


                if (oData.SearchCondition.StudentID <= 0 || oData.PageIndex < 0 || oData.PageSize <= 0)
                    return BadRequest();

                PageCriteria criteria = new PageCriteria();

                criteria.Condition += string.Format("cu.StudentID='{0}'", oData.SearchCondition.StudentID);

                //当日
                if (oData.RequestType == 1)
                {
                    if (!string.IsNullOrEmpty(oData.CurrentDate))
                    {
                        criteria.Condition += string.Format(" and t.CurriculumDate= '{0}'", oData.CurrentDate);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(oData.CurrentDate) && string.IsNullOrEmpty(oData.BeginDate) && string.IsNullOrEmpty(oData.EndDate))
                    {

                        criteria.Condition += string.Format(" and t.CurriculumDate<> '{0}'", oData.CurrentDate);
                    }
                }


                if (oData.RequestType == 2)
                {
                    criteria.Condition += string.Format(" and cu.State<>{0} ", 1);
                }
                else if (oData.RequestType == 3)
                {
                    criteria.Condition += string.Format(" and cu.State={0} ", 1);
                }


                //开始日期
                if (!string.IsNullOrEmpty(oData.BeginDate))
                {
                    criteria.Condition += string.Format(" and t.CurriculumDate>= '{0}'", oData.BeginDate);
                }
                //结束日期
                if (!string.IsNullOrEmpty(oData.EndDate))
                {
                    criteria.Condition += string.Format(" and t.CurriculumDate< '{0}'", oData.EndDate);
                }




                criteria.CurrentPage = oData.PageIndex;
                criteria.Fields = "t.*,v.VenueName,c.CampusName,'KSstate'=cu.State,'CoachFullName'=co.FullName,cu.CurriculumID ";
                criteria.PageSize = oData.PageSize;
                criteria.TableName = "TeachingSchedule t with(nolock) inner join Venue v with(nolock) on t.VenueID = v.VenueID left join Campus c with(nolock) on t.CampusID = t.CampusID inner join Curriculum cu with(nolock) on t.PKID = cu.PKID inner join Coach co with(nolock) on co.CoachID=t.CoachID ";
                criteria.PrimaryKey = "t.PKID";
                if (oData.RequestType == 3)
                {
                    criteria.Sort = "CurriculumDate desc,t.PKID desc";
                }
                else
                {
                    criteria.Sort = "CurriculumDate,CurriculumBeginTime,t.PKID";
                }

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
                logs.Error("学生成长查询失败", ex);
                return BadRequest();
            }

        }

        [HttpGet]
        public IHttpActionResult GetStudentCurriculumByID(int PKID)
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

                string sql = "select distinct T.*,v.VenueName,c.CampusName,'KSstate'=cu.State,co.FullName,cu.CurriculumID,co.HeadUrl from TeachingSchedule t with(nolock) inner join Venue v with(nolock) on t.VenueID = v.VenueID left join Campus c with(nolock) on  c.CampusID = t.CampusID and v.VenueID=c.VenueID inner join Curriculum cu with(nolock) on t.PKID = cu.PKID inner join Coach co with(nolock) on co.CoachID=t.CoachID where t.PKID=" + PKID;

                var query = Comm.Helper.DapperHelper.Instance.Query<TeachingScheduleResponse>(sql);
                return Ok(query);
            }
            catch (Exception ex)
            {
                logs.Error("课程详细信息", ex);
                return BadRequest();
            }

        }

        ///申请请假
        public IHttpActionResult ApplyLeave(int State,int cID)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            string sql = "update Curriculum set State=@State,ModifyTime=GETDATE() where CurriculumID=@CurriculumID ";

            
            try
            {
                //Models.VenueInfo venueInfo = Comm.Helper.DapperHelper.Instance.Get<Models.VenueInfo>(cityId);

                var result = Comm.Helper.DapperHelper.Instance.Execute(sql, new { State= State, CurriculumID = cID});

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
                logs.Error("学生申请请假", ex);
                return BadRequest();
            }
        }

        /// <summary>
        /// 申请购买课时
        /// </summary>
        /// <param name="ApplyBuyHours"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult ApplyBuy(YY.Edu.Sys.Models.ApplyBuyHours ApplyBuyHours)
        {

            //if (!ModelState.IsValid)
            //    return BadRequest();

            try
            {
                //Models.VenueInfo venueInfo = Comm.Helper.DapperHelper.Instance.Get<Models.VenueInfo>(cityId);

                var result = Comm.Helper.DapperHelper.Instance.Insert(ApplyBuyHours);

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
                logs.Error("申请购买课时失败", ex);
                return BadRequest();
            }
        }

        //购买课时
        [HttpGet]
        public IHttpActionResult BuyCurriculum(int StudentID, int CoachID, int number, int VenueID, decimal CMoney)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("declare @id int;");
            sql.Append(" select @id = CHNID from ClassHoursNumber with(nolock) where [StudentID] = '" + StudentID + "' and CoachID = '" + CoachID + "' ");
            sql.Append(" if (@id > 0)   begin ");
            sql.Append("  update ClassHoursNumber set ClassNumber = ClassNumber +" + number + " where CHNID = @id ");
            sql.Append(" end else begin ");

            sql.Append(" insert into ClassHoursNumber(StudentID, CoachID, ClassNumber,VenueID) values('" + StudentID + "', '" + CoachID + "', '" + number + "','" + VenueID + "')");
            sql.Append(" end ");
            //1购买，2约课3学生请假退回，4老师请假退回，5学校停课退回
            sql.Append(" INSERT INTO [ClassHoursDetailed]([DType],[VenueID],[CoachID],[StudentID],[Remark],[DNumber],CMoney) values(1,'" + VenueID + "','" + CoachID + "','" + StudentID + "','购买课时','" + number + "','" + CMoney + "') ");


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
        public IHttpActionResult GetClassHoursNumberByStudentID(int StudentID)//?
        {
            var query = Comm.Helper.DapperHelper.Instance.Query<YY.Edu.Sys.Models.ClassHoursNumber>("select CHNID, ClassNumber, 'CoachFullName' = c.FullName from ClassHoursNumber n with(nolock) inner join Coach c with(nolock) on n.CoachID = c.CoachID order by CHNID desc ");

            //链表直接写sql传参

            return Ok(query);
        }



        [HttpGet]
        //取的当前学生课时消息明细
        public IHttpActionResult GetClassHoursDetailedByStudentID(int StudentID, int PageIndex, int PageSize)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();


                PageCriteria criteria = new PageCriteria();

                criteria.Condition += string.Format("StudentID='{0}'", StudentID);


                criteria.CurrentPage = PageIndex;
                criteria.Fields = "d.*,VenueName,c.FullName";
                criteria.PageSize = PageSize;
                criteria.TableName = "ClassHoursDetailed d with(nolock) inner join Venue v with(nolock) on d.VenueID=v.VenueID  inner join Coach c with(nolock) on d.CoachID = c.CoachID";
                criteria.PrimaryKey = "DetailedID";

                criteria.Sort = "DetailedID desc";


                var r = Comm.Helper.DapperHelper.GetPageData<ClassHoursDetailedResponse>(criteria);

                return Ok(new Comm.ResponseModel.ResponseModel4Page<ClassHoursDetailedResponse>()
                {
                    data = r.Items,
                    recordsFiltered = r.TotalNum,
                    recordsTotal = r.TotalNum,
                });

            }
            catch (Exception ex)
            {
                logs.Error("所有当前学生可以约的课程", ex);
                return BadRequest();
            }
        }


        [HttpGet]
        //所有当前学生可以约的课程
        public IHttpActionResult GetTeachingScheduleByStudent(string query)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                Comm.RequestModel.RequestModelBase<Sys.Models.Student> oData = Newtonsoft.Json.JsonConvert.DeserializeObject<Comm.RequestModel.RequestModelBase<Sys.Models.Student>>(query);


                if (oData.SearchCondition.StudentID <= 0 || oData.PageIndex < 0 || oData.PageSize <= 0)
                    return BadRequest();

                PageCriteria criteria = new PageCriteria();

                criteria.Condition += string.Format("ch.StudentID='{0}'", oData.SearchCondition.StudentID);

                if (oData.PKType > 0)
                {
                    criteria.Condition += string.Format(" and t.PKType='{0}'", oData.PKType);
                }
                criteria.Condition += " and CurriculumDate >= '" + DateTime.Now.ToString("yyyy-MM-dd") + "' and cm.CurriculumID is null and(t.State = 0 or(t.State = 1 and t.PKType = 2)) ";

                criteria.CurrentPage = oData.PageIndex;
                criteria.Fields = "t.*,'CoachFullName'=c.FullName,v.VenueName,cs.CampusName,ClassNumber ";
                criteria.PageSize = oData.PageSize;
                criteria.TableName = "                TeachingSchedule t with(nolock) inner join Coach c with(nolock) on t.CoachID = c.CoachID  inner join Venue v with(nolock) on t.VenueID = v.VenueID  inner join Campus cs with(nolock) on v.VenueID = cs.VenueID and t.CampusID = cs.CampusID inner join ClassHoursNumber ch with(nolock) on t.CoachID = ch.CoachID and ch.PKType=t.PKType and t.Venueid = ch.VenueID  left join Curriculum cm with(nolock) on t.PKID = cm.PKID and cm.StudentID=ch.StudentID ";
                criteria.PrimaryKey = "t.PKID";

                criteria.Sort = "t.CoachID, CurriculumDate ,CurriculumBeginTime,t.pkid";


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
                logs.Error("所有当前学生可以约的课程", ex);
                return BadRequest();
            }
        }
        /// <summary>
        /// 取的购买课时次数交费明细
        /// </summary>
        /// <param name="StudentID"></param>
        /// <returns></returns>
        public IHttpActionResult GetBuyBuyCurriculumDetail(int StudentID)
        {
            var query = Comm.Helper.DapperHelper.Instance.Query<ClassHoursOrderResponse>("select o.*,'CoachFullName'=c.FullName from  ClassHoursOrder o with(nolock) inner join Coach c with(nolock) on o.CoachID=c.CoachID order by OrderID desc ");

            //链表直接写sql传参

            return Ok(query);
        }



        //约课
        [HttpGet]
        public IHttpActionResult SubscribeCurriculum(int sid, int pkid, int cid, int vid, string sname)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("declare @count int ;declare @yk int ;");
            sql.Append(" select @count = ClassNumber from ClassHoursNumber with(nolock) where StudentID ='" + sid + "' and CoachID ='" + cid + "'  and VenueID = '" + vid + "'  ");
            sql.Append(" select @yk=count(1) from TeachingSchedule with(nolock) where (state = 0 or(state = 1 and pkType = 2)) and PKID ='" + pkid + "' ");

            sql.Append(" if (@count > 0 and @yk>0) begin");
            sql.Append("   insert into Curriculum(StudentID, PKID, CoachID, StudentFullName) values('" + sid + "','" + pkid + "','" + cid + "','" + sname + "'); ");
            sql.Append(" update ClassHoursNumber set ClassNumber=ClassNumber-1 where  StudentID ='" + sid + "' and CoachID ='" + cid + "'  and VenueID = '" + vid + "'");
            //1购买，2约课3学生请假退回，4老师请假退回，5学校停课退回
            sql.Append(" INSERT INTO [ClassHoursDetailed]([DType],[VenueID],[CoachID],[StudentID],[Remark],[DNumber]) values(2,'" + vid + "','" + cid + "','" + sid + "','预约课程',1) ");
            sql.Append(" end");

            try
            {

                var result = Comm.Helper.DapperHelper.Instance.Execute(sql.ToString());

                if (result > 0)
                {
                    return Ok(Comm.ResponseModel.ResponseModelBase.Success());
                }
                else
                {
                    return Ok(Comm.ResponseModel.ResponseModelBase.GetRes("fail"));
                    // return Content(HttpStatusCode.OK, Comm.ResponseModel.ResponseModelBase.GetRes(Comm.ResponseModel.ResponseModelErrorEnum.SystemError));
                }

            }
            catch (Exception ex)
            {
                logs.Error("约课", ex);
                return BadRequest();
            }

        }


   
        /// <summary>
        /// 预约课时d
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
                sql.Append(" INSERT INTO [StudentWithdrawApply_Sub]([CoachID],[ClassNumber],[Price],[ApplyID])  values('" + s.CoachID + "', '" + s.ClassNumber + "','" + s.Price + "', @id) ");
            }

            try
            {

                var result = Comm.Helper.DapperHelper.Instance.Execute(sql.ToString(), new { StudentID = cp.StudentID, KSNumber = cp.KSNumber, Remark = cp.Remark, RefundablePrice = cp.RefundablePrice, RealRetreat = cp.RealRetreat });

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


        #region  学生评价

        /// <summary>
        ///评价
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public IHttpActionResult AddCurriculumEvaluate(YY.Edu.Sys.Models.CurriculumEvaluate c)
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
                logs.Error("学生课时评价", ex);
                return BadRequest();
            }
        }



        /// <summary>
        /// 取的当前学生对课时的评价
        /// </summary>
        /// <param name="StudentID"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetCurriculumEvaluateByID(int StudentID, int CurriculumID)
        {
            var query = Comm.Helper.DapperHelper.Instance.Query<YY.Edu.Sys.Models.CurriculumEvaluate>("select * from CurriculumEvaluate with(nolock) where StudentID=@StudentID and CurriculumID=@CurriculumID order by PJID desc ", new { StudentID = StudentID, CurriculumID = CurriculumID });

            //链表直接写sql传参

            return Ok(query);
        }
        #endregion
    }
}
