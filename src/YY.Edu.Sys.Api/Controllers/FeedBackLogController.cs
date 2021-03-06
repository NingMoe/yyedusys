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
using YY.Edu.Sys.Models;

namespace YY.Edu.Sys.Api.Controllers
{
    [Authorize]
    [RoutePrefix("api/FeedBackLog")]
    public class FeedBackLogController : BaseController
    {
        private static readonly ILog logs = Comm.Helper.LogHelper.GetInstance();

        // GET: api/FeedBackLog
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        [AllowAnonymous]
        // GET: api/VenueNotice/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                var result = Comm.Helper.DapperHelper.Instance.Get<Sys.Models.FeedBackLog>(id);
                return Ok(new Comm.ResponseModel.ResponseModel4Res<Sys.Models.FeedBackLog>()
                {
                    Info = result
                });
            }
            catch (Exception ex)
            {
                logs.Error("反馈查询失败", ex);
                return BadRequest();
            }
        }
        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult Create(Sys.Models.FeedBackLog feedBackLogInfo)
        {

            logs.Info(Newtonsoft.Json.JsonConvert.SerializeObject(feedBackLogInfo));

            if (!ModelState.IsValid)
                return Ok(Comm.ResponseModel.ResponseModelBase.GetRes(ValidationMsg));

            try
            {

                var result = Comm.Helper.DapperHelper.Instance.Insert(feedBackLogInfo);

                if (result > 0)
                {
                    return Ok(Comm.ResponseModel.ResponseModelBase.Success());
                }
                else
                {
                    return Ok(Comm.ResponseModel.ResponseModelBase.SysError());
                }

            }
            catch (Exception ex)
            {
                logs.Error("反馈添加失败", ex);
                return BadRequest();
            }

        }

        [HttpPost]
        public IHttpActionResult Reply(Sys.Models.FeedBackLog feedBackLogInfo)
        {

            if (!ModelState.IsValid)
                return Ok(Comm.ResponseModel.ResponseModelBase.GetRes(ValidationMsg));

            if (string.IsNullOrWhiteSpace(feedBackLogInfo.ReplyMsg))
                return Ok(Comm.ResponseModel.ResponseModelBase.GetRes("请输入反馈内容"));

            try
            {

                var model = Comm.Helper.DapperHelper.Instance.Get<Sys.Models.FeedBackLog>(feedBackLogInfo.FDId);
                model.ReplyMsg = feedBackLogInfo.ReplyMsg;
                model.ReplyTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                model.State = 1;
                var result = Comm.Helper.DapperHelper.Instance.Update(model);

                if (result)
                {
                    return Ok(Comm.ResponseModel.ResponseModelBase.Success());
                }
                else
                {
                    return Ok(Comm.ResponseModel.ResponseModelBase.SysError());
                }
            }
            catch (Exception ex)
            {
                logs.Error("反馈回复失败", ex);
                return BadRequest();
            }

        }
        [AllowAnonymous]
        [HttpGet]
        public IHttpActionResult Page(string query)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            try
            {

                Comm.RequestModel.RequestModelBase<Sys.Models.FeedBackLog> oData = Newtonsoft.Json.JsonConvert.DeserializeObject<Comm.RequestModel.RequestModelBase<Sys.Models.FeedBackLog>>(query);

                if (oData.PageIndex < 0 || oData.PageSize <= 0 || oData.SearchCondition == null || oData.SearchCondition.VenueID <= 0)
                    return BadRequest();

                PageCriteria criteria = new PageCriteria();
                criteria.Condition = "1=1";

                if (oData.SearchCondition.VenueID > 0)
                    criteria.Condition += string.Format(" and f.VenueID = {0}", oData.SearchCondition.VenueID);
                if (oData.SearchCondition.StudentID > 0)
                    criteria.Condition += string.Format(" and f.[StudentID] = {0}", oData.SearchCondition.StudentID);
                if (oData.SearchCondition.State > -1)
                    criteria.Condition += string.Format(" and f.[State] = {0}", oData.SearchCondition.State);
                //if (!string.IsNullOrEmpty(oData.SearchCondition.UserName))
                //    criteria.Condition += string.Format(" and c.UserName = '{0}'", oData.SearchCondition.UserName);
                if (!string.IsNullOrEmpty(oData.SearchCondition.FullName))
                    criteria.Condition += string.Format(" and s.FullName like '%{0}%'", oData.SearchCondition.FullName);

                criteria.CurrentPage = oData.PageIndex + 1;//adminlte 加载的datatable起始页为0
                criteria.Fields = "[FDId],f.[UserId],f.[UserName],[FDMsg],f.[AddTime],f.[VenueID],f.[StudentID],s.fullname as [FullName],f.[State],[ReplyMsg],[ReplyTime]";
                criteria.PageSize = oData.PageSize;
                criteria.TableName = "[dbo].[FeedBackLog] as f join student as s on f.studentid=s.studentid";
                criteria.PrimaryKey = "f.[FDId]";

                var r = Comm.Helper.DapperHelper.GetPageData<YY.Edu.Sys.Models.FeedBackLog>(criteria);

                return Ok(new Comm.ResponseModel.ResponseModel4Page<YY.Edu.Sys.Models.FeedBackLog>()
                {
                    data = r.Items,
                    recordsFiltered = r.TotalNum,
                    recordsTotal = r.TotalNum,
                });

                //IList<IPredicate> predList = new List<IPredicate>();

                //predList.Add(Predicates.Field<Sys.Models.FeedBackLog>(f => f.VenueID, Operator.Eq, oData.SearchCondition.VenueID));
                //if (oData.SearchCondition.StudentID > 0)
                //    predList.Add(Predicates.Field<Sys.Models.FeedBackLog>(f => f.StudentID, Operator.Eq, oData.SearchCondition.StudentID));
                //if (!string.IsNullOrWhiteSpace(oData.SearchCondition.FullName))
                //    predList.Add(Predicates.Field<Sys.Models.FeedBackLog>(f => f.FullName, Operator.Like, "%" + oData.SearchCondition.FullName + "%"));
                //if (oData.SearchCondition.State > 0)
                //    predList.Add(Predicates.Field<Sys.Models.FeedBackLog>(f => f.State, Operator.Eq, oData.SearchCondition.State));

                //IPredicateGroup predGroup = Predicates.Group(GroupOperator.And, predList.ToArray());

                //var result = Comm.Helper.DapperHelper.Instance.GetPage<Sys.Models.FeedBackLog>(
                //    predGroup,
                //    new List<ISort>() {
                //        new Sort { PropertyName = "FDId", Ascending = false},
                //    },
                //    oData.PageIndex,
                //    oData.PageSize
                //);

                //long allRowsCount = Comm.Helper.DapperHelper.Instance.Count<YY.Edu.Sys.Models.FeedBackLog>(predGroup);

                //return Ok(new Comm.ResponseModel.ResponseModel4Page<YY.Edu.Sys.Models.FeedBackLog>()
                //{
                //    data = result.AsList(),
                //    recordsFiltered = allRowsCount,
                //    recordsTotal = allRowsCount,
                //});

            }
            catch (Exception ex)
            {
                logs.Error("公告分页查询失败", ex);
                return BadRequest();
            }
        }

        // POST: api/FeedBackLog
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/FeedBackLog/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/FeedBackLog/5
        public void Delete(int id)
        {
        }


        #region  //前台
        [AllowAnonymous]
        [HttpGet]
        //取得学生的留言信息
        public IHttpActionResult GetFeedBackLog(string query)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                Comm.RequestModel.RequestModelBase<Sys.Models.Student> oData = Newtonsoft.Json.JsonConvert.DeserializeObject<Comm.RequestModel.RequestModelBase<Sys.Models.Student>>(query);

                if (oData.SearchCondition.StudentID <= 0 || oData.PageIndex < 0 || oData.PageSize <= 0)
                    return BadRequest();

                PageCriteria criteria = new PageCriteria();

                criteria.Condition += string.Format("StudentID={0}", oData.SearchCondition.StudentID);


                criteria.CurrentPage = oData.PageIndex;
                criteria.Fields = "* ";
                criteria.PageSize = oData.PageSize;
                criteria.TableName = "Feedbacklog";
                criteria.PrimaryKey = "FDid";

                criteria.Sort = "FDid";

                var r = Comm.Helper.DapperHelper.GetPageData<FeedBackLog>(criteria);

                return Ok(new Comm.ResponseModel.ResponseModel4Page<FeedBackLog>()
                {
                    data = r.Items,
                    recordsFiltered = r.TotalNum,
                    recordsTotal = r.TotalNum,
                });

            }
            catch (Exception ex)
            {
                logs.Error("取得学生的留言信息", ex);
                return BadRequest();
            }
        }
        #endregion
    }
}
