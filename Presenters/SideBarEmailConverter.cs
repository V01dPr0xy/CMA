using ContactManager.Model;
using ContactManagerLib.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ContactManager.Presenters
{
    class SideBarEmailConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Contact contact = value as Contact;
            Regex reg = new Regex(@"(\w+@\w+.\w+)");
            string activeEmail = "";
            if (contact.PrimaryEmail != null && !contact.PrimaryEmail.Equals("") && reg.IsMatch(contact.PrimaryEmail))
            {
                activeEmail = contact.PrimaryEmail;
            }
            else if (contact.PrimaryEmail != null && !contact.SecondaryEmail.Equals("") && reg.IsMatch(contact.SecondaryEmail))
            {
                activeEmail = contact.SecondaryEmail;
            }
            return activeEmail;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
