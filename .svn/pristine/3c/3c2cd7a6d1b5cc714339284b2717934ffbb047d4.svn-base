using Dapper;
using DapperExtensions;
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
    [RoutePrefix("api/VenueInfo")]
    public class VenueInfoController : ApiController
    {

        private static readonly ILog logs = Comm.Helper.LogHelper.GetInstance();

        // GET: api/VenueInfo
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        // GET: api/VenueInfo/5
        public IHttpActionResult Get(int id)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            if (id < 0)
                return BadRequest();

            //IList<IPredicate> predList = new List<IPredicate>();
            //predList.Add(Predicates.Field<YY.Edu.Sys.Models.VenueInfo>(f => f.VenueID, Operator.Eq, id));

            YY.Edu.Sys.Models.VenueInfo venueInfo = Comm.Helper.DapperHelper.Instance.Get<YY.Edu.Sys.Models.VenueInfo>(id);

            return Ok(new Comm.ResponseModel.ResponseModel4Res<YY.Edu.Sys.Models.VenueInfo>()
            {
                Info = venueInfo
            });

        }

        [HttpPost]
        public IHttpActionResult Save(YY.Edu.Sys.Models.VenueInfo venueInfo)
        {

            //todo 实体验证
            if (!ModelState.IsValid)
                return BadRequest();

            IPredicate predList = Predicates.Field<YY.Edu.Sys.Models.VenueInfo>(
                f => f.VenueID, Operator.Eq, venueInfo.VenueID
            );

            int count = Comm.Helper.DapperHelper.Instance.Count<YY.Edu.Sys.Models.VenueInfo>(predList);

            return count > 0 ? Edit(venueInfo) : Create(venueInfo);

        }

        [HttpPost]
        public IHttpActionResult Create(YY.Edu.Sys.Models.VenueInfo venueInfo)
        {

            //todo 实体验证
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                venueInfo.AddTime = DateTime.Now;
                var result = Comm.Helper.DapperHelper.Instance.Insert(venueInfo);

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
                logs.Error("场馆信息添加失败", ex);
                return BadRequest();
            }
        }

        [HttpPost]
        public IHttpActionResult Edit(YY.Edu.Sys.Models.VenueInfo venueInfo)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                //Models.VenueInfo venueInfo = Comm.Helper.DapperHelper.Instance.Get<Models.VenueInfo>(cityId);
                venueInfo.ModifyTime = DateTime.Now;
                var result = Comm.Helper.DapperHelper.Instance.Update(venueInfo);

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

        // POST: api/VenueInfo
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/VenueInfo/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/VenueInfo/5
        public void Delete(int id)
        {
        }
    }
}
