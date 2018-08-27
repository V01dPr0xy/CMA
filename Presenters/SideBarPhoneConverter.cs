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

    //Add in another converter for the "sideBarEmailConverter" Basically the same as this except it displays the most valid email.
    //Add in the getMethods to the email designation.
    class SideBarPhoneConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Contact contact = value as Contact;
            string activePhone = "";

            if(contact.OfficePhone != null)
            {
                if (!contact.OfficePhone.Equals(""))
                {
                    activePhone = contact.OfficePhone;
                }
                else if (!contact.CellPhone.Equals(""))
                {
                    activePhone = contact.CellPhone;
                }
                else if (!contact.HomePhone.Equals(""))
                {
                    activePhone = contact.HomePhone;
                }
            }

            string result = activePhone;

            if (!string.IsNullOrEmpty(result))
            {
                string filteredResult = FilterNonNumeric(result);
                long theNumber = System.Convert.ToInt64(filteredResult);
                switch (filteredResult.Length)
                {
                    case 11:
                        result = string.Format("{0:+# (###) ###-####}", theNumber);
                        break;

                    case 10:
                        result = string.Format("{0:(###) ###-####}", theNumber);
                        break;

                    case 7:
                        result = string.Format("{0:###-####}", theNumber);
                        break;
                }
            }

            return result;
        }

        private static string FilterNonNumeric(string stringToFilter)
        {
            if (string.IsNullOrEmpty(stringToFilter))
                return string.Empty;

            string filteredResult = string.Empty;

            foreach (char c in stringToFilter)
            {
                if (Char.IsDigit(c))
                    filteredResult += c;
            }

            return filteredResult;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
