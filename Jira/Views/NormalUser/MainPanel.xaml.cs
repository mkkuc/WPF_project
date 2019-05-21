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
        ObservableCollection<DataTransferObjects.Models.Issue> issues = new ObservableCollection<DataTransferObjects.Models.Issue>();
        public MainPanel()
        {
            InitializeComponent();

            issues.Add(new DataTransferObjects.Models.Issue
            {
                IssueID = 0,
                Title = "Testowe zadanie 1",
                Description = "Opis zadania testowego numer 1",
                PriorityID = 0,
                Priority = new DataTransferObjects.Models.Priority { PriorityID = 0, Name = "Ważne" }
            });

        }
    }
}
