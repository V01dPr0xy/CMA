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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ContactManager.UserControls
{
    /// <summary>
    /// Interaction logic for RegisterControl.xaml
    /// </summary>
    public partial class RegisterControl : UserControl
    {
        private RoutedEventHandler registerEvent, cancelEvent;

        public RoutedEventHandler RegisterEvent
        {
            get { return registerEvent; }
            set { registerEvent = value; }
        }

        public RoutedEventHandler CancelEvent
        {
            get { return cancelEvent; }
            set { cancelEvent = value; }
        }

        public RegisterControl()
        {
            InitializeComponent();
        }
        private void RegisterButtonClick(object sender, RoutedEventArgs e)
        {
            registerEvent.Invoke(sender, e);
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            cancelEvent.Invoke(sender, e);
        }
    }
}
