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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Jira.Views.GroupOwner
{
    /// <summary>
    /// Interaction logic for GroupOwnerPanel.xaml
    /// </summary>
    public partial class GroupOwnerPanel : Window
    {
        List<Issue> list = new List<Issue>();
        private ListCollectionView View
        {
            get { return (ListCollectionView)CollectionViewSource.GetDefaultView(list); }
        }
        public GroupOwnerPanel()
        {
            InitializeComponent();
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
    }
}
