using ContactManager.UserControls;
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
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButtonFuction(object sender, RoutedEventArgs e)
        {

        }
        private void RegisterButtonFuction(object sender, RoutedEventArgs e)
        {
            ControlGrid.Children.RemoveAt(0);
            RegisterControl registerControl = new RegisterControl();
            ControlGrid.Children.Add(registerControl);
        }
    }
}
