using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YY.Edu.Sys.Web.Controllers
{

    [Filter.CustomOAuth(null, "/oauth2/UserInfoCallback")]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            if (Session["OpenId"] != null)
            {

                ViewBag.OpenId = Session["OpenId"];

                var templateId = Comm.WeiXin.NoticeTemplates.OrderPayTemplate;
                var data = new
                {
                    first = new TemplateDataItem("您的订单已经支付"),
                    keyword1 = new TemplateDataItem("飞翔的企鹅"),
                    keyword2 = new TemplateDataItem("123456789"),
                    keyword3 = new TemplateDataItem("1000", "#ff0000"),//显示为红色
                    keyword4 = new TemplateDataItem("购买课时"),
                    remark = new TemplateDataItem("详细信息,查看（http://www.baidu.com）！")
                };

                var result = TemplateApi.SendTemplateMessage(base.AppId, Session["OpenId"].ToString(), templateId, "http://www.baidu.com", data);

            }
            ViewBag.Title = "Home Page";

            var jssdkUiPackage = Senparc.Weixin.MP.Helpers.JSSDKHelper.GetJsSdkUiPackage(base.AppId, base.AppSecret, Request.Url.AbsoluteUri);
            return View(jssdkUiPackage);

        }
    }
}
