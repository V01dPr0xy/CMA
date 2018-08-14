using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;

namespace ContactManager.Model
{
    public class ContactRepository
    {
        private List<Contact> _contactStore;
        private readonly string _stateFile;

        public ContactRepository()
        {
            _stateFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ContactManager.state");
            //MessageBox.Show(_stateFile);
            Deserialize();
        }

        public void Save(Contact contact)
        {
            if (contact.Id == Guid.Empty)
                contact.Id = Guid.NewGuid();

            if (!_contactStore.Contains(contact))
                _contactStore.Add(contact);

            Serialize();
        }

        public void Delete(Contact contact)
        {
            _contactStore.Remove(contact);
            Serialize();
        }

        public List<Contact> FindByLookup(string lookup, ContactFields selectedContactField)
        {
            //Switch based upon which enum is passed in that is being searched by.
            List<Contact> result = new List<Contact>();

            switch(selectedContactField)
            {
                case ContactFields.NAME:
                    result = _contactStore.Where(c => c.FirstName.StartsWith(lookup) || c.LastName.StartsWith(lookup)).ToList();
                    break;
                case ContactFields.PHONENUMBER:
                    result = _contactStore.Where(c => c.CellPhone.StartsWith(lookup) || c.HomePhone.StartsWith(lookup) || c.OfficePhone.StartsWith(lookup)).ToList();
                    break;
                case ContactFields.CITY:
                    result = _contactStore.Where(c => c.Address.City.StartsWith(lookup)).ToList();
                    break;
                case ContactFields.JOB:
                    result = _contactStore.Where(c => c.JobTitle.StartsWith(lookup)).ToList();
                    break;
                case ContactFields.ORGANIZATION:
                    result = _contactStore.Where(c => c.Organization.StartsWith(lookup)).ToList();
                    break;
                case ContactFields.EMAIL:
                    result = _contactStore.Where(c => c.PrimaryEmail.StartsWith(lookup) || c.SecondaryEmail.StartsWith(lookup)).ToList();
                    break;
            }



            return result;
        }

        public List<Contact> FindAll()
        {
            return new List<Contact>(_contactStore);
        }

        private void Serialize()
        {
            using(FileStream stream = File.Open(_stateFile, FileMode.OpenOrCreate))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, _contactStore);
            }
        }

        private void Deserialize()
        {
            if (File.Exists(_stateFile))
            {
                using (FileStream stream = File.Open(_stateFile, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    _contactStore = (List<Contact>)formatter.Deserialize(stream);
                }
            }
            else _contactStore = new List<Contact>();
        }
    }
}
