using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MJ.Core
{
    /// <summary>
    /// 邮件发送接口
    /// </summary>
    public class Email : IDisposable
    {
        private int _defalut_timeout = 30000;  //同步发送默认超时时间 单位：毫秒

        private SmtpClient _smtpClient = null;

        /// <summary>
        /// 
        /// </summary>
        public Email()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="senderServer"></param>
        /// <param name="senderServerPort"></param>
        /// <param name="authorizedCode"></param>

        public Email(string senderServer, int senderServerPort, string authorizedCode)
        {
            this.SenderServer = senderServer;
            this.SenderServerPort = senderServerPort;
            this.AuthorizedCode = authorizedCode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="senderServer"></param>
        /// <param name="senderServerPort"></param>
        /// <param name="senderAccount"></param>
        /// <param name="senderAccountPassword"></param>
        public Email(string senderServer, int senderServerPort, string senderAccount, string senderAccountPassword)
        {
            this.SenderServer = senderServer;
            this.SenderServerPort = senderServerPort;
            this.SenderAccount = senderAccount;
            this.SenderAccountPassword = senderAccountPassword;

        }

        /// <summary>
        /// 邮件发送接口
        /// </summary>
        /// <param name="to">收件人列表</param>
        /// <param name="cc">抄送人列表</param>
        /// <param name="mailSubject">标题</param>
        /// <param name="mailBody">邮件正文</param>
        public virtual void SendEmail(List<string> to, List<string> cc, string mailSubject, string mailBody)
        {
            if (to == null && to.Count <= 0) throw new Exception("请设定收件人");
            InitSmtpClient();
            MailMessage msg = GetMailMessage(to, cc, mailSubject, mailBody);
            _smtpClient.Send(msg);
            _smtpClient.Dispose();

        }


        /// <summary>
        /// 异步邮件发送完毕回调方法
        /// </summary>
        public SendCompletedEventHandler ActionSendCompletedCallback = null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="to"></param>
        /// <param name="cc"></param>
        /// <param name="mailSubject"></param>
        /// <param name="mailBody"></param>
        /// <param name="userToken"></param>
        public virtual void SendEmailAsync(List<string> to, List<string> cc, string mailSubject, string mailBody, object userToken = null)
        {
            InitSmtpClient();
            MailMessage msg = GetMailMessage(to, cc, mailSubject, mailBody);
            _smtpClient.SendAsync(msg, userToken);
            if (userToken != null)
            {
                _smtpClient.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
            }
           
        }


        /// <summary>
        /// 异步操作完成后执行回调方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SendCompletedCallback(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            //同一组件下不需要回调方法,直接在此写入日志即可
            //写入日志
            //return;
            
            string message = string.Empty;
            if (e.Cancelled)
            {
                message = "异步操作取消";
            }
            else if (e.Error != null)
            {
                message = (string.Format("UserState:{0},Message:{1}", Newtonsoft.Json.JsonConvert.SerializeObject(e.UserState), e.Error.ToString()));
            }
            else
            {
                //Console.WriteLine("SendCompletedCallback");
                // 没有错误返回一般发送成功，执行回调
                ActionSendCompletedCallback?.Invoke(sender, e);
            }

            //if (!string.IsNullOrEmpty(message))
            //{
            //    throw new Exception(message);
            //}

            
            //执行回调方法
            _smtpClient.Dispose();
        }




        #region 私有方法


        #region 通过属性获取Smtp客户端

        private void InitSmtpClient()
        {
            _smtpClient = new SmtpClient();
            _smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式
            _smtpClient.Host = this.SenderServer;//邮件服务器
            _smtpClient.Port = this.SenderServerPort;   // 邮件端口
            _smtpClient.Timeout = _defalut_timeout;
            if (!string.IsNullOrEmpty(this.SenderAccount) && !string.IsNullOrEmpty(this.SenderAccountPassword))
            {
                _smtpClient.UseDefaultCredentials = true;
                _smtpClient.Credentials = new System.Net.NetworkCredential(this.SenderAccount, this.SenderAccountPassword);//用户名、密码
            }
            else
            {
                _smtpClient.UseDefaultCredentials = false;
            }
        }


        #endregion

        #region 初始化MailMessage

        /// <summary>
        /// 
        /// </summary>
        /// <param name="to"></param>
        /// <param name="cc"></param>
        /// <param name="mailSubject"></param>
        /// <param name="mailBody"></param>
        /// <returns></returns>
        private MailMessage GetMailMessage(List<string> to, List<string> cc, string mailSubject, string mailBody)
        {
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

            msg.From = new MailAddress(this.SenderAccount, string.IsNullOrEmpty(this.SenderDisplayName) ? this.SenderAccount : this.SenderDisplayName, Encoding.UTF8);
            foreach (string toUser in to)
            {
                msg.To.Add(toUser);
            }
            if (cc != null && cc.Count > 0)
            {
                foreach (string ccUser in cc)
                {
                    msg.CC.Add(ccUser);
                }
            }

            msg.Subject = mailSubject;//邮件标题   
            msg.Body = mailBody;//邮件内容   
            msg.BodyEncoding = System.Text.Encoding.UTF8;//邮件内容编码   
            msg.IsBodyHtml = true;//是否是HTML邮件   
            msg.Priority = MailPriority.High;//邮件优先级

            return msg;
        }


        #endregion


        #endregion

        #region 基本属性
        /// <summary>
        /// 发件服务器
        /// </summary>
        public string SenderServer { get; set; }

        /// <summary>
        /// 发件服务器端口
        /// </summary>
        public int SenderServerPort { get; set; } = 25;

        /// <summary>
        /// 发件账户
        /// </summary>
        public string SenderAccount { get; set; }

        /// <summary>
        /// 发件密码
        /// </summary>

        public string SenderAccountPassword { get; set; }

        /// <summary>
        /// 授权码
        /// </summary>
        public string AuthorizedCode { get; set; }

        /// <summary>
        /// 发件人显示名
        /// </summary>
        public string SenderDisplayName { get; set; }



        #endregion

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                    this._smtpClient.Dispose();
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        // TODO: 仅当以上 Dispose(bool disposing) 拥有用于释放未托管资源的代码时才替代终结器。
        // ~Email() {
        //   // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
        //   Dispose(false);
        // }

        // 添加此代码以正确实现可处置模式。
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
