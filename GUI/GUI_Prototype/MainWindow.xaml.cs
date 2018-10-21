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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI_Prototype
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
    }
}
