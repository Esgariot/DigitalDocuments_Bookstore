using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Mail;

using OpenPop.Pop3;
using OpenPop.Mime;

using System.IO;
using System.ComponentModel;

namespace GUI_Prototype
{
    public enum MailStatus
    {
        CLIENT_OFFER,
        CLIENT_OFFER_REJECTED,
        CLIENT_OFFER_ACCEPTED,
        ADDING_PRODUCTS_REQUEST,
        ADDING_PRODUCTS_RESPONSE,
        PRODUCTS_LIST_MERGED,
        PRODUCTS_LIST_ACCEPTED,
        RESPONSE_TO_CLIENT,
        RESPONSE_TO_SERVER,
        RESPONSE_TO_ARCHIVE,
        TEST
    }

    public class Attachment
    {
        public string Filename { get; set; }
        public string Extension { get; set; }
        public byte[] Body { get; set; }
    }

    public class Email
    {
        public string From { get; set; }
        public MailStatus State { get; set; }
        public string Subject { get; set; }
        public TypeEnum Type { get; set; }
        public List<Attachment> Attachments { get; set; }
        public string Date { get; set; }//not neccesarily string
        //public DateTime Date { get; set; }

        /*public enum StateEnum
        {
            New,
            Rejected,
            InProgress
        }*/

        public enum TypeEnum
        {
            Offer
        }
    }
    public class EmailWrapper
    {
        const string mainMailAddress = "ksiegarnia.longinus@gmail.com";
        const string mainMailPassword = "Myszykiszki18";
        List<Message> messages;
        public BindingList<Email> Emails { get; set; }
        //maybe not neccesary at all
        Dictionary<string, MailStatus> MailStatusDictionary = new Dictionary<string, MailStatus>
        {
            {"CLIENT_OFFER", MailStatus.CLIENT_OFFER},
            {"CLIENT_OFFER_REJECTED", MailStatus.CLIENT_OFFER_REJECTED},
            {"CLIENT_OFFER_ACCEPTED", MailStatus.CLIENT_OFFER_ACCEPTED},
            {"ADDING_PRODUCTS_REQUEST", MailStatus.ADDING_PRODUCTS_REQUEST},
            {"ADDING_PRODUCTS_RESPONSE", MailStatus.ADDING_PRODUCTS_RESPONSE},
            {"PRODUCTS_LIST_MERGED", MailStatus.PRODUCTS_LIST_MERGED},
            {"PRODUCTS_LIST_ACCEPTED", MailStatus.PRODUCTS_LIST_ACCEPTED},
            {"RESPONSE_TO_CLIENT", MailStatus.RESPONSE_TO_CLIENT},
            {"RESPONSE_TO_SERVER", MailStatus.RESPONSE_TO_SERVER},
            {"RESPONSE_TO_ARCHIVE", MailStatus.RESPONSE_TO_ARCHIVE},
            {"TEST", MailStatus.TEST }
        };

        public EmailWrapper()
        {
            messages = ReceiveAllMessages();
            Emails = new BindingList<Email>();

            foreach(Message message in messages)
            {
                string applicationHeader = message.Headers.UnknownHeaders["X-LonginusMailStatus"];

                if(applicationHeader == null || applicationHeader.Length == 0)
                {
                    continue;

                    //debug
                    //applicationHeader = "TEST";
                }

                string date = "";
                try
                {
                    date = DateTime.Parse(message.Headers.Date).ToString("dd-MM-yyyy HH:mm:ss");
                }
                catch
                {
                    date = message.Headers.Date;
                }

                Emails.Add(new Email {
                    From = message.Headers.From.ToString(),
                    State = MailStatusDictionary[applicationHeader],
                    Subject = message.Headers.Subject,
                    Type = Email.TypeEnum.Offer,
                    Attachments = new List<Attachment>(),
                    Date = date });

                List<MessagePart> attachments = message.FindAllAttachments();

                foreach(MessagePart attachment in attachments)
                {
                    Emails.Last().Attachments.Add(new Attachment {
                        Filename = attachment.FileName,
                        Body = attachment.Body,
                        Extension = attachment.FileName.Split('.').Last() });
                }
            }
        }

        //if there was an application-wide variable indicating step in the process, we could
        //ditch this function and limit the bindinglist instead
        public BindingList<Email> EmailsWithStatus(MailStatus status)
        {
            return new BindingList<Email>(Emails.Where(e => e.State == status).ToList());
        }

        public static List<Message> ReceiveAllMessages()
        {
            Pop3Client client = new Pop3Client();
            client.Connect("pop.gmail.com", 995, true);

            client.Authenticate(mainMailAddress, mainMailPassword);

            List<Message> messages = new List<Message>();
            for(int i = 1; i <= client.GetMessageCount(); i++)
            {
                messages.Add(client.GetMessage(i));
            }

            return messages;
        }
    }
}
