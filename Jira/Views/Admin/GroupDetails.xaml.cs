using DataTransferObjects.Models;
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
        public GroupDetails(Group group)
        {
            actualGroup = group;
            InitializeComponent();
            GroupDetailsWindow.DataContext = actualGroup;
        }

        private void SaveChangesInGroup(object sender, EventArgs e)
        {

        }

        private void DeleteGroup(object sender, EventArgs e)
        {

        }

        private void ChangeOwner(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
