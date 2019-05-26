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
    /// Interaction logic for GroupDetails.xaml
    /// </summary>
    public partial class GroupDetails : Window
    {
        public Group actualGroup { get; set; }
        public GroupDetailsVM groupDetailsVM { get; set; }
        AccountRepository accountRepository = new AccountRepository();
        GroupRepository groupRepository = new GroupRepository();
        public GroupDetails(Group group)
        {
            actualGroup = group;
            InitializeComponent();
            groupDetailsVM = new GroupDetailsVM();
            groupDetailsVM.Name = group.Name;
            Account owner = accountRepository.Get(group.GroupOwnerID);
            if(owner != null)
            {
                groupDetailsVM.NameAndSurnameOwner = owner.Name + " " + owner.Surname;
            }
            else
            {
                groupDetailsVM.NameAndSurnameOwner = "Brak właściciela grupy";
            }
            List<Account> accountList = groupRepository.GetUsersFromGroup(group.GroupID);
            foreach (Account account in accountList)
            {
                groupDetailsVM.AccountList.Add(account);
                groupDetailsVM.NameAndSurnameList.Add(account.Name + " " + account.Surname + "#" + account.AccountID);
            }
            GroupDetailsWindow.DataContext = groupDetailsVM;
        }

        private void SaveChangesInGroup(object sender, EventArgs e)
        {
            Group updatedGroup = groupRepository.Get(actualGroup.GroupID);
            if(groupDetailsVM.Name.Equals("") || groupDetailsVM.Name == null)
            {
                MessageBox.Show("Podaj nazwę grupy.", "Nazwa grupy", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else if (groupDetailsVM.Name.Length < 3)
            {
                MessageBox.Show("Nazwa grupy nie może być krótsza niż 3 znaki.", "Nazwa grupy", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                updatedGroup.Name = groupDetailsVM.Name;
                if (PickOwner.Text.Equals("") || PickOwner.Text == null)
                {
                    groupRepository.Edit(updatedGroup);
                    MessageBox.Show("Zapisano zmiany poprawnie.", "Zapis", MessageBoxButton.OK, MessageBoxImage.Information);
                    Close();
                    return;
                }
                else
                {
                    foreach(string nameAndSurname in groupDetailsVM.NameAndSurnameList)
                    {
                        if (PickOwner.Text.Equals(nameAndSurname))
                        {
                            string[] splited = nameAndSurname.Split('#');
                            int id = Int32.Parse(splited[1]);
                            Account account = groupDetailsVM.AccountList.Find(g => g.AccountID == id);
                            int oldOwnerID = updatedGroup.GroupOwnerID;          
                            updatedGroup.GroupOwnerID = account.AccountID;

                            account = accountRepository.Get(oldOwnerID);
                            Role role = accountRepository.GetRole("User");
                            account.RoleID = role.RoleID;
                            account.Role = role;
                            accountRepository.Edit(account);

                            int newOnwerID = updatedGroup.GroupOwnerID;
                            account = accountRepository.Get(newOnwerID);
                            role = accountRepository.GetRole("GroupOwner");
                            account.RoleID = role.RoleID;
                            account.Role = role;
                            accountRepository.Edit(account);

                            groupRepository.Edit(updatedGroup);
                            MessageBox.Show("Zapisano zmiany poprawnie.", "Zapis", MessageBoxButton.OK, MessageBoxImage.Information);
                            Close();
                            return;
                        }
                    }
                    
                }
            }
            
        }

        private void DeleteGroup(object sender, EventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Czy na pewno chcesz usunąć tę grupę? Zmiany będą nieodwracalne.", "Usuwanie grupy", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                List<Account> accountList = groupRepository.GetUsersFromGroup(actualGroup.GroupID);
                Role role = accountRepository.GetRole("User");
                foreach(Account account in accountList)
                {
                    Account editAccount = accountRepository.Get(account.AccountID);
                    if (!editAccount.Role.Name.Equals("User"))
                    {
                        editAccount.RoleID = role.RoleID;
                        editAccount.Role = role;
                        accountRepository.Edit(editAccount);
                    }
                }
                groupRepository.Delete(actualGroup.GroupID);
                MessageBox.Show("Konto zostało usunięte.", "Usuwanie konta zakończone", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
        }
    }
}
