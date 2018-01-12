// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MailHelper.cs" company="Wedn.Net">
//     Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>
//   邮件发送助手类  0.10
//   Verion:0.10
//   Description:通过SMTP发送邮件
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Micua.Infrastructure.Utility
{
    using System;
    using System.Linq;
    using System.Net.Mail;
    using System.Text;

    /// <summary>
    /// 邮件发送助手类
    /// </summary>
    /// <remarks>
    ///  2013-11-18 18:56 Created By iceStone
    /// </remarks>
    public static class MailHelper
    {
        private readonly static string SmtpServer = Setting.GetString("smtp_server", "smtp.wedn.net");
        private readonly static int SmtpServerPort = Setting.GetInt32("smtp_server_port", 25);
        private readonly static bool SmtpEnableSsl = Setting.GetBoolean("smtp_enablessl", false);
        private readonly static string SmtpUsername = Setting.GetString("smtp_username", "server@wedn.net");
        private readonly static string SmtpDisplayName = Setting.GetString("smtp_display_name", "Wedn.Net");
        private readonly static string SmtpPassword = Setting.GetString("smtp_password", "123456");
        //private readonly static string PopServer = Setting.GetString("pop_server", "pop.wedn.net");
        //private readonly static int PopServerPort = Setting.GetInt32("pop_server_port", 110);
        //private readonly static bool PopEnableSsl = Setting.GetBoolean("pop_enablessl", false);
        //private readonly static string PopUsername = Setting.GetString("pop_username", "server.wedn.net");
        //private readonly static string PopPassword = Setting.GetString("pop_password", "5love100");

        /// <summary>
        /// 发送邮件到指定收件人
        /// </summary>
        /// <remarks>
        ///  2013-11-18 18:55 Created By iceStone
        /// </remarks>
        /// <param name="to">收件人地址</param>
        /// <param name="subject">主题</param>
        /// <param name="mailBody">正文内容(支持HTML)</param>
        /// <param name="copyTos">抄送地址列表</param>
        /// <returns>是否发送成功</returns>
        public static bool Send(string to, string subject, string mailBody, params string[] copyTos)
        {
            #region Old
            //if (string.IsNullOrEmpty(to)) return false;
            ////创建Email实体
            //var message = new MailMessage
            //{
            //    From = new MailAddress(SmtpUserName, Setting.GetString(Setting.Key.site_name)),
            //    Subject = subject,
            //    Body = mailBody,
            //    BodyEncoding = Encoding.UTF8,
            //    IsBodyHtml = true
            //};
            ////插入收件人地址和抄送地址
            //message.To.Add(new MailAddress(to));
            //foreach (var copyTo in copyTos.Where(c => !string.IsNullOrEmpty(c)))
            //{
            //    message.CC.Add(new MailAddress(copyTo));
            //}
            ////创建SMTP客户端
            //var client = new SmtpClient
            //{
            //    Host = SmtpServer,
            //    Credentials = new System.Net.NetworkCredential(SmtpUserName, SmtpPassword),
            //    DeliveryMethod = SmtpDeliveryMethod.Network,
            //    EnableSsl = SmtpEnableSsl,
            //    Port = SmtpServerPort
            //};
            //try
            //{
            //    //发送邮件
            //    client.Send(message);
            //    return true;
            //}
            //catch (Exception)
            //{
            //    throw;
            //} 
            #endregion

            return Send(new[] { to }, subject, mailBody, copyTos, new string[] { }, MailPriority.Normal);
        }

        /// <summary>
        /// 发送邮件到指定收件人
        /// </summary>
        /// <remarks>
        ///  2013-11-18 18:55 Created By iceStone
        /// </remarks>
        /// <param name="tos">收件人地址列表</param>
        /// <param name="subject">主题</param>
        /// <param name="mailBody">正文内容(支持HTML)</param>
        /// <param name="ccs">抄送地址列表</param>
        /// <param name="bccs">密件抄送地址列表</param>
        /// <param name="priority">此邮件的优先级</param>
        /// <param name="attachments">附件列表</param>
        /// <returns>是否发送成功</returns>
        /// <exception cref="System.ArgumentNullException">attachments</exception>
        public static bool Send(string[] tos, string subject, string mailBody, string[] ccs, string[] bccs, MailPriority priority, params Attachment[] attachments)
        {
            if (attachments == null) throw new ArgumentNullException("attachments");
            if (tos.Length == 0) return false;
            //创建Email实体
            var message = new MailMessage();
            message.From = new MailAddress(SmtpUsername, SmtpDisplayName);
            message.Subject = subject;
            message.Body = mailBody;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            message.Priority = priority;
            //插入附件
            foreach (var attachment in attachments)
            {
                message.Attachments.Add(attachment);
            }
            //插入收件人地址,抄送地址和密件抄送地址
            foreach (var to in tos.Where(c => !string.IsNullOrEmpty(c)))
            {
                message.To.Add(new MailAddress(to));
            }
            foreach (var cc in ccs.Where(c => !string.IsNullOrEmpty(c)))
            {
                message.CC.Add(new MailAddress(cc));
            }
            foreach (var bcc in bccs.Where(c => !string.IsNullOrEmpty(c)))
            {
                message.CC.Add(new MailAddress(bcc));
            }
            //创建SMTP客户端
            var client = new SmtpClient
            {
                Host = SmtpServer,
                Credentials = new System.Net.NetworkCredential(SmtpUsername, SmtpPassword),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                EnableSsl = SmtpEnableSsl,
                Port = SmtpServerPort
            };
            //client.SendCompleted += Client_SendCompleted;
            //try
            //{
            //发送邮件
            client.Send(message);
            //client.SendAsync(message,DateTime.Now.ToString());

            //client.Dispose();
            //message.Dispose();
            return true;
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
        }
        ///// <summary>
        ///// 发送完成执行事件
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //static void Client_SendCompleted(object sender, AsyncCompletedEventArgs e)
        //{
        //    var token = (string)e.UserState;
        //    if (e.Cancelled)
        //    {
        //        Console.WriteLine("[{0}] Send canceled.", token);
        //    }
        //    if (e.Error != null)
        //    {
        //        Console.WriteLine("[{0}] {1}", token, e.Error);
        //    }
        //    else
        //    {
        //        Console.WriteLine("Message sent.");
        //    }
        //    var client = sender as SmtpClient;
        //    if (client != null) client.Dispose();
        //}

        //public static IList<MailMessage> Receive()
        //{
        //    var tcpClient = new TcpClient(PopServer, PopServerPort);
        //    var networkStream = tcpClient.GetStream();
        //}
    }
}
