using DataTransferObjects.Models;
using Jira.Views.Admin;
using Jira.Views.GroupOwner;
using Jira.Views.NormalUser;
using RepositoryLayer.Repositories;
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

namespace Jira.Views.NotLogIn
{
    /// <summary>
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : Window
    {

        AccountRepository accountRepository = new AccountRepository();

        public LogIn()
        {
            InitializeComponent();
        }

        private void Login(object sender, EventArgs e)
        {
            Account account = accountRepository.GetAccount(LoginTextBox.Text, PasswordTextBoxP.Password);
            if (account != null)
            {
                if (account.IsConfirmed)
                {
                    if (account.Role.Name.Equals("Admin"))
                    {
                        AdminPanel window = new AdminPanel(account);
                        Close();
                        window.Show();
                    }
                    else if (account.Role.Name.Equals("GroupOwner"))
                    {
                        GroupOwnerPanel window = new GroupOwnerPanel(account);
                        Close();
                        window.Show();
                    }
                    else if (account.Role.Name.Equals("User"))
                    {
                        MainPanel window = new MainPanel(account);
                        Close();
                        window.Show();
                    }
                    else
                    {
                        MessageBox.Show("Nieznany błąd. Skontaktuj się z administratorem.", "Niezany błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Twoje konto jeszcze nie jest zatwierdzone. Spróbuj ponownie później.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Błędny login lub hasło.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SignUp (object sender, EventArgs e)
        {
            Registration window = new Registration();
            Close();
            window.Show();
        }

        private void LoginEnter(object sender, EventArgs e)
        {
            if(LoginTextBox.Text == "Login")
            {
                LoginTextBox.Text = "";
                LoginTextBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
            }
        }

        private void LoginLeave(object sender, EventArgs e)
        {
            if (LoginTextBox.Text == "" || LoginTextBox.Text == null)
            {
                LoginTextBox.Text = "Login";
                LoginTextBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Silver"));
            }
        }

        private void PasswordEnter(object sender, EventArgs e)
        {
            if (PasswordTextBox.Text == "Hasło")
            {
                PasswordTextBox.Text = "";
                PasswordTextBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
            }
        }

        private void PasswordLeave(object sender, EventArgs e)
        {
            if (PasswordTextBoxP.Password == "" || PasswordTextBoxP.Password == null)
            {
                PasswordTextBox.Text = "Hasło";
                PasswordTextBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Silver"));
            }
        }

    }
}
