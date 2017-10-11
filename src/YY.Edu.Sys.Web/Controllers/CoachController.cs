using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Mvc;
using YY.Edu.Sys.Web.Models;

namespace YY.Edu.Sys.Web.Controllers
{
    public class CoachController : BaseCoachController
    {

        public async Task<ActionResult> Index()
        {
            ViewBag.Me = Newtonsoft.Json.JsonConvert.SerializeObject(await base.Me());

            if (ViewBag.Me == null || ViewBag.Me == "null")
            {
                string url = string.Format("/Coach/BindingUser/?opid={0}&url={1}&name={2}", OpenId, WxUserInfo.headimgurl, WxUserInfo.nickname);
                Response.Redirect(url);
                return View();
            }
            else
            {
                //var templateId = Comm.WeiXin.NoticeTemplates.OrderPayTemplate;
                //var data = new
                //{
                //    first = new TemplateDataItem("学生登录成功"),
                //    keyword1 = new TemplateDataItem("飞翔的企鹅"),
                //    keyword2 = new TemplateDataItem("123456789"),
                //    keyword3 = new TemplateDataItem("1000", "#ff0000"),//显示为红色
                //    keyword4 = new TemplateDataItem("购买课时"),
                //    remark = new TemplateDataItem("详细信息,查看 http://www.baidu.com")
                //};

                //var result = TemplateApi.SendTemplateMessage(base.AppId, Session["OpenId"].ToString(), templateId, "http://www.baidu.com", data);
                return View();
            }
        }

        public async Task<System.Web.Mvc.ActionResult> MyCurriculum()
        { 
            var obj =await base.Me();
            Models.LoginCoachInfo s = obj;

            CoachResponse c = s.CoachInfo;
            ViewBag.VenueID = c.VenueID;
            ViewBag.CoachID = c.CoachID;
            ViewBag.FullName = c.FullName;
          
            return View();
        }



        /// <summary>
        /// 学生点评
        /// </summary>
        /// <param name="pkid"></param>
        /// <param name="cid"></param>
        /// <returns></returns>
        public async Task<System.Web.Mvc.ActionResult> MyComment(int pkid, int cid)
        {
            var obj = await base.Me();
            Models.LoginCoachInfo s = obj;

            CoachResponse c = s.CoachInfo;
            ViewBag.VenueID = c.VenueID;
            ViewBag.CoachID = c.CoachID;
            ViewBag.FullName = c.FullName;
            ViewBag.PKID = pkid;
            ViewBag.CID = cid;
            return View();
        }
     

        /// <summary>
        /// 课程点评记录
        /// </summary>
        /// <param name="pkid"></param>
        /// <returns></returns>
        public ActionResult CommentLog(int pkid)
        {
            ViewBag.PKID = pkid;
            return View();
        }

        /// <summary>
        /// 课程详细信息
        /// </summary>
        /// <param name="cuid">课程ID</param>
        /// <param name="pkid">排课ID</param>
        /// <returns></returns>
        public ActionResult CurriculumDetail(int cuid, int pkid)
        {
            ViewBag.PKID = pkid;
           //ViewBag.CoashID = coachid;
            ViewBag.CID = cuid;
            return View();
        }

        /// <summary>
        /// 课程学生明细
        /// </summary>
        /// <param name="pkid"></param>
        /// <param name="coashid"></param>
        /// <param name="cid"></param>
        /// <returns></returns>
        public ActionResult CurriculumStudent(int pkid, int coachid, int cid)
        {
            ViewBag.PKID = pkid;
            ViewBag.CoachID = coachid;
            ViewBag.CID = cid;
            return View();
        }


        public async Task<System.Web.Mvc.ActionResult> MyPresence()
        {
            var obj = await base.Me();
            Models.LoginCoachInfo s = obj;

            CoachResponse c = s.CoachInfo;
            ViewBag.VenueID = c.VenueID;
            ViewBag.CoachID = c.CoachID;
            ViewBag.FullName = c.FullName;
            return View();
        }

        public ActionResult BindingUser()
        {
            ViewBag.OpenID = "ozLW4wIKJuOzimRmGbZxJcrBvaoY";     
            ViewBag.HeadUrl = "http://s.amazeui.org/media/i/demos/bing-1.jpg";
            ViewBag.NickName = "测试";
            return View();
        }



        public async Task<System.Web.Mvc.ActionResult> MyMessage()
        {
            var obj = await base.Me();
            Models.LoginCoachInfo s = obj;

            CoachResponse c = s.CoachInfo;
            ViewBag.VenueID = c.VenueID;
            ViewBag.CoachID = c.CoachID;
            ViewBag.FullName = c.FullName;
            return View();
        }


        public async Task<System.Web.Mvc.ActionResult> MyInfo()
        {
            var obj = await base.Me();
            Models.LoginCoachInfo s = obj;

            CoachResponse c = s.CoachInfo;
            ViewBag.VenueID = c.VenueID;
            ViewBag.CoachID = c.CoachID;
            ViewBag.FullName = c.FullName;
            return View();
        }


        public System.Web.Mvc.ActionResult CoachInfo(int coachid,int venueid)
        {
          
            ViewBag.CoachID = coachid;
            ViewBag.VenueID = venueid;
            return View();
        }


        public async Task<System.Web.Mvc.ActionResult> Wages()
        {
            var obj = await base.Me();
            Models.LoginCoachInfo s = obj;

            CoachResponse c = s.CoachInfo;
            ViewBag.VenueID = c.VenueID;
            ViewBag.CoachID = c.CoachID;
            ViewBag.FullName = c.FullName;
            return View();
        }
    }
}
