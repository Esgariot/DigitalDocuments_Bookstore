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
        private MainWindow mW;

        public MessageBox(string message)
        {
            InitializeComponent();
            msgText.Text = string.Format(message);
        }

        public MessageBox(string message, MainWindow mW)
        {
            InitializeComponent();
            msgText.Text = string.Format(message);
            this.mW = mW;
        }

        private void MsgButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            return;
        }
    }
}
