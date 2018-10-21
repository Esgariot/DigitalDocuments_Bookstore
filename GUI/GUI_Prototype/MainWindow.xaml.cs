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
            
            MailList.ItemsSource = emailWrapper.Emails;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ReasonWindow rw = new ReasonWindow();
            rw.Show();
        }

        private void MailList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
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
