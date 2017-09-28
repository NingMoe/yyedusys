using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Dapper;
using System.Web.Http.Cors;
using DapperExtensions;
using log4net;

namespace YY.Edu.Sys.Api.Controllers
{

    /// <summary>
    /// 路由前缀  
    /// 访问 http://localhost:53262/api/city 默认请求无参Get方法
    /// 访问 http://localhost:53262/api/city/1 请求一个参数的Get方法
    /// 参考restful api 设计原则，资源的概念 http://www.ruanyifeng.com/blog/2014/05/restful_api.html
    /// </summary>
    [RoutePrefix("api/City")]
    //[EnableCors(origins: "http://localhost:37315/", headers: "*", methods: "GET,POST,PUT,DELETE")]
    public class CityController : ApiController
    {

        private static readonly ILog logs = Comm.Helper.LogHelper.GetInstance();

        [HttpGet]
        // GET api/<controller>
        public IHttpActionResult Get()
        {
            //传参 http://www.cnblogs.com/landeanfen/p/5337072.html
            //dynamic

            //返回值 http://www.cnblogs.com/landeanfen/p/5501487.html
            //1 return Json<obj>;
            //2 return Ok<obj>;
            //3 return NotFound<obj>;
            //4 return Content<HttpStatusCode.OK,obj>;
            //5 return BadRequest(); 返回400错误
            var query = Comm.Helper.DapperHelper.Instance.Query<YY.Edu.Sys.Models.City>("select CityID,CityName from city where 1=1");

            return Ok(new Comm.ResponseModel.ResponseModel4Res<YY.Edu.Sys.Models.City>()
            {
                data = query.AsList(),
            });
        }

        [HttpGet]
        public IHttpActionResult Page(string query)
        {
            try
            {

                if (!ModelState.IsValid)
                    return BadRequest();

                Comm.RequestModel.RequestModelBase<Sys.Models.City> oData = Newtonsoft.Json.JsonConvert.DeserializeObject<Comm.RequestModel.RequestModelBase<Sys.Models.City>>(query);

                //System.Web.HttpContextBase context = (System.Web.HttpContextBase)Request.Properties["MS_HttpContext"];//获取传统context
                //System.Web.HttpRequestBase request = context.Request;//定义传统request对象
                //string name = Comm.Helper.ParamHelper<string>.GetParam(request["cityName"], "");
                //int cityId = Comm.Helper.ParamHelper<int>.GetParam(request["cityId"], 0);
                //int start = Comm.Helper.ParamHelper<int>.GetParam(request["start"], 0);
                //int length = Comm.Helper.ParamHelper<int>.GetParam(request["length"], 0);

                if (oData.PageIndex < 0 || oData.PageSize <= 0)
                    return BadRequest();

                IList<ISort> sort = new List<ISort>();
                sort.Add(new Sort { PropertyName = "CityID", Ascending = false });

                long allRowsCount = 0;

                /*
                 GT: Greater Than , > 
                GE: Greater than or Equivalent with , >= 
                LT: Less than, < 
                LE: Less than or Equivalent with, <= 
                EQ: EQuivalent with, == 
                NE: Not Equivalent with, /= 
                 */

                /*
                 var pg = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                pg.Predicates.Add(Predicates.Field<Person>(f => f.Active, Operator.Eq, true));
                pg.Predicates.Add(Predicates.Field<Person>(f => f.LastName, Operator.Like, "Br%"));
                IEnumerable<Person> list = cn.GetList<Person>(pg);
                 */
                IList<IPredicate> predList = new List<IPredicate>();

                if (oData.SearchCondition != null)
                {
                    if (!string.IsNullOrWhiteSpace(oData.SearchCondition.CityName))
                        predList.Add(Predicates.Field<YY.Edu.Sys.Models.City>(f => f.CityName, Operator.Like, "%" + oData.SearchCondition.CityName + "%"));
                    if (oData.SearchCondition.CityID > 0)
                        predList.Add(Predicates.Field<YY.Edu.Sys.Models.City>(f => f.CityID, Operator.Le, oData.SearchCondition.CityID));
                }

                IPredicateGroup predGroup = Predicates.Group(GroupOperator.And, predList.ToArray());

                var result = Comm.Helper.DapperHelper.Instance.GetPage<YY.Edu.Sys.Models.City>(predGroup, sort, oData.PageIndex, oData.PageSize);
                allRowsCount = Comm.Helper.DapperHelper.Instance.Count<YY.Edu.Sys.Models.City>(predGroup);

                return Ok(new Comm.ResponseModel.ResponseModel4Page<YY.Edu.Sys.Models.City>()
                {
                    data = result.AsList(),
                    recordsFiltered = allRowsCount,
                    recordsTotal = allRowsCount,
                });

            }
            catch (Exception ex)
            {
                logs.Error("城市分页查询失败", ex);
                return BadRequest();
            }
        }

        [HttpGet]
        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            //查询
            var query = Comm.Helper.DapperHelper.Instance.Query<YY.Edu.Sys.Models.City>("select * from city where CityID=@id", new { Id = 1 });
            return Ok(query);
            //return Json(query.ToString());
        }

        [HttpPost]
        // POST api/<controller>
        public void Post([FromBody]string value)
        {

        }

        [HttpPost]
        public IHttpActionResult Add(YY.Edu.Sys.Models.City city)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            //单条添加
            var result = Comm.Helper.DapperHelper.Instance.Execute("Insert into city values (@cityname)",
                                   new { CityName = city.CityName });

            if (result > 0)
            {
                return Ok(new { error = false, code = "1000", message = "添加成功" });
            }
            else
            {
                return Content(HttpStatusCode.OK, new { error = true, code = "1001", message = "添加失败" });
            }
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}