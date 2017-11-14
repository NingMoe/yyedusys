using Senparc.Weixin.MP.AdvancedAPIs;
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

            var templateId = Comm.WeiXin.NoticeTemplates.BindSucessTemplate;

            //{ { first.DATA} }
            //绑定帐号：{ { keyword1.DATA} }
            //绑定状态：{ { keyword2.DATA} }
            //绑定时间：{ { keyword3.DATA} }
            //{ { remark.DATA} }

            if (domain.ToLower().Contains("student"))
            {
                var data = new
                {
                    first = new TemplateDataItem("您已经成功绑定"),
                    keyword1 = new TemplateDataItem(base.WxUserInfo.nickname),
                    keyword2 = new TemplateDataItem("绑定成功"),
                    keyword3 = new TemplateDataItem(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                    //keyword3 = new TemplateDataItem("1000", "#ff0000"),//显示为红色
                    remark = new TemplateDataItem("约课请查看")
                };

                var result = TemplateApi.SendTemplateMessage(base.AppId, base.OpenId, templateId, "http://m.yiyust.com/student/index", data);

            }
            else if (domain.ToLower().Contains("coach"))
            {
                var data = new
                {
                    first = new TemplateDataItem("您已经成功绑定"),
                    keyword1 = new TemplateDataItem(base.WxUserInfo.nickname),
                    keyword2 = new TemplateDataItem("绑定成功"),
                    keyword3 = new TemplateDataItem(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                    remark = new TemplateDataItem("您可登录系统查看预约课程信息")
                };

                var result = TemplateApi.SendTemplateMessage(base.AppId, base.OpenId, templateId, "http://m.yiyust.com/coach/index", data);

            }

            //templateId = Comm.WeiXin.NoticeTemplates.TestOrderPayTemplate;
            //var data = new
            //{
            //    first = new TemplateDataItem("学生登录成功"),
            //    keyword1 = new TemplateDataItem("飞翔的企鹅"),
            //    keyword2 = new TemplateDataItem("123456789"),
            //    keyword3 = new TemplateDataItem("1000", "#ff0000"),//显示为红色
            //    keyword4 = new TemplateDataItem("购买课时"),
            //    remark = new TemplateDataItem("详细信息,查看 http://www.baidu.com")
            //};

            return View();
        }

    }
}