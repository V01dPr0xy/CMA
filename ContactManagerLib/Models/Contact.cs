﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagerLib.Models
{
    public class Contact : Notifier
    {
        //GUID
        private Guid _id = Guid.Empty;

        //Address
        private Address _address = new Address();

        //Phone numbers
        private string _cellPhone;
        private string _homePhone;
        private string _officePhone;

        //Name
        private string _firstName;
        private string _lastName;

        //Job and organization
        private string _jobTitle;
        private string _organization;

        //Emails
        private string _primaryEmail;
        private string _secondaryEmail;
        private byte[] _image;

        public Contact GetContact
        {
            get { return this; }
        }

        public Guid Id
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("Id");
            }
        }
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged("FirstName");
                OnPropertyChanged("LookupName");
            }
        }

        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged("LastName");
                OnPropertyChanged("LookupName");
            }
        }
        public string Organization
        {
            get { return _organization; }
            set
            {
                _organization = value;
                OnPropertyChanged("Organization");
            }
        }

        public string JobTitle
        {
            get { return _jobTitle; }
            set
            {
                _jobTitle = value;
                OnPropertyChanged("JobTitle");
            }
        }
        public string OfficePhone
        {
            get { return _officePhone; }
            set
            {
                _officePhone = value;
                OnPropertyChanged("OfficePhone");
                OnPropertyChanged("GetContact");
            }
        }
        public string CellPhone
        {
            get { return _cellPhone; }
            set
            {
                _cellPhone = value;
                OnPropertyChanged("CellPhone");
                OnPropertyChanged("GetContact");
            }
        }
        public string HomePhone
        {
            get { return _homePhone; }
            set
            {
                _homePhone = value;
                OnPropertyChanged("HomePhone");
                OnPropertyChanged("GetContact");
            }
        }
        public string PrimaryEmail
        {
            get { return _primaryEmail; }
            set
            {
                _primaryEmail = value;
                OnPropertyChanged("PrimaryEmail");
                OnPropertyChanged("GetContact");
            }
        }
        public string SecondaryEmail
        {
            get { return _secondaryEmail; }
            set
            {
                _secondaryEmail = value;
                OnPropertyChanged("SecondaryEmail");
                OnPropertyChanged("GetContact");
            }
        }
        public Address Address
        {
            get { return _address; }
            set
            {
                _address = value;
                OnPropertyChanged("Address");
            }
        }

        public byte[] Image
        {
            get { return _image; }
            set
            {
                _image = value;
                OnPropertyChanged("Image");
            }

        }

        public string FullName {
            get { return string.Format("{0}, {1}", _lastName, _firstName); }
        }

        public override bool Equals(object obj)
        {
            Contact other = obj as Contact;
            return other != null && other.Id == Id;
        }
    }
}
