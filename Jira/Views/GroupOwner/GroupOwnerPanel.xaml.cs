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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Jira.Views.GroupOwner
{
    /// <summary>
    /// Interaction logic for GroupOwnerPanel.xaml
    /// </summary>
    public partial class GroupOwnerPanel : Window
    {
        Account groupOwner;
        Group group;
        AccountRepository accountRepository = new AccountRepository();
        GroupRepository groupRepository = new GroupRepository();
        IssueRepository issueRepository = new IssueRepository();
        List<Account> usersList;
        List<Account> membersList;
        List<Issue> tasksList;
        public GroupOwnerPanel(Account account)
        {
            groupOwner = account;
            InitializeComponent();
            group = groupRepository.GetByUser(groupOwner);
            OwnerMenu.DataContext = groupOwner;
            EditProfile.DataContext = groupOwner;

            tasksList = issueRepository.GetGroupIssues(group.GroupID);
            listOfTasks.ItemsSource = tasksList;

        }
        private void SelectedTask(object sender, MouseButtonEventArgs e)
        {
            int i = 0;
            while (listOfTasks.SelectedIndex != i)
            {
                i++;
            }
            //usersInGroupList = groupList[i].Accounts.ToList();
            //listOfUsersInGroup.ItemsSource = usersInGroupList;
            //listOfUsersInGroup.Items.Refresh();
        }

        public GroupOwnerPanel()
        {
            InitializeComponent();
        }

        private void DeleteTask(object sender, ExecutedRoutedEventArgs e)
        {
            //list.RemoveAt(listOfItems.SelectedIndex);
            //listOfItems.Items.Refresh();
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
            //listOfItems.Items.Refresh();
        }

        private void AccessToUpdate(object sender, CanExecuteRoutedEventArgs e)
        {
            //if (listOfItems.SelectedItem == null)
            //    e.CanExecute = false;
            //else
            //    e.CanExecute = true;
        }

        private void LogOut(object sender, EventArgs e)
        {
            LogIn window = new LogIn();
            Close();
            window.Show();
        }

        private void ChangePass(object sender, EventArgs e)
        {
            ChangePassword changePassword = new ChangePassword(groupOwner);
            Close();
            changePassword.Show();
        }

        private void SaveChangesInProfile(object sender, EventArgs e)
        {
            if (groupOwner.Name.Length < 3 || groupOwner.Name == null)
            {
                MessageBox.Show("Imię musi mieć przynajmniej 3 znaki.", "Błędne imię", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (groupOwner.Surname.Length < 3 || groupOwner.Surname == null)
            {
                MessageBox.Show("Nazwisko musi mieć przynajmniej 3 znaki.", "Błędne nazwisko", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (groupOwner.Email.Equals("Email") || groupOwner.Email == null || groupOwner.Email.Equals(""))
            {
                MessageBox.Show("Podaj email", "Błędny email", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (!accountRepository.IsEmailCorrect(groupOwner.AccountID, groupOwner.Email))
            {
                MessageBox.Show("Email jest zajęty lub niepoprawny.", "Błędny email", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Account edited = accountRepository.Get(groupOwner.AccountID);
                edited.Name = groupOwner.Name;
                edited.Surname = groupOwner.Surname;
                edited.Email = groupOwner.Email;
                accountRepository.Edit(edited);
                MessageBox.Show("Zmiany zostały zapisane.", "Aktualizacja danych osobowych", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void DeleteAccountInProfile(object sender, EventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz usunąć swoje konto? Zmiany będą nieodwracalne.", "Usuwanie konta", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                accountRepository.Delete(groupOwner.AccountID);
                MessageBox.Show("Konto zostało usunięte.", "Usuwanie konta zakończone", MessageBoxButton.OK, MessageBoxImage.Information);
                MainWindow window = new MainWindow();
                Close();
                window.Show();
            }
        }
    }
}
