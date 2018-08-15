using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ContactManager.Presenters;
using Microsoft.Win32;
namespace ContactManager.Views
{
    public partial class EditContactView : UserControl
    {
        public EditContactView()
        {
            InitializeComponent();
        }
        public EditContactPresenter Presenter
        {
            get { return DataContext as EditContactPresenter; }
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Presenter.Save();
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Presenter.Delete();
        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Presenter.Close();
        }
        private void SelectImage_Click(object sender, RoutedEventArgs e)
        {
            Presenter.SelectImage();
        }
        public string AskUserForImagePath()
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.ShowDialog();
            return dlg.FileName;
        }

        private void phoneNumberTextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox text = sender as TextBox;
            
            PhoneConverter converter = new PhoneConverter();
            if (converter.CheckNumericLength(text.Text)) text.Text = text.Text.Remove(text.Text.Length - 1);

            text.CaretIndex = text.Text.Length;
            
                        
        }

        private void EmailLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox email = sender as TextBox;
            //StackPanel relatedParent = email.Parent as StackPanel;
            //foreach (object child in relatedParent.Children)
            //{
            //    if (child.GetType().Equals(typeof(Label)))
            //    {
            //        (child as Label).Background = new SolidColorBrush(Colors.Red);
            //    }
            //    else if (child.GetType().Equals(typeof(TextBox)))
            //    {
            //        (child as TextBox).Background = new SolidColorBrush(Colors.Red);
            //    }
            //}

            if (EmailLenChecker(email.Text)) email.Text = email.Text.Remove(email.Text.Length - 1);

            Regex reg = new Regex(@"(\w+@\w+\.\w+)");
            if (!reg.IsMatch(email.Text))
            {
                if (email.Name.Equals("primaryEmail"))
                {
                    _primary.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
                    email.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
                }
                else if (email.Name.Equals("secondaryEmail"))
                {
                    _secondary.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
                    email.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
                }

                
            }else if (reg.IsMatch(email.Text))
            {
                if (email.Name.Equals("primaryEmail"))
                {
                    _primary.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White);
                    email.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White);
                }
                else if (email.Name.Equals("secondaryEmail"))
                {
                    _secondary.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White);
                    email.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White);
                }
            }
            
        }

        private bool EmailLenChecker(string email)
        {
            return email.Length>256;
        }
    }
}