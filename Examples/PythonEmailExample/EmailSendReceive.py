import email
import poplib
import smtplib

from email.message import EmailMessage

email_address = 'ksiegarnia.longinus@gmail.com'
email_pass = 'Myszykiszki18'
email_addressee = 'ksiegarnia.longinus@gmail.com'


def send_test_message(sender: str, password: str, addressee: str, msg_number=''):
    client = smtplib.SMTP_SSL('smtp.gmail.com', 465)
    client.login(sender, password)

    msg = EmailMessage()
    msg['Subject'] = 'Python test message'
    msg['From'] = sender
    msg['To'] = addressee
    msg.set_content("This is test message " + msg_number)

    client.send_message(msg)
    client.quit()


def receive_test_message(user_email: str, password: str):
    client = poplib.POP3_SSL('pop.gmail.com', 995)
    client.user(user_email)
    client.pass_(password)

    messages = client.list()[1]
    for i in range(len(messages)):
        (client_msg, body, _) = client.retr(i+1)
        for j in body:
            msg = email.message_from_string(j.decode("utf-8"))
            text = msg.get_payload()
            if msg['Subject']:
                print("Subject: " + msg['Subject'])

            if msg['From']:
                print("From: " + msg['From'])

            if msg['To']:
                print("To: " + msg['To'])

            if text:
                print(text + '\n')

    client.quit()


def main():
    for i in range(5):
        message_number = str(i + 1)
        send_test_message(email_address, email_pass, email_addressee, msg_number=message_number)
    receive_test_message(email_address, email_pass)


if __name__ == '__main__':
    main()
