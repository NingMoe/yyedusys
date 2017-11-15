﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;
using YY.Edu.Sys.Comm.Helper;
using Newtonsoft.Json.Linq;

namespace YY.Edu.Sys.BgTask.QuartzJobs
{
    public class ClassBegin4StudentJob : BaseJob
    {

        private HttpRequestHelper RequestHelper;

        public ClassBegin4StudentJob()
        {
            RequestHelper = new HttpRequestHelper();
        }


        public override void Execute(IJobExecutionContext context)
        {

            string apiUrl = System.Configuration.ConfigurationManager.AppSettings["ApiUrl"];
            string url = string.Format("{0}/WxNotice/WaitClass4Coach", apiUrl);

            RequestHelper.Create(url);
            RequestHelper.WebRequest.ContentType = "application/json;charset=UTF-8";
            RequestHelper.WebRequest.Headers.Add("Accept-Encoding: gzip, deflate");
            RequestHelper.WebRequest.AutomaticDecompression = System.Net.DecompressionMethods.Deflate;

            string result = RequestHelper.RunGetRequest();
            JObject jo = JObject.Parse(result);
            if (Convert.ToBoolean(jo["Error"].ToString()))
            {
                throw new Comm.YYException.YYException(jo["Msg"].ToString());
            }
            else
            {
                var data = JToken.Parse(jo["data"].ToString()).ToList();
                foreach (var item in data)
                {
                    var templateId = Comm.WeiXin.NoticeTemplates.ClassBeginTemplate;
                    var send_data = new
                    {
                        first = new TemplateDataItem("预约课程开始提醒"),
                        keyword1 = new TemplateDataItem(item["Title"].ToString()),
                        keyword2 = new TemplateDataItem(item["CurriculumDate"].ToString() + item["CurriculumBeginTime"].ToString()),
                        remark = new TemplateDataItem("查看 http://m.yiyust.com/student/index")
                    };

                    var send_result = TemplateApi.SendTemplateMessage(base.AppId, item["openid"].ToString(), templateId, "http://m.yiyust.com/student/index", send_data);
                    //todo 记录日志
                }
            }
        }
    }
}
