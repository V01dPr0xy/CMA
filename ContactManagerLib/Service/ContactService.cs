using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ContactDAL;
using ContactManagerLib.Models;

namespace ContactManagerLib.Service
{
    public class ContactService : IContactService
    {
        public bool CheckIfUserNameAlreadyExisits(UserData userData)
        {
            bool uniqueUserName = false;
            using (ContactEntities db = new ContactEntities())
            {
                IQueryable<User> query = db.Users.Where(user => user.userName == userData.userName);
                uniqueUserName = query.ToList().Count == 0;
            }
            return uniqueUserName;
        }

        public void CreateNewUser(UserData userData)
        {
            using (ContactEntities db = new ContactEntities())
            {
                User newUser = new User()
                {
                    emailAddress = userData.email,
                    phoneNumber = userData.phoneNumber,
                    userName = userData.userName,
                    passwordHash = userData.password
                };
                db.Users.Add(newUser);
                db.SaveChangesAsync();
            }
        }

        public void DeleteUserContact(Models.Contact contact, Guid userId)
        {
            using (ContactEntities db = new ContactEntities())
            {
                IQueryable<User> userQuery = db.Users.Where(users => users.id == userId);
                User userData = userQuery.First();

                IQueryable<ContactDAL.Contact> contactQuery = db.Contacts.Where(contactLocation => contactLocation.userId == userData.id && contactLocation.id == contact.Id);
                ContactDAL.Contact contactData = contactQuery.First();

                db.Contacts.Remove(contactData);
                db.SaveChangesAsync();
            }
        }

        public Guid RetrieveUserId(UserData userData)
        {
            Guid userId = Guid.Empty;
            using (ContactEntities db = new ContactEntities())
            {
                IQueryable<Guid> query = db.Users
                    .Where(user => user.passwordHash == userData.password && user.userName == userData.userName)
                    .Select(user => user.id);
                userId = query.First();
            }
            return userId;
        }

        public void UpsertUserContact(Models.Contact contact, Guid userId)
        {
            using (ContactEntities db = new ContactEntities())
            {
                IQueryable<User> userQuery = db.Users.Where(user => user.id == userId);
                User userData = userQuery.First();

                if (contact.Id == Guid.Empty)
                {
                    ContactDAL.Contact newContact = new ContactDAL.Contact()
                    {
                        User = userData,
                        userId = userData.id,
                        contactFirstName = contact.FirstName,
                        contactLastName = contact.LastName,
                        contactJobTitle = contact.JobTitle,
                        contactOrganization = contact.Organization                    
                    };
                    Email newEmail = new Email()
                    {
                        Contact = newContact,
                        primaryEmail = contact.PrimaryEmail,
                        secondaryEmail = contact.SecondaryEmail
                    };
                    Image newImage = new Image()
                    {
                        Contact = newContact,
                        contactImage = contact.Image
                    };
                    PhoneNumber newPhoneNumber = new PhoneNumber()
                    {
                        Contact = newContact,
                        cellNumber = contact.CellPhone,
                        homeNumber = contact.HomePhone,
                        officeNumber = contact.OfficePhone
                    };
                    ContactDAL.Address newAddress = new ContactDAL.Address()
                    {
                        Contact = newContact,
                        addressLine1 = contact.Address.Line1,
                        addressLine2 = contact.Address.Line2,
                        addressCity = contact.Address.City,
                        addressCountry = contact.Address.Country,
                        addressStateCounty = contact.Address.State,
                        addressZip = contact.Address.Zip
                    };

                    newContact.Emails.Add(newEmail);
                    newContact.Images.Add(newImage);
                    newContact.PhoneNumbers.Add(newPhoneNumber);
                    newContact.Addresses.Add(newAddress);

                    db.Contacts.Add(newContact);
                    db.Emails.Add(newEmail);
                    db.Images.Add(newImage);
                    db.PhoneNumbers.Add(newPhoneNumber);
                    db.Addresses.Add(newAddress);

                    db.SaveChangesAsync();
                }
                else
                {
                    IQueryable<ContactDAL.Contact> contactQuery = db.Contacts.Where(contactLocatingData => contactLocatingData.userId == userData.id && contactLocatingData.id == contact.Id);
                    ContactDAL.Contact contactData = contactQuery.First();

                    contactData.contactFirstName = contact.FirstName;
                    contactData.contactLastName = contact.LastName;
                    contactData.contactJobTitle = contact.JobTitle;
                    contactData.contactOrganization = contact.Organization;
                    contactData.Emails.First().primaryEmail = contact.PrimaryEmail;
                    contactData.Emails.First().secondaryEmail = contact.SecondaryEmail;
                    contactData.Images.First().contactImage = contact.Image;
                    contactData.PhoneNumbers.First().cellNumber = contact.CellPhone;
                    contactData.PhoneNumbers.First().homeNumber = contact.HomePhone;
                    contactData.PhoneNumbers.First().officeNumber = contact.OfficePhone;
                    contactData.Addresses.First().addressLine1 = contact.Address.Line1;
                    contactData.Addresses.First().addressLine2 = contact.Address.Line2;
                    contactData.Addresses.First().addressCity = contact.Address.City;
                    contactData.Addresses.First().addressCountry = contact.Address.Country;
                    contactData.Addresses.First().addressStateCounty = contact.Address.State;
                    contactData.Addresses.First().addressZip = contact.Address.Zip;

                    db.SaveChangesAsync();
                }
            }
        }

        public bool ValidateUserInformation(UserData userData)
        {
            bool validUser = false;
            using (ContactEntities db = new ContactEntities())
            {
                IQueryable<User> query = db.Users.Where(user => user.userName == userData.userName && user.passwordHash == userData.password);
                validUser = query.ToList().Count == 1;
            }
            return validUser;
        }

        List<Models.Contact> IContactService.RetrieveAllUserContacts(Guid userid)
        {
            List<Models.Contact> userContacts = new List<Models.Contact>();
            using (ContactEntities db = new ContactEntities())
            {
                IQueryable<User> query = db.Users.Where(user => user.id == userid);
                User userData = query.First();
                foreach (ContactDAL.Contact contactData in userData.Contacts)
                {
                    Models.Contact contact = new Models.Contact()
                    {
                        Id = contactData.id,
                        FirstName = contactData.contactFirstName,
                        LastName = contactData.contactLastName,
                        JobTitle = contactData.contactJobTitle,
                        Organization = contactData.contactOrganization,
                        CellPhone = contactData.PhoneNumbers.First().cellNumber,
                        HomePhone = contactData.PhoneNumbers.First().homeNumber,
                        OfficePhone = contactData.PhoneNumbers.First().officeNumber,
                        PrimaryEmail = contactData.Emails.First().primaryEmail,
                        SecondaryEmail = contactData.Emails.First().secondaryEmail,
                        Image = contactData.Images.First().contactImage,
                        Address = new Models.Address
                        {
                            Line1 = contactData.Addresses.First().addressLine1,
                            Line2 = contactData.Addresses.First().addressLine2,
                            City = contactData.Addresses.First().addressCity,
                            Country = contactData.Addresses.First().addressCountry,
                            State = contactData.Addresses.First().addressStateCounty,
                            Zip = contactData.Addresses.First().addressZip
                        }
                    };
                    userContacts.Add(contact);
                }
            }
            return userContacts;
        }
    }
}
