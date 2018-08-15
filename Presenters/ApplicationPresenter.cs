using System;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using ContactManager.Model;
using ContactManager.Views;

namespace ContactManager.Presenters
{
    public class ApplicationPresenter : PresenterBase<Shell>
    {
        private readonly ContactRepository _contactRepository;
        private ObservableCollection<Contact> _currentContacts;
        private string _statusText;

        public ApplicationPresenter(Shell view, ContactRepository contactRepository) : base(view)
        {
            _contactRepository = contactRepository;
            _currentContacts = new ObservableCollection<Contact>(_contactRepository.FindAll());

        }

        public ObservableCollection<Contact> CurrentContacts
        {
            get { return _currentContacts; }
            set
            {
                _currentContacts = value;
                OnPropertyChanged("CurrentContacts");
            }
        }
        public string StatusText
        {
            get { return _statusText; }
            set
            {
                _statusText = value;
                OnPropertyChanged("StatusText");
            }
        }

        public void Search(string criteria, int index)
        {
            if (!string.IsNullOrEmpty(criteria) && criteria.Length > 0 && index != 0)
            {
                //Search looks through the repo by lookup on the criteria.

                ContactFields selectedContactField = ContactFields.NONE;

                switch (index)
                {
                    case 1:
                        selectedContactField = ContactFields.NAME;
                        break;
                    case 2:
                        selectedContactField = ContactFields.ORGANIZATION;
                        break;
                    case 3:
                        selectedContactField = ContactFields.JOB;
                        break;
                    case 4:
                        selectedContactField = ContactFields.CITY;
                        break;
                    case 5:
                        selectedContactField = ContactFields.PHONENUMBER;
                        break;
                    case 6:
                        selectedContactField = ContactFields.EMAIL;
                        break;
                }

                CurrentContacts = new ObservableCollection<Contact>(_contactRepository.FindByLookup(criteria, selectedContactField));
                StatusText = string.Format("{0} contacts found.", CurrentContacts.Count);
            }
            else
            {
                CurrentContacts = new ObservableCollection<Contact>(
                _contactRepository.FindAll()
                );
                StatusText = "Displaying all contacts.";
            }
        }

        public void NewContact()
        {
            OpenContact(new Contact());
        }

        public void SaveContact(Contact contact)
        {
            if (!CurrentContacts.Contains(contact))
                CurrentContacts.Add(contact);

            _contactRepository.Save(contact);
            StatusText = string.Format("Contact ‘{0}’ was saved.", contact.ToString());
        }

        public void DeleteContact(Contact contact)
        {
            if (CurrentContacts.Contains(contact))
                CurrentContacts.Remove(contact);

            _contactRepository.Delete(contact);
            StatusText = string.Format("Contact ‘{0}’ was deleted.", contact.ToString());

        }

        public void CloseTab<T>(PresenterBase<T> presenter)
        {
            view.RemoveTab(presenter);
        }

        public void OpenContact(Contact contact)
        {
            if (contact == null) return;
            view.AddTab(new EditContactPresenter(this, new EditContactView(), contact));
        }

        public void DisplayAllContacts()
        {
            view.AddTab(
                new ContactListPresenter(
                    this,
                    new ContactListView()
                    )
                );
        }
    }
}