using OpenPop.Mime;
using System;
using System.IO;
using System.Net.Mail;

namespace CSharpEmailLibrary
{
    public class Mail
    {
        //Field below should be private,
        //  but I don't want to block other teammates 
        //  if they want to set/get something in that fields 
        //      but there isn't implemented any proper setter/getter
        public MailMessage messageToSend; 
        public Message messageReceived;

        #region Constructors

        public Mail(string senderMailAddress, string addresseMailAddress)
        {
            this.messageToSend = new MailMessage(senderMailAddress, addresseMailAddress);
        }

        public Mail(Message received)
        {
            this.messageReceived = received;
        }

        #endregion

        #region Subject

        public void SetSubject(string subject)
        {
            this.messageToSend.Subject = subject;
        }

        public string GetSubject()
        {
            return this.messageReceived.Headers.Subject;
        }

        #endregion

        #region Body

        public void SetBody(string body)
        {
            this.messageToSend.Body = body;
        }

        public string GetBody()
        {
            return this.messageReceived.MessagePart.MessageParts[0].GetBodyAsText();
        }

        #endregion

        #region ReplyTo

        public void SetReplyTo(string replyToAddress)
        {
            this.messageToSend.ReplyToList.Add(replyToAddress);
        }

        //TODO: Getter from revceived one

        #endregion

        #region Attachment

        public void AddAttachment(string filePath)
        {
            Attachment attachment = new Attachment(filePath);
            this.messageToSend.Attachments.Add(attachment);
        }

        public void GetAttachments(string prefix="new_")
        {
            foreach (var attachment in this.messageReceived.FindAllAttachments())
            {
                string filePath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, prefix + attachment.FileName);
                FileStream Stream = new FileStream(filePath, FileMode.Create);
                BinaryWriter BinaryStream = new BinaryWriter(Stream);
                BinaryStream.Write(attachment.Body);
                BinaryStream.Close();
            }
        }

        #endregion

        #region Header

        public void SetHeader(string headerKey, string headerValue)
        {
            this.messageToSend.Headers[headerKey] = headerValue;
        }

        public string GetHeader(string headerKey)
        {
            return this.messageReceived.Headers.UnknownHeaders[headerKey];
        }

        #endregion

        public MailMessage GetMessageToSend()
        {
            return this.messageToSend;
        }
    }
}
