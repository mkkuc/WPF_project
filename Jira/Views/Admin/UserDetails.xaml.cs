using DataTransferObjects.Models;
using Jira.Models;
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
    /// Interaction logic for UserDetails.xaml
    /// </summary>
    public partial class UserDetails : Window
    {
        public UserDetailsVM userDetailsVM { get; set; } = new UserDetailsVM();
        public Account userFromGroup { get; set; }
        AccountRepository accountRepository = new AccountRepository();
        GroupRepository groupRepository = new GroupRepository();

        public UserDetails(Account account)
        {
            userFromGroup = account;
            InitializeComponent();

            userDetailsVM.Name = userFromGroup.Name;
            userDetailsVM.Surname = userFromGroup.Surname;
            userDetailsVM.Email = userFromGroup.Email;
            userDetailsVM.CurrentRoleName = userFromGroup.Role.Name;

            ViewUserDetails.DataContext = userDetailsVM;
        }

        private void SaveChangesInUser (object sender, EventArgs e)
        {
            if (YesRadioButton.IsChecked == true)
            {
                Group currentGroup = groupRepository.GetByUser(accountRepository.Get(userFromGroup.AccountID));
                Account oldOwner = accountRepository.Get(currentGroup.GroupOwnerID);
                Role role = accountRepository.GetRole("User");
                oldOwner.RoleID = role.RoleID;
                oldOwner.Role = role;
                accountRepository.Edit(oldOwner);
                currentGroup.GroupOwnerID = userFromGroup.AccountID;

                Account newOwner = accountRepository.Get(userFromGroup.AccountID);
                role = accountRepository.GetRole("GroupOwner");
                newOwner.RoleID = role.RoleID;
                newOwner.Role = role;
                accountRepository.Edit(newOwner);

                groupRepository.Edit(currentGroup);
                MessageBox.Show("Zapisano zmiany poprawnie.", "Zapis", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
                return;
            }

            else
            {
                MessageBox.Show("Zapisano zmiany poprawnie.", "Zapis", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
                return;
            }

        }

        private void DeleteUserFromGroup (object sender, EventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz usunąć członka grupy? Zmiany będą nieodwracalne.", "Usuwanie grupy", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                Account account = accountRepository.Get(userFromGroup.AccountID);
                Group currentGroup = groupRepository.GetByUser(account);
                ICollection<Account> list = currentGroup.Accounts;
                foreach (Account a in list)
                {
                    if (a.AccountID == account.AccountID)
                    {
                        list.Remove(a);
                        break;
                    }
                }
                currentGroup.Accounts = list;
                if (userFromGroup.Role.Name.Equals("GroupOwner"))
                {
                    Role role = accountRepository.GetRole("User");
                    account.RoleID = role.RoleID;
                    account.Role = role;
                    accountRepository.Edit(account);
                }
                groupRepository.Edit(currentGroup);
                MessageBox.Show("Usunięto użytkownika z grupy.", "Usuwanie", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
                return;
            }     
        }
    }
}
