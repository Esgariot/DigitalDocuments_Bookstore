import email
import smtplib
import imaplib

from email.mime.application import MIMEApplication
from email.mime.multipart import MIMEMultipart
from email.mime.text import MIMEText

email_address = 'ksiegarnia.longinus@gmail.com'
email_pass = 'Myszykiszki18'
email_addressee = 'ksiegarnia.longinus@gmail.com'


def send_test_message(sender: str, password: str, addressee: str, msg_number='', files=[]):
    client = smtplib.SMTP_SSL('smtp.gmail.com', 465)
    client.login(sender, password)

    msg = MIMEMultipart()
    msg['Subject'] = 'Python test message'
    msg['From'] = sender
    msg['To'] = addressee
    msg['CustomHeader'] = 'test header'
    msg.attach(MIMEText("This is test message " + msg_number))

    for file in files:
        with open(file, 'rb') as attachment_file:
            attachment = MIMEApplication(attachment_file.read())

        attachment['Content-Disposition'] = 'attachment; filename="%s"' % file
        msg.attach(attachment)

    client.send_message(msg)
    client.quit()


def receive_test_message(user_email: str, password: str):
    con = imaplib.IMAP4_SSL('imap.gmail.com')
    con.login(user_email, password)
    con.select('INBOX')

    typ, data = con.search(None, 'ALL')
    email_number = 0
    for num in data[0].split():
        typ, data = con.fetch(num, '(RFC822)')
        email_number += 1
        text = data[0][1]
        msg = email.message_from_bytes(text)

        print("Subject: " + msg['Subject'])
        print("From: " + msg['From'])
        print("To: " + msg['To'])
        print("Custom header: " + msg['CustomHeader'])
        print(msg.get_payload()[0].get_payload() + '\n')

        for part in msg.walk():
            if part.get_content_maintype() == 'multipart':
                continue
            if part.get('Content-Disposition') is None:
                continue
            filename = part.get_filename()
            data = part.get_payload(decode=True)
            if not data:
                continue
            f = open('attachment' + str(email_number) + filename, 'w')
            data = str(data).strip('\'').strip('b\'')
            f.write(data)
            f.close()

    con.close()
    con.logout()


def main():
    for i in range(2):
        message_number = str(i + 1)
        send_test_message(email_address, email_pass, email_addressee,
                          msg_number=message_number, files=['ExampleAttachment.txt'])
    receive_test_message(email_address, email_pass)


if __name__ == '__main__':
    main()
