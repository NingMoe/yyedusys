using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using YY.Edu.Sys.Api.Models.ResponseModel;
using YY.Edu.Sys.Api.Models.RequestModel;
using YY.Edu.Sys.Comm.Helper;
using YY.Edu.Sys.Comm.RequestModel;

namespace YY.Edu.Sys.Api.Controllers
{

    [Authorize]
    [RoutePrefix("api/ClassHoursOrder")]
    public class ClassHoursOrderController : BaseController
    {

        private static readonly ILog logs = Comm.Helper.LogHelper.GetInstance();

        [HttpGet]
        public IHttpActionResult Page4Venue(string query)
        {
            try
            {

                if (!ModelState.IsValid)
                    return BadRequest();

                RequestModelBase<ClassHoursOrderRequest> oData = JsonConvert.DeserializeObject<RequestModelBase<ClassHoursOrderRequest>>(query);

                if (oData.PageIndex < 0 || oData.PageSize <= 0)
                    return BadRequest();

                PageCriteria criteria = new PageCriteria();
                criteria.Condition = "1=1";

                //if (!string.IsNullOrWhiteSpace(oData.SearchCondition.StudentMobile))
                //    criteria.Condition += string.Format(" and s.Mobile = '{0}'", oData.SearchCondition.StudentMobile);
                //if (!string.IsNullOrWhiteSpace(oData.SearchCondition.StudentParentMobile))
                //    criteria.Condition += string.Format(" and s.ParentMobile = '{0}'", oData.SearchCondition.StudentParentMobile);
                //if (!string.IsNullOrWhiteSpace(oData.SearchCondition.CoachMobile))
                //    criteria.Condition += string.Format(" and c.Mobile = '{0}'", oData.SearchCondition.CoachMobile);
                if (!string.IsNullOrEmpty(oData.SearchCondition.ChannelOrderID))
                    criteria.Condition += string.Format(" and o.ChannelOrderID = '{0}'", oData.SearchCondition.ChannelOrderID);
                if (oData.SearchCondition.StudentID > 0)
                    criteria.Condition += string.Format(" and o.StudentID = {0}", oData.SearchCondition.StudentID);
                if (oData.SearchCondition.CoachID > 0)
                    criteria.Condition += string.Format(" and o.CoachID = {0}", oData.SearchCondition.CoachID);
                if (oData.SearchCondition.OrderType > -1)
                    criteria.Condition += string.Format(" and o.OrderType = {0}", oData.SearchCondition.OrderType);
                if (oData.SearchCondition.PayState > -1)
                    criteria.Condition += string.Format(" and o.PayState = {0}", oData.SearchCondition.PayState);
                if (!string.IsNullOrEmpty(oData.SearchCondition.StartAddTime))
                    criteria.Condition += string.Format(" and o.AddTime >= '{0}'", oData.SearchCondition.StartAddTime);
                if (!string.IsNullOrEmpty(oData.SearchCondition.EndAddTime))
                    criteria.Condition += string.Format(" and o.AddTime < '{0}'", Convert.ToDateTime(oData.SearchCondition.EndAddTime).AddDays(1));
                if (!string.IsNullOrEmpty(oData.SearchCondition.StartPaySuccessTime))
                    criteria.Condition += string.Format(" and o.PaySuccessTime >= '{0}'", oData.SearchCondition.StartPaySuccessTime);
                if (!string.IsNullOrEmpty(oData.SearchCondition.EndPaySuccessTime))
                    criteria.Condition += string.Format(" and o.PaySuccessTime < '{0}'", Convert.ToDateTime(oData.SearchCondition.EndPaySuccessTime).AddDays(1));

                criteria.CurrentPage = oData.PageIndex + 1;
                criteria.Fields = "[OrderID],[OrderType],[ChannelOrderID],o.[StudentID],[PayMoney],[AddTime],[PayInfo],[PayType],o.[CoachID],[HoursNum],[Remark],[PayState],[PaySuccessTime],s.FullName as StudentFullName,c.FullName as CoachFullName";
                criteria.PageSize = oData.PageSize;
                criteria.TableName = "ClassHoursOrder as o join Student as s on o.StudentID=s.StudentID join Coach as c on o.CoachID=c.CoachID";
                criteria.PrimaryKey = "o.[OrderID]";

                var r = DapperHelper.GetPageData<ClassHoursOrderResponse>(criteria);

                return Ok(new Comm.ResponseModel.ResponseModel4Page<ClassHoursOrderResponse>()
                {
                    data = r.Items,
                    recordsFiltered = r.TotalNum,
                    recordsTotal = r.TotalNum,
                });
            }
            catch (Exception ex)
            {
                logs.Error("试课列表查询失败", ex);
                return BadRequest();
            }
        }

    }
}
