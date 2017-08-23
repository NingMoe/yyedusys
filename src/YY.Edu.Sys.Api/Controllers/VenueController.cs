using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Dapper;
using log4net;
using DapperExtensions;

namespace YY.Edu.Sys.Api.Controllers
{
    [RoutePrefix("api/Venue")]
    public class VenueController : ApiController
    {

        private static readonly ILog logs = Comm.Helper.LogHelper.GetInstance();

        // GET: api/Venue
        public IEnumerable<string> Get()
        {
            ILog logs = LogManager.GetLogger(typeof(VenueController));

            logs.Fatal("Excption:这里就是要提示的LOG信息");

            logs.Error("error info");

            return new string[] { "value1", "value2" };
        }

        [HttpGet]
        // GET: api/Venue/5
        public IHttpActionResult Get(int id)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            if (id < 0)
                return BadRequest();

            //查询
            var predicate = Predicates.Field<YY.Edu.Sys.Models.Venue>(f => f.VenueID, Operator.Eq, id);
            IEnumerable<YY.Edu.Sys.Models.Venue> list = Comm.Helper.DapperHelper.Instance.GetList<YY.Edu.Sys.Models.Venue>(predicate);

            return Ok(new Comm.ResponseModel.ResponseModel4Res<YY.Edu.Sys.Models.Venue>()
            {
                data = list.AsList(),
            });

        }

        [HttpPost]
        // POST: api/Venue
        public IHttpActionResult Create(YY.Edu.Sys.Models.Venue venue)
        {

            //todo 实体验证
            if (!ModelState.IsValid)
                return BadRequest();

            //单条添加
            //            var result = Comm.Helper.DapperHelper.Instance.Execute(@"INSERT INTO [dbo].[Venue] ([UserName],[Pwd],[VenueCode],[VenueName],[VenueAddress],[LinkMan],[LinkManMobile],[LinkManWX],[VenueFax],[LegalPerson],[CardNumber],[AddTime],[BusinessLicense],[LogoUrl],[AddUser],[Status],[SystemRoleIDS]) VALUES 
            //(@UserName,@Pwd,@VenueCode,@VenueName,@VenueAddress,@LinkMan,@LinkManMobile,@LinkManWX,@VenueFax,@LegalPerson,@CardNumber,@AddTime,@BusinessLicense,@LogoUrl,@AddUser,@Status,@SystemRoleIDS)",
            //                                   venue);
            try
            {

                var result = Comm.Helper.DapperHelper.Instance.Insert(venue);

                if (result > 0)
                {

                    venue.VenueID = result;
                    venue.VenueCode = new Services.VenueService().GenVenueCode(venue);
                    Comm.Helper.DapperHelper.Instance.Update(venue);

                    return Ok(new Comm.ResponseModel.ResponseModel4Res<string>()
                    {
                        Info = venue.VenueCode,
                    });
                }
                else
                {
                    return Content(HttpStatusCode.OK, Comm.ResponseModel.ResponseModelBase.GetRes(Comm.ResponseModel.ResponseModelErrorEnum.SystemError));
                }
            }
            catch (Exception ex)
            {
                logs.Error("场馆添加失败", ex);
                return BadRequest();
            }
        }

        // PUT: api/Venue/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE: api/Venue/5
        public void Delete(int id)
        {
        }
    }
}
