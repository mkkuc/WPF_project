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

            }
        }
    }
}
