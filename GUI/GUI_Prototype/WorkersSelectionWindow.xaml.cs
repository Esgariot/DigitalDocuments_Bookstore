using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using System.Net;
using System.Net.Mail;

namespace GUI_Prototype
{
    /// <summary>
    /// Interaction logic for WorkersChooseWindow.xaml
    /// </summary>
    public partial class WorkersChooseWindow : Window
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

        const string mainMailAddress = "ksiegarnia.longinus@gmail.com";
        const string mainMailPassword = "Myszykiszki18";

        List<string> workerMailAddresses = new List<string>()
        {
            "ks.longinus.dzial1@gmail.com",
            "ks.longinus.dzial2@gmail.com",
            "ks.longinus.dzial3@gmail.com",
            "ks.longinus.dzial4@gmail.com",
        };

        public WorkersChooseWindow()
        {
            InitializeComponent();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void approveButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.department_1_Checkbox.IsChecked == true)
                sendWorkerRequestMessage(workerMailAddresses[0]);
            if (this.department_2_Checkbox.IsChecked == true)
                sendWorkerRequestMessage(workerMailAddresses[1]);
            if (this.department_3_Checkbox.IsChecked == true)
                sendWorkerRequestMessage(workerMailAddresses[2]);
            if (this.department_4_Checkbox.IsChecked == true)
                sendWorkerRequestMessage(workerMailAddresses[3]);

            this.Close();
        }

        private void sendWorkerRequestMessage(string workerMailAddress)
        {
            MailAddress ownMailAddress = new MailAddress(mainMailAddress);

            SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential(mainMailAddress, mainMailPassword),
                EnableSsl = true
            };

            MailMessage mail = new MailMessage(mainMailAddress, workerMailAddress);
            mail.Subject = "Ksiegarnia Longinus. Dodawanie produktu.";
            mail.Body = "Prosze wypelnic zalaczony plik produktami z Pani/Pana dzialu. \n";

            MailAddress replyTo = new MailAddress("ksiegarnia.longinus@gmail.com");
            mail.ReplyToList.Add(replyTo);

            //mail.Attachments.Add(new Attachment(".\\PlikTXT.txt"));

            mail.Headers["X-LonginusMailStatus"] = MailStatus.ADDING_PRODUCTS_REQUEST.ToString();

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

        private void approveButtonEnabling()
        {
            bool checkIfAnyCheckboxIsChecked = false;

            if (this.department_1_Checkbox.IsChecked == true ||
                    this.department_2_Checkbox.IsChecked == true ||
                    this.department_3_Checkbox.IsChecked == true ||
                    this.department_4_Checkbox.IsChecked == true)
                checkIfAnyCheckboxIsChecked = true;

            this.approveButton.IsEnabled = checkIfAnyCheckboxIsChecked;
        }

        private void department_1_Checkbox_Click(object sender, RoutedEventArgs e)
        {
           approveButtonEnabling();
        }

        private void department_2_Checkbox_Click(object sender, RoutedEventArgs e)
        {
            approveButtonEnabling();
        }

        private void department_3_Checkbox_Click(object sender, RoutedEventArgs e)
        {
            approveButtonEnabling();
        }

        private void department_4_Checkbox_Click(object sender, RoutedEventArgs e)
        {
            approveButtonEnabling();
        }
    }
}
