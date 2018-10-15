import poplib
import smtplib

from email.message import EmailMessage

email_adress = 'ksiegarnia.longinus@gmail.com'
email_pass = 'Myszykiszki18'
email_addrressee = 'ksiegarnia.longinus@gmail.com'


def send_test_message(sender: str, password: str, addressee: str, msg_number=''):
    client = smtplib.SMTP_SSL('smtp.gmail.com', 465)
    client.login(sender, password)

    msg = EmailMessage()
    msg['Subject'] = 'Python test message'
    msg['From'] = sender
    msg['To'] = addressee
    msg.set_content("This is test message " + msg_number)

    client.send_message(msg)


def main():
    for i in range(5):
        message_number = str(i + 1)
        send_test_message(email_adress, email_pass, email_addrressee, msg_number=message_number)


if __name__ == '__main__':
    main()