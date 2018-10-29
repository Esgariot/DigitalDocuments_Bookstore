using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
    /// Interaction logic for LogInWindow.xaml
    /// </summary>
    public partial class LogInWindow : Window
    {
        public LogInWindow()
        {
            InitializeComponent();
        }

        private void logInButton_Click(object sender, RoutedEventArgs e)
        {
            SearchForUser(userNameTextBox.Text, userPassword.Password);
            this.Close();
        }

        private void SearchForUser(string userEmail, string userPassword)
        {
            if (!Utils.DirectoryExists(Utils.usersDirPath, userEmail))
            {
                Utils.loggedUser = null;
                Utils.loggedRole = null;
                new MessageBox("Użytkownik o podanym loginie nie istnieje. \nSkontaktuj się z administratorem.").Show();
                return;
            }

            string userEmailText = String.Empty;
            string userPasswordText = String.Empty;
            string userRoleText = String.Empty;

            using (StreamReader reader = new StreamReader(System.IO.Path.Combine(Utils.usersDirPath, userEmail, userEmail) + ".txt"))
            {
                userEmailText = reader.ReadLine();
                userPasswordText = reader.ReadLine();
                userRoleText = reader.ReadLine();

                SHA256 sHA256 = SHA256Managed.Create();
                var hashValue = sHA256.ComputeHash(Encoding.ASCII.GetBytes(userPassword));
                var stringValue = Convert.ToBase64String(hashValue);

                bool wrongEmail = !userEmailText.Equals(userEmail);
                bool wrongPassword = !userPasswordText.Equals(stringValue);

                if (wrongEmail || wrongPassword)
                {
                    Utils.loggedUser = null;
                    Utils.loggedRole = null;
                    new MessageBox("Błędne hasło lub login. Spróbuj jeszcze raz.").Show();
                    this.Close();
                }

                Utils.loggedUser = userEmail;
                Utils.loggedRole = userRoleText;
                return;
            }
        }
    }
}
