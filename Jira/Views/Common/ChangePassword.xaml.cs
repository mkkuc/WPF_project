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

namespace Jira.Views.Common
{
    /// <summary>
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {
        public Account account { get; set; }

        AccountRepository accountRepository = new AccountRepository();

        public ChangePassword(Account user)
        {
            account = user;
            InitializeComponent();
        }

        private void SaveChanges(object sender, EventArgs e)
        {
            if (!OldPassBoxP.Password.Equals(account.Password))
            {
                MessageBox.Show("Aktualne hasło jest nieprawidłowe", "Błędne hasło", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (OldPassBoxP.Password.Equals(NewPassBoxP.Password))
            {
                MessageBox.Show("Aktualne hasło musi różnić się od nowego.", "Błędne hasło", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (!NewPassBoxP.Password.Equals(RepeatPassBoxP.Password))
            {
                MessageBox.Show("Hasła są różne.", "Błędne hasło", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if(NewPassBoxP.Password.Length < 3 || NewPassBoxP.Password == null)
            {
                MessageBox.Show("Hasło musi mieć przynajmniej 3 znaki.", "Błędne hasło", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Account edited = accountRepository.Get(account.AccountID);
                edited.Password = NewPassBoxP.Password;
                accountRepository.Edit(edited);
                MessageBox.Show("Hasło zostało zmienione.", "Zmiana hasła", MessageBoxButton.OK, MessageBoxImage.Information);
                if (edited.Role.Name.Equals("Admin"))
                {
                    AdminPanel window = new AdminPanel(edited);
                    Close();
                    window.Show();
                }
                else if (edited.Role.Name.Equals("GroupOwner"))
                {
                    GroupOwnerPanel window = new GroupOwnerPanel(edited);
                    Close();
                    window.Show();
                }
                else if (edited.Role.Name.Equals("User"))
                {
                    MainPanel window = new MainPanel(edited);
                    Close();
                    window.Show();
                }
                else
                {
                    MessageBox.Show("Nieznany błąd.", "Niezany błąd", MessageBoxButton.OK);
                }
            }
        }

        private void ReturnToMainMenu(object sender, EventArgs e)
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
                MessageBox.Show("Nieznany błąd.", "Niezany błąd", MessageBoxButton.OK);
            }
        }

        private void OldEnter(object sender, EventArgs e)
        {
            if (OldPassBox.Text == "Aktualne hasło")
            {
                OldPassBox.Text = "";
                OldPassBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
            }
        }

        private void OldLeave(object sender, EventArgs e)
        {
            if (OldPassBoxP.Password == "" || OldPassBoxP.Password == null)
            {
                OldPassBox.Text = "Aktualne hasło";
                OldPassBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Silver"));
            }
        }

        private void NewPasswordEnter(object sender, EventArgs e)
        {
            if (NewPassBox.Text == "Nowe hasło")
            {
                NewPassBox.Text = "";
                NewPassBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
            }
        }

        private void NewPasswordLeave(object sender, EventArgs e)
        {
            if (NewPassBoxP.Password == "" || NewPassBoxP.Password == null)
            {
                NewPassBox.Text = "Nowe hasło";
                NewPassBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Silver"));
            }
        }

        private void RepeatPasswordEnter(object sender, EventArgs e)
        {
            if (RepeatPassBox.Text == "Powtórz hasło")
            {
                RepeatPassBox.Text = "";
                RepeatPassBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
            }
        }

        private void RepeatPasswordLeave(object sender, EventArgs e)
        {
            if (RepeatPassBoxP.Password == "" || RepeatPassBoxP.Password == null)
            {
                RepeatPassBox.Text = "Powtórz hasło";
                RepeatPassBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Silver"));
            }
        }

    }
}
