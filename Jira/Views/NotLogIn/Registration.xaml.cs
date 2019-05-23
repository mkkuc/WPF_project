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
using DataTransferObjects.Models;
using RepositoryLayer.Interfaces;
using RepositoryLayer.Repositories;

namespace Jira.Views.NotLogIn
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        AccountRepository accountRepository = new AccountRepository();

        public Registration()
        {
            InitializeComponent();
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            LogIn logIn = new LogIn();
            Close();
            logIn.Show();
        }

        private void MouseRejestration(object sender, RoutedEventArgs e)
        {
            if (!accountRepository.IsLoginCorrect(LoginTextBox.Text))
            {
                MessageBox.Show("Login jest zajęty.", "Błędny login", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (LoginTextBox.Text.Length < 3 || LoginTextBox.Text == null)
            {
                MessageBox.Show("Login musi mieć przynajmniej 3 znaki.", "Błędny login", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (PasswordTextBoxP.Password.Length < 3 || PasswordTextBoxP.Password == null)
            {
                MessageBox.Show("Hasło musi mieć przynajmniej 3 znaki.", "Błędne hasło", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (!RepeatPasswordTextBoxP.Password.Equals(PasswordTextBoxP.Password))
            {
                MessageBox.Show("Hasła są różne.", "Błędne hasło", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (NameTextBox.Text.Length < 3 || NameTextBox.Text == null || NameTextBox.Text.Equals("Imię"))
            {
                MessageBox.Show("Imię musi mieć przynajmniej 3 znaki.", "Błędne imię", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (SurnameTextBox.Text.Length < 3 || SurnameTextBox.Text == null || SurnameTextBox.Text.Equals("Nazwisko"))
            {
                MessageBox.Show("Nazwisko musi mieć przynajmniej 3 znaki.", "Błędne nazwisko", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (EmailTextBox.Text.Equals("Email"))
            {
                MessageBox.Show("Podaj email", "Błędny email", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (!accountRepository.IsEmailCorrect(EmailTextBox.Text))
            {
                MessageBox.Show("Email jest zajęty lub niepoprawny.", "Błędny email", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Konto założono poprawnie.", "Rejestracja zakończona", MessageBoxButton.OK);
                Account account = accountRepository.Create(LoginTextBox.Text, PasswordTextBoxP.Password, EmailTextBox.Text, NameTextBox.Text, SurnameTextBox.Text, accountRepository.GetRole("User").RoleID, accountRepository.GetRole("User"), false, null, null);
                accountRepository.Add(account);
                LogIn logIn = new LogIn();
                Close();
                logIn.Show();
            }
        }

        private void FinishRejestration(object sender, RoutedEventArgs e)
        {
            if (!accountRepository.IsLoginCorrect(LoginTextBox.Text))
            {
                MessageBox.Show("Login jest zajęty.", "Błędny login", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (LoginTextBox.Text.Length < 3 || LoginTextBox.Text == null)
            {
                MessageBox.Show("Login musi mieć przynajmniej 3 znaki.", "Błędny login", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (PasswordTextBoxP.Password.Length < 3 || PasswordTextBoxP.Password == null)
            {
                MessageBox.Show("Hasło musi mieć przynajmniej 3 znaki.", "Błędne hasło", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (!RepeatPasswordTextBoxP.Password.Equals(PasswordTextBoxP.Password))
            {
                MessageBox.Show("Hasła są różne.", "Błędne hasło", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (NameTextBox.Text.Length < 3 || NameTextBox.Text == null || NameTextBox.Text.Equals("Imię"))
            {
                MessageBox.Show("Imię musi mieć przynajmniej 3 znaki.", "Błędne imię", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (SurnameTextBox.Text.Length < 3 || SurnameTextBox.Text == null || SurnameTextBox.Text.Equals("Nazwisko"))
            {
                MessageBox.Show("Nazwisko musi mieć przynajmniej 3 znaki.", "Błędne nazwisko", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (EmailTextBox.Text.Equals("Email"))
            {
                MessageBox.Show("Podaj email", "Błędny email", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (!accountRepository.IsEmailCorrect(EmailTextBox.Text))
            {
                MessageBox.Show("Email jest zajęty lub niepoprawny.", "Błędny email", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                MessageBox.Show("Konto założono poprawnie.", "Rejestracja zakończona", MessageBoxButton.OK);
                Account account = accountRepository.Create(LoginTextBox.Text, PasswordTextBoxP.Password, EmailTextBox.Text, NameTextBox.Text, SurnameTextBox.Text, accountRepository.GetRole("User").RoleID, accountRepository.GetRole("User"), false, null, null);
                accountRepository.Add(account);
                LogIn logIn = new LogIn();
                Close();
                logIn.Show();
            }
        }

        private void LoginEnter(object sender, EventArgs e)
        {
            if (LoginTextBox.Text == "Login")
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

        private void RepeatPasswordEnter(object sender, EventArgs e)
        {
            if (RepeatPasswordTextBox.Text == "Powtórz hasło")
            {
                RepeatPasswordTextBox.Text = "";
                RepeatPasswordTextBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
            }
        }

        private void RepeatPasswordLeave(object sender, EventArgs e)
        {
            if (RepeatPasswordTextBoxP.Password == "" || RepeatPasswordTextBoxP.Password == null)
            {
                RepeatPasswordTextBox.Text = "Powtórz hasło";
                RepeatPasswordTextBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Silver"));
            }
        }

        private void NameEnter(object sender, EventArgs e)
        {
            if (NameTextBox.Text == "Imię")
            {
                NameTextBox.Text = "";
                NameTextBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
            }
        }

        private void NameLeave(object sender, EventArgs e)
        {
            if (NameTextBox.Text == "" || NameTextBox.Text == null)
            {
                NameTextBox.Text = "Imię";
                NameTextBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Silver"));
            }
        }

        private void SurnameEnter(object sender, EventArgs e)
        {
            if (SurnameTextBox.Text == "Nazwisko")
            {
                SurnameTextBox.Text = "";
                SurnameTextBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
            }
        }

        private void SurnameLeave(object sender, EventArgs e)
        {
            if (SurnameTextBox.Text == "" || SurnameTextBox.Text == null)
            {
                SurnameTextBox.Text = "Nazwisko";
                SurnameTextBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Silver"));
            }
        }

        private void EmailEnter(object sender, EventArgs e)
        {
            if (EmailTextBox.Text == "Email")
            {
                EmailTextBox.Text = "";
                EmailTextBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
            }
        }

        private void EmailLeave(object sender, EventArgs e)
        {
            if (EmailTextBox.Text == "" || SurnameTextBox.Text == null)
            {
                EmailTextBox.Text = "Email";
                EmailTextBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Silver"));
            }
        }
    }
}
