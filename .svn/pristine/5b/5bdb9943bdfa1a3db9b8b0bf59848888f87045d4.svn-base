using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using Dapper;
using log4net;
using DapperExtensions;
using YY.Edu.Sys.Comm.Helper;
using System.Net.Http;

namespace YY.Edu.Sys.Api.Controllers
{
    [Authorize]
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

        /// <summary>
        /// 获取PC场馆系统我的信息
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetMe(string email)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            if (string.IsNullOrEmpty(email))
                return BadRequest();

            try
            {
                return Ok(new Comm.ResponseModel.ResponseModel4Res<Sys.Models.PCLoginUser>()
                {
                    Info = Services.VenueService.GetMe(email),
                });
            }
            catch (Comm.YYException.YYException ex)
            {
                return Ok(Comm.ResponseModel.ResponseModelBase.GetRes(ex.Message));
            }
            catch (Exception ex)
            {
                logs.Error("场馆获取我的信息失败", ex);
                return BadRequest();
            }
        }

        [HttpPost]
        // POST: api/Venue
        public async System.Threading.Tasks.Task<IHttpActionResult> Create()
        {
            //http://blog.csdn.net/starfd/article/details/48652871
            //todo 实体验证
            if (!ModelState.IsValid)
                return BadRequest();

            if (!Request.Content.IsMimeMultipartContent())
                return BadRequest();

            Dictionary<string, string> dic = new Dictionary<string, string>();
            string root = System.Web.HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new Providers.MultipartFileWithExtensionStreamProvider(root);
            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                Sys.Models.Venue venue = Newtonsoft.Json.JsonConvert.DeserializeObject<Sys.Models.Venue>(provider.FormData["venueInfo"]);
                venue.BusinessLicense = provider.FileData[0].LocalFileName;

                var result = Comm.Helper.DapperHelper.Instance.Insert(venue);

                if (result > 0)
                {

                    venue.VenueID = result;
                    venue.VenueCode = new Services.VenueService().GenVenueCode(venue);
                    Comm.Helper.DapperHelper.Instance.Update(venue);

                    return Ok(Comm.ResponseModel.ResponseModelBase.Success());

                }
                else
                {
                    return Ok(Comm.ResponseModel.ResponseModelBase.SysError());
                }

            }
            catch (Exception ex)
            {
                logs.Error("场馆添加失败", ex);
                return BadRequest();
            }

        }

        /// <summary>
        /// 场馆分页查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Page(string query)
        {
            try
            {

                if (!ModelState.IsValid)
                    return BadRequest();

                Comm.RequestModel.RequestModelBase<Sys.Models.Venue> oData = Newtonsoft.Json.JsonConvert.DeserializeObject<Comm.RequestModel.RequestModelBase<Sys.Models.Venue>>(query);

                if (oData.PageIndex < 0 || oData.PageSize <= 0)
                    return BadRequest();

                PageCriteria criteria = new PageCriteria();
                criteria.Condition = "1=1";

                if (!string.IsNullOrWhiteSpace(oData.SearchCondition.LinkManMobile))
                    criteria.Condition += string.Format(" and v.LinkManMobile = {0}", oData.SearchCondition.LinkManMobile);
                if (!string.IsNullOrWhiteSpace(oData.SearchCondition.LinkMan))
                    criteria.Condition += string.Format(" and v.LinkMan like '%{0}%'", oData.SearchCondition.LinkMan);
                if (!string.IsNullOrWhiteSpace(oData.SearchCondition.VenueName))
                    criteria.Condition += string.Format(" and v.VenueName like '%{0}%'", oData.SearchCondition.VenueName);

                criteria.CurrentPage = oData.PageIndex + 1;//adminlte 加载的datatable起始页为0
                criteria.Fields = "v.[VenueID],v.[CityID],v.[UserName],v.[VenueCode],v.[VenueName],v.[VenueAddress],v.[LinkMan],v.[LinkManMobile],v.[LinkManWX],v.[VenueFax],v.[LegalPerson],v.[CardNumber],v.[AddTime],v.[BusinessLicense],v.[LogoUrl],v.[AddUser],v.[Status],v.[SystemRoleIDS]";
                criteria.PageSize = oData.PageSize;
                criteria.TableName = "Venue as v";
                criteria.PrimaryKey = "v.VenueID";

                var r = Comm.Helper.DapperHelper.GetPageData<Sys.Models.Venue>(criteria);

                return Ok(new Comm.ResponseModel.ResponseModel4Page<Sys.Models.Venue>()
                {
                    data = r.Items,
                    recordsFiltered = r.TotalNum,
                    recordsTotal = r.TotalNum,
                });
            }
            catch (Exception ex)
            {
                logs.Error("场馆查询失败", ex);
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
