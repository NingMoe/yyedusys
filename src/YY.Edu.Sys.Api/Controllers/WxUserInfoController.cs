﻿using DapperExtensions;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace YY.Edu.Sys.Api.Controllers
{
    [RoutePrefix("api/WxUserInfo")]
    public class WxUserInfoController : ApiController
    {

        private static readonly ILog logs = Comm.Helper.LogHelper.GetInstance();

        // GET: api/WxUserInfo
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/WxUserInfo/5
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public IHttpActionResult Create(Sys.Models.WxUserInfo wxUserInfo)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            try
            {

                wxUserInfo.AddTime = DateTime.Now;
                var result = Comm.Helper.DapperHelper.Instance.Insert(wxUserInfo);

                return result > 0 ? Ok(Comm.ResponseModel.ResponseModelBase.Success()) : Ok(Comm.ResponseModel.ResponseModelBase.SysError());

            }
            catch (Exception ex)
            {
                logs.Error("添加关注用户信息失败", ex);
                return BadRequest();
            }

        }

        // POST: api/WxUserInfo
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/WxUserInfo/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/WxUserInfo/5
        public void Delete(int id)
        {
        }
    }
}