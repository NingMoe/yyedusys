using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using YY.Edu.Sys.Comm.Helper;

namespace YY.Edu.Sys.Api.Controllers
{

    [Authorize]
    [RoutePrefix("api/ClassHoursDetailed")]
    public class ClassHoursDetailedController : ApiController
    {

        private static readonly log4net.ILog logs = Comm.Helper.LogHelper.GetInstance();

        /// <summary>
        /// 收入流水查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Page4VenueIncome(string query)
        {
            try
            {

                if (!ModelState.IsValid)
                    return BadRequest();

                Comm.RequestModel.RequestModelBase<Models.RequestModel.ClassHoursDetailedRequest> oData = Newtonsoft.Json.JsonConvert.DeserializeObject<Comm.RequestModel.RequestModelBase<Models.RequestModel.ClassHoursDetailedRequest>>(query);

                if (oData.SearchCondition.VenueID <= 0 || oData.PageIndex < 0 || oData.PageSize <= 0)
                    return BadRequest();

                PageCriteria criteria = new PageCriteria();
                criteria.Condition = "1=1";

                criteria.Condition += string.Format(" and c.VenueID = {0}", oData.SearchCondition.VenueID);
                //查询购买课时和约课的流水记录
                criteria.Condition += string.Format(" and c.DType in ({0}) ", "1,2");

                if (oData.SearchCondition.CoachID > 0)
                    criteria.Condition += string.Format(" and CoachID = {0}", oData.SearchCondition.CoachID);
                if (!string.IsNullOrEmpty(oData.SearchCondition.StartDate))
                    criteria.Condition += string.Format(" and AddTime >= '{0}'", oData.SearchCondition.StartDate);
                if (!string.IsNullOrEmpty(oData.SearchCondition.EndDate))
                    criteria.Condition += string.Format(" and AddTime < '{0}'", Convert.ToDateTime(oData.SearchCondition.EndDate).AddDays(1));

                criteria.CurrentPage = oData.PageIndex + 1;//adminlte 加载的datatable起始页为0
                criteria.Fields = "[DetailedID],[DType],c.[VenueID],c.[CoachID],c.[StudentID],s.FullName as studentfullname,c2.FullName as fullname,c.[AddTime],[Remark],[DNumber],[CMoney],[PKType]";
                criteria.PageSize = oData.PageSize;
                criteria.TableName = "[dbo].[ClassHoursDetailed] as c left join Student as s on c.studentid=s.studentid left join coach as c2 on c.CoachID=c2.CoachID";
                criteria.PrimaryKey = "c.[DetailedID]";

                var r = DapperHelper.GetPageData<Models.ResponseModel.ClassHoursDetailedResponse>(criteria);

                return Ok(new Comm.ResponseModel.ResponseModel4Page<Models.ResponseModel.ClassHoursDetailedResponse>()
                {
                    data = r.Items,
                    recordsFiltered = r.TotalNum,
                    recordsTotal = r.TotalNum,
                });
            }
            catch (Exception ex)
            {
                logs.Error("收入流水查询失败", ex);
                return BadRequest();
            }
        }

    }
}
