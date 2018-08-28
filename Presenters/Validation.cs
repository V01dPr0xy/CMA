using ContactManager.Model;
using ContactManagerLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ContactManager.Presenters
{
    public static class Validation
    {
        static Regex reg = new Regex(@"(\w+@\w+\.\w+)");
        //Ask fox if azure has blob support
        public static Contact validateContact(Contact contact)
        {
            Contact validation = contact;

            validateEmail(validation);
            //Phone Validation is not needed here, however I placed it for sake of completion
            //validatePhone(contact);
            return null;
        }

        private static void validateEmail(Contact contact)
        {
            if (contact.PrimaryEmail != null && !reg.IsMatch(contact.PrimaryEmail))
            {
                contact.PrimaryEmail = "";
            }
            if (contact.SecondaryEmail != null && !reg.IsMatch(contact.SecondaryEmail))
            {
                contact.SecondaryEmail = "";
            }
        }
        private static void validatePhone(Contact contact)
        {

        }
    }
}
