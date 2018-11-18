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

namespace GUI_Prototype
{
    /// <summary>
    /// Interaction logic for MessageBox.xaml
    /// </summary>
    public partial class MessageBox : Window
    {
        private const string MB_OK = "MB_OK";
        private const string MB_YESNO = "MB_YESNO";

        public MessageBox(string message)
        {
            InitializeComponent();

            yesButton.Visibility = Visibility.Hidden;
            noButton.Visibility = Visibility.Hidden;
            okButton.Visibility = Visibility.Visible;

            msgText.Text = string.Format(message);
        }

        public MessageBox(string message, string type)
        {
            InitializeComponent();
            
            PrepareButtons(type);

            msgText.Text = string.Format(message);
        }

        private void PrepareButtons(string type)
        {
            switch (type)
            {
                case MB_OK:
                    yesButton.Visibility = Visibility.Hidden;
                    noButton.Visibility = Visibility.Hidden;
                    okButton.Visibility = Visibility.Visible;
                    break;

                case MB_YESNO:
                    yesButton.Visibility = Visibility.Visible;
                    noButton.Visibility = Visibility.Visible;
                    okButton.Visibility = Visibility.Hidden;
                    break;
            }
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            return;
        }

        private void noButton_Click(object sender, RoutedEventArgs e)
        {
            Utils.MSGBOX_RESPONSE = false;
            this.Close();
            return;
        }

        private void yesButton_Click(object sender, RoutedEventArgs e)
        {
            Utils.MSGBOX_RESPONSE = true;
            this.Close();
            return;
        }
    }
}
