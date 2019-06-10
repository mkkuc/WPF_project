using DataTransferObjects.Models;
using Jira.Views.Common;
using Jira.Views.NotLogIn;
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
        public Account user { get; set; }
        public bool showAll { get; set; } = true;
        public bool showConfirmed { get; set; } = false;
        public bool showNotConfirmed { get; set; } = false;
        AccountRepository accountRepository = new AccountRepository();
        GroupRepository groupRepository = new GroupRepository();
        List<Account> usersList;
        List<Group> groupList;
        List<Account> usersInGroupList;
        List<Account> notAssignedList;
        List<Account> assignedList;

        public AdminPanel(Account account)
        {
            admin = account;
            InitializeComponent();
            EditProfile.DataContext = admin;
            AdminMenu.DataContext = admin;
            usersList = accountRepository.GetAll();
            Account yourAccount = accountRepository.Get(admin.AccountID);
            usersList.Remove(yourAccount);

            listOfUsers.ItemsSource = usersList;

            groupList = groupRepository.GetAll();
            if (groupList != null)
            {
                usersInGroupList = groupList[0].Accounts.ToList();
            }
            else
            {
                groupList = new List<Group>();
                usersInGroupList = new List<Account>();
            }
            listOfGroups.ItemsSource = groupList;
            listOfUsersInGroup.ItemsSource = usersInGroupList;

            notAssignedList = accountRepository.GetNotAssigned();
            assignedList = new List<Account>();
            listOfNotAssigned.ItemsSource = notAssignedList;
            listOfAssigned.ItemsSource = assignedList;

            RefreshAddGroup();

        }

        private ListCollectionView NotAssignedList
        {
            get { return (ListCollectionView)CollectionViewSource.GetDefaultView(notAssignedList); }
        }

        private ListCollectionView AssignedList
        {
            get { return (ListCollectionView)CollectionViewSource.GetDefaultView(assignedList); }
        }

        private ListCollectionView UsersList
        {
            get { return (ListCollectionView)CollectionViewSource.GetDefaultView(usersList); }
        }

        private ListCollectionView GroupList
        {
            get { return (ListCollectionView)CollectionViewSource.GetDefaultView(groupList); }
        }

        private ListCollectionView UsersInGroupList
        {
            get { return (ListCollectionView)CollectionViewSource.GetDefaultView(usersInGroupList); }
        }

        private void RefreshAddGroup()
        {
            AssignOwnerCombobox.ItemsSource = assignedList;
            AssignOwnerCombobox.DisplayMemberPath = "Surname";
            AssignOwnerCombobox.SelectedValuePath = ".";
            AssignOwnerCombobox.SelectedIndex = 0;
            AssignOwnerCombobox.Items.Refresh();
        }

        //Menu
        private void LogOut(object sender, EventArgs e)
        {
            LogIn window = new LogIn();
            Close();
            window.Show();
        }

        private void Refresh(object sender, EventArgs e)
        {
            accountRepository = new AccountRepository();
            groupRepository = new GroupRepository();

            usersList = accountRepository.GetAll();
            Account yourAccount = accountRepository.Get(admin.AccountID);
            usersList.Remove(yourAccount);

            listOfUsers.ItemsSource = usersList;
            listOfUsers.Items.Refresh();

            groupList = groupRepository.GetAll();
            if (groupList != null)
            {
                usersInGroupList = groupList[0].Accounts.ToList();
            }
            else
            {
                groupList = new List<Group>();
                usersInGroupList = new List<Account>();
            }
            listOfGroups.ItemsSource = groupList;
            listOfGroups.Items.Refresh();

            listOfUsersInGroup.ItemsSource = usersInGroupList;
            listOfUsersInGroup.Items.Refresh();
        }

        //Groups
        private void DoubleClickGroupItem(object sender, EventArgs e)
        {
            int i = 0;
            while (listOfGroups.SelectedIndex != i)
            {
                i++;
            }
            Group group = groupList[i];
            GroupDetails groupDetails = new GroupDetails(group);
            groupDetails.Show();
        }

        private void DoubleClickUserItem(object sender, EventArgs e)
        {
            int i = 0;
            while (listOfUsersInGroup.SelectedIndex != i)
            {
                i++;
            }
            Account account = usersInGroupList[i];
            UserDetails userDetails = new UserDetails(account);
            userDetails.Show();
        }

        private void SelectedGroup(object sender, MouseButtonEventArgs e)
        {
            int i = 0;
            while (listOfGroups.SelectedIndex != i)
            {
                i++;
            }
            usersInGroupList = groupList[i].Accounts.ToList();
            listOfUsersInGroup.ItemsSource = usersInGroupList;
            listOfUsersInGroup.Items.Refresh();
        }

        private void DoubleClickNotAssigned(object sender, EventArgs e)
        {
            int i = 0;
            while (listOfNotAssigned.SelectedIndex != i)
            {
                i++;
            }
            assignedList.Add(notAssignedList[i]);
            notAssignedList.Remove(notAssignedList[i]);
            listOfNotAssigned.Items.Refresh();
            listOfAssigned.Items.Refresh();
            RefreshAddGroup();
        }

        private void DoubleClickAssigned(object sender, EventArgs e)
        {
            int i = 0;
            while (listOfAssigned.SelectedIndex != i)
            {
                i++;
            }
            notAssignedList.Add(assignedList[i]);
            assignedList.Remove(assignedList[i]);
            listOfNotAssigned.Items.Refresh();
            listOfAssigned.Items.Refresh();
            RefreshAddGroup();
        }

        private void CreateGroupButton(object sender, EventArgs e)
        {
            var owner = (Account)AssignOwnerCombobox.SelectedValue;

            if (GroupName.Text == null || GroupName.Text.Equals("") || GroupName.Text.Length < 3)
            {
                MessageBox.Show("Nazwa grupy musi mieć przynajmniej 3 znaki.", "Błędna nazwa grupy", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (assignedList.Count == 0)
            {
                MessageBox.Show("Przypisz użytkowników do grupy.", "Brak użytkowników", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (owner == null || owner.Surname.Equals(""))
            {
                MessageBox.Show("Wybierz właściciela grupy.", "Brak właściciela grupy", MessageBoxButton.OK, MessageBoxImage.Error);
            }        
            else
            {
                


                var newOwner = accountRepository.Get(owner.AccountID);
                Role role = accountRepository.GetRole("GroupOwner");
                newOwner.RoleID = role.RoleID;
                newOwner.Role = role;
                accountRepository.Edit(newOwner);
                assignedList.Remove(assignedList.FirstOrDefault(a => a.AccountID == newOwner.AccountID));
                assignedList.Add(newOwner);

                //Group group = groupRepository.Create();
                groupRepository.Add(GroupName.Text, owner.AccountID, assignedList, null, null);
                MessageBox.Show("Grupa została założona!", "Tworzenie grupy", MessageBoxButton.OK);

                assignedList = new List<Account>();
                listOfAssigned.Items.Refresh();
            }
        }

        //Users
        private void ShowAll(object sender, EventArgs e)
        {
            usersList = accountRepository.GetAll();
            Account yourAccount = accountRepository.Get(admin.AccountID);
            usersList.Remove(yourAccount);
            showAll = true;
            showConfirmed = false;
            showNotConfirmed = false;
            listOfUsers.ItemsSource = usersList;
            listOfUsers.Items.Refresh();
        }

        private void ShowConfirmed(object sender, EventArgs e)
        {
            usersList = accountRepository.GetConfirmed();
            Account yourAccount = accountRepository.Get(admin.AccountID);
            usersList.Remove(yourAccount);
            showAll = false;
            showConfirmed = true;
            showNotConfirmed = false;
            listOfUsers.ItemsSource = usersList;
            listOfUsers.Items.Refresh();
        }

        private void ShowNotConfirmed(object sender, EventArgs e)
        {
            usersList = accountRepository.GetNotConfirmed();
            showAll = false;
            showConfirmed = false;
            showNotConfirmed = true;
            listOfUsers.ItemsSource = usersList;
            listOfUsers.Items.Refresh();
        }

        private void DeleteFromUserList(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz usunąć wybrane konto? Zmiany będą nieodwracalne.", "Usuwanie konta", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                int i = 0;

                while (listOfUsers.SelectedIndex != i)
                {
                    i++;
                }

                Account userToDelete = usersList[i];
                accountRepository.Delete(userToDelete.AccountID);

                usersList.RemoveAt(listOfUsers.SelectedIndex);
                listOfUsers.Items.Refresh();
                MessageBox.Show("Konto zostało usunięte.", "Usuwanie konta zakończone", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void AccessToDeleteUserFromList(object sender, CanExecuteRoutedEventArgs e)
        {
            if (listOfUsers.SelectedItem == null)
                e.CanExecute = false;
            else
                e.CanExecute = true;
        }

        private void UpdateUserOnList(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz zaktulizować wybrane konto? Zmiany będą nieodwracalne.", "Usuwanie konta", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                int i = 0;

                while (listOfUsers.SelectedIndex != i)
                {
                    i++;
                }

                Account userToUpdate = usersList[i];
                accountRepository.Edit(userToUpdate);
                if (showAll)
                {
                    usersList = accountRepository.GetAll();
                    Account yourAccount = accountRepository.Get(admin.AccountID);
                    usersList.Remove(yourAccount);
                }
                else if (showConfirmed)
                {
                    usersList = accountRepository.GetConfirmed();
                    Account yourAccount = accountRepository.Get(admin.AccountID);
                    usersList.Remove(yourAccount);
                }
                else if (showNotConfirmed)
                {
                    usersList = accountRepository.GetNotConfirmed();
                }
                else
                {
                    MessageBox.Show("Nieznany błąd. Skontaktuj się z administracją.", "Nieznany błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                listOfUsers.ItemsSource = usersList;
                listOfUsers.Items.Refresh();
                MessageBox.Show("Konto zostało zaktualizowane.", "Aktualizacja konta", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void AccessToUpdateUserOnList(object sender, CanExecuteRoutedEventArgs e)
        {
            if (listOfUsers.SelectedItem == null)
                e.CanExecute = false;
            else
                e.CanExecute = true;
        }

        //My profile
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

        //Main method and placeholders - Create Administrator
        private void CreateAdmin(object sender, EventArgs e)
        {
            if (!accountRepository.IsLoginCorrect(LoginTextBox.Text))
            {
                MessageBox.Show("Login jest zajęty.", "Błędny login", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (LoginTextBox.Text.Length < 3 || LoginTextBox.Text == null || LoginTextBox.Text.Equals("Login"))
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
                Account account = accountRepository.Create(LoginTextBox.Text, PasswordTextBoxP.Password, EmailTextBox.Text, NameTextBox.Text, SurnameTextBox.Text, accountRepository.GetRole("Admin").RoleID, accountRepository.GetRole("Admin"), true, null, null);
                accountRepository.Add(account);
                MessageBox.Show("Konto administratora założono poprawnie.", "Rejestracja zakończona", MessageBoxButton.OK);
                if (showAll)
                {
                    usersList = accountRepository.GetAll();
                    Account yourAccount = accountRepository.Get(admin.AccountID);
                    usersList.Remove(yourAccount);
                }
                else if (showConfirmed)
                {
                    usersList = accountRepository.GetConfirmed();
                    Account yourAccount = accountRepository.Get(admin.AccountID);
                    usersList.Remove(yourAccount);
                }
                else if (showNotConfirmed)
                {
                    usersList = accountRepository.GetNotConfirmed();
                }
                else
                {
                    MessageBox.Show("Nieznany błąd. Skontaktuj się z administracją.", "Nieznany błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                listOfUsers.ItemsSource = usersList;
                listOfUsers.Items.Refresh();
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
