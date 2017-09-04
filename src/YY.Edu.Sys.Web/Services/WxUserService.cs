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

        private static HttpRequestHelper RequestHelper;

        /// <summary>
        /// 关注用户回写数据库
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public static bool FollowMP(OAuthUserInfo userInfo)
        {

            //todo 用户取关检测
            //检查输入项
            string apiUrl = System.Configuration.ConfigurationManager.AppSettings["ApiUrl"];
            string url = string.Format("{0}/WxUserInfo/Create", apiUrl);

            RequestHelper.Create(url);
            RequestHelper.WebRequest.ContentType = "application/json;charset=UTF-8";
            RequestHelper.WebRequest.Headers.Add("Accept-Encoding: gzip, deflate");
            RequestHelper.WebRequest.AutomaticDecompression = System.Net.DecompressionMethods.Deflate;

            var tmp = new
            {
                data = new Sys.Models.WxUserInfo()
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
                }
            };

            string jsonStr = JsonHelper.ToJsonStringByNewtonsoft(tmp);

            string result = RequestHelper.PostString(jsonStr);
            JObject jo = JObject.Parse(result);
            if (!Convert.ToBoolean(jo["Error"].ToString()))
                throw new Comm.YYException.YYException(jo["Msg"].ToString());

            return true;
        }

    }

    public abstract class JsonHelper
    {
        /// <summary>
        /// Json序列化,用于发送到客户端
        /// </summary>
        public static string ToJsonString(object item)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(item.GetType());
            using (MemoryStream ms = new MemoryStream())
            {
                serializer.WriteObject(ms, item);
                StringBuilder sb = new StringBuilder();
                sb.Append(Encoding.UTF8.GetString(ms.ToArray()));
                return sb.ToString();
            }
        }

        /// <summary>
        /// Json序列化 使用Newtonsoft
        /// 不会出现时间转换问题 不用加[DataContract]属性 [DataMember]属性
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string ToJsonStringByNewtonsoft(object o)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(o);
            return json;
        }


        /// <summary>
        /// Json反序列化,用于接收客户端Json后生成对应的对象
        /// </summary>
        public static T FromJsonToObject<T>(string jsonString)
        {
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
            MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            T jsonObject = (T)ser.ReadObject(ms);
            ms.Close();
            return jsonObject;
        }
    }
}