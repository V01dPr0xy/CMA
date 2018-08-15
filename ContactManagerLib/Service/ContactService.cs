using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactManagerLib.Models;

namespace ContactManagerLib.Service
{
    public class ContactService : IContactService
    {
        public void CreateNewUser(UserData userData)
        {
            throw new NotImplementedException();
        }

        public void Delete(Contact contact)
        {
            throw new NotImplementedException();
        }

        public List<Contact> RetrieveAllUserContacts(Guid userid)
        {
            throw new NotImplementedException();
        }

        public Guid RetrieveUserId(UserData userData)
        {
            throw new NotImplementedException();
        }

        public void Upsert(Contact contact)
        {
            throw new NotImplementedException();
        }

        public bool ValidateUserInformation(UserData userData)
        {
            throw new NotImplementedException();
        }
    }
}
