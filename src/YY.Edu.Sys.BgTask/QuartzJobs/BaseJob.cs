using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YY.Edu.Sys.BgTask.QuartzJobs
{
    public abstract class BaseJob : IJob
    {

        public string AppId
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["WeixinAppId"];//与微信公众账号后台的AppId设置保持一致，区分大小写。
            }
        }

        /// <summary>
        /// 需要重写的做任务方法
        /// </summary>
        /// <param name="context"></param>
        public abstract void Execute(IJobExecutionContext context);

    }
}
