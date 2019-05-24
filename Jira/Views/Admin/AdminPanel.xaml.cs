using DataTransferObjects.Models;
using Jira.Views.Common;
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

namespace Jira.Views.Admin
{
    /// <summary>
    /// Interaction logic for AdminPanel.xaml
    /// </summary>
    public partial class AdminPanel : Window
    {
        public Account admin { get; set; }
        AccountRepository accountRepository = new AccountRepository();

        public AdminPanel(Account account)
        {
            admin = account;
            InitializeComponent();
            EditProfile.DataContext = admin;
            var person = new Account
            {
                Name = "Jan Dodany"
            };
            list.Add(person);

            listOfItems.ItemsSource = list;

            var group = new Group
            {
                Name = "Grupa1"
            };
            listGroup.Add(group);
            listOfGroups.ItemsSource = listGroup;

            listOfUsers.ItemsSource = list;
        }

        List<Account> list = new List<Account>();
        private ListCollectionView View
        {
            get { return (ListCollectionView)CollectionViewSource.GetDefaultView(list); }
        }
        List<Group> listGroup = new List<Group>();
        private ListCollectionView View2
        {
            get { return (ListCollectionView)CollectionViewSource.GetDefaultView(listGroup); }
        }

        private void NewPerson(object sender, SelectionChangedEventArgs e)
        {
            int quantity = list.Count - 1;
            if (listOfItems.SelectedIndex == quantity)
            {
                var account = new Account();
                list.Insert(quantity, account);
                listOfItems.UnselectAll();
            }
            listOfItems.Items.Refresh();
        }

        private void NewUser(object sender, SelectionChangedEventArgs e)
        {
            int quantity = list.Count - 1;
            if (listOfUsers.SelectedIndex == quantity)
            {
                var account = new Account();
                list.Insert(quantity, account);
                listOfUsers.UnselectAll();
            }
            listOfUsers.Items.Refresh();
        }


        private void NewGroup(object sender, SelectionChangedEventArgs e)
        {
            int quantity = list.Count - 1;
            if (listOfGroups.SelectedIndex == quantity)
            {
                var account = new Account();
                list.Insert(quantity, account);
                listOfGroups.UnselectAll();
            }
            listOfGroups.Items.Refresh();
        }

        private void DoubleClickGroupItem(object sender, EventArgs e)
        {
            var groupDetails = new GroupDetails();
            groupDetails.Show();
        }

        private void DoubleClickUserItem(object sender, EventArgs e)
        {
            var userDetails = new UserDetails();
            userDetails.Show();
        }

        private void Delete(object sender, ExecutedRoutedEventArgs e)
        {
            list.RemoveAt(listOfItems.SelectedIndex);
            listOfItems.Items.Refresh();
        }

        private void AccessToDelete(object sender, CanExecuteRoutedEventArgs e)
        {
            //if (listOfItems.SelectedItem == null)
            //    e.CanExecute = false;
            //else
            //    e.CanExecute = true;
        }

        private void Update(object sender, ExecutedRoutedEventArgs e)
        {
            //list.RemoveAt(listOfItems.SelectedIndex);
            listOfItems.Items.Refresh();
        }

        private void AccessToUpdate(object sender, CanExecuteRoutedEventArgs e)
        {
            //if (listOfItems.SelectedItem == null)
            //    e.CanExecute = false;
            //else
            //    e.CanExecute = true;
        }

        private void ChangePass(object sender, EventArgs e)
        {
            ChangePassword changePassword = new ChangePassword(admin);
            Close();
            changePassword.Show();
        }

        private void SaveChangesInProfile(object sender, EventArgs e)
        {
            if (admin.Name.Length < 3 || admin.Name == null)
            {
                MessageBox.Show("Imię musi mieć przynajmniej 3 znaki.", "Błędne imię", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (admin.Surname.Length < 3 || admin.Surname == null)
            {
                MessageBox.Show("Nazwisko musi mieć przynajmniej 3 znaki.", "Błędne nazwisko", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (admin.Email.Equals("Email") || admin.Email == null || admin.Email.Equals(""))
            {
                MessageBox.Show("Podaj email", "Błędny email", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (!accountRepository.IsEmailCorrect(admin.AccountID, admin.Email))
            {
                MessageBox.Show("Email jest zajęty lub niepoprawny.", "Błędny email", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Account edited = accountRepository.Get(admin.AccountID);
                edited.Name = admin.Name;
                edited.Surname = admin.Surname;
                edited.Email = admin.Email;
                accountRepository.Edit(edited);
                MessageBox.Show("Zmiany zostały zapisane.", "Aktualizacja danych osobowych", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void DeleteAccountInProfile(object sender, EventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz usunąć swoje konto? Zmiany będą nieodwracalne.", "Usuwanie konta", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                accountRepository.Delete(admin.AccountID);
                MessageBox.Show("Konto zostało usunięte.", "Usuwanie konta zakończone", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow window = new MainWindow();
                Close();
                window.Show();
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
            if (PasswordTextBox.Text == "" || PasswordTextBox.Text == null)
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
            if (RepeatPasswordTextBox.Text == "" || RepeatPasswordTextBox.Text == null)
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

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
