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
    /// Interaction logic for WorkersChooseWindow.xaml
    /// </summary>
    public partial class WorkersChooseWindow : Window
    {
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

        }
    }
}
