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

using CSharpEmailLibrary;

namespace GUI_Prototype
{
    /// <summary>
    /// Interaction logic for WorkersChooseWindow.xaml
    /// </summary>
    public partial class WorkersChooseWindow : Window
    {
        List<string> workerMailAddresses;

        public WorkersChooseWindow()
        {
            InitializeComponent();
            workerMailAddresses = Utils.getWorkersEmails();
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
            Mail mail = new Mail(CSharpEmailLibrary.MailsCredentials.mainMailAddress, workerMailAddress);
            mail.SetSubject("Ksiegarnia Longinus. Dodawanie produktu.");
            mail.SetBody("Prosze wypelnic zalaczony plik produktami z Pani/Pana dzialu. \n");
            mail.SetReplyTo(CSharpEmailLibrary.MailsCredentials.mainMailAddress);
            mail.AddAttachment(System.IO.Path.Combine(Utils.excelDirPath, Utils.excelFileName));
            mail.SetHeader(CSharpEmailLibrary.MailsCredentials.mainMailStatusHeaderKey, 
                MailStatus.ADDING_PRODUCTS_REQUEST.ToString());

            MailSender ms = new MailSender(CSharpEmailLibrary.MailsCredentials.mainMailAddress,
                CSharpEmailLibrary.MailsCredentials.mainMailPassword);
            ms.SendMail(mail);
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
