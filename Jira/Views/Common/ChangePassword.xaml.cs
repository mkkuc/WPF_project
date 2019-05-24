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

namespace Jira.Views.Common
{
    /// <summary>
    /// Interaction logic for ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {
        public Account account { get; set; }

        public ChangePassword(Account user)
        {
            account = user;
            InitializeComponent();
        }

        private void OldEnter(object sender, EventArgs e)
        {
            if (OldPassBox.Text == "Aktualne hasło")
            {
                OldPassBox.Text = "";
                OldPassBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
            }
        }

        private void OldLeave(object sender, EventArgs e)
        {
            if (OldPassBoxP.Password == "" || OldPassBoxP.Password == null)
            {
                OldPassBox.Text = "Aktualne hasło";
                OldPassBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Silver"));
            }
        }

        private void NewPasswordEnter(object sender, EventArgs e)
        {
            if (NewPassBox.Text == "Nowe hasło")
            {
                NewPassBox.Text = "";
                NewPassBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
            }
        }

        private void NewPasswordLeave(object sender, EventArgs e)
        {
            if (NewPassBoxP.Password == "" || NewPassBoxP.Password == null)
            {
                NewPassBox.Text = "Nowe hasło";
                NewPassBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Silver"));
            }
        }

        private void RepeatPasswordEnter(object sender, EventArgs e)
        {
            if (RepeatPassBox.Text == "Powtórz hasło")
            {
                RepeatPassBox.Text = "";
                RepeatPassBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
            }
        }

        private void RepeatPasswordLeave(object sender, EventArgs e)
        {
            if (RepeatPassBoxP.Password == "" || RepeatPassBoxP.Password == null)
            {
                RepeatPassBox.Text = "Powtórz hasło";
                RepeatPassBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Silver"));
            }
        }

    }
}
