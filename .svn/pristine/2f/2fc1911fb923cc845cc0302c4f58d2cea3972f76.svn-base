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
    [RoutePrefix("api/Campus")]
    public class CampusController : ApiController
    {
        private static readonly ILog logs = Comm.Helper.LogHelper.GetInstance();

        // GET: api/Campus
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        // GET: api/Campus/5
        public IHttpActionResult Get(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (id < 0)
                return BadRequest();

            try
            {

                var sql = @"select c.CampusID,c.CampusName,c.LinkMan,c.LinkTel,c.CampusStatus,c.Address,c.CityID,ci.CityName from Campus as c join City as ci on c.CityID=ci.CityID
where c.CampusID=@campusID";

                var result = Comm.Helper.DapperHelper.Instance.Query<Models.ResponseModel.CampusResponse, Sys.Models.City, Models.ResponseModel.CampusResponse>(sql,
                                        splitOn: "CityName",
                                        param: new { campusID = id },
                                        map: (campus, city) =>
                                        {
                                            campus.CityName = city.CityName; return campus;
                                        });


                return Ok(new Comm.ResponseModel.ResponseModel4Res<Models.ResponseModel.CampusResponse>()
                {
                    Info = result.AsList().FirstOrDefault()
                });
            }
            catch (Exception ex)
            {
                logs.Error("校区查询失败", ex);
                return BadRequest();
            }
        }

        [HttpGet]
        // GET: api/Campus/5
        public IHttpActionResult Page(int venueId)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            if (venueId < 0)
                return BadRequest();

            try
            {

                var sql = @"select c.CampusID,c.CampusName,c.LinkMan,c.LinkTel,c.CampusStatus,ci.CityName from Campus as c join City as ci on c.CityID=ci.CityID
where c.VenueID=@venueId ";

                var result = Comm.Helper.DapperHelper.Instance.Query<Models.ResponseModel.CampusResponse, Sys.Models.City, Models.ResponseModel.CampusResponse>(sql,
                                        splitOn: "CityName",
                                        param: new { venueId = venueId },
                                        map: (campus, city) =>
                                        {
                                            campus.CityName = city.CityName; return campus;
                                        });

                return Ok(new Comm.ResponseModel.ResponseModel4Res<Models.ResponseModel.CampusResponse>()
                {
                    data = result.AsList()
                });
            }
            catch (Exception ex)
            {
                logs.Error("校区page查询失败", ex);
                return BadRequest();
            }
        }

        [HttpPost]
        public IHttpActionResult Create(Sys.Models.Campus campusInfo)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            try
            {

                campusInfo.CampusStatus = 1;
                var result = Comm.Helper.DapperHelper.Instance.Insert(campusInfo);

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
                logs.Error("校区信息添加失败", ex);
                return BadRequest();
            }

        }

        [HttpPost]
        public IHttpActionResult Edit(YY.Edu.Sys.Models.Campus campusInfo)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                var result = Comm.Helper.DapperHelper.Instance.Update(campusInfo);

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
                logs.Error("校区信息编辑失败", ex);
                return BadRequest();
            }

        }

        // POST: api/Campus
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Campus/5
        public void Put(int id, [FromBody]string value)
        {
        }

        [HttpPost]
        // DELETE: api/Campus/5
        public IHttpActionResult SetDisable(dynamic obj)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                int id = obj.id;
                YY.Edu.Sys.Models.Campus result = Comm.Helper.DapperHelper.Instance.Get<YY.Edu.Sys.Models.Campus>(id);
                result.CampusStatus = 2;
                bool update = Comm.Helper.DapperHelper.Instance.Update<YY.Edu.Sys.Models.Campus>(result);
                
                if (update)
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
                logs.Error("校区停用失败", ex);
                return BadRequest();
            }
        }

        [HttpPost]
        public IHttpActionResult SetEnable(dynamic obj)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            try
            {
                int id = obj.id;

                YY.Edu.Sys.Models.Campus result = Comm.Helper.DapperHelper.Instance.Get<YY.Edu.Sys.Models.Campus>(id);
                result.CampusStatus = 1;
                bool update = Comm.Helper.DapperHelper.Instance.Update<YY.Edu.Sys.Models.Campus>(result);

                if (update)
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
                logs.Error("校区启用失败", ex);
                return BadRequest();
            }
        }

    }
}
