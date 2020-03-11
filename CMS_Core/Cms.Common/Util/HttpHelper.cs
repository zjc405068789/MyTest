using Microsoft.AspNetCore.Http;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Cms.Common.Util
{
    public class HttpHelper
    {
        /// <summary>
        /// http get 请求，返回html
        /// </summary>
        /// <param name="url">发起请求的网址</param>
        /// <param name="url">请求超时前等待的毫秒数</param>
        /// <returns></returns>
        public static string HttpGetRequest(string url, int timeout = 5000)
        {
            HttpWebRequest httpWebRequest = null;
            HttpWebResponse httpWebResponse = null;
            if (url.Contains("https://"))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                httpWebRequest = (HttpWebRequest)WebRequest.CreateDefault(new Uri(url));
            }
            else
            {
                httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            }
            httpWebRequest.Method = "get";
            httpWebRequest.Timeout = timeout;
            //httpRequest.Headers.Add("Authorization", authorization);//token

            try
            {
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            }
            catch (WebException ex)
            {
                //httpWebResponse = (HttpWebResponse)ex.Response;
                return ex.Status.ToString();
            }

            Stream st = httpWebResponse.GetResponseStream();
            StreamReader reader = new StreamReader(st, Encoding.GetEncoding("utf-8"));
            return reader.ReadToEnd();
        }

        public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        //body是要传递的参数,格式"roleId=1&uid=2"
        //post的cotentType填写:
        //"application/x-www-form-urlencoded"
        //soap填写:"text/xml; charset=utf-8"
        public static string HttpPostRequest(string url, string body, string contentType = "application/x-www-form-urlencoded", int timeOut = 20000)
        {
            System.Net.ServicePointManager.DefaultConnectionLimit = 1024;
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);

            httpWebRequest.ContentType = contentType;
            httpWebRequest.Method = "POST";
            httpWebRequest.Timeout = timeOut;

            byte[] btBodys = Encoding.UTF8.GetBytes(body);
            httpWebRequest.ContentLength = btBodys.Length;
            httpWebRequest.GetRequestStream().Write(btBodys, 0, btBodys.Length);

            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
            string responseContent = streamReader.ReadToEnd();

            httpWebResponse.Close();
            streamReader.Close();
            httpWebRequest.Abort();
            httpWebResponse.Close();

            return responseContent;
        }

        /// <summary>
        /// http post 请求
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string HttpPostRequest(string url)
        {
            HttpWebRequest httpRequest = null;
            HttpWebResponse httpResponse = null;
            if (url.Contains("https://"))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                httpRequest = (HttpWebRequest)WebRequest.CreateDefault(new Uri(url));
            }
            else
            {
                httpRequest = (HttpWebRequest)WebRequest.Create(url);
            }
            httpRequest.Method = "post";
            httpRequest.ContentType = "application/x-www-form-urlencoded";
            //httpRequest.Headers.Add("Authorization", authorization);//token
            try
            {
                httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            }
            catch (WebException ex)
            {
                httpResponse = (HttpWebResponse)ex.Response;
            }

            Stream st = httpResponse.GetResponseStream();
            StreamReader reader = new StreamReader(st, Encoding.GetEncoding("utf-8"));
            return reader.ReadToEnd();
        }

        /// <summary>
        /// 获取CooKie
        /// </summary>
        /// <param name="loginUrl"></param>
        /// <param name="postdata"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        public static CookieContainer GetCooKie(string loginUrl, string postdata, HttpHeader header)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;
            try
            {
                CookieContainer cc = new CookieContainer();
                request = (HttpWebRequest)WebRequest.Create(loginUrl);
                request.Method = header.method;
                request.ContentType = header.contentType;
                byte[] postdatabyte = Encoding.UTF8.GetBytes(postdata);     //提交的请求主体的内容
                request.ContentLength = postdatabyte.Length;    //提交的请求主体的长度
                request.AllowAutoRedirect = false;
                request.CookieContainer = cc;
                request.KeepAlive = true;

                //提交请求
                Stream stream;
                stream = request.GetRequestStream();
                stream.Write(postdatabyte, 0, postdatabyte.Length);     //带上请求主体
                stream.Close();

                //接收响应
                response = (HttpWebResponse)request.GetResponse();      //正式发起请求
                response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);

                CookieCollection cook = response.Cookies;
                //Cookie字符串格式
                string strcrook = request.CookieContainer.GetCookieHeader(request.RequestUri);

                return cc;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //获取CookieContainer中所有的cookie
        public static List<Cookie> GetAllCookies(CookieContainer cc)
        {
            List<Cookie> lstCookies = new List<Cookie>();
            Hashtable table = (Hashtable)cc.GetType().InvokeMember("m_domainTable",
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField |
                System.Reflection.BindingFlags.Instance, null, cc, new object[] { });
            foreach (object pathList in table.Values)
            {
                SortedList lstCookieCol = (SortedList)pathList.GetType().InvokeMember("m_list",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.GetField
                    | System.Reflection.BindingFlags.Instance, null, pathList, new object[] { });
                foreach (CookieCollection colCookies in lstCookieCol.Values)
                    foreach (Cookie c in colCookies) lstCookies.Add(c);
            }
            return lstCookies;
        }

        /// <summary>
        /// 获取html 携带cookies
        /// </summary>
        /// <param name="getUrl"></param>
        /// <param name="cookieContainer"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        public static string HttpGetRequestWithCookies(string getUrl, CookieContainer cookieContainer, HttpHeader header)
        {
            HttpWebRequest httpWebRequest = null;
            HttpWebResponse httpWebResponse = null;
            try
            {
                httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(getUrl);
                httpWebRequest.CookieContainer = cookieContainer;
                httpWebRequest.ContentType = header.contentType;
                httpWebRequest.ServicePoint.ConnectionLimit = header.maxTry;//客户端同时可以建立的 http 连接数
                httpWebRequest.Referer = getUrl;
                httpWebRequest.Accept = header.accept;
                httpWebRequest.UserAgent = header.userAgent;
                httpWebRequest.Method = "GET";
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Stream responseStream = httpWebResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
                string html = streamReader.ReadToEnd();
                streamReader.Close();
                responseStream.Close();
                httpWebRequest.Abort();
                httpWebResponse.Close();
                return html;
            }
            catch (Exception e)
            {
                if (httpWebRequest != null) httpWebRequest.Abort();
                if (httpWebResponse != null) httpWebResponse.Close();
                return string.Empty;
            }
        }


        /// <summary>
        /// 发送文件到指定服务器
        /// </summary>
        /// <param name="uploadurl">服务器地址</param>
        /// <param name="absolutepath">文件绝对路径</param>
        public static void HttpPostFile(string uploadurl, string absolutepath)
        {
            string[] pathlist = absolutepath.Split('\\');
            string boundary = "----------" + DateTime.Now.Ticks.ToString("x");//元素分割标记 
            StringBuilder sb = new StringBuilder();
            sb.Append("--" + boundary);
            sb.Append("\r\n");
            sb.Append("Content-Disposition: form-data; name=\"file\"; filename=\"" + pathlist[pathlist.Length - 1] + "" + "\"");
            sb.Append("\r\n");
            sb.Append("Content-Type: application/octet-stream");
            sb.Append("\r\n");
            sb.Append("\r\n");//value前面必须有2个换行  

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uploadurl);
            request.ContentType = "multipart/form-data; boundary=" + boundary;//其他地方的boundary要比这里多--  
            request.Method = "POST";

            FileStream fileStream = new FileStream(absolutepath, FileMode.OpenOrCreate, FileAccess.Read);

            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(sb.ToString());
            byte[] boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
            //http请求总长度  
            request.ContentLength = postHeaderBytes.Length + fileStream.Length + boundaryBytes.Length;
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);
            byte[] buffer = new Byte[checked((uint)Math.Min(4096, (int)fileStream.Length))];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                requestStream.Write(buffer, 0, bytesRead);
            }
            fileStream.Dispose();
            requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
            WebResponse webResponse2 = request.GetResponse();
            Stream htmlStream = webResponse2.GetResponseStream();
            StreamReader reader = new StreamReader(htmlStream);
            string HTML = reader.ReadToEnd();
            htmlStream.Dispose();
        }

        /// <summary>
        /// 全局错误捕获，获取http请求的信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static object GetRequestParams(HttpContext context)
        {
            try
            {
                Dictionary<string, string> _dic = new Dictionary<string, string>();
                foreach (var i in context.Request.Query)
                {
                    _dic.Add(i.Key, i.Value.ToString());
                }
                return new
                {
                    IP = context.Connection.RemoteIpAddress.ToString(),
                    QueryString = context.Request.QueryString.ToString(),
                    Method = context.Request.Method,
                    Path = context.Request.Path,
                    Query = _dic,
                };
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public class HttpHeader
        {
            public string contentType { get; set; }

            public string accept { get; set; }

            public string userAgent { get; set; }

            public string method { get; set; }

            public int maxTry { get; set; }
        }
    }
}