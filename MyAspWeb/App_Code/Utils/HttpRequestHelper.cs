using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

namespace MyAspWeb
{
    public class HttpRequestHelper
    {
        private HttpWebRequest request = null;
        private HttpWebResponse response = null;
        public static CookieContainer cookieContainer
        {
            get;
            set;
        }

        public static string NextRequestUrl
        {
            get;
            set;
        }

        public static string CookiesString
        {
            get;
            set;
        }

        public HttpRequestHelper()
        {
           
        }

        /// <summary>
        /// 发送Get请求
        /// </summary>
        /// <param name="requestUrl">请求Url</param>
        /// <returns>JObject JsonObject</returns>
        public JObject SendGetRequest(string requestUrl)
        {
            request = (HttpWebRequest)WebRequest.Create(requestUrl);
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Method = "GET";
            if (CookiesString != null)
            {
                request.Headers.Add("Cookie:" + CookiesString);
            }
            request.KeepAlive = true;
            request.AllowAutoRedirect = false;
            if (cookieContainer != null)
            {
                request.CookieContainer = cookieContainer;
            }
            else
            {
                request.CookieContainer = new CookieContainer();
                cookieContainer = request.CookieContainer;
            }

            response = (HttpWebResponse)request.GetResponse();
            response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);
            CookieCollection cook = response.Cookies;
            CookiesString = request.CookieContainer.GetCookieHeader(request.RequestUri);

            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string content = sr.ReadToEnd();
            sr.Close();
            request.Abort();
            response.Close();

            return JObject.Parse(content);
        }

        /// <summary>
        /// 发送Post请求
        /// </summary>
        /// <param name="requestUrl">请求Url</param>
        /// <param name="postdata">数据</param>
        /// <returns>JObject JsonObject</returns>
        public JObject SendPostRequest(string requestUrl, string postdata)
        {
            request = (HttpWebRequest) WebRequest.Create(requestUrl);
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Method = "GET";
            request.KeepAlive = true;
            request.ContentType = "application/x-www-form-urlencoded";
            request.AllowAutoRedirect = false;
            if (cookieContainer != null)
            {
                request.CookieContainer = cookieContainer;
            }
            else
            {
                request.CookieContainer = new CookieContainer();
                cookieContainer = request.CookieContainer;
            }

            byte[] postdatabytes = Encoding.UTF8.GetBytes(postdata);
            request.ContentLength = postdatabytes.Length;
            Stream stream = request.GetRequestStream();
            stream.Write(postdatabytes, 0, postdatabytes.Length);
            stream.Close();

            response = (HttpWebResponse) request.GetResponse();

            response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);
            CookieCollection cook = response.Cookies;
            string strcrook = request.CookieContainer.GetCookieHeader(request.RequestUri);
            CookiesString = strcrook;

            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string content = sr.ReadToEnd();
            sr.Close();
            request.Abort();
            response.Close();

            return JObject.Parse(content);
        }
    }
}