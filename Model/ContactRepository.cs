using ContactManagerLib.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using ContactManagerLib.Service;

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
            contactControl.UpsertUserContact(contact, user.userId);

            if (!_contactStore.Contains(contact))
                _contactStore.Add(contact);
        }

        public void Delete(Contact contact)
        {
            contactControl.DeleteUserContact(contact, user.userId);

            _contactStore.Remove(contact);
        }

        public List<Contact> FindByLookup(string lookupName)
        {
            IEnumerable<Contact> found = 
                from c in _contactStore
                where c.LookupName.StartsWith(
                    lookupName,
                    StringComparison.OrdinalIgnoreCase)
                select c;

            return found.ToList();
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
