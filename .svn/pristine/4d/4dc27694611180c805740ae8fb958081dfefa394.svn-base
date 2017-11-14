using Dapper;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using YY.Edu.Sys.Api.Models.RequestModel;
using YY.Edu.Sys.Comm.Helper;
using YY.Edu.Sys.Comm.RequestModel;
using YY.Edu.Sys.Models;

namespace YY.Edu.Sys.Api.Controllers
{

    [Authorize]
    [RoutePrefix("api/ApplyTryoutCurriculum")]
    public class ApplyTryoutCurriculumController : BaseController
    {

        private static readonly ILog logs = Comm.Helper.LogHelper.GetInstance();

        [HttpGet]
        public IHttpActionResult Page4Venue(string query)
        {
            try
            {

                if (!ModelState.IsValid)
                    return BadRequest();

                RequestModelBase<ApplyTryoutCurriculumRequest> oData = JsonConvert.DeserializeObject<RequestModelBase<ApplyTryoutCurriculumRequest>>(query);

                if (oData.PageIndex < 0 || oData.PageSize <= 0)
                    return BadRequest();

                PageCriteria criteria = new PageCriteria();
                criteria.Condition = "1=1";

                if (!string.IsNullOrWhiteSpace(oData.SearchCondition.StudentMobile))
                    criteria.Condition += string.Format(" and s.Mobile = '{0}'", oData.SearchCondition.StudentMobile);
                if (!string.IsNullOrWhiteSpace(oData.SearchCondition.StudentParentMobile))
                    criteria.Condition += string.Format(" and s.ParentMobile = '{0}'", oData.SearchCondition.StudentParentMobile);
                if (!string.IsNullOrWhiteSpace(oData.SearchCondition.CoachMobile))
                    criteria.Condition += string.Format(" and c.Mobile = '{0}'", oData.SearchCondition.CoachMobile);
                if (oData.SearchCondition.StudentID > 0)
                    criteria.Condition += string.Format(" and a.StudentID = {0}", oData.SearchCondition.StudentID);
                if (oData.SearchCondition.coachID > 0)
                    criteria.Condition += string.Format(" and a.CoachID = {0}", oData.SearchCondition.coachID);
                if (oData.SearchCondition.TryoutState > -1)
                    criteria.Condition += string.Format(" and a.TryoutState = {0}", oData.SearchCondition.TryoutState);
                if (!string.IsNullOrEmpty(oData.SearchCondition.StartAddTime))
                    criteria.Condition += string.Format(" and a.AddTime >= '{0}'", oData.SearchCondition.StartAddTime);
                if (!string.IsNullOrEmpty(oData.SearchCondition.EndAddTime))
                    criteria.Condition += string.Format(" and a.AddTime < '{0}'", Convert.ToDateTime(oData.SearchCondition.EndAddTime).AddDays(1));

                criteria.CurrentPage = oData.PageIndex + 1;
                criteria.Fields = "[id],[TSMID],[AddTime],[TryoutState],[AduitTime],[PayState],[PayMoney],[PayTime],[PKID],a.[StudentID],s.FullName as StudentFullName,s.ParentFullName as StudentParentFullName,s.Mobile as StudentMobile,s.ParentMobile as StudentParentMobile,[TryoutMoney],a.[coachID],c.FullName as CoachFullName,c.Mobile as CoachMobile";
                criteria.PageSize = oData.PageSize;
                criteria.TableName = "[dbo].[ApplyTryoutCurriculum] as a join Student as s on a.studentid=s.StudentID join Coach as c on a.coachID=c.CoachID";
                criteria.PrimaryKey = "a.id";

                var r = DapperHelper.GetPageData<Models.ResponseModel.ApplyTryoutCurriculumResponse>(criteria);

                return Ok(new Comm.ResponseModel.ResponseModel4Page<Models.ResponseModel.ApplyTryoutCurriculumResponse>()
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


        [HttpPost]
        public IHttpActionResult ChangeState(dynamic applyTryoutCurriculum)
        {

            try
            {

                if (!ModelState.IsValid)
                    return BadRequest();

                if (applyTryoutCurriculum == null || applyTryoutCurriculum.Id <= 0)
                    return BadRequest();

                //if (!IsExist(Convert.ToInt32(applyBuyHours.ApplyID), Convert.ToInt32(applyBuyHours.VenueID)))
                //    return Ok(Comm.ResponseModel.ResponseModelBase.GetRes("场馆下没有要审核的支付记录"));

                var sql = "UPDATE [dbo].[ApplyTryoutCurriculum] SET [TryoutState] =1 WHERE id=@id";

                var flag = DapperHelper.Instance.Execute(sql, new { id = Convert.ToInt32(applyTryoutCurriculum.Id) });

                return flag > 1 ? Ok(Comm.ResponseModel.ResponseModelBase.Success()) : Ok(Comm.ResponseModel.ResponseModelBase.SysError());

            }
            catch (Exception ex)
            {
                logs.Error("更改状态失败", ex);
                return BadRequest();
            }
        }


        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (id < 0)
                return BadRequest();

            try
            {

                var sql = @"select c.CampusID,c.CampusName,c.LinkMan,c.LinkTel,c.CampusStatus,c.Address,c.CityID,c.Latitude,c.Longitude,ci.CityName from Campus as c join City as ci on c.CityID=ci.CityID
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


    }
}
