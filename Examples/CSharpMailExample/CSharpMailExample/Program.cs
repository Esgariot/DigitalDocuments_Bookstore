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

namespace CSharpMailExample
{
    enum MailStatus
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

    class Program
    {
        const string mainMailAddress = "ksiegarnia.longinus@gmail.com";
        const string mainMailPassword = "Myszykiszki18";

        public static void SendTestMessage(string emailTargetAdress, string emailBody, MailStatus messeageStatus)
        {
            MailAddress ownMailAddress = new MailAddress(mainMailAddress);

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(mainMailAddress, mainMailPassword),
                EnableSsl = true
            };

            MailMessage mail = new MailMessage(mainMailAddress, emailTargetAdress);
            mail.Subject = "Ksiegarnia Longinus. Wiadomosc testowa.";
            mail.Body = "Dzien dobry, to wiadomosc testowa. \n"+ emailBody;

            MailAddress replyTo = new MailAddress("ksiegarnia.longinus@gmail.com");
            mail.ReplyToList.Add(replyTo);

            mail.Attachments.Add(new Attachment(".\\PlikTXT.txt"));

            mail.Headers["X-LonginusMailStatus"] = messeageStatus.ToString();

            try
            {
                client.Send(mail);
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

        public static void ReceiveTestMessage()
        {
            Pop3Client client = new Pop3Client();
            client.Connect("pop.gmail.com", 995, true);

            client.Authenticate(mainMailAddress, mainMailPassword);

            var count = client.GetMessageCount();
            //that means get last messeage (a count one)
            Message message = client.GetMessage(count);
            //  if we want to get all messages, there should be this:
            /*
            List<Message> messageList = new List<Message>();
            for (int i = 0; i < count; i++)
                messageList.Add(client.GetMessage(i));
            */

            Console.WriteLine("Subject: "+message.Headers.Subject);
            Console.WriteLine("Body:");
            Console.WriteLine(message.MessagePart.MessageParts[0].GetBodyAsText());

            foreach (var attachment in message.FindAllAttachments())
            {
                string filePath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "nowy_"+attachment.FileName);
                FileStream Stream = new FileStream(filePath, FileMode.Create);
                BinaryWriter BinaryStream = new BinaryWriter(Stream);
                BinaryStream.Write(attachment.Body);
                BinaryStream.Close();
            }

            Console.WriteLine("Headers: ");
            string[] keys = message.Headers.UnknownHeaders.AllKeys;
            foreach (string key in keys)
                Console.WriteLine("\t" + key + ": " + message.Headers.UnknownHeaders[key]);
            //or just simply
            //Console.WriteLine("\tStatus: " + message.Headers.UnknownHeaders["X-LonginusMailStatus"]);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("####Test wyslania wiadomosci####\n");

            Console.Write("Wpisz docelowy adres email: ");
            string emailTargetAdress = Console.ReadLine();
            Console.WriteLine("Wpisz tresc (i zatwierdz Enterem): ");
            string  emailBody = Console.ReadLine();

            MailStatus mailStatus = MailStatus.CLIENT_OFFER;

            if (emailTargetAdress.CompareTo("") == 1)
                SendTestMessage(emailTargetAdress, emailBody, mailStatus);
            SendTestMessage("ksiegarnia.longinus@gmail.com", emailBody, mailStatus);

            Console.WriteLine("\n Wiadomosc do " + emailTargetAdress +" i " + mainMailAddress + " zostala wyslana! \n" +
                "Nacisnij klawisz by przejsc do testu odbierania wiadomosci.\n");
            Console.ReadKey();

            Console.WriteLine("####Test odbierania wiadomosci####\n");

            ReceiveTestMessage();

            Console.WriteLine("\n Ostatnia wiadomosc w skrzynce pocztowej " + mainMailAddress + " zostala odczytana! \n" +
                "Nacisnij klawisz by zakonczyc testy.\n");
            Console.ReadKey();
        }
    }
}
