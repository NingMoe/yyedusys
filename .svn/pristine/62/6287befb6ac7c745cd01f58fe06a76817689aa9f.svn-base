using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace YY.Edu.Sys.Comm.Helper
{
    /// <summary>
    /// HTTP 请求帮助类
    /// </summary>
    public class HttpRequestHelper
    {
        #region 内部私有字段
        private HttpWebRequest webRequest;
        private HttpWebResponse webResponse;
        private CookieContainer cookieContainer = null;
        private WebProxy proxy = null;
        private List<string> parameter;
        #endregion

        #region 属性
        public HttpWebRequest WebRequest
        {
            get { return webRequest; }
            set { webRequest = value; }
        }

        public HttpWebResponse WebResponse
        {
            get { return webResponse; }
            set { webResponse = value; }
        }

        /// <summary>
        /// 代理信息，调用Create方法之前设置有效,否则无效
        /// </summary>
        public WebProxy ProxyInfo
        {
            get
            {
                proxyUsedCount++;
                return proxy;
            }
            set
            {
                proxy = value;
                proxyUsedCount = 0;
            }
        }

        private int proxyUsedCount;
        public int ProxyUsedCount
        {
            get { return proxyUsedCount; }
        }

        /// <summary>
        /// cookie,调用Create方法之前设置有效,否则无效
        /// </summary>
        public CookieContainer CookieContainer
        {
            get
            {
                if (cookieContainer == null)
                {
                    cookieContainer = new CookieContainer();
                }
                return cookieContainer;
            }
            set { cookieContainer = value; }
        }
        #endregion

        #region 构造函数
        public HttpRequestHelper()
        {
            parameter = new List<string>();
        }
        #endregion

        #region 辅助方法
        /// <summary>
        /// 创建webRequest,CookieContainer属性和ProxyInfo属性需要在此之前设置才能生效
        /// </summary>
        /// <param name="url">请求地址</param>
        public void Create(string url)
        {
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);

            webRequest = HttpWebRequest.Create(url) as HttpWebRequest;
            webRequest.CookieContainer = this.CookieContainer;
            if (ProxyInfo != null)
            {
                webRequest.Proxy = ProxyInfo;
            }
            //默认值为2
            webRequest.ServicePoint.ConnectionLimit = 512;
            webRequest.ReadWriteTimeout = 50 * 1000;
            parameter.Clear();
            //HttpWebProxy.SetHttpRequest(webRequest, false);
        }

        /// <summary>
        /// 创建webRequest,CookieContainer属性和ProxyInfo属性需要在此之前设置才能生效
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="timeout">超时时间，单位秒</param>
        public void CreateAndInitialization(string url, string referUrl = "", CookieContainer cc = null, int timeout = 20, int readWriteTimeout = 20)
        {
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);

            webRequest = HttpWebRequest.Create(url) as HttpWebRequest;
            if (cc == null)
                webRequest.CookieContainer = this.CookieContainer;
            else
                webRequest.CookieContainer = cc;
            if (ProxyInfo != null)
            {
                webRequest.Proxy = ProxyInfo;
            }
            parameter.Clear();
            webRequest.Headers.Add("Accept-Language", "zh-CN");
            webRequest.Accept = "text/html, application/xhtml+xml, */*";
            webRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; .NET4.0C; InfoPath.3; .NET4.0E; Tablet PC 2.0)";

            //webRequest.Headers.Add("Accept-Language", "zh-CN");
            //webRequest.Accept = "text/html, application/xhtml+xml, */*";
            //webRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/38.0.2125.101 Safari/537.36 OPR/25.0.1614.50 (Edition Campaign 18)";
            //webRequest.Headers.Add("Accept-Encoding", "gzip,deflate,lzma,sdch");


            webRequest.KeepAlive = true;
            webRequest.Timeout = timeout * 1000;
            //webRequest.ReadWriteTimeout = 4 * timeout * 1000;
            webRequest.ReadWriteTimeout = readWriteTimeout * 1000;
            //HttpWebProxy.SetHttpRequest(webRequest, false);
            if (!string.IsNullOrEmpty(referUrl))
                this.WebRequest.Referer = referUrl;
        }

        /// <summary>
        /// 添加请求参数 POST GET 通用
        /// 内部自动构建 key=value请求数据
        /// </summary>
        /// <param name="key">key值</param>
        /// <param name="value">value值</param>
        public void AddParameter(string key, string value)
        {
            parameter.Add(key + "=" + value);
        }

        #endregion

        #region GET方法
        /// <summary>
        /// 发送GET请求并返回结果 可以指定编码方式
        /// </summary>
        /// <param name="encoding">返回数据流读取编码方式</param>
        /// <returns>结果字符串</returns>
        public string RunGetRequest(Encoding encoding = null)
        {
            webRequest.Method = "GET";
            //StreamReader sr = null;
            string result = string.Empty;
            //try
            //{
            if (encoding == null) encoding = Encoding.UTF8;
            webResponse = (HttpWebResponse)webRequest.GetResponse();
            CheckProxy();

            if (webResponse.ContentEncoding.ToLower() == "gzip")//如果使用了GZip则先解压
            {
                using (System.IO.Stream stream = webResponse.GetResponseStream())
                {
                    using (var zipStream =
                        new System.IO.Compression.GZipStream(stream, System.IO.Compression.CompressionMode.Decompress))
                    {
                        using (System.IO.StreamReader sr = new System.IO.StreamReader(zipStream, encoding))
                        {
                            result = sr.ReadToEnd();
                        }
                    }
                }
            }
            else
            {
                using (System.IO.Stream stream = webResponse.GetResponseStream())
                {
                    using (System.IO.StreamReader sr = new System.IO.StreamReader(stream, encoding))
                    {
                        result = sr.ReadToEnd();
                    }
                }
            }

            //sr = new StreamReader(webResponse.GetResponseStream(), encoding);

            //string s = sr.ReadToEnd();
            return result;
            //}
            //finally
            //{
            //    if (sr != null)
            //    {
            //        sr.Close();
            //    }
            //}
        }

        /// <summary>
        /// GET方式获取网页内容
        /// </summary>
        /// <param name="url">请求地址 必传</param>
        /// <param name="referUrl">Referer</param>
        /// <param name="cc">CookieContainer</param>
        /// <param name="encoding">读取数据时的编码方式，默认UTF8</param>
        /// <param name="timeOUnt">毫秒</param>
        /// <returns></returns>
        public static string GetHttpWebResponse(string url, string referUrl = "", CookieContainer cc = null, Encoding encoding = null, WebProxy webProxy = null, int timeOUnt = 1000000)
        {
            try
            {
                if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                    ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);

                var httpWebRequest = System.Net.WebRequest.Create(url) as HttpWebRequest;
                httpWebRequest.Method = "GET";
                httpWebRequest.ServicePoint.Expect100Continue = false;
                httpWebRequest.UserAgent = "Mozilla/4.0 (compatible; MSIE 8.0; Windows NT 6.1; Trident/4.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0; InfoPath.3; .NET4.0C; .NET4.0E)";
                httpWebRequest.Accept = "*/*";
                httpWebRequest.Referer = referUrl;
                httpWebRequest.Timeout = timeOUnt;

                if (webProxy != null)
                {
                    httpWebRequest.Proxy = webProxy;
                }

                if (cc != null)
                    httpWebRequest.CookieContainer = cc;
                if (encoding == null)
                    encoding = Encoding.UTF8;

                HttpWebResponse response = null;

                return GetResponseStream(httpWebRequest, response, encoding);
            }
            catch (TimeoutException)
            {
                throw;
            }
            catch (WebException)
            {
                throw;
            }
            catch (Exception ex)
            {
                //Logger.Error(url + ex.ToString());
                throw;
            }
        }

        /// <summary>
        /// Get并返回图片
        /// </summary>
        /// <param name="url">请求地址 必传</param>
        /// <param name="cc"></param>
        /// <param name="referUrl"></param>
        /// <returns></returns>
        public static Bitmap GetHttpWebResponseBit(string url, CookieContainer cc = null, string referUrl = "", WebProxy webProxy = null)
        {
            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
                ServicePointManager.ServerCertificateValidationCallback = new System.Net.Security.RemoteCertificateValidationCallback(CheckValidationResult);

            Bitmap bit = null;
            HttpWebRequest httpWebRequest = System.Net.WebRequest.Create(url) as HttpWebRequest;
            httpWebRequest.Method = "GET";

            if (webProxy != null)
            {
                httpWebRequest.Proxy = webProxy;
            }

            httpWebRequest.ServicePoint.Expect100Continue = false;
            httpWebRequest.UserAgent = "Mozilla/5.0 (compatible; MSIE 9.0; Windows NT 6.1; Trident/5.0)";
            httpWebRequest.Accept = "text/html, application/xhtml+xml, */*";

            if (!string.IsNullOrEmpty(referUrl) && referUrl.Length > 0) httpWebRequest.Referer = referUrl;
            if (cc != null) httpWebRequest.CookieContainer = cc;

            HttpWebResponse response = null;
            try
            {
                response = (HttpWebResponse)httpWebRequest.GetResponse();
                bit = new Bitmap(response.GetResponseStream());
            }
            catch (Exception ex)
            {
                //Logger.Error(url + ex.ToString());
                throw;
            }
            finally
            {
                if (response != null) { response.Close(); }
            }
            return bit;
        }

        /// <summary>
        /// 发送GET请求并返回图片
        /// </summary>
        /// <returns>图片</returns>
        public Bitmap RunGetRequestBitmap()
        {
            webRequest.Method = "GET";

            Bitmap bitmap = null;

            try
            {
                webResponse = (HttpWebResponse)webRequest.GetResponse();
                CheckProxy();
                bitmap = new Bitmap(webResponse.GetResponseStream());

            }
            catch
            {
                throw;
            }
            return bitmap;
        }

        public Stream RunGetRequestBitmapForStream()
        {
            webRequest.Method = "GET";

            Stream stream = null;

            try
            {
                webResponse = (HttpWebResponse)webRequest.GetResponse();
                CheckProxy();
                stream = webResponse.GetResponseStream();

            }
            catch
            {
                throw;
            }
            return stream;
        }
        #endregion

        #region POST方法

        /// <summary>
        /// 发送POST请求并返回结果,ContentType不指定则默认application/x-www-form-urlencoded
        /// </summary>
        /// <param name="EncodingPostString">请求数据的编码方式,默认为Encoding.UTF8</param>
        /// <param name="EncodingReadStream">获取请求数据流时的编码方式，默认为Encoding.UTF8</param>
        /// <returns>结果字符串</returns>
        public string PostString(Encoding EncodingPostString = null, Encoding EncodingReadStream = null)
        {
            string postData = GetParameterPostData();
            return PostString(postData, EncodingPostString, EncodingReadStream);
        }

        /// <summary>
        /// 发送POST请求并返回结果,数据使用gzip压缩
        /// Content-Encoding无需指定gzip（方法内部已处理）
        /// ContentType不指定则默认application/x-www-form-urlencoded
        /// </summary>
        /// <param name="EncodingPostString">请求数据的编码方式,默认为Encoding.UTF8</param>
        /// <param name="EncodingReadStream">获取请求数据流时的编码方式，默认为Encoding.UTF8</param>
        /// <returns>结果字符串</returns>
        public string PostGzipCompressString(Encoding EncodingPostString = null, Encoding EncodingReadStream = null)
        {
            string postData = GetParameterPostData();
            return PostString(postData, EncodingPostString, EncodingReadStream, true);
        }

        /// <summary>
        /// 发送POST请求并返回结果,ContentType不指定则默认application/x-www-form-urlencoded
        /// </summary>
        /// <param name="postDataString">POST请求数据</param>
        /// <param name="EncodingPostString">请求数据的编码方式,默认为Encoding.UTF8</param>
        /// <param name="EncodingReadStream">获取请求数据流时的编码方式,默认为Encoding.UTF8</param>
        /// <param name="IsGzipCompress">是否压缩</param>
        /// <returns>结果字符串</returns>
        public string PostString(string paramData, Encoding EncodingPostData = null, Encoding EncodingReadStream = null, bool IsGzipCompress = false)
        {
            webRequest.Method = "POST";
            Stream newStream = null;
            System.IO.Compression.GZipStream gzipStream = null;

            if (EncodingReadStream == null)
            {
                EncodingReadStream = Encoding.UTF8;
            }
            if (EncodingPostData == null)
            {
                EncodingPostData = Encoding.UTF8;
            }
            if (string.IsNullOrEmpty(webRequest.ContentType))
            {
                webRequest.ContentType = "application/x-www-form-urlencoded";
            }
            if (IsGzipCompress)
            {
                webRequest.Headers.Add("Content-Encoding", "gzip");
            }
            try
            {
                byte[] byteArray = EncodingPostData.GetBytes(paramData); //转化
                ServicePointManager.Expect100Continue = false;

                if (IsGzipCompress)
                {
                    newStream = webRequest.GetRequestStream();
                    gzipStream = new System.IO.Compression.GZipStream(newStream, System.IO.Compression.CompressionMode.Compress);
                    gzipStream.Write(byteArray, 0, byteArray.Length);
                    gzipStream.Close();
                    newStream.Close();
                }
                else
                {
                    webRequest.ContentLength = byteArray.Length;
                    newStream = webRequest.GetRequestStream();
                    newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                    newStream.Close();
                }

                string result = string.Empty;
                webResponse = (HttpWebResponse)webRequest.GetResponse();
                CheckProxy();

                if (webResponse.ContentEncoding.ToLower() == "gzip")//如果使用了GZip则先解压
                {
                    using (System.IO.Stream stream = webResponse.GetResponseStream())
                    {
                        using (var zipStream =
                            new System.IO.Compression.GZipStream(stream, System.IO.Compression.CompressionMode.Decompress))
                        {
                            using (System.IO.StreamReader sr = new System.IO.StreamReader(zipStream, EncodingReadStream))
                            {
                                result = sr.ReadToEnd();
                            }
                        }
                    }
                }
                else
                {
                    using (System.IO.Stream stream = webResponse.GetResponseStream())
                    {
                        using (System.IO.StreamReader sr = new System.IO.StreamReader(stream, EncodingReadStream))
                        {
                            result = sr.ReadToEnd();
                        }
                    }
                }

                return result;
            }
            finally
            {
                if (WebResponse != null)
                    WebResponse.Close();
                if (gzipStream != null)
                    gzipStream.Close();
                if (newStream != null)
                    newStream.Close();
            }
        }

        public string PostStringFormdata(Dictionary<string, string> paramData, Encoding EncodingPostData = null, Encoding EncodingReadStream = null)
        {
            webRequest.Method = "POST";
            //string ret = string.Empty;
            Stream newStream = null;
            //StreamReader sr = null;

            if (EncodingReadStream == null)
            {
                EncodingReadStream = Encoding.UTF8;
            }
            if (EncodingPostData == null)
            {
                EncodingPostData = Encoding.UTF8;
            }
            if (string.IsNullOrEmpty(webRequest.ContentType))
            {

                webRequest.ContentType = "application/x-www-form-urlencoded";
            }
            try
            {
                //EncodingPostData = Encoding.GetEncoding("GB2312");
                // 边界符  
                var boundary = "---------------" + DateTime.Now.Ticks.ToString("x");
                // 边界符  
                var beginBoundary = Encoding.ASCII.GetBytes("--" + boundary + "\r\n");
                // 最后的结束符  
                var endBoundary = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
                webRequest.ContentType = "multipart/form-data; boundary=" + boundary;
                var memStream = new MemoryStream();
                memStream.Write(beginBoundary, 0, beginBoundary.Length);

                var header = "Content-Disposition: form-data; name=\"litpic\"; filename=\"\"\r\n" +
         "Content-Type: application/octet-stream\r\n\r\n";
                var headerbytes = EncodingPostData.GetBytes(header);
                memStream.Write(headerbytes, 0, headerbytes.Length);

                var stringKeyHeader = "\r\n--" + boundary +
                          "\r\nContent-Disposition: form-data; name=\"{0}\"" +
                          "\r\n\r\n{1}";


                // var rn = "\r\n";//换行
                // var rnlength = rn.Length;
                foreach (var item in paramData)
                {


                    string data = string.Format(stringKeyHeader, item.Key, item.Value);
                    byte[] bdata = EncodingPostData.GetBytes(data);
                    memStream.Write(bdata, 0, bdata.Length);
                    //memStream.Write(EncodingPostData.GetBytes(rn), 0, rnlength);

                }


                // 写入最后的结束边界符  
                memStream.Write(endBoundary, 0, endBoundary.Length);

                webRequest.ContentLength = memStream.Length;
                //byte[] byteArray = EncodingPostData.GetBytes(paramData); //转化
                // byte[] byteArray = Encoding.ASCII.GetBytes(paramData);
                // webRequest.ContentLength = byteArray.Length;
                ServicePointManager.Expect100Continue = false;

                newStream = webRequest.GetRequestStream();

                memStream.Position = 0;
                var tempBuffer = new byte[memStream.Length];
                memStream.Read(tempBuffer, 0, tempBuffer.Length);
                memStream.Close();

                newStream.Write(tempBuffer, 0, tempBuffer.Length);//写入参数
                newStream.Close();
                //webResponse = (HttpWebResponse)webRequest.GetResponse();
                ////CheckProxy();
                //sr = new StreamReader(webResponse.GetResponseStream(), EncodingReadStream);
                //ret = sr.ReadToEnd();
                //return ret;

                string result = string.Empty;
                webResponse = (HttpWebResponse)webRequest.GetResponse();
                CheckProxy();

                if (webResponse.ContentEncoding.ToLower() == "gzip")//如果使用了GZip则先解压
                {
                    using (System.IO.Stream stream = webResponse.GetResponseStream())
                    {
                        using (var zipStream =
                            new System.IO.Compression.GZipStream(stream, System.IO.Compression.CompressionMode.Decompress))
                        {
                            using (System.IO.StreamReader sr = new System.IO.StreamReader(zipStream, EncodingReadStream))
                            {
                                result = sr.ReadToEnd();
                            }
                        }
                    }
                }
                else
                {
                    using (System.IO.Stream stream = webResponse.GetResponseStream())
                    {
                        using (System.IO.StreamReader sr = new System.IO.StreamReader(stream, EncodingReadStream))
                        {
                            result = sr.ReadToEnd();
                        }
                    }
                }

                return result;
            }
            finally
            {
                //if (sr != null)
                //    sr.Close();
                if (WebResponse != null)
                    WebResponse.Close();
                if (newStream != null)
                    newStream.Close();
            }
        }
        public string PostStringFormPic(Dictionary<string, string> paramData, PostFile postfile, Encoding EncodingPostData = null, Encoding EncodingReadStream = null)
        {
            webRequest.Method = "POST";
            //string ret = string.Empty;
            Stream newStream = null;
            //StreamReader sr = null;

            if (EncodingReadStream == null)
            {
                EncodingReadStream = Encoding.UTF8;
            }
            if (EncodingPostData == null)
            {
                EncodingPostData = Encoding.UTF8;
            }

            try
            {
                //EncodingPostData = Encoding.GetEncoding("GB2312");
                // 边界符  
                var boundary = "---------------" + DateTime.Now.Ticks.ToString("x");
                // 边界符  
                var beginBoundary = Encoding.ASCII.GetBytes("--" + boundary + "\r\n");
                // 最后的结束符  
                var endBoundary = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
                webRequest.ContentType = "multipart/form-data; boundary=" + boundary;
                var memStream = new MemoryStream();
                memStream.Write(beginBoundary, 0, beginBoundary.Length);

                var header = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n" +
         "Content-Type: image/jpeg\r\n\r\n";
                header = string.Format(header, postfile.name, postfile.filename);
                var headerbytes = EncodingPostData.GetBytes(header);
                memStream.Write(headerbytes, 0, headerbytes.Length);



                postfile.bitmapStream.CopyTo(memStream);



                var stringKeyHeader = "\r\n--" + boundary +
                          "\r\nContent-Disposition: form-data; name=\"{0}\"" +
                          "\r\n\r\n{1}";


                // var rn = "\r\n";//换行
                // var rnlength = rn.Length;
                foreach (var item in paramData)
                {


                    string data = string.Format(stringKeyHeader, item.Key, item.Value);
                    byte[] bdata = EncodingPostData.GetBytes(data);
                    memStream.Write(bdata, 0, bdata.Length);
                    //memStream.Write(EncodingPostData.GetBytes(rn), 0, rnlength);

                }


                // 写入最后的结束边界符  
                memStream.Write(endBoundary, 0, endBoundary.Length);

                webRequest.ContentLength = memStream.Length;
                //byte[] byteArray = EncodingPostData.GetBytes(paramData); //转化
                // byte[] byteArray = Encoding.ASCII.GetBytes(paramData);
                // webRequest.ContentLength = byteArray.Length;
                ServicePointManager.Expect100Continue = false;

                newStream = webRequest.GetRequestStream();

                memStream.Position = 0;
                var tempBuffer = new byte[memStream.Length];
                memStream.Read(tempBuffer, 0, tempBuffer.Length);
                memStream.Close();

                newStream.Write(tempBuffer, 0, tempBuffer.Length);//写入参数
                newStream.Close();
                //webResponse = (HttpWebResponse)webRequest.GetResponse();
                ////CheckProxy();
                //sr = new StreamReader(webResponse.GetResponseStream(), EncodingReadStream);
                //ret = sr.ReadToEnd();
                //return ret;

                string result = string.Empty;
                webResponse = (HttpWebResponse)webRequest.GetResponse();
                //CheckProxy();

                if (webResponse.ContentEncoding.ToLower() == "gzip")//如果使用了GZip则先解压
                {
                    using (System.IO.Stream stream = webResponse.GetResponseStream())
                    {
                        using (var zipStream =
                            new System.IO.Compression.GZipStream(stream, System.IO.Compression.CompressionMode.Decompress))
                        {
                            using (System.IO.StreamReader sr = new System.IO.StreamReader(zipStream, EncodingReadStream))
                            {
                                result = sr.ReadToEnd();
                            }
                        }
                    }
                }
                else
                {
                    using (System.IO.Stream stream = webResponse.GetResponseStream())
                    {
                        using (System.IO.StreamReader sr = new System.IO.StreamReader(stream, EncodingReadStream))
                        {
                            result = sr.ReadToEnd();
                        }
                    }
                }

                return result;
            }
            finally
            {
                //if (sr != null)
                //    sr.Close();
                if (WebResponse != null)
                    WebResponse.Close();
                if (newStream != null)
                    newStream.Close();
            }
        }

        #endregion

        #region 私有方法
        /// <summary>
        /// 组装请求数据
        /// </summary>
        /// <returns></returns>
        private string GetParameterPostData()
        {
            string s = string.Join("&", parameter.ToArray());
            parameter.Clear();
            return s;
        }

        /// <summary>
        /// https支持
        /// </summary>
        static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        /// <summary>
        /// 读取数据流信息
        /// </summary>
        /// <param name="httpWebResponse"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        private static string GetResponseStream(HttpWebRequest httpWebRequest, HttpWebResponse httpWebResponse, Encoding encoding)
        {
            StreamReader sr = null;
            try
            {
                //Logger.Debug("4开始" + System.DateTime.Now);
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                //Logger.Debug("4结束" + System.DateTime.Now);

                //Logger.Debug("5开始" + System.DateTime.Now);
                sr = new StreamReader(httpWebResponse.GetResponseStream(), encoding);
                string s = sr.ReadToEnd();
                //Logger.Debug("5结束" + System.DateTime.Now);

                return s;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (sr != null)
                    sr.Close();
            }
        }

        /// <summary>
        /// 检查代理是否有效
        /// 有些代理渠道请求后返回了http://m.flashapp.cn/nav.html的页面
        /// http://zhidao.baidu.com/error.html
        /// </summary>
        private void CheckProxy()
        {
            //if (ProxyInfo == null)
            //    return;
            //if (this.webResponse.ResponseUri.ToString().Contains("m.flashapp.cn"))
            //    throw new Exceptions.ProxyException("请求响应异常，更换代理");
        }

        #endregion
    }

       /// <summary>
    /// 上传图片
    /// </summary>
    public struct PostFile
    {
        public string name;
        public string filename;
        public Stream bitmapStream;
    }

}
