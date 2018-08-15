using ContactManagerLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagerLib.Service
{
    interface IContactService
    {
        void Upsert(Contact contact);
        void Delete(Contact contact);
        List<Contact> RetrieveAllUserContacts(Guid userid);
        bool ValidateUserInformation(UserData userData);
        void CreateNewUser(UserData userData);
        Guid RetrieveUserId(UserData userData);
    }
}
