using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YY.Edu.Sys.Admin.Controllers
{
    public class WeixinMsgController : BaseController
    {
        // GET: WeixinMsg
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult ClassBegin()
        {
            string openId = "ozLW4wHYTcApj55HIUT0o8Qdet6U";
            var templateId = Comm.WeiXin.NoticeTemplates.TestOrderPayTemplate;
            var data = new
            {
                first = new TemplateDataItem("学生登录成功"),
                keyword1 = new TemplateDataItem("飞翔的企鹅"),
                keyword2 = new TemplateDataItem("123456789"),
                keyword3 = new TemplateDataItem("1000", "#ff0000"),//显示为红色
                keyword4 = new TemplateDataItem("购买课时"),
                remark = new TemplateDataItem("详细信息,查看 http://www.baidu.com")
            };

            var result = TemplateApi.SendTemplateMessage(base.AppId, openId, templateId, "http://www.baidu.com", data);

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult BuySuccess(string openId, string coachName, string classNumber, string className)
        {

            //string openId = "ozLW4wHYTcApj55HIUT0o8Qdet6U";
            var templateId = Comm.WeiXin.NoticeTemplates.BuySuccessTemplate;

            var data = new
            {
                keyword1 = new TemplateDataItem(string.Format("{0}的{1}节{2}课已支付成功",
                coachName, classNumber, className)),
                remark = new TemplateDataItem("点击马上约课")
            };

            var result = TemplateApi.SendTemplateMessage(base.AppId, openId, templateId, "http://m.yiyust.com/student/index", data);

            return View();

        }

    }
}