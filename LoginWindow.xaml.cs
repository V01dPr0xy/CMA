using ContactManager.UserControls;
using ContactManagerLib.Models;
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

namespace ContactManager
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private UserData userData;
        public LoginWindow()
        {
            InitializeComponent();
            userData = new UserData();
            DataContext = userData;
        }

        private void LoginButtonFuction(object sender, RoutedEventArgs e)
        {

        }
        private void LoginRegisterButtonFuction(object sender, RoutedEventArgs e)
        {
            ControlGrid.Children.RemoveAt(0);
            RegisterControl registerControl = new RegisterControl();
            registerControl.RegisterEvent = RegisterNewUserButtonFuction;
            registerControl.CancelEvent = CancelNewUserButtonFuction;
            registerControl.VerticalAlignment = VerticalAlignment.Center;
            registerControl.HorizontalAlignment = HorizontalAlignment.Center;
            ControlGrid.Children.Add(registerControl);
        }
        private void RegisterNewUserButtonFuction(object sender, RoutedEventArgs e)
        {

        }
        private void CancelNewUserButtonFuction(object sender, RoutedEventArgs e)
        {
            ControlGrid.Children.RemoveAt(0);
            LoginControl loginControl = new LoginControl();
            loginControl.RegisterEvent = LoginRegisterButtonFuction;
            loginControl.LoginEvent = LoginButtonFuction;
            loginControl.VerticalAlignment = VerticalAlignment.Center;
            loginControl.HorizontalAlignment = HorizontalAlignment.Center;
            ControlGrid.Children.Add(loginControl);
        }
    }
}
