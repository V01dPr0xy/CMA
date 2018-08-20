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
                if (email.Name.Equals("primaryEmail") && !email.Text.Equals(""))
                {
                    _primary.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
                    email.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
                    email.Text = "";
                    AddLabel("InvalidPrim", 1, 1, email.Parent);
                }
                else if (email.Name.Equals("secondaryEmail") && !email.Text.Equals(""))
                {
                    _secondary.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
                    email.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
                    email.Text = "";
                    AddLabel("InvalidSec", 1, 3, email.Parent);
                    
                }
                
            }else if (reg.IsMatch(email.Text) || email.Text.Equals(""))
            {
                if (email.Name.Equals("primaryEmail"))
                {
                    _primary.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White);
                    email.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White);
                    RemoveLabel("InvalidPrim", email.Parent);

                }
                else if (email.Name.Equals("secondaryEmail"))
                {
                    _secondary.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White);
                    email.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White);
                    RemoveLabel("InvalidSec", email.Parent);
                }

            }
            
        }
        private void AddLabel(string name, int column, int row, DependencyObject _parent)
        {
            Label temp = new Label();
            Grid parent = _parent as Grid;
            temp.Name = name;
            temp.Content = "The above email is invalid try again.";


            Grid.SetColumn(temp, column);
            Grid.SetRow(temp, row);
            parent.Children.Add(temp);
        }
        private void RemoveLabel(string labelName, DependencyObject _parent)
        {
            Label tempLabel = null;
            Grid parent = _parent as Grid;
            foreach (object child in parent.Children)
            {
                if (child.GetType().Equals(typeof(Label)))
                {
                    if ((child as Label).Name.Equals(labelName))
                    {
                        tempLabel = child as Label;
                    }
                }
            }
            if (tempLabel != null)
            {
                parent.Children.Remove(tempLabel);
            }

        }
        private bool EmailLenChecker(string email)
        {
            return email.Length>256;
        }
    }
}