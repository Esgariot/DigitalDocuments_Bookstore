using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

//using OpenPop.Pop3;
using OpenPop.Mime;

using System.IO;
using System.ComponentModel;
using Microsoft.Win32;

namespace GUI_Prototype
{
    public partial class MainWindow : Window
    {
        EmailWrapper emailWrapper = new EmailWrapper();
        List<Attachment> visibleAttachments = new List<Attachment>();
        public static bool isLogged = false;

        public MainWindow()
        {
            InitializeComponent();

            //demonstration purposes only
            emailWrapper.Emails.Add(new Email { Attachments = new List<Attachment>(), Date = "data", From = "klient", Subject = "fffff", Type = Email.TypeEnum.Offer, State = MailStatus.ADDING_PRODUCTS_REQUEST });
            emailWrapper.Emails.Add(new Email { Attachments = new List<Attachment>(), Date = "data", From = "klient", Subject = "fffff", Type = Email.TypeEnum.Offer, State = MailStatus.ADDING_PRODUCTS_RESPONSE });
            emailWrapper.Emails.Add(new Email { Attachments = new List<Attachment>(), Date = "data", From = "klient", Subject = "oferta klienta", Type = Email.TypeEnum.Offer, State = MailStatus.CLIENT_OFFER });
            emailWrapper.Emails.Add(new Email { Attachments = new List<Attachment>(), Date = "data", From = "klient", Subject = "zaakceptowana oferta", Type = Email.TypeEnum.Offer, State = MailStatus.CLIENT_OFFER_ACCEPTED });
            emailWrapper.Emails.Add(new Email { Attachments = new List<Attachment>(), Date = "data", From = "klient", Subject = "fffff", Type = Email.TypeEnum.Offer, State = MailStatus.CLIENT_OFFER_REJECTED });
            emailWrapper.Emails.Add(new Email { Attachments = new List<Attachment>(), Date = "data", From = "klient", Subject = "zaakceptowana lista produktów", Type = Email.TypeEnum.Offer, State = MailStatus.PRODUCTS_LIST_ACCEPTED });
            emailWrapper.Emails.Add(new Email { Attachments = new List<Attachment>(), Date = "data", From = "klient", Subject = "fffff", Type = Email.TypeEnum.Offer, State = MailStatus.RESPONSE_TO_ARCHIVE });
            emailWrapper.Emails.Add(new Email { Attachments = new List<Attachment>(), Date = "data", From = "klient", Subject = "połączona lista produktów", Type = Email.TypeEnum.Offer, State = MailStatus.PRODUCTS_LIST_MERGED });
            ////////////////////////////

            //MailList.ItemsSource = emailWrapper.Emails;
            MailList.ItemsSource = emailWrapper.EmailsWithStatus(MailStatus.CLIENT_OFFER);

            acceptButton.IsEnabled = false;
            rejectButton.IsEnabled = false;

            Utils.CreateDirectory(Utils.rootPath, Utils.usersDirName);
            Utils.CreateDirectory(Utils.rootPath, Utils.excelDirName);
        }

        private void rejectButton_Click(object sender, RoutedEventArgs e)
        {
            ReasonWindow rw = new ReasonWindow();
            rw.Show();
        }

        private void acceptButton_Click(object sender, RoutedEventArgs e)
        {
            prepareTemplateButton.IsEnabled = true;
            MailList.ItemsSource = emailWrapper.EmailsWithStatus(MailStatus.CLIENT_OFFER_ACCEPTED);
        }

