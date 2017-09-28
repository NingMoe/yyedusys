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
    [RoutePrefix("api/VenueNotice")]
    public class VenueNoticeController : ApiController
    {
        private static readonly ILog logs = Comm.Helper.LogHelper.GetInstance();

        // GET: api/VenueNotice
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/VenueNotice/5
        public IHttpActionResult Get(int id)
        {
            try
            {
                var result = Comm.Helper.DapperHelper.Instance.Get<Sys.Models.VenueNotice>(id);
                return Ok(new Comm.ResponseModel.ResponseModel4Res<Sys.Models.VenueNotice>()
                {
                    Info = result
                });
            }
            catch (Exception ex)
            {
                logs.Error("公告查询失败", ex);
                return BadRequest();
            }
        }

        [HttpPost]
        public IHttpActionResult Create(Sys.Models.VenueNotice venueNoticeInfo)
        {

            logs.Info(Newtonsoft.Json.JsonConvert.SerializeObject(venueNoticeInfo));

            if (!ModelState.IsValid)
                return BadRequest();

            try
            {

                venueNoticeInfo.State = 1;
                var result = Comm.Helper.DapperHelper.Instance.Insert(venueNoticeInfo);

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
                logs.Error("通知添加失败", ex);
                return BadRequest();
            }

        }

        [HttpPost]
        public IHttpActionResult Reply(Sys.Models.VenueNotice venueNoticeInfo)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var result = Comm.Helper.DapperHelper.Instance.Update(venueNoticeInfo);

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
                logs.Error("通知回复失败", ex);
                return BadRequest();
            }

        }

        [HttpGet]
        public IHttpActionResult Page(string query)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            Comm.RequestModel.RequestModelBase<Sys.Models.VenueNotice> oData = Newtonsoft.Json.JsonConvert.DeserializeObject<Comm.RequestModel.RequestModelBase<Sys.Models.VenueNotice>>(query);

            if (oData.PageIndex < 0 || oData.PageSize <= 0 || oData.SearchCondition == null||oData.SearchCondition.VenueID<=0)
                return BadRequest();

            try
            {

                IList<IPredicate> predList = new List<IPredicate>();

                predList.Add(Predicates.Field<Sys.Models.VenueNotice>(f => f.VenueID, Operator.Eq, oData.SearchCondition.VenueID));
                if (oData.SearchCondition.State > -1)
                    predList.Add(Predicates.Field<Sys.Models.VenueNotice>(f => f.State, Operator.Eq, oData.SearchCondition.State));

                IPredicateGroup predGroup = Predicates.Group(GroupOperator.And, predList.ToArray());

                var result = Comm.Helper.DapperHelper.Instance.GetPage<Sys.Models.VenueNotice>(
                    predGroup,
                    new List<ISort>() {
                        new Sort { PropertyName = "NoticeId", Ascending = false},
                    },
                    oData.PageIndex,
                    oData.PageSize
                );

                long allRowsCount = Comm.Helper.DapperHelper.Instance.Count<YY.Edu.Sys.Models.VenueNotice>(predGroup);

                return Ok(new Comm.ResponseModel.ResponseModel4Page<YY.Edu.Sys.Models.VenueNotice>()
                {
                    data = result.AsList(),
                    recordsFiltered = allRowsCount,
                    recordsTotal = allRowsCount,
                });

            }
            catch (Exception ex)
            {
                logs.Error("公告分页查询失败", ex);
                return BadRequest();
            }
        }

        // POST: api/VenueNotice
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/VenueNotice/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/VenueNotice/5
        public void Delete(int id)
        {
        }
    }
}
