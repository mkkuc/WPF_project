using DataTransferObjects.Models;
using Jira.Models;
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
        List<Account> membersList;
        List<IssueDetailsVM> tasksList;
        List<Status> statuses;
        List<Priority> priorities;
        public GroupOwnerPanel(Account account)
        {
            groupOwner = account;
            InitializeComponent();
            group = groupRepository.GetByUser(groupOwner);
            statuses = issueRepository.GetAllStatuses();
            priorities = issueRepository.GetAllPriorities();
            membersList = groupRepository.GetUsersFromGroup(group.GroupID);
            OwnerMenu.DataContext = groupOwner;
            EditProfile.DataContext = groupOwner;
            tasksList = new List<IssueDetailsVM>();

            foreach (var task in issueRepository.GetGroupIssues(group.GroupID))
            {
                tasksList.Add(new IssueDetailsVM
                {
                    IssueID = task.IssueID,
                    Title = task.Title,
                    Description = task.Description,
                    Assignee = task.Assignee.Name + ' ' + task.Assignee.Surname,
                    PriorityName = task.Priority.Name,
                    StatusName = task.Status.Name
                });
            }
            listOfTasks.ItemsSource = tasksList;
            RefreshGroupOwner();

        }

        private void RefreshGroupOwner()
        {
            listOfTasks.ItemsSource = tasksList;

            CurrentStatus.ItemsSource = statuses;
            CurrentStatus.DisplayMemberPath = "Name";
            CurrentStatus.SelectedValuePath = ".";
            CurrentStatus.SelectedIndex = 0;

            CurrentPriority.ItemsSource = priorities;
            CurrentPriority.DisplayMemberPath = "Name";
            CurrentPriority.SelectedValuePath = ".";
            CurrentPriority.SelectedIndex = 0;

            CurrentAssignee.ItemsSource = membersList;
            CurrentAssignee.DisplayMemberPath = "Surname";
            CurrentAssignee.SelectedValuePath = ".";
            CurrentAssignee.SelectedIndex = 0;

            NewAssignee.ItemsSource = membersList;
            NewAssignee.DisplayMemberPath = "Surname";
            NewAssignee.SelectedValuePath = ".";
            NewAssignee.SelectedIndex = 0;

            NewPriority.ItemsSource = priorities;
            NewPriority.DisplayMemberPath = "Name";
            NewPriority.SelectedValuePath = ".";
            NewPriority.SelectedIndex = 0;

            group = groupRepository.GetByUser(groupOwner);
            statuses = issueRepository.GetAllStatuses();
            priorities = issueRepository.GetAllPriorities();
            membersList = groupRepository.GetUsersFromGroup(group.GroupID);
            OwnerMenu.DataContext = groupOwner;
            EditProfile.DataContext = groupOwner;
            tasksList = new List<IssueDetailsVM>();

            foreach (var task in issueRepository.GetGroupIssues(group.GroupID))
            {
                tasksList.Add(new IssueDetailsVM
                {
                    IssueID = task.IssueID,
                    Title = task.Title,
                    Description = task.Description,
                    Assignee = task.Assignee.Name + ' ' + task.Assignee.Surname,
                    PriorityName = task.Priority.Name,
                    StatusName = task.Status.Name
                });
            }
            listOfTasks.ItemsSource = tasksList;

        }

        private void AddNewIssue(object sender, EventArgs e)
        {

            var newPriority = (Priority)NewPriority.SelectedValue;
            var newAssignee = (Account)NewAssignee.SelectedValue;

            var newIssue = new Issue
            {
                GroupID = group.GroupID,
                AssigneeID = newAssignee.AccountID,
                Description = NewDescription.Text,
                Title = NewTitle.Text,
                PriorityID = newPriority.PriorityID,
                StatusID = 1,
            };

            issueRepository.Add(newIssue);
            RefreshGroupOwner();
            NewTitle.Text = "";
            NewDescription.Text = "";
            MessageBox.Show("Pomyślnie dodano zadanie", "Dodawanie zadania", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void DeleteFromIssueList(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz usunąć wybrane zadanie? Zmiany będą nieodwracalne.", "Usuwanie zadania", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                int i = 0;

                while (listOfTasks.SelectedIndex != i)
                {
                    i++;
                }

                Issue issueToDelete = issueRepository.Get(tasksList[i].IssueID);
                issueRepository.Delete(issueToDelete.IssueID);

                tasksList.RemoveAt(listOfTasks.SelectedIndex);
                listOfTasks.Items.Refresh();
                MessageBox.Show("Zadanie zostało usunięte.", "Usuwanie zadania zakończone", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void AccessToDeleteIssueFromList(object sender, CanExecuteRoutedEventArgs e)
        {
            if (listOfTasks.SelectedItem == null)
                e.CanExecute = false;
            else
                e.CanExecute = true;
        }

        private void UpdateIssueOnList(object sender, ExecutedRoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz zaktulizować wybrane zadanie?", "Aktualizacja zadania", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                int i = 0;

                while (listOfTasks.SelectedIndex != i)
                {
                    i++;
                }

                Issue taskToUpdate = issueRepository.Get(tasksList[i].IssueID);
                var status = (Status)CurrentStatus.SelectedValue;
                var priority = (Priority)CurrentPriority.SelectedValue;
                var assignee = (Account)CurrentAssignee.SelectedValue;

                //taskToUpdate.Status = issueRepository.GetStatus(status.StatusID);
                taskToUpdate.StatusID = status.StatusID;

                //taskToUpdate.Priority = issueRepository.GetPriority(priority.PriorityID);
                taskToUpdate.PriorityID = priority.PriorityID;

                //taskToUpdate.Assignee = accountRepository.Get(assignee.AccountID);
                taskToUpdate.AssigneeID = assignee.AccountID;

                issueRepository.Edit(taskToUpdate);

                tasksList.Clear();
                foreach (var task in issueRepository.GetGroupIssues(group.GroupID))
                {
                    tasksList.Add(new IssueDetailsVM
                    {
                        IssueID = task.IssueID,
                        Title = task.Title,
                        Description = task.Description,
                        Assignee = task.Assignee.Name + ' ' + task.Assignee.Surname,
                        PriorityName = task.Priority.Name,
                        StatusName = task.Status.Name
                    });
                }

                //Issue currentIssue = issueRepository.Get(taskToUpdate.IssueID);
                //tasksList.Remove(currentIssue);
                
                
               
                listOfTasks.ItemsSource = tasksList;
                listOfTasks.Items.Refresh();
                MessageBox.Show("Zadanie zostało zaktualizowane.", "Aktualizacja zadania", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void AccessToUpdateIssueOnList(object sender, CanExecuteRoutedEventArgs e)
        {
            if (listOfTasks.SelectedItem == null)
                e.CanExecute = false;
            else
                e.CanExecute = true;
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
