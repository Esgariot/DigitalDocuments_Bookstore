﻿using System;
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

            MailList.ItemsSource = emailWrapper.Emails;

            acceptButton.IsEnabled = false;
            rejectButton.IsEnabled = false;
        }

        private void rejectButton_Click(object sender, RoutedEventArgs e)
        {
            ReasonWindow rw = new ReasonWindow();
            rw.Show();
        }

        private void acceptButton_Click(object sender, RoutedEventArgs e)
        {
            prepareTemplateButton.IsEnabled = true;
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
                    break;
            }
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
    }
}
