using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace YY.Edu.Sys.Comm.Helper
{
    /// <summary>
    /// json帮助类
    /// </summary>
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
