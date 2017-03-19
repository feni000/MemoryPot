using MemoryPot.DAL;
using System;
using System.Collections.Generic;
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

namespace MemoryPot.View
{
    /// <summary>
    /// Interaction logic for LoginWindodw.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        public string HashPassword(string password)
        {
            byte[] salt;
            new RNGCryptoServiceProvider().GetBytes(salt = new byte[16]);
            var pbkdf2 = new Rfc2898DeriveBytes(PasswordTextbox.Password, salt, 10000);
            byte[] hash = pbkdf2.GetBytes(20);
            byte[] hashBytes = new byte[36];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);
            string savedPasswordHash = Convert.ToBase64String(hashBytes);
            return savedPasswordHash;
        }

        private void LoginDB_Click(object sender, RoutedEventArgs e)
        {
            string hashedPassword = HashPassword(PasswordTextbox.Password);
            if (DatabaseFunctions.LoginDB("DBMemoryPot.db", UserNameTextBox.Text, hashedPassword) == true)
            {
                //UserWindow userActiveWindow = new UserWindow();
                //User loggedUser = new User(1, UserNameTextBox.Text, PasswordTextbox.Password, 1);
                //userActiveWindow.Show();
                //this.Close();
            }
        }
    }
}
