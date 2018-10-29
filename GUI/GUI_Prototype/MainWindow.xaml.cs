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

namespace GUI_Prototype
{
    public partial class MainWindow : Window
    {
        EmailWrapper emailWrapper = new EmailWrapper();
        List<Attachment> visibleAttachments = new List<Attachment>();
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
            WorkersChooseWindow workersChooseWindow = new WorkersChooseWindow();
            workersChooseWindow.Show();

            confirmTemplateButton.IsEnabled = true;
        }

        private void confirmTemplateButton_Click(object sender, RoutedEventArgs e)
        {
            prepareProductsButton.IsEnabled = true;
        }

        private void prepareProductsButton_Click(object sender, RoutedEventArgs e)
        {
            confirmProductsButton.IsEnabled = true;
        }

        private void confirmProductsButton_Click(object sender, RoutedEventArgs e)
        {
            approveOrderButton.IsEnabled = true;
        }

        private void approveOrderButton_Click(object sender, RoutedEventArgs e)
        {
            finishOrderButton.IsEnabled = true;
        }

        private void finishOrderButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void attachmentImage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void logInButton_Click_1(object sender, RoutedEventArgs e)
        {
            LogInWindow logInWindow = new LogInWindow();
            logInWindow.Show();
        }

        private void logOutButton_Click(object sender, RoutedEventArgs e)
        {
            logOutButton.IsEnabled = false;
            logOutButton.Visibility = Visibility.Hidden;
            logInButton.IsEnabled = true;
            logInButton.Visibility = Visibility.Visible;
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
            
            if(!File.Exists(filePath))
            {
                FileStream Stream = new FileStream(filePath, FileMode.Create);
                BinaryWriter BinaryStream = new BinaryWriter(Stream);
                BinaryStream.Write(clickedAttachment.Body);
                BinaryStream.Close();
            }
            System.Diagnostics.Process.Start(filePath);
        }
    }
}
