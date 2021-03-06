﻿using ContactManagerLib.Models;
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

namespace ContactManager.UserControls
{
    /// <summary>
    /// Interaction logic for LoginControl.xaml
    /// </summary>
    public partial class LoginControl : UserControl
    {
        private RoutedEventHandler registerEvent, loginEvent;
        private bool invalidLoginAttempt = false;

        public LoginControl()
        {
            InitializeComponent();
        }

        public RoutedEventHandler RegisterEvent
        {
            get { return registerEvent; }
            set { registerEvent = value; }
        }

        public RoutedEventHandler LoginEvent
        {
            get { return loginEvent; }
            set { loginEvent = value; }
        }

        private void RegisterButtonClick(object sender, RoutedEventArgs e)
        {
            registerEvent.Invoke(sender, e);
        }

        private void LoginButtonClick(object sender, RoutedEventArgs e)
        {
            loginEvent.Invoke(sender, e);
        }

        private void passwordTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            if (invalidLoginAttempt)
            {
                InvalidUserLabel.Content = "";

                passwordTextBox.Background = new SolidColorBrush(Colors.White);
                userNameTextBox.Background = new SolidColorBrush(Colors.White);

                invalidLoginAttempt = false;
            }
            (DataContext as UserData).password = passwordTextBox.Password;
        }

        public void InvalidUser()
        {
            if (!invalidLoginAttempt)
            {
                InvalidUserLabel.Content = "Username or password was incorrect, look back and try again.";
                passwordTextBox.Password = "";
            
                passwordTextBox.Background = new SolidColorBrush(Colors.PaleVioletRed);
                userNameTextBox.Background = new SolidColorBrush(Colors.PaleVioletRed);

                invalidLoginAttempt = true;
            }
        }
    }
}
