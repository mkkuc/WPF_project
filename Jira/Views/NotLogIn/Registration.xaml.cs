﻿using System;
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

namespace Jira.Views.NotLogIn
{
    /// <summary>
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void LoginEnter(object sender, EventArgs e)
        {
            if (LoginTextBox.Text == "Login")
            {
                LoginTextBox.Text = "";
                LoginTextBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
            }
        }

        private void LoginLeave(object sender, EventArgs e)
        {
            if (LoginTextBox.Text == "" || LoginTextBox.Text == null)
            {
                LoginTextBox.Text = "Login";
                LoginTextBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Silver"));
            }
        }

        private void PasswordEnter(object sender, EventArgs e)
        {
            if (PasswordTextBox.Text == "Hasło")
            {
                PasswordTextBox.Text = "";
                PasswordTextBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
            }
        }

        private void PasswordLeave(object sender, EventArgs e)
        {
            if (PasswordTextBox.Text == "" || PasswordTextBox.Text == null)
            {
                PasswordTextBox.Text = "Hasło";
                PasswordTextBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Silver"));
            }
        }

        private void RepeatPasswordEnter(object sender, EventArgs e)
        {
            if (RepeatPasswordTextBox.Text == "Powtórz hasło")
            {
                RepeatPasswordTextBox.Text = "";
                RepeatPasswordTextBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
            }
        }

        private void RepeatPasswordLeave(object sender, EventArgs e)
        {
            if (RepeatPasswordTextBox.Text == "" || RepeatPasswordTextBox.Text == null)
            {
                RepeatPasswordTextBox.Text = "Powtórz hasło";
                RepeatPasswordTextBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Silver"));
            }
        }

        private void NameEnter(object sender, EventArgs e)
        {
            if (NameTextBox.Text == "Imię")
            {
                NameTextBox.Text = "";
                NameTextBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
            }
        }

        private void NameLeave(object sender, EventArgs e)
        {
            if (NameTextBox.Text == "" || NameTextBox.Text == null)
            {
                NameTextBox.Text = "Imię";
                NameTextBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Silver"));
            }
        }

        private void SurnameEnter(object sender, EventArgs e)
        {
            if (SurnameTextBox.Text == "Nazwisko")
            {
                SurnameTextBox.Text = "";
                SurnameTextBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
            }
        }

        private void SurnameLeave(object sender, EventArgs e)
        {
            if (SurnameTextBox.Text == "" || SurnameTextBox.Text == null)
            {
                SurnameTextBox.Text = "Nazwisko";
                SurnameTextBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Silver"));
            }
        }

        private void EmailEnter(object sender, EventArgs e)
        {
            if (EmailTextBox.Text == "Email")
            {
                EmailTextBox.Text = "";
                EmailTextBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Black"));
            }
        }

        private void EmailLeave(object sender, EventArgs e)
        {
            if (EmailTextBox.Text == "" || SurnameTextBox.Text == null)
            {
                EmailTextBox.Text = "Email";
                EmailTextBox.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("Silver"));
            }
        }
    }
}