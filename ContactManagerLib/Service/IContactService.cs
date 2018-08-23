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
        void UpsertUserContact(Contact contact, Guid userId);
        void DeleteUserContact(Contact contact, Guid userId);
        List<Contact> RetrieveAllUserContacts(Guid userid);
        bool ValidateUserInformation(UserData userData);
        void CreateNewUser(UserData userData);
        Guid RetrieveUserId(UserData userData);
        bool CheckIfUserNameAlreadyExisits(UserData userData);
    }
}
