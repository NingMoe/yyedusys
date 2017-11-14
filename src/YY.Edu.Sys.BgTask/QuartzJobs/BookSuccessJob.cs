using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Senparc.Weixin.MP.AdvancedAPIs.TemplateMessage;
using Senparc.Weixin.MP.AdvancedAPIs;

namespace YY.Edu.Sys.BgTask.QuartzJobs
{
    public class BookSuccessJob : BaseJob
    {
        public override void Execute(IJobExecutionContext context)
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

        }
    }
}
