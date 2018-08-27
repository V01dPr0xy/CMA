﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using ContactManager.Presenters;

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

        public List<Contact> SearchByPhoneNumber(string lookup)
        {
            List<Contact> result = new List<Contact>();

            foreach(var c in _contactStore)
            {
                if (c.CellPhone != null)
                    if (c.CellPhone.StartsWith(PhoneConverter.FilterNonNumeric(lookup)))
                        result.Add(c);

                else if (c.OfficePhone != null)
                    if (c.OfficePhone.StartsWith(PhoneConverter.FilterNonNumeric(lookup)))
                        result.Add(c);

                else if (c.HomePhone != null)
                    if (c.HomePhone.StartsWith(PhoneConverter.FilterNonNumeric(lookup)))
                        result.Add(c);
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
                    result = _contactStore.Where(c => c.FirstName.Contains(lookup) || c.LastName.Contains(lookup)).ToList();
                    break;
                case ContactFields.PHONENUMBER:
                    result = SearchByPhoneNumber(lookup);
                    break;
                case ContactFields.CITY:
                    result = _contactStore.Where(c => c.Address.City.Contains(lookup)).ToList();
                    break;
                case ContactFields.JOB:
                    result = _contactStore.Where(c => c.JobTitle.Contains(lookup)).ToList();
                    break;
                case ContactFields.ORGANIZATION:
                    result = _contactStore.Where(c => c.Organization.Contains(lookup)).ToList();
                    break;
                case ContactFields.EMAIL:
                    result = _contactStore.Where(c => c.PrimaryEmail.Contains(lookup) || c.SecondaryEmail.Contains(lookup)).ToList();
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
