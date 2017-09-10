using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace YY.Edu.Sys.Web.data
{
    /// <summary>
    /// UpLoad 的摘要说明
    /// </summary>
    public class UpLoad : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {

            HttpRequest req = context.Request;
            HttpPostedFile _PostedFile = context.Request.Files["file"];
            string FileName = _PostedFile.FileName;
            string FileEx = context.Request["extention"] ;
            string utype = context.Request["utype"];
            string sysPaht = System.Web.HttpContext.Current.Server.MapPath("~/");
            string newname ="UpLoad\\Coach\\"+DateTime.Now.ToString("yyyyMMddHHmmssfff")+ FileEx;
            if (utype == "1")
            { newname = "UpLoad\\Student\\" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + FileEx; }

          
            //接收二级制数据并保存
            Stream stream = _PostedFile.InputStream;
            byte[] dataOne = new byte[stream.Length];
            stream.Read(dataOne, 0, dataOne.Length);

            int iRes = 0;
            string Msg = "";
            FileStream fs = null;
            try
            {
                fs = new FileStream(sysPaht + newname, FileMode.Create, FileAccess.Write);
                fs.Write(dataOne, 0, dataOne.Length);
                iRes = 1;
                Msg = newname.Replace("\\","-");
              
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (fs != null)
                {
                    fs.Close();
                }
                stream.Close();
            }
            context.Response.ContentType = "text/plain";
            context.Response.Write("{success:"+ iRes + ",msg:'"+ Msg + "'}");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}