        private void prepareTemplateButton_Click(object sender, RoutedEventArgs e)
        {
            //ExcelManager.openfile();
            confirmTemplateButton.IsEnabled = true;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "XML Files (*.xml)|*.xml";
            string xmlFile = "";
            if(openFileDialog.ShowDialog() == true)
            {
                xmlFile = openFileDialog.FileName;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV Files (*.csv)|*.csv";
            saveFileDialog.Title = "Save as...";
            string csvFile = "";
            if (saveFileDialog.ShowDialog() == true)
            {
                csvFile = saveFileDialog.FileName;
            }
            string args = $"{xmlFile} {csvFile}";
            PythonManager.Call(PythonManager.PYTHON , @"..\..\python_scripts\XmlToExcel.py", args);
        }

        private void confirmTemplateButton_Click(object sender, RoutedEventArgs e)
        {
            WorkersChooseWindow workersChooseWindow = new WorkersChooseWindow();
            workersChooseWindow.Show();
            //prepareProductsButton.IsEnabled = true;
        }

        private void prepareProductsButton_Click(object sender, RoutedEventArgs e)
        {
            //ExcelManager.openfile();
            confirmProductsButton.IsEnabled = true;
        }

        private void confirmProductsButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox confirmProductsMsgBox = new MessageBox("Czy na pewno chcesz potwierdzić produkty?", "MB_YESNO");
            confirmProductsMsgBox.Show();
            confirmProductsMsgBox.Closing += confirmProductsMsgBox_Closing;

            confirmTemplateButton.IsEnabled = true;

            // count departments, conditions for yes button, need add "if yes"
            if (Utils.loggedUser == "dokumenty.department.1@gmail.com")
                WorkersChooseWindow.counters.departmentsList.Remove("dokumenty.department.1@gmail.com");
            else if (Utils.loggedUser == "dokumenty.department.2@gmail.com")
                WorkersChooseWindow.counters.departmentsList.Remove("dokumenty.department.2@gmail.com");
            else if (Utils.loggedUser == "dokumenty.department.3@gmail.com")
                WorkersChooseWindow.counters.departmentsList.Remove("dokumenty.department.3@gmail.com");
            else if (Utils.loggedUser == "dokumenty.department.4@gmail.com")
                WorkersChooseWindow.counters.departmentsList.Remove("dokumenty.department.4@gmail.com");
            
            if (WorkersChooseWindow.counters.departmentsList.Count == 0)
                finishOrderButton.IsEnabled = true;

            // end count departments

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "CSV Files (*.csv)|*.csv";
            string csvFile = "";
            if (openFileDialog.ShowDialog() == true)
            {
                csvFile = openFileDialog.FileName;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "XML Files (*.xml)|*.xml";
            saveFileDialog.Title = "Save as...";
            string xmlFile = "";
            if (saveFileDialog.ShowDialog() == true)
            {
                xmlFile = saveFileDialog.FileName;
            }
            string args = $"{csvFile} {xmlFile}";
            PythonManager.Call(PythonManager.PYTHON, @"..\..\python_scripts\CsvToXml.py", args);

        }

        private void confirmProductsMsgBox_Closing(object sender, CancelEventArgs e)
        {
            if (Utils.MSGBOX_RESPONSE)
            {
                //approveOrderButton.IsEnabled = true;
            }
            else
            {

            }
        }

        private void approveOrderButton_Click(object sender, RoutedEventArgs e)
        {
            //ExcelManager.openfile();
            finishOrderButton.IsEnabled = true;
        }

        private void finishOrderButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox finishOrderMsgBox = new MessageBox("Czy na pewno chcesz zakończyć zamówienie?", "MB_YESNO");
            finishOrderMsgBox.Show();
            finishOrderMsgBox.Closing += finishOrderMsgBox_Closing;
        }

        private void finishOrderMsgBox_Closing(object sender, CancelEventArgs e)
        {
            if (Utils.MSGBOX_RESPONSE)
            {

            }
            else
            {
                
            }

            ArchiviseCurrentTransaction();
        }

        private void attachmentImage_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        #region LogIn - LogOut
        private void LogInButton_Click_1(object sender, RoutedEventArgs e)
        {
            LogInWindow logInWindow = new LogInWindow();
            logInWindow.Show();
            logInWindow.Closing += LogInWindow_Closing;
        }

        private void LogInWindow_Closing(object sender, CancelEventArgs e)
        {
            if (Utils.loggedUser != null && Utils.loggedRole != null)
            {
                isLogged = true;
                logInButton.IsEnabled = false;
                logInButton.Visibility = Visibility.Hidden;
                logOutButton.IsEnabled = true;
                logOutButton.Visibility = Visibility.Visible;
                userLabel.Content = Utils.loggedUser;

                PrepareRoles();
                PrepareGUI();
            }
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            Utils.loggedUser = null;
            Utils.loggedRole = null;
            isLogged = false;
            UnloadRoles();
            BlockGUI();
            userLabel.Content = "Gość";
            logOutButton.IsEnabled = false;
            logOutButton.Visibility = Visibility.Hidden;
            logInButton.IsEnabled = true;
            logInButton.Visibility = Visibility.Visible;
        }
        #endregion

        private void PrepareRoles()
        {
            switch (Utils.loggedRole)
            {
                case "GUEST":
                    break;
                case "MAIN_EMPLOYEE":
                    Utils.CAN_ACCEPT_ORDER = true;
                    Utils.CAN_REJECT_ORDER = true;
                    Utils.CAN_PREPARE_TEMPLATE = true;
                    Utils.CAN_CONFIRM_TEMPLATE = true;
                    Utils.CAN_CONFIRM_ORDER = true;
                    Utils.CAN_FINISH_ORDER = true;
                    break;
                case "DEPARTMENT_EMPLOYEE":
                    Utils.CAN_ADD_PRODUCTS = true;
                    Utils.CAN_CONFIRM_PRODUCTS = true;
                    break;
            }
        }

