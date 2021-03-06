﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;
using ContactManager.Model;

namespace ContactManager.Presenters
{
    public class PhoneConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

                string result = value as string;

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

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return FilterNonNumeric(value as string);
        }
        //change to check for nulls over empty strings upon database implementation.
        
        public static string FilterNonNumeric(string stringToFilter)
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
        public bool CheckNumericLength(string stringToCheck) 
        {
            string temp = FilterNonNumeric(stringToCheck);
            

            return temp.Length>11;
        }
    }
}
