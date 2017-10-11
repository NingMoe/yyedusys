﻿using Dapper;
using DapperExtensions;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using YY.Edu.Sys.Api.Models.ResponseModel;
using YY.Edu.Sys.Comm.Helper;


namespace YY.Edu.Sys.Api.Controllers
{
    [Authorize]
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
            try
            {
                //查询

                var sql = @"select c.CoachID,c.FullName,c.UserName,c.State,c.Introduce,c.NickName,c.HeadUrl,c.Address,c.Mobile,c.Sex,v.VenueID,v.VenueName,cv.Wage,cv.Price,cv.WageMore,cv.PriceMore
                    from Coach as c left join Coach_Venue as cv on c.CoachID = cv.CoachID left join Venue as v on v.VenueID = cv.VenueID where c.CoachID = @CoachID";

                var result = DapperHelper.Instance.Query<CoachResponse>(sql, new { CoachID = id });

                return Ok(new Comm.ResponseModel.ResponseModel4Res<YY.Edu.Sys.Models.Coach>()
                {
                    Info = result.AsList().FirstOrDefault()
                });
            }
            catch (Exception ex)
            {
                logs.Error("获取教练详情失败", ex);
                return BadRequest();
            }
        }

        /// <summary>
        /// 获取我的信息
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetMe(string openId)
        {

            //if (!ModelState.IsValid)
            //    return BadRequest();

            //if (string.IsNullOrEmpty(openId))
            //    return BadRequest();

            try
            {

                var sql = @"select c.CoachID,c.FullName,c.UserName,c.State,c.Introduce,c.NickName,c.HeadUrl,c.Address,c.Mobile,c.Sex,v.VenueID,v.VenueName,cv.Wage,cv.Price,cv.WageMore,cv.PriceMore
                    from Coach as c left join Coach_Venue as cv on c.CoachID = cv.CoachID left join Venue as v on v.VenueID = cv.VenueID where c.OpenID =@openId ";

                var result = DapperHelper.Instance.QueryFirst<CoachResponse>(sql, new
                {
                    openId = openId
                });

                return Ok(new Comm.ResponseModel.ResponseModel4Res<CoachResponse>()
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
                logs.Error("教练取我的信息失败", ex);
                return BadRequest();
            }
        }

        /// <summary>
        /// 获取校区下可以任教的教练
        /// </summary>
        /// <param name="venueId"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetCoach4Teaching(int venueId)
        {
            try
            {

                if (!ModelState.IsValid)
                    return BadRequest();

                if (venueId <= 0)
                    return BadRequest();

                var sql = @"select c.CoachID,c.FullName,c.UserName,c.State,c.Introduce,c.NickName,c.HeadUrl,c.Address,c.Mobile,c.Sex,v.VenueID,v.VenueName,cv.Wage,cv.Price,cv.WageMore,cv.PriceMore
                    from Coach as c left join Coach_Venue as cv on c.CoachID = cv.CoachID left join Venue as v on v.VenueID = cv.VenueID
                    where v.VenueID = @VenueID and c.state=1 ";

                var result = DapperHelper.Instance.Query<CoachResponse>(sql, new { VenueID = venueId });

                return Ok(new Comm.ResponseModel.ResponseModel4Res<CoachResponse>()
                {
                    data = result.AsList(),
                });

            }
            catch (Exception ex)
            {
                logs.Error("获取排课教练查询失败", ex);
                return BadRequest();
            }
        }

        /// <summary>
        /// 导入教练
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
            string root = System.Web.HttpContext.Current.Server.MapPath("~/App_Data");
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
                    logs.Error("导入教练信息表格解析失败", ex);
                    return Ok(new Comm.ResponseModel.ResponseModel4Res<string>() { Error = false, Info = "导入教练信息失败", });
                }

                List<string> containStudents = new List<string>();

                foreach (System.Data.DataRow item in dt.Rows)
                {
                    Sys.Models.Coach coach = new Sys.Models.Coach()
                    {
                        UserName = item["手机号"].ToString(),
                        FullName = item["姓名"].ToString(),
                        VenueID = Convert.ToInt32(provider.FormData["venueID"]),
                        Pwd = "888888",
                        Mobile = item["手机号"].ToString(),
                        State = 1,
                        Sex = item["性别"].ToString() == "男" ? 1 : 0,
                    };

                    try
                    {
                        Services.CoachService.Create(coach);
                    }
                    catch (Comm.YYException.YYException ex)
                    {
                        return Ok(Comm.ResponseModel.ResponseModelBase.GetRes(ex.Message));
                    }
                    catch (Exception ex)
                    {
                        logs.Error("导入教练信息表格入库失败", ex);
                    }
                }


                return Ok(Comm.ResponseModel.ResponseModelBase.Success());
            }
            catch (Exception ex)
            {
                logs.Error("教练信息导入失败", ex);
                return BadRequest();
            }
            finally
            {
                System.IO.File.Delete(provider.FileData[0].LocalFileName);
            }
        }


        [HttpPost]
        public IHttpActionResult Create(YY.Edu.Sys.Models.Coach coach)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            try
            {

                bool isExist = Services.CoachService.IsExist(coach);
                if (isExist)
                    return Ok(Comm.ResponseModel.ResponseModelBase.GetRes("该手机号已经注册"));

                bool isExistByOpenid = Services.CoachService.IsExistByOpenId(coach.OpenID);
                if (isExistByOpenid)
                    return Ok(Comm.ResponseModel.ResponseModelBase.GetRes("该微信号已经绑定"));

                StringBuilder sql = new StringBuilder();

                sql.Append("");
                coach.CardPositiveUrl = coach.CardPositiveUrl.Replace("-", "/");
                coach.CardReverseUrl = coach.CardReverseUrl.Replace("-", "/");
                bool flag = Services.CoachService.Reginster(coach);

                return flag ? Ok(Comm.ResponseModel.ResponseModelBase.Success()) : Ok(Comm.ResponseModel.ResponseModelBase.SysError());

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

        /// <summary>
        /// 教练信息分页查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Page4Venue(string query)
        {
            try
            {

                if (!ModelState.IsValid)
                    return BadRequest();

                Comm.RequestModel.RequestModelBase<Sys.Models.Coach> oData = Newtonsoft.Json.JsonConvert.DeserializeObject<Comm.RequestModel.RequestModelBase<Sys.Models.Coach>>(query);

                if (oData.PageIndex < 0 || oData.PageSize <= 0)
                    return BadRequest();

                PageCriteria criteria = new PageCriteria();
                criteria.Condition = "1=1";

                if (oData.SearchCondition.VenueID > 0)
                    criteria.Condition += string.Format(" and cv.VenueID = {0}", oData.SearchCondition.VenueID);
                if (!string.IsNullOrEmpty(oData.SearchCondition.UserName))
                    criteria.Condition += string.Format(" and c.UserName = '{0}'", oData.SearchCondition.UserName);
                if (!string.IsNullOrEmpty(oData.SearchCondition.FullName))
                    criteria.Condition += string.Format(" and c.FullName like '%{0}%'", oData.SearchCondition.FullName);

                criteria.CurrentPage = oData.PageIndex + 1;//adminlte 加载的datatable起始页为0
                criteria.Fields = "c.CoachID,c.FullName,c.UserName,c.State,c.Introduce,c.NickName,c.HeadUrl,c.Address,c.Mobile,c.Sex,v.VenueID,v.VenueName,cv.Wage,cv.Price,cv.WageMore,cv.PriceMore";
                criteria.PageSize = oData.PageSize;
                criteria.TableName = "Coach as c left join Coach_Venue as cv on c.CoachID = cv.CoachID left join Venue as v on v.VenueID = cv.VenueID";
                criteria.PrimaryKey = "c.CoachID";

                var r = Comm.Helper.DapperHelper.GetPageData<Models.ResponseModel.CoachResponse>(criteria);

                return Ok(new Comm.ResponseModel.ResponseModel4Page<Models.ResponseModel.CoachResponse>()
                {
                    data = r.Items,
                    recordsFiltered = r.TotalNum,
                    recordsTotal = r.TotalNum,
                });
            }
            catch (Exception ex)
            {
                logs.Error("教练查询失败", ex);
                return BadRequest();
            }
        }

        /// <summary>
        /// 教练绑定场馆
        /// </summary>
        /// <param name="coach"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult BindVenue(YY.Edu.Sys.Models.Coach_Venue coach)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                if (coach == null || coach.VenueID == null || coach.VenueID <= 0 ||
                    coach.CoachID == null || coach.CoachID <= 0)
                    return BadRequest();

                var result = DapperHelper.Instance.Get<Sys.Models.Coach>(coach.CoachID);

                //判断教练状态
                if (result.State != 1)
                    return Content(HttpStatusCode.OK, Comm.ResponseModel.ResponseModelBase.GetRes("此教练未审核通过"));

                IList<IPredicate> predList = new List<IPredicate>();
                predList.Add(Predicates.Field<Sys.Models.Coach_Venue>(f => f.CoachID, Operator.Eq, coach.CoachID));
                predList.Add(Predicates.Field<Sys.Models.Coach_Venue>(f => f.VenueID, Operator.Eq, coach.VenueID));
                IPredicateGroup predGroup = Predicates.Group(GroupOperator.And, predList.ToArray());

                //查询该学校下是否有此教练
                int venue_info_count = DapperHelper.Instance.Count<Sys.Models.Coach_Venue>(predGroup);
                //没有绑定过此学校
                if (venue_info_count > 0)
                {
                    return Ok(Comm.ResponseModel.ResponseModelBase.GetRes("已经绑定过此场馆"));
                }
                else
                {
                    coach.AddTime = DateTime.Now;
                    bool flag = DapperHelper.Instance.Insert(coach);
                    return flag ? Ok(Comm.ResponseModel.ResponseModelBase.Success()) : Ok(Comm.ResponseModel.ResponseModelBase.SysError());
                }
            }
            catch (Exception ex)
            {
                logs.Error("设置教练工资失败", ex);
                return BadRequest();
            }
        }

        /// <summary>
        /// 设置教练工资
        /// </summary>
        /// <param name="coach"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult SetCoachWages(YY.Edu.Sys.Models.Coach_Venue coach)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                if (coach == null || coach.VenueID == null || coach.VenueID <= 0 ||
                    coach.CoachID == null || coach.CoachID <= 0 || coach.Wage <= 0)
                    return BadRequest();

                try
                {
                    VenueContainCoach(coach.VenueID.Value, coach.CoachID.Value);

                    //判断教练状态
                    var result = DapperHelper.Instance.Get<Sys.Models.Coach>(coach.CoachID);
                    if (result.State != 1)
                        return Ok(Comm.ResponseModel.ResponseModelBase.GetRes("此教练未审核通过"));

                    var sql = "update Coach_Venue set Wage=@Wage,WageMore=@WageMore where CoachID=@CoachID and VenueID=@VenueID";
                    //设置工资
                    int flag = DapperHelper.Instance.Execute(sql, coach);

                    return flag > 0 ? Ok(Comm.ResponseModel.ResponseModelBase.Success()) : Ok(Comm.ResponseModel.ResponseModelBase.SysError());
                }
                catch (Exception ex)
                {
                    return Ok(Comm.ResponseModel.ResponseModelBase.GetRes(ex.Message));
                }
            }
            catch (Exception ex)
            {
                logs.Error("设置教练工资失败", ex);
                return BadRequest();
            }
        }

        /// <summary>
        /// 设置教练课时费
        /// </summary>
        /// <param name="coach"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult SetCoachPrice(YY.Edu.Sys.Models.Coach_Venue coach)
        {
            try
            {
                try
                {
                    VenueContainCoach(coach.VenueID.Value, coach.CoachID.Value);

                    //判断教练状态
                    var result = DapperHelper.Instance.Get<Sys.Models.Coach>(coach.CoachID);
                    if (result.State != 1)
                        return Ok(Comm.ResponseModel.ResponseModelBase.GetRes("此教练未审核通过"));

                    var sql = "update Coach_Venue set Price=@Price,PriceMore=@PriceMore where CoachID=@CoachID and VenueID=@VenueID";
                    //设置工资
                    int flag = DapperHelper.Instance.Execute(sql, coach);

                    return flag > 0 ? Ok(Comm.ResponseModel.ResponseModelBase.Success()) : Ok(Comm.ResponseModel.ResponseModelBase.SysError());
                }
                catch (Exception ex)
                {
                    return Ok(Comm.ResponseModel.ResponseModelBase.GetRes(ex.Message));
                }
            }
            catch (Exception ex)
            {
                logs.Error("设置教练课时费失败", ex);
                return BadRequest();
            }
        }

        /// <summary>
        /// 设置教练审核通过
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult SetCoachAudited(dynamic coach)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                if (coach == null || coach.CoachID <= 0)
                    return BadRequest();

                int coachId = coach.CoachID;
                int venudId = coach.VenueID;

                try
                {

                    VenueContainCoach(venudId, coachId);
                    var result = DapperHelper.Instance.Get<Sys.Models.Coach>(coachId);
                    result.State = 1;
                    bool flag = DapperHelper.Instance.Update(result);

                    return flag ? Ok(Comm.ResponseModel.ResponseModelBase.Success()) : Ok(Comm.ResponseModel.ResponseModelBase.SysError());
                }
                catch (Exception ex)
                {
                    return Ok(Comm.ResponseModel.ResponseModelBase.GetRes(ex.Message));
                }
            }
            catch (Exception ex)
            {
                logs.Error("设置教练审核通过失败", ex);
                return BadRequest();
            }
        }

        /// <summary>
        /// 删除教练
        /// </summary>
        /// <param name="coach"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult Delete(dynamic coach)
        {
            try
            {

                if (!ModelState.IsValid)
                    return BadRequest();

                if (coach == null || coach.CoachID <= 0)
                    return BadRequest();

                int coachId = coach.CoachID;
                int venudId = coach.VenueID;

                try
                {
                    VenueContainCoach(venudId, coachId);
                    var result = DapperHelper.Instance.Get<Sys.Models.Coach>(coachId);
                    result.State = 2;
                    bool flag = DapperHelper.Instance.Update(result);

                    return flag ? Ok(Comm.ResponseModel.ResponseModelBase.Success()) : Ok(Comm.ResponseModel.ResponseModelBase.SysError());
                }
                catch (Exception ex)
                {
                    return Ok(Comm.ResponseModel.ResponseModelBase.GetRes(ex.Message));
                }
            }
            catch (Exception ex)
            {
                logs.Error("删除教练失败", ex);
                return BadRequest();
            }
        }

        /// <summary>
        /// 检查教练是否再此场馆任职
        /// </summary>
        /// <param name="venudId"></param>
        /// <param name="coachId"></param>
        /// <exception cref="Exception">抛出异常</exception>
        private void VenueContainCoach(int venudId, int coachId)
        {

            //查询该学校下是否有此教练
            var sql = "select * from Coach_Venue where CoachID=@CoachID";
            var predicate = Predicates.Field<Sys.Models.Coach_Venue>(f => f.CoachID, Operator.Eq, coachId);
            var venue_info = DapperHelper.Instance.Query<Sys.Models.Coach_Venue>(sql, new { CoachID = coachId });

            if (venue_info == null)
                throw new Exception("此教练未绑定场馆");

            bool contain = false;
            venue_info.AsList().ForEach((v) => { if (v.VenueID == venudId) contain = true; });
            if (!contain)
                throw new Exception("此教练没有在该场馆任职");

        }

        /// <summary>
        /// 获取教练排课课程
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetCoachTeachingSche(int coachID, string curriculumDate)
        {

            var sql = @"select CurriculumDate,CurriculumStr = ( STUFF(( SELECT    ',' + CurriculumBeginTime+'-'+CurriculumEndTime+'|'+CONVERT(varchar(1), [State])+'|'+CONVERT(varchar(1), VenueID)+'|'+CONVERT(varchar(1), CampusID) FROM TeachingSchedule WHERE  CurriculumDate = t.CurriculumDate FOR XML PATH('') ), 1, 1, '') )  
from TeachingSchedule as t where CoachID = @CoachID and CurriculumDate>= @CurriculumDateStart and CurriculumDate< @CurriculumDateEnd
group by CurriculumDate";

            try
            {

                string[] curriculumDateArr = curriculumDate
                    .Split("to".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);

                var result = DapperHelper.Instance.Query(sql,
                    new
                    {
                        CoachID = coachID,
                        CurriculumDateStart = curriculumDateArr[0],
                        CurriculumDateEnd = Convert.ToDateTime(curriculumDateArr[1]).AddDays(1).ToString("yyy-MM-dd")
                    });

                return Ok(new Comm.ResponseModel.ResponseModel4Page<object>()
                {
                    data = result.ToList(),
                    recordsFiltered = result.Count(),
                    recordsTotal = result.Count()
                });
            }
            catch (Exception ex)
            {
                logs.Error("获取教练排课失败", ex);
                return BadRequest();
            }
        }



        #endregion


        #region //教师风采

        [HttpGet]
        // GET api/<controller>
        public IHttpActionResult GetCoachingPresence(int VenueID, int CoachID, int FCType)
        {
            try
            {

                if (!ModelState.IsValid)
                    return BadRequest();


                string strWhere = string.Format("CoachID = {0} and FCState=1 and FCType={1} ", CoachID, FCType);
                if (VenueID > 0)
                {
                    strWhere += string.Format(" and VenueID = {0} ", VenueID);
                }



                string sql = " select top 6 * from CoachingPresence with(nolock) where " + strWhere + " order by FCID desc ";



                var result = DapperHelper.Instance.Query<YY.Edu.Sys.Models.CoachingPresence>(sql);
                return Ok(new Comm.ResponseModel.ResponseModel4Res<YY.Edu.Sys.Models.CoachingPresence>()
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
        public IHttpActionResult AddCoachingPresence(YY.Edu.Sys.Models.CoachingPresence cp)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            cp.FCUrl = cp.FCUrl.Replace("-", "/");
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
        [HttpGet]
        /// <summary>
        /// 教练申请请假
        /// </summary>
        /// <param name="PKID">排课ID</param>
        /// <param name="State">0 排课完成 1学生约课完成，2学校停课（需要判断，有没有学生预约）,3请假申请 4请假 5上课中 6上课完成 7发放工资完成</param>
        /// <returns></returns>
        public IHttpActionResult ApplyLeaveByPKID(int PKID, int State)
        {
            string sql = " update TeachingSchedule set State=@State where PKID=@PKID where State in(0,1) ";
            if (!ModelState.IsValid)
                return BadRequest();

            //单条添加
            var result = Comm.Helper.DapperHelper.Instance.Execute(sql.ToString(),
                                   new { State = State, PKID = PKID });

            if (result > 0)
            {
                return Ok(new { error = false, code = "1001", message = "操作成功" });
            }
            else
            {
                return Content(HttpStatusCode.OK, new { error = true, code = "1001", message = "操作失败,请重新操作" });
            }

        }
        /// <summary>
        /// 教案
        /// </summary>
        /// <param name="cp"></param>
        /// <returns></returns>
        public IHttpActionResult AddCurriculumTeachingPlan(YY.Edu.Sys.Models.CurriculumTeachingPlan cp)
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
                logs.Error("教案信息添加失败", ex);
                return BadRequest();
            }
        }


        /// <summary>
        /// 总结
        /// </summary>
        /// <param name="cp"></param>
        /// <returns></returns>
        public IHttpActionResult AddCurriculumSummary(YY.Edu.Sys.Models.CurriculumSummary cp)
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
                logs.Error("总结信息添加失败", ex);
                return BadRequest();
            }
        }


        /// <summary>
        /// 购买课时时选择教练
        /// </summary>
        /// <param name="VenueID"></param>
        /// <param name="StudentID"></param>
        /// <returns></returns>
        public IHttpActionResult GetCoachListByHourClass(int VenueID, int StudentID,int PKType)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append("select c.*,'hNumber'=isnull(ClassNumber,0),cv.Price,cv.PriceMore,v.VenueName,ch.PKType from Coach c with(nolock)");
            sql.Append(" inner join Coach_Venue cv with(nolock) on c.CoachID=cv.CoachID");
            sql.Append(" inner join Venue v with(nolock) on cv.VenueID=v.VenueID");
            sql.Append(" left join ClassHoursNumber ch with(nolock) on c.coachID = ch.coachID and ch.StudentID =@StudentID and PKType='"+PKType+"' ");
            sql.Append("where c.state = 1 and cv.VenueID =@VenueID ");
            if (PKType == 1)
            {
                sql.Append(" and cv.Price>0 ");
            }
            else
            {
                sql.Append(" and cv.PriceMore>0 ");
            }
            var query = Comm.Helper.DapperHelper.Instance.Query<CoachHourClassResponse>(sql.ToString(), new { StudentID = StudentID, VenueID = VenueID });
            return Ok(query);

        }
        /// <summary>
        /// 取的教练下的所有课程
        /// </summary>
        /// <param name="CoachID"></param>
        /// <returns></returns>
        public IHttpActionResult GetCoachCurriculum(string query)
        {
            //
            //select T.*,v.VenueName,c.CampusName from  TeachingSchedule t with(nolock) inner join Venue v with(nolock) on t.VenueID=v.VenueID             left join Campus c with(nolock) on t.CampusID = t.CampusID  where PKID in(select PKID from Curriculum c with(nolock) where CoachID = 0 ) and CoachID = 0
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                Comm.RequestModel.RequestModelBase<Sys.Models.Coach> oData = Newtonsoft.Json.JsonConvert.DeserializeObject<Comm.RequestModel.RequestModelBase<Sys.Models.Coach>>(query);


                if (oData.SearchCondition.CoachID <= 0 || oData.PageIndex < 0 || oData.PageSize <= 0)
                    return BadRequest();

                PageCriteria criteria = new PageCriteria();


                criteria.Condition += string.Format("t.PKID in(select PKID from Curriculum with(nolock) where CoachID=" + oData.SearchCondition.CoachID + ") ", oData.SearchCondition.CoachID);
                criteria.Condition += string.Format(" and t.CoachID={0} ", oData.SearchCondition.CoachID);

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
                    criteria.Condition += " and t.State in (1,2,3,4) ";
                }
                else if (oData.RequestType == 3)
                {
                    criteria.Condition +=" and t.State in(5,6,7) ";
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
                criteria.Fields = "t.*,v.VenueName,c.CampusName,co.FullName,'Sucount'=(select COUNT(1) from Curriculum with(nolock) where PKID=t.PKID ) ";
                criteria.PageSize = oData.PageSize;
                criteria.TableName = "TeachingSchedule t with(nolock) inner join Venue v with(nolock) on t.VenueID = v.VenueID left join Campus c with(nolock) on t.CampusID = c.CampusID  inner join Coach co with(nolock) on co.CoachID=t.CoachID  ";
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
                logs.Error("取的教练下的所有课程", ex);
                return BadRequest();
            }
        }


        /// <summary>
        /// 课程 详细信息
        /// </summary>
        /// <param name="PKID"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetCoachCurriculumByID(int PKID)
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

                string sql = "select top 1 t.*,v.VenueName,c.CampusName,'CoachFullName'=co.FullName,'Sucount'=(select COUNT(1) from Curriculum with(nolock) where PKID=t.PKID ),'sqjcount'=(select count(1) from Curriculum c with(nolock) where c.PKID = t.PKID and State = 2 ),PlanContent,SContent  from TeachingSchedule t with(nolock) inner join Venue v with(nolock) on t.VenueID = v.VenueID left join Campus c with(nolock) on t.CampusID =c.CampusID  inner join Coach co with(nolock) on co.CoachID=t.CoachID left join CurriculumTeachingPlan tp with(nolock) on t.PKID=tp.PKID  left join  CurriculumSummary  cs  with(nolock) on t.PKID=cs.PKID where t.PKID=" + PKID;

                var query = Comm.Helper.DapperHelper.Instance.Query<TeachingScheduleResponse>(sql);
                return Ok(query);
            }
            catch (Exception ex)
            {
                logs.Error("课程详细信息", ex);
                return BadRequest();
            }

        }

        [HttpGet]
        /// <summary>
        /// 取的课程下预约的学生
        /// </summary>
        /// <param name="pkid"></param>
        /// <returns></returns>
        public IHttpActionResult GetCurriculumStudentByPKID(int pkid)
        {

            string sql = "select c.CurriculumID,c.PKID,c.CoachID,'CState'=c.State,s.StudentID,s.FullName,s.Mobile,s.ParentFullName,s.ParentMobile,s.HeadUrl from Curriculum c with(nolock) inner join Student s with(nolock) on c.StudentID=s.StudentID  where PKID=@PKID ";
            var query = Comm.Helper.DapperHelper.Instance.Query<StudentCurriculumResponse>(sql, new { PKID = pkid });
            return Ok(query);
        }




        /// <summary>
        ///课程表 学生进行签到或请假（由教练发起）df
        /// </summary>
        /// <param name="State">0预约成功，1上课成功，2学生请假，3老师请假4场馆停课</param>
        /// <param name="StudentID"></param>
        /// <param name="pkid"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult UpdateCurriculumState(int State, string StudentIDs, int pkid, int VenueID, int CoachID)
        {

            if (!ModelState.IsValid)
                return BadRequest();
            string sql = "update Curriculum set State=@State,ModifyTime=GETDATE() where PKID=@PKID and StudentID in(@StudentID); ";

            if (State == 2)
            {
                StringBuilder Tsql = new StringBuilder();
                string[] aryS = StudentIDs.Split(',');
                foreach (string id in aryS)
                {
                    if (id != "")
                    {
                        //1购买，2约课3学生请假退回，4老师请假退回，5学校停课退回
                        Tsql.Append(" INSERT INTO [ClassHoursDetailed]([DType],[VenueID],[CoachID],[StudentID],[Remark],[DNumber]) values(3,'" + VenueID + "','" + CoachID + "','" + id + "','学生请假充回','1') ");
                    }
                }
                sql = sql + Tsql.ToString();

            }
            try
            {
                //Models.VenueInfo venueInfo = Comm.Helper.DapperHelper.Instance.Get<Models.VenueInfo>(cityId);

                var result = Comm.Helper.DapperHelper.Instance.Execute(sql, new { State = State, PKID = pkid, StudentID = StudentIDs });

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
                logs.Error("学生进行签到失败", ex);
                return BadRequest();
            }
        }

        #region 点评

        [HttpPost]
        public IHttpActionResult AddCoachComment(YY.Edu.Sys.Models.CoachComment cm)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
               // Comm.Helper.DapperHelper.Instance.Delete(cm);
                var result = Comm.Helper.DapperHelper.Instance.Insert(cm);

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
                logs.Error("点评", ex);
                return BadRequest();
            }
        }

        [HttpPost]
        public IHttpActionResult UpdateCoachComment(YY.Edu.Sys.Models.CoachComment cm)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                string sql= "update CoachComment set Info=@Info,ModifyTime=getdate() where CommentID=@CommentID";
                // Comm.Helper.DapperHelper.Instance.Delete(cm);
                var result = Comm.Helper.DapperHelper.Instance.Execute(sql, new { Info = cm.Info, CommentID = cm.CommentID });

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
                logs.Error("点评", ex);
                return BadRequest();
            }
        }

        [HttpGet]
        /// <summary>
        /// 取的教练的点评信息
        /// </summary>
        /// <param name="pkid"></param>
        /// <returns></returns>
        public IHttpActionResult GetCoachCommentByPKID(int PKID)
        {

            string sql = "select c.*,'StudentFullName'=s.FullName from CoachComment c with(nolock) inner join Curriculum cm with(nolock) on c.CurriculumID=cm.CurriculumID inner join Student s with(nolock) on c.StudentID=s.StudentID where  c.PKID=@PKID ";
            var query = Comm.Helper.DapperHelper.Instance.Query<CoachCommentResponse>(sql, new { PKID = PKID });
            return Ok(query);
        }

        [HttpGet]
        /// <summary>
        /// 取的教练的对某一学生某一课时的点评信息
        /// </summary>
        /// <param name="pkid"></param>
        /// <returns></returns>
        public IHttpActionResult GetCoachCommentDetail(int StudentID, int CurriculumID)
        {

            string sql = "select * from CoachComment c with(nolock) where  CurriculumID=@CurriculumID and StudentID=@StudentID ";
            var query = Comm.Helper.DapperHelper.Instance.Query<CoachCommentResponse>(sql, new { CurriculumID = CurriculumID, StudentID=StudentID });
            return Ok(query);
        }

        #endregion


        #region  //取的最新10条的课程评价，用于记算教练星级s
        public IHttpActionResult GetCurriculumEvaluateByCoachID(int CoachID)
        {
            string sql = "select top 10 PJID,StarLevel,EffectStarLevel from CurriculumEvaluate ce with(nolock) inner join TeachingSchedule t with(nolock) on ce.pkID = t.PKID where t.CoachID ='"+CoachID+"' order by pjID desc";
            var query = Comm.Helper.DapperHelper.Instance.Query<YY.Edu.Sys.Models.CurriculumEvaluate>(sql);
            return Ok(query);
        }

        public IHttpActionResult GetCurriculumEvaluateByPKID(int pkid)
        {
            string sql = "select PJID,StarLevel,EffectStarLevel from CurriculumEvaluate ce with(nolock) inner join TeachingSchedule t with(nolock) on ce.pkID = t.PKID where t.PKID ='" + pkid + "' order by pjID desc";
            var query = Comm.Helper.DapperHelper.Instance.Query<YY.Edu.Sys.Models.CurriculumEvaluate>(sql);
            return Ok(query);
        }

        #endregion

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

                criteria.Condition += string.Format(" and CoachID={0} and VenueID={1} ", CoachID, VenueID);

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

        #region 工资
        /// <summary>
        /// 获取教练工资，只获取12个月的数据
        /// </summary>
        /// <param name="CoachID"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetWages(int CoachID)
        {
            string sql = " select cw.*,v.VenueName,'Number'=(select count(1) from CoachWages_sub s with(nolock) where s.WagesID=cw.wagesid) from CoachWages cw with(nolock) inner join Venue v with(nolock) on cw.VenueID = v.VenueID where cw.state = 1 and cw.CoachID =@CoachID order by wagesID desc";
            var query = Comm.Helper.DapperHelper.Instance.Query<CoachWagesResponse>(sql, new { CoachID=CoachID });
            return Ok(query);
        }

        [HttpGet]
        /// <summary>
        /// 取得已发放或未发放工资金额
        /// </summary>
        /// <param name="CoachID"></param>
        /// <param name="State">6上课完成，7发工资完成</param>
        /// <returns></returns>
        public IHttpActionResult GetWagesSumMoney(int CoachID,int State)
        {
            string sql = "select isnull(sum(coachPrice),0) from TeachingSchedule t with(nolock) where CoachID=@CoachID and State=@State ";// --6上课完成7发工资完成";
            var query = Comm.Helper.DapperHelper.Instance.Query<decimal>(sql, new { CoachID = CoachID, State= State });
            return Ok(query);
        }

        #endregion

        [HttpGet]
        public IHttpActionResult IsExisitVenueByVCode(string VenueCode)
        {
            string sql = "select VenueID, VenueName from Venue with(nolock) where VenueCode=@VenueCode";
            var query = Comm.Helper.DapperHelper.Instance.Query<YY.Edu.Sys.Models.Venue>(sql, new { VenueCode = VenueCode });

            //链表直接写sql传参

            return Ok(query);
        }

    }
}
