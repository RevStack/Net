using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace RevStack.Net
{
    public static class Smtp
    {

        private const string MAIL_HOST = "Revstack.Mail.Host";
        /// <summary>
        /// </summary>
        /// <param name="to"></param>
        /// <param name="from"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="isHTML"></param>
        /// <returns></returns>
        public static bool SendMail(string to, string from, string subject, string body, bool isHTML)
        {
            try
            {
                var objMail = new MailMessage();
                var fromMail = new MailAddress(from);
                objMail.To.Add(to);
                objMail.From = fromMail;
                objMail.Priority = MailPriority.Normal;
                objMail.Subject = subject;
                objMail.Body = body;
                objMail.IsBodyHtml = isHTML;

                var client = new SmtpClient();
                client.Send(objMail);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="to"></param>
        /// <param name="from"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="isHTML"></param>
        /// <param name="host"></param>
        /// <param name="credentials"></param>
        /// <returns></returns>
        public static bool SendMail(string to, string from, string subject, string body, bool isHTML, string host,
            NetworkCredential credentials)
        {
            try
            {
                var objMail = new MailMessage();
                var fromMail = new MailAddress(from);
                objMail.To.Add(to);
                objMail.From = fromMail;
                objMail.Priority = MailPriority.Normal;
                objMail.Subject = subject;
                objMail.Body = body;
                objMail.IsBodyHtml = isHTML;

                var client = new SmtpClient(host);
                client.Credentials = credentials;
                client.Send(objMail);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="to"></param>
        /// <param name="from"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="isHTML"></param>
        /// <returns></returns>
        public static bool SendMail(List<string> to, string from, string subject, string body, bool isHTML)
        {
            try
            {
                //var mailServer = ConfigurationManager.AppSettings[MAIL_HOST];
                var objMail = new MailMessage();
                var fromMail = new MailAddress(from);
                foreach (var item in to)
                {
                    objMail.To.Add(item);
                }
                objMail.From = fromMail;
                objMail.Priority = MailPriority.High;
                objMail.Subject = subject;
                objMail.Body = body;
                objMail.IsBodyHtml = isHTML;

                var client = new SmtpClient();
                client.Send(objMail);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="to"></param>
        /// <param name="from"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="isHTML"></param>
        /// <param name="host"></param>
        /// <param name="credentials"></param>
        /// <returns></returns>
        public static bool SendMail(List<string> to, string from, string subject, string body, bool isHTML, string host,
            NetworkCredential credentials)
        {
            try
            {
                var objMail = new MailMessage();
                var fromMail = new MailAddress(from);
                foreach (var item in to)
                {
                    objMail.To.Add(item);
                }
                objMail.From = fromMail;
                objMail.Priority = MailPriority.High;
                objMail.Subject = subject;
                objMail.Body = body;
                objMail.IsBodyHtml = isHTML;

                var client = new SmtpClient(host);
                client.Credentials = credentials;
                client.Send(objMail);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