        private void PrepareGUI()
        {
            BlockGUI();

            switch (Utils.loggedRole)
            {
                case "GUEST":
                    break;
                case "MAIN_EMPLOYEE":
                    acceptButton.IsEnabled = true;
                    rejectButton.IsEnabled = true;
                    break;
                case "DEPARTMENT_EMPLOYEE":
                    acceptButton.IsEnabled = false;
                    rejectButton.IsEnabled = false;
                    prepareProductsButton.IsEnabled = true;
                    //confirmProductsButton.IsEnabled = true;
                    break;
            }
        }

        private void BlockGUI()
        {
            acceptButton.IsEnabled = false;
            rejectButton.IsEnabled = false;
            prepareTemplateButton.IsEnabled = false;
            confirmTemplateButton.IsEnabled = false;
            prepareProductsButton.IsEnabled = false;
            confirmProductsButton.IsEnabled = false;
            approveOrderButton.IsEnabled = false;
            finishOrderButton.IsEnabled = false;
        }

        private void UnloadRoles()
        {
            Utils.CAN_ACCEPT_ORDER = false;
            Utils.CAN_REJECT_ORDER = false;
            Utils.CAN_PREPARE_TEMPLATE = false;
            Utils.CAN_CONFIRM_TEMPLATE = false;
            Utils.CAN_CONFIRM_ORDER = false;
            Utils.CAN_FINISH_ORDER = false;
            Utils.CAN_ADD_PRODUCTS = false;
            Utils.CAN_CONFIRM_PRODUCTS = false;
        }

        private void MailList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MailList.SelectedIndex == -1)
                return;

            AttachmentsPanel.Children.Clear();
            Email selectedEmail = emailWrapper.Emails[MailList.SelectedIndex];
            int attachmentID = -1;
            foreach (Attachment attachment in selectedEmail.Attachments)
            {
                Image image = new Image();
                var converter = new ImageSourceConverter();
                image.Source = (ImageSource)converter.ConvertFromString("pack://application:,,,/Resources/Icons/pdf_icon.png");
                image.Width = 32;
                image.Height = 32;
                image.ToolTip = (++attachmentID).ToString();
                AttachmentsPanel.Children.Add(image);

                image.MouseDown += new MouseButtonEventHandler(Image_Click);
            }
        }

        void Image_Click(object sender, RoutedEventArgs e)
        {
            Email selectedEmail = emailWrapper.Emails[MailList.SelectedIndex];
            Image clickedImage = ((Image)e.OriginalSource);
            Attachment clickedAttachment = selectedEmail.Attachments[Int32.Parse(clickedImage.ToolTip.ToString())];
            string directoryPath = System.IO.Path.Combine(
                System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                + "\\temp\\"
                + (UInt32)selectedEmail.Date.GetHashCode());
            System.IO.Directory.CreateDirectory(directoryPath);
            string filePath = System.IO.Path.Combine(
                directoryPath
                + "\\"
                + clickedImage.ToolTip
                + "."
                + clickedAttachment.Extension);

            if (!File.Exists(filePath))
            {
                FileStream Stream = new FileStream(filePath, FileMode.Create);
                BinaryWriter BinaryStream = new BinaryWriter(Stream);
                BinaryStream.Write(clickedAttachment.Body);
                BinaryStream.Close();
            }
            System.Diagnostics.Process.Start(filePath);
        }

        private void ArchiviseCurrentTransaction()
        {
            //Nie pojęcia jak wskazać:
            //  *   plikow xml i xpdl do archiwizowania
            //  *   danych klienta
            //  *   numeru transakcji

            string pathToDatabaseFolder = @".\Archive\";
			if (System.IO.Directory.Exists(pathToDatabaseFolder))
				System.IO.Directory.CreateDirectory(pathToDatabaseFolder);
            string pathToFilesFolder = @"..\..\..\Tools\";

            FileStream xmlFile = File.Open(pathToFilesFolder + "PurchaseOrderTemplate.xml",
                System.IO.FileMode.Open);

            FileStream xpdlFile = File.Open(pathToFilesFolder + "XPDL.txt",
                System.IO.FileMode.Open);

            CustomerEntity customer = new CustomerEntity();
            customer.name = "";
            customer.mailAddress = "";

            string transactionId = "JPA8296_30742581";

            Archive archive = new Archive(pathToDatabaseFolder);
            archive.ArchiviseTransaction(transactionId, customer, xmlFile, xpdlFile);
        }
    }
}
