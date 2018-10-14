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
    class Program
    {
        const string mainMailAddress = "ksiegarnia.longinus@gmail.com";
        const string mainMailPassword = "Myszykiszki18";

        public static void SendTestMessage(string emailTargetAdress, string emailBody)
        {
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
            Message message = client.GetMessage(count);
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
        }

        static void Main(string[] args)
        {
            Console.WriteLine("####Test wyslania wiadomosci####\n");

            Console.Write("Wpisz docelowy adres email: ");
            string emailTargetAdress = Console.ReadLine();
            Console.WriteLine("Wpisz tresc (i zatwierdz Enterem): ");
            string  emailBody = Console.ReadLine();

            SendTestMessage(emailTargetAdress, emailBody);
            SendTestMessage("ksiegarnia.longinus@gmail.com", emailBody);

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
