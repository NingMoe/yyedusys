﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using YY.Edu.Sys.Comm.Helper;
using YY.Edu.Sys.Models;

namespace YY.Edu.Sys.Web.Services
{
    public class OrderService
    {
        private HttpRequestHelper RequestHelper;
        private System.Net.Http.HttpClient _httpClient;


        public OrderService()
        {
            _httpClient = new System.Net.Http.HttpClient();
            RequestHelper = new HttpRequestHelper();
        }

        /// <summary>
        /// 通过订单号取的详细
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public dynamic GetOrderByOrderID(int OrderID)
        {

            //todo 用户取关检测
            //检查输入项
            string apiUrl = System.Configuration.ConfigurationManager.AppSettings["ApiUrl"];
            string url = string.Format("{0}/Student/GetOrderByOrderID/?OrderID=" + OrderID, apiUrl);

            RequestHelper.Create(url);
            RequestHelper.WebRequest.ContentType = "application/json;charset=UTF-8";
            RequestHelper.WebRequest.Headers.Add("Accept-Encoding: gzip, deflate");
            RequestHelper.WebRequest.AutomaticDecompression = System.Net.DecompressionMethods.Deflate;



            string result = RequestHelper.RunGetRequest();

            dynamic DynamicObject = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(result);
            return DynamicObject;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public dynamic UPdateOrderByOrderID(string channelOrderID, string wxOrderID, int payState, string attach)
        {

            string apiUrl = System.Configuration.ConfigurationManager.AppSettings["ApiUrl"];
            string url = string.Format("{0}/Student/UpdatePayState", apiUrl);

            RequestHelper.Create(url);
            RequestHelper.WebRequest.ContentType = "application/json;charset=UTF-8";
            RequestHelper.WebRequest.Headers.Add("Accept-Encoding: gzip, deflate");
            RequestHelper.WebRequest.AutomaticDecompression = System.Net.DecompressionMethods.Deflate;

            var tmp = new
            {
                channelOrderID = channelOrderID,
                wxOrderID = wxOrderID,
                payState = payState,
            };

            string jsonStr = JsonHelper.ToJsonStringByNewtonsoft(tmp);

            string result = RequestHelper.PostString(jsonStr);
            JObject jo = JObject.Parse(result);
            if (Convert.ToBoolean(jo["Error"].ToString()))
            {
                throw new Comm.YYException.YYException(jo["Msg"].ToString());
            }
            else
            {
                string appid = attach.Split(',')[0];
                string openid = attach.Split(',')[1];
                string productInfo = attach.Split(',')[2];

                new Services.WeixinNoticeService(appid, openid).BuySuccess("支付成功", productInfo);
            }
            dynamic DynamicObject = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(result);
            return DynamicObject;
        }

    }
}