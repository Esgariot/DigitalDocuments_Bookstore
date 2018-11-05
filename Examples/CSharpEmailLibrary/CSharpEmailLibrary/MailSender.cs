using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace CSharpEmailLibrary
{
    public class MailSender
    {
        private const string smtpServerAddress = "smtp.gmail.com";
        private const int smtpServerPort = 587;

        private SmtpClient client;

        public MailSender(string mailAddress, string mailPassword)
        {
            this.client = new SmtpClient(smtpServerAddress, smtpServerPort)
            {
                Credentials = new NetworkCredential(mailAddress, mailPassword),
                EnableSsl = true
            };
        }

        public void SendMail(Mail mailToSend)
        {
            try
            {
                client.Send(mailToSend.GetMessageToSend());
            }
            catch (SmtpException ex)
            {
                throw new ApplicationException
                  ("SmtpException has occured: " + ex.Message);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
