﻿using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace YY.Edu.Sys.Web.Controllers
{
    public class CoachTestController : BaseCoachController
    {
        // GET: CoachTest
        public async Task<ActionResult> Index()
        {
            if (Session["OpenId"] != null)
            {

                ViewBag.Me = Newtonsoft.Json.JsonConvert.SerializeObject(await base.Me());

                var templateId = Comm.WeiXin.NoticeTemplates.OrderPayTemplate;
                var data = new
                {
                    first = new TemplateDataItem("教练登录成功"),
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