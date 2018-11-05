using OpenPop.Mime;
using OpenPop.Pop3;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpEmailLibrary
{
    public class MailReceiver
    {
        private const string popServerAddress = "pop.gmail.com";
        private const int popServerPort = 995;

        private Pop3Client client;

        public MailReceiver(string mailAddress, string mailPassword)
        {
            this.client = new Pop3Client();
            this.client.Connect(popServerAddress, popServerPort, true);
            this.client.Authenticate(mailAddress, mailPassword);
        }

        public Mail GetLastMessage()
        {
            int lastMessageNumber = client.GetMessageCount();
            Message message = client.GetMessage(lastMessageNumber);

            Mail receivedMail = new Mail(message);

            return receivedMail;
        }
    }
}
