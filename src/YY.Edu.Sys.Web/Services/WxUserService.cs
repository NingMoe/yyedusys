using Newtonsoft.Json.Linq;
using Senparc.Weixin.MP.AdvancedAPIs.OAuth;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using YY.Edu.Sys.Comm.Helper;

namespace YY.Edu.Sys.Web.Services
{
    public class WxUserService
    {

        private HttpRequestHelper RequestHelper;

        public WxUserService()
        {

            RequestHelper = new HttpRequestHelper();
        }

        /// <summary>
        /// 关注用户回写数据库
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public bool FollowMP(OAuthUserInfo userInfo)
        {

            //todo 用户取关检测
            //检查输入项
            string apiUrl = System.Configuration.ConfigurationManager.AppSettings["ApiUrl"];
            string url = string.Format("{0}/WxUserInfo/Create", apiUrl);

            RequestHelper.Create(url);
            RequestHelper.WebRequest.ContentType = "application/json;charset=UTF-8";
            RequestHelper.WebRequest.Headers.Add("Accept-Encoding: gzip, deflate");
            RequestHelper.WebRequest.AutomaticDecompression = System.Net.DecompressionMethods.Deflate;

            var tmp = new Sys.Models.WxUserInfo()
            {
                City = userInfo.city,
                Country = userInfo.country,
                HeadImgUrl = userInfo.headimgurl,
                LastLoginTime = DateTime.Now,
                NickName = userInfo.nickname,
                OpenId = userInfo.openid,
                Province = userInfo.province,
                Sex = userInfo.sex == 1,
                State = 1,
            };

            string jsonStr = JsonHelper.ToJsonStringByNewtonsoft(tmp);

            string result = RequestHelper.PostString(jsonStr);
            JObject jo = JObject.Parse(result);
            if (Convert.ToBoolean(jo["Error"].ToString()))
                throw new Comm.YYException.YYException(jo["Msg"].ToString());

            return true;
        }

    }

    
}