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

                if (ViewBag.Me == null || ViewBag.Me == "null")
                {

                    Response.Redirect("Coash/BindingUser/?opid=" + Session["OpenId"] + "&url=http://wx.qlogo.cn/mmopen/vi_32/Q0j4TwGTfTLmqP7fEiaQdThp5Q9KH0TZEvoAut4dex2zicNNsa7iccRfMcJzbw34jQoATDLZ2j4OibErMpGriboOIsg/0&name=我是2017");
                    return View();
                }
                else
                {

                    var templateId = Comm.WeiXin.NoticeTemplates.OrderPayTemplate;
                    var data = new
                    {
                        first = new TemplateDataItem("老师登录成功"),
                        keyword1 = new TemplateDataItem("飞翔的企鹅"),
                        keyword2 = new TemplateDataItem("123456789"),
                        keyword3 = new TemplateDataItem("1000", "#ff0000"),//显示为红色
                        keyword4 = new TemplateDataItem("购买课时"),
                        remark = new TemplateDataItem("详细信息,查看（http://www.baidu.com）！")
                    };

                    var result = TemplateApi.SendTemplateMessage(base.AppId, Session["OpenId"].ToString(), templateId, "http://www.baidu.com", data);                 
                    Response.Redirect("Coash/Index");
                    return View();
                }

            }
            ViewBag.Title = "Home Page";

            var jssdkUiPackage = Senparc.Weixin.MP.Helpers.JSSDKHelper.GetJsSdkUiPackage(base.AppId, base.AppSecret, Request.Url.AbsoluteUri);
            return View(jssdkUiPackage);

        }
    }
}