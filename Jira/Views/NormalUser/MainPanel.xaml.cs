﻿using DataTransferObjects.Models;
using Jira.Views.Common;
using RepositoryLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Jira.Views.NormalUser
{
    /// <summary>
    /// Logika interakcji dla klasy MainPanel.xaml
    /// </summary>
    public partial class MainPanel : Window
    {
        Account user;
        AccountRepository accountRepository = new AccountRepository();
        GroupRepository groupRepository = new GroupRepository();
        public List<Issue> issuesList;
        public List<Group> groupsList { get; set; }
        public Group userGroup;
        public List<Account> usersInGroupList;
        Issue selectedIssue;
        public bool IsGroupContributor { get; set; }
        public bool IsNotGroupContributor { get => !IsGroupContributor; }
        public MainPanel(Account account)
        {
            user = account;
            InitializeComponent();
            MainPanelWindow.DataContext = this;
            EditProfile.DataContext = user;
            Account yourAccount = accountRepository.Get(user.AccountID);

            userGroup = groupRepository.GetByUser(yourAccount);
            if (userGroup != null)
            {
                IsGroupContributor = true;
            }
            else
            {
                groupsList = groupRepository.GetAll();
                AvailableGroupsListView.ItemsSource = groupsList;
            }

            if (userGroup != null)
            {
                issuesList = groupRepository.GetIssues(userGroup.GroupID).ToList();
            }
            IssuesListView.ItemsSource = issuesList;
            StatusComboBox.ItemsSource = issuesList;
            StatusComboBox.DisplayMemberPath = "Status.Name";
            StatusComboBox.SelectedValuePath = "Status.Name";
            StatusComboBox.SelectedValue = "Status.StatusID";
        }

        public MainPanel()
        {
            InitializeComponent();

        }

        //Edit Account-------------------------------------
        private void SaveChangesInProfile(object sender, EventArgs e)
        {
            if (user.Name.Length < 3 || user.Name == null)
            {
                MessageBox.Show("Imię musi mieć przynajmniej 3 znaki.", "Błędne imię", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (user.Surname.Length < 3 || user.Surname == null)
            {
                MessageBox.Show("Nazwisko musi mieć przynajmniej 3 znaki.", "Błędne nazwisko", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (user.Email.Equals("Email") || user.Email == null || user.Email.Equals(""))
            {
                MessageBox.Show("Podaj email", "Błędny email", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (!accountRepository.IsEmailCorrect(user.AccountID, user.Email))
            {
                MessageBox.Show("Email jest zajęty lub niepoprawny.", "Błędny email", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Account edited = accountRepository.Get(user.AccountID);
                edited.Name = user.Name;
                edited.Surname = user.Surname;
                edited.Email = user.Email;
                accountRepository.Edit(edited);
                MessageBox.Show("Zmiany zostały zapisane.", "Aktualizacja danych osobowych", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ChangePass(object sender, EventArgs e)
        {
            ChangePassword changePassword = new ChangePassword(user);
            Close();
            changePassword.Show();
        }


        //IssuesList---------------------------------------------
        private void SelectedIssue(object sender, MouseButtonEventArgs e)
        {
            selectedIssue = (Issue)IssuesListView.SelectedItem;
            SelectedItemView.DataContext = selectedIssue;
        }

        //Groups-------------------------------------------------

    }
}
