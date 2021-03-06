﻿using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YY.Edu.Sys.Web.Controllers
{
    public class WeixinMsgController : BaseController
    {
        // GET: WeixinMsg
        public ActionResult Index()
        {
            return View();
        }

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
        /// 用户绑定成功
        /// </summary>
        /// <param name="role">当前用户domain</param>
        /// <returns></returns>
        public ActionResult BindSuccess(string domain)
        {

            //{ { first.DATA} }
            //绑定帐号：{ { keyword1.DATA} }
            //绑定状态：{ { keyword2.DATA} }
            //绑定时间：{ { keyword3.DATA} }
            //{ { remark.DATA} }

            if (domain.ToLower().Contains("student"))
            {
                new Services.WeixinNoticeService(base.AppId, base.OpenId).BindSuccess4Student(base.WxUserInfo.nickname);
            }
            else if (domain.ToLower().Contains("coach"))
            {
                new Services.WeixinNoticeService(base.AppId, base.OpenId).BindSuccess4Coach(base.WxUserInfo.nickname);
            }

            return View();
        }

    }
}