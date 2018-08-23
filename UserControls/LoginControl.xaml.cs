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

        public LoginControl()
        {
            InitializeComponent();
            Button tets = new Button();
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
    }
}
