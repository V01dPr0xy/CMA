using ContactManagerLib.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using ContactManagerLib.Service;
using ContactManager.Presenters;

namespace ContactManager.Model
{
    public class ContactRepository
    {
        private List<Contact> _contactStore;
        private readonly string _stateFile;
        private IContactService contactControl;
        private UserData user;

        public ContactRepository(UserData userData)
        {

            contactControl = new ContactService();
            user = userData;
            RetrieveAllContacts();
        }

        public void Save(Contact contact)
        {
            contact = contactControl.UpsertUserContact(contact, user.userId);

            if (!_contactStore.Contains(contact))
                _contactStore.Add(contact);

        }

        public void Delete(Contact contact)
        {
            contactControl.DeleteUserContact(contact, user.userId);

            _contactStore.Remove(contact);
        }

        public List<Contact> SearchByPhoneNumber(string lookup)
        {
            List<Contact> result = new List<Contact>();

            foreach(var c in _contactStore)
            {
                if (c.CellPhone != null)
                {
                    if (c.CellPhone.StartsWith(PhoneConverter.FilterNonNumeric(lookup)))
                    {
                        result.Add(c);
                    }
                }
                else if (c.OfficePhone != null)
                {
                    if (c.OfficePhone.StartsWith(PhoneConverter.FilterNonNumeric(lookup)))
                    {
                        result.Add(c);
                    }
                }
                else if (c.HomePhone != null)
                {
                    if (c.HomePhone.StartsWith(PhoneConverter.FilterNonNumeric(lookup)))
                    {
                        result.Add(c);
                    }
                }
            }

            return result;
        }

        public List<Contact> FindByLookup(string lookup, ContactFields selectedContactField)
        {
            //Switch based upon which enum is passed in that is being searched by.
            List<Contact> result = new List<Contact>();

            switch(selectedContactField)
            {
                case ContactFields.NAME:
                    result = _contactStore.Where(c => c.FirstName.StartsWith(lookup, StringComparison.InvariantCultureIgnoreCase) || c.LastName.StartsWith(lookup, StringComparison.InvariantCultureIgnoreCase)).ToList();
                    break;
                case ContactFields.PHONENUMBER:
                    result = SearchByPhoneNumber(lookup);
                    break;
                case ContactFields.CITY:
                    result = _contactStore.Where(c => c.Address.City.StartsWith(lookup, StringComparison.InvariantCultureIgnoreCase)).ToList();
                    break;
                case ContactFields.JOB:
                    result = _contactStore.Where(c => c.JobTitle.StartsWith(lookup, StringComparison.InvariantCultureIgnoreCase)).ToList();
                    break;
                case ContactFields.ORGANIZATION:
                    result = _contactStore.Where(c => c.Organization.StartsWith(lookup, StringComparison.InvariantCultureIgnoreCase)).ToList();
                    break;
                case ContactFields.EMAIL:
                    result = _contactStore.Where(c => (c.PrimaryEmail != null && c.PrimaryEmail.StartsWith(lookup, StringComparison.InvariantCultureIgnoreCase)) || (c.SecondaryEmail != null && c.SecondaryEmail.StartsWith(lookup, StringComparison.InvariantCultureIgnoreCase))).ToList();
                    break;
            }

            return result;
        }

        public List<Contact> FindAll()
        {
            return new List<Contact>(_contactStore);
        }

        private void RetrieveAllContacts()
        {
            _contactStore = contactControl.RetrieveAllUserContacts(user.userId);
        }
    }
}
