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
        //Add on focusLost event to all phone number input fields, validate phone numbers are not in between acceptable values. 5, 7, 11, if they are remove
        // the excess characters and warn the user with a label.

        private void phoneLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox phone = sender as TextBox;
            bool office = phone.Name.Equals("office");
            bool cell = phone.Name.Equals("cell");
            bool home = phone.Name.Equals("home");

            if (PhoneConverter.FilterNonNumeric(phone.Text).Length > 7 && PhoneConverter.FilterNonNumeric(phone.Text).Length < 10)
            {
                phone.Text = phone.Text.Remove(6);
                
                //remove any pre-existing labels
                phoneLabelManager(office, cell, home, phone, "", false);
                //Add new label with new text
                phoneLabelManager(office, cell, home, phone, "The " + phone.Name + " phone is to short, last characters will be removed to make it fit a valid format.", true);
            }else if (PhoneConverter.FilterNonNumeric(phone.Text).Length < 7 && !PhoneConverter.FilterNonNumeric(phone.Text).Equals(""))
            {
                //remove any pre-existing labels
                phoneLabelManager(office, cell, home, phone, "", false);
                //Add new label with new text
                phoneLabelManager(office, cell, home, phone, "The " + phone.Name + " phone is to short, please check that the phone number is at least 5 digits in length.", true);
            }
            else if(PhoneConverter.FilterNonNumeric(phone.Text).Length == 5 ||
                PhoneConverter.FilterNonNumeric(phone.Text).Length == 7 ||
                PhoneConverter.FilterNonNumeric(phone.Text).Length == 11 ||
                PhoneConverter.FilterNonNumeric(phone.Text).Equals(""))
            {
                //calling labelManager to remove the labels that were added when the phone number is valid.
                phoneLabelManager(office, cell, home, phone, "", false);
            }
            

            
        }
        private void phoneLabelManager(bool office, bool cell, bool home, TextBox phone, string message, bool addLabel)
        {
            if (addLabel)
            {
                if (office)
                {
                    //row 2
                    AddLabel("OfficePhone", 2, 2, phone.Parent, message);
                }
                else if (cell)
                {
                    //row 4
                    AddLabel("CellPhone", 2, 4, phone.Parent, message);
                }
                else if (home)
                {
                    //row 6
                    AddLabel("HomePhone", 2, 6, phone.Parent, message);
                }
            }else
            {
                if (office)
                {
                   
                    RemoveLabel("OfficePhone", phone.Parent);
                }
                else if (cell)
                {
                    
                    RemoveLabel("CellPhone", phone.Parent);
                }
                else if (home)
                {
                    
                    RemoveLabel("HomePhone", phone.Parent);
                }
            }
        }
        private void EmailLostFocus(object sender, RoutedEventArgs e)
        {
            TextBox email = sender as TextBox;

            if (EmailLenChecker(email.Text)) email.Text = email.Text.Remove(email.Text.Length - 1);

            Regex reg = new Regex(@"(\w+@\w+\.\w+)");
            if (!reg.IsMatch(email.Text))
            {
                if (email.Name.Equals("primaryEmail") && !email.Text.Equals(""))
                {
                    _primary.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
                    email.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
                    //email.Text = "";
                    AddLabel("InvalidPrim", 1, 1, email.Parent, "The primary email is invalid");
                }
                else if (email.Name.Equals("secondaryEmail") && !email.Text.Equals(""))
                {
                    _secondary.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
                    email.Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Red);
                    //email.Text = "";
                    AddLabel("InvalidSec", 1, 3, email.Parent, "The secondary email is invalid");
                    
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
        private void AddLabel(string name, int column, int row, DependencyObject _parent, string message)
        {
            Label temp = new Label();
            Grid parent = _parent as Grid;
            temp.Name = name;
            temp.Content = message;


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