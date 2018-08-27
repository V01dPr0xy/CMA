using ContactManager.UserControls;
using ContactManagerLib.Models;
using ContactManagerLib.Service;
using System.Windows;

namespace ContactManager
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private UserData userData;
        private IContactService contactService;

        public LoginWindow()
        {
            InitializeComponent();
            SetUserDataContext();
            contactService = new ContactService();
        }

        private void LoginButtonFuction(object sender, RoutedEventArgs e)
        {
            LoginControl tempControl = null;
            foreach (object child in ControlGrid.Children)
            {
                if (child.GetType().Equals(typeof(LoginControl)))
                {
                    tempControl = child as LoginControl;
                }
            }
            if (string.IsNullOrEmpty(userData.userName) || string.IsNullOrEmpty(userData.password))
            {
                tempControl.InvalidUser();
            }
            else if (contactService.ValidateUserInformation(userData))
            {
                userData.userId = contactService.RetrieveUserId(userData);

                Shell newWindow = new Shell(userData);
                Application.Current.MainWindow = newWindow;
                Close();
                newWindow.Show();
            }
            else
            {
                tempControl.InvalidUser();
            }            
        }
        private void LoginRegisterButtonFuction(object sender, RoutedEventArgs e)
        {
            SetUserDataContext();
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
            RegisterControl tempControl = null;
            foreach (object child in ControlGrid.Children)
            {
                if (child.GetType().Equals(typeof(RegisterControl)))
                {
                    tempControl = child as RegisterControl;
                }
            }
            if (!string.IsNullOrEmpty(userData.userName))
            {
                if (contactService.CheckIfUserNameAlreadyExisits(userData))
                {
                    tempControl.InvalidUserName();
                }
                else
                {
                    contactService.CreateNewUser(userData);
                    CancelNewUserButtonFuction(sender, e);
                }
            }
            else
            {
                tempControl.InvalidUserName(true);
            }
        }
        private void CancelNewUserButtonFuction(object sender, RoutedEventArgs e)
        {
            SetUserDataContext();
            ControlGrid.Children.RemoveAt(0);
            LoginControl loginControl = new LoginControl();
            loginControl.RegisterEvent = LoginRegisterButtonFuction;
            loginControl.LoginEvent = LoginButtonFuction;
            loginControl.VerticalAlignment = VerticalAlignment.Center;
            loginControl.HorizontalAlignment = HorizontalAlignment.Center;
            ControlGrid.Children.Add(loginControl);
        }
        private void SetUserDataContext()
        {
            userData = new UserData();
            DataContext = userData;
        }
    }
}
