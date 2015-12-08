using System;
using System.Text;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Globalization;

namespace RevStack.Net
{
    public static class Http
    {
        #region "public"

        public static string Get(string address)
        {
            try
            {
                var result = "";

                using (var client = new WebClient())
                {
                    using (var reader = new StreamReader(client.OpenRead(address)))
                    {
                        var s = reader.ReadToEnd();
                        result = s;
                    }
                }

                return result;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static HttpResult Get(string address, string baseUrl)
        {
            var httpResult = new HttpResult();
            try
            {
                var result = "";

                using (var client = new WebClient())
                {
                    using (var reader = new StreamReader(client.OpenRead(address)))
                    {
                        var s = reader.ReadToEnd();
                        result = s;
                    }
                }
                httpResult.Html = result;
                httpResult.StatusCode = HttpStatusCode.OK;
                httpResult.Error = "";
                return httpResult;
            }
            catch (Exception e)
            {
                httpResult.Html = "";
                if (e.GetType().Name == "WebException")
                {
                    var we = (WebException)e;
                    var response = (HttpWebResponse)we.Response;
                    httpResult.StatusCode = response.StatusCode;
                    httpResult.Error = we.Message;
                }
                else
                {
                    httpResult.StatusCode = HttpStatusCode.InternalServerError;
                    httpResult.Error = e.Message;
                }

                return httpResult;
            }
        }

        public static HttpResult Get(string address, string username, string password)
        {
            var httpResult = new HttpResult();
            try
            {
                var result = "";

                using (var client = new WebClient())
                {
                    client.Headers.Add("Authorization",
                        "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(username + ":" + password)));
                    using (var reader = new StreamReader(client.OpenRead(address)))
                    {
                        var s = reader.ReadToEnd();
                        result = s;
                    }
                }
                httpResult.Html = result;
                httpResult.StatusCode = HttpStatusCode.OK;
                httpResult.Error = "";
                return httpResult;
            }
            catch (Exception e)
            {
                httpResult.Html = "";
                if (e.GetType().Name == "WebException")
                {
                    var we = (WebException)e;
                    var response = (HttpWebResponse)we.Response;
                    httpResult.StatusCode = response.StatusCode;
                    httpResult.Error = we.Message;
                }
                else
                {
                    httpResult.StatusCode = HttpStatusCode.InternalServerError;
                    httpResult.Error = e.Message;
                }

                return httpResult;
            }
        }

        public static bool IsValidEmail(string strIn)
        {
            if (String.IsNullOrEmpty(strIn))
                return false;

            // Use IdnMapping class to convert Unicode domain names. 
            try
            {
                strIn = Regex.Replace(strIn, @"(@)(.+)$", DomainMapper,
                    RegexOptions.None, TimeSpan.FromMilliseconds(200));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }

            if (!isValidDomain)
                return false;

            // Return true if strIn is in valid e-mail format. 
            try
            {
                return Regex.IsMatch(strIn,
                    @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                    @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        public static string ConvertPathToUrl(string path, string domain)
        {
            var url = "";
            if (path.IndexOf("http://") == -1)
            {
                url = "http://" + domain + path;
            }
            else
            {
                url = path;
            }
            return path;
        }

        public static string ConvertPathToUrl(string path, string domain, string port)
        {
            var url = "";
            if (path.IndexOf("http://") == -1)
            {
                url = "http://" + domain + ":" + port + path;
            }
            else
            {
                url = path;
            }
            return path;
        }

        public static string ConvertPathToUrl(string path, string domain, string host, string port)
        {
            var url = "";
            if (path.IndexOf("http://") == -1)
            {
                url = "http://" + host + "." + domain + ":" + port + path;
            }
            else
            {
                url = path;
            }
            return path;
        }

        public static BasicCredentials DecodeBasicAuthorization(string auth)
        {
            var basicCredentials = new BasicCredentials();
            try
            {
                var decoded = Encoding.ASCII.GetString(Convert.FromBase64String(auth));
                var authArray = decoded.Split(':');
                var user = authArray[0];
                var pass = authArray[1];
                basicCredentials.Username = user;
                basicCredentials.Password = pass;
                basicCredentials.Error = false;
                return basicCredentials;
            }
            catch (Exception)
            {
                basicCredentials.Error = true;
                return basicCredentials;
            }
        }

        #endregion

        #region "private"

        private static bool isValidDomain = true;

        private static string DomainMapper(Match match)
        {
            // IdnMapping class with default property values.
            var idn = new IdnMapping();

            var domainName = match.Groups[2].Value;
            try
            {
                domainName = idn.GetAscii(domainName);
            }
            catch (ArgumentException)
            {
                isValidDomain = false;
            }
            return match.Groups[1].Value + domainName;
        }

        #endregion
    }
}
