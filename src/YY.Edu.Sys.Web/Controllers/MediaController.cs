using Senparc.Weixin;
using Senparc.Weixin.HttpUtility;
using Senparc.Weixin.MP;
using Senparc.Weixin.MP.AdvancedAPIs.Media;
using Senparc.Weixin.MP.CommonAPIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YY.Edu.Sys.Web.Controllers
{
    public class MediaController : Controller
    {
        // GET: Media
        public ActionResult Index()
        {

            string accessTokenOrAppId = "inKdF_vg-6jACO3A7rXZrfapuilOsAp33RRFGjApfDqIJnmJKn5BKM59JxyoxzGd5QrgqoD_P6K4DsKedsts5JAZbrFYjmgXo3xq0IhZNwDNpuBkcydJfA0S085St-AAROVeAAAZWD";
            MediaList_NewsResult media = ApiHandlerWapper.TryCommonApi(accessToken =>
            {
                string url = string.Format("https://api.weixin.qq.com/cgi-bin/material/batchget_material?access_token={0}", accessToken.AsUrlData());

                var date = new
                {
                    type = "news",
                    offset = 0,
                    count = 15
                };

                return CommonJsonSend.Send<MediaList_NewsResult>(null, url, date, CommonJsonSendType.POST, 10000);

            }, accessTokenOrAppId);

            string jsonStr = Comm.Helper.JsonHelper.ToJsonStringByNewtonsoft(media);

            ViewBag.media = jsonStr;
            return View();
        }
    }
}