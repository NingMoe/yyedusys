using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YY.Edu.Sys.BgTask
{
    class Program
    {


        static void Main(string[] args)
        {

            IScheduler scheduler = null;

            #region 配置文件方式

            try
            {

                var factory = new StdSchedulerFactory(new System.Collections.Specialized.NameValueCollection()
                {
                        {"quartz.plugin.xml.fileNames","~/quartz_jobs.xml" },
                        {"quartz.plugin.xml.type","Quartz.Plugin.Xml.XMLSchedulingDataProcessorPlugin,Quartz"}
                });

                scheduler = factory.GetScheduler();

                scheduler.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            #endregion

            //if (scheduler != null && scheduler.IsStarted)
            //{
            //    scheduler.Shutdown();
            //}
            Console.ReadLine();
        }
    }
}
