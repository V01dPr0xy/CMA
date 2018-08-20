using ContactManager.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ContactManager.Presenters
{
    class SideBarEmailConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Contact contact = value as Contact;
            string activeEmail = "";

            if (contact.PrimaryEmail != null && !contact.PrimaryEmail.Equals(""))
            {
                activeEmail = contact.PrimaryEmail;
            } else if (contact.PrimaryEmail != null && !contact.SecondaryEmail.Equals(""))
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
