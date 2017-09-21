using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using YY.Edu.Sys.Comm.Helper;

namespace YY.Edu.Sys.Manage.Services
{

    public class LoginService
    {
        /// <summary>
        /// 获取token信息
        /// </summary>
        /// <param name="email"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
        public static async System.Threading.Tasks.Task<string> GetToken(string email, string passWord)
        {

            string url = System.Configuration.ConfigurationManager.AppSettings["ApiUrl"];
            HttpClient _httpClient = new HttpClient();
            _httpClient.BaseAddress = new System.Uri(url);

            string clientId = System.Web.Configuration.WebConfigurationManager.AppSettings["ClientId"];
            string clientSecret = System.Web.Configuration.WebConfigurationManager.AppSettings["ClientSecret"];
            string domain = System.Web.Configuration.WebConfigurationManager.AppSettings["ManageDomain"];

            var parameters = new Dictionary<string, string>();
            parameters.Add("grant_type", "password");
            parameters.Add("username", email);
            parameters.Add("password", passWord);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                "Basic",
                System.Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(clientId + ":" + clientSecret)));

            var response = await _httpClient.PostAsync("/token?domain=" + domain, new FormUrlEncodedContent(parameters));
            var responseValue = await response.Content.ReadAsStringAsync();

            return responseValue;
        }


        public static void Register(string token, Sys.Models.Venue model)
        {

            HttpRequestHelper RequestHelper = new HttpRequestHelper();

            string apiUrl = System.Configuration.ConfigurationManager.AppSettings["ApiUrl"];
            string url = string.Format("{0}/api/Venue/Create", apiUrl);

            RequestHelper.Create(url);
            RequestHelper.WebRequest.Headers.Add("Authorization", "Bearer " + token);
            RequestHelper.WebRequest.ContentType = "application/json;charset=UTF-8";
            RequestHelper.WebRequest.Headers.Add("Accept-Encoding: gzip, deflate");
            RequestHelper.WebRequest.AutomaticDecompression = System.Net.DecompressionMethods.Deflate;

            string jsonStr = JsonHelper.ToJsonStringByNewtonsoft(model);

            string result = RequestHelper.PostString(jsonStr);
            JObject jo = JObject.Parse(result);
            if (Convert.ToBoolean(jo["Error"].ToString()))
                throw new Comm.YYException.YYException(jo["Msg"].ToString());

        }


        /// <summary>
        /// 获取我的信息-场馆系统
        /// </summary>
        /// <param name="token"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public static async System.Threading.Tasks.Task<string> GetMe(string token, string email)
        {

            string url = System.Configuration.ConfigurationManager.AppSettings["ApiUrl"];
            HttpClient _httpClient = new HttpClient();
            _httpClient.BaseAddress = new System.Uri(url);

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return (await (await _httpClient.GetAsync("api/Venue/GetMe?email=" + email)).Content.ReadAsStringAsync());

        }

    }
}