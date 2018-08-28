using ContactManager.Model;
using ContactManager.Views;
using ContactManagerLib.Models;
using System.Drawing;

namespace ContactManager.Presenters
{
    public class EditContactPresenter : PresenterBase<EditContactView>
    {
        private readonly ApplicationPresenter _applicationPresenter;
        private readonly Contact _contact;
        public EditContactPresenter(ApplicationPresenter applicationPresenter, EditContactView view, Contact contact):base(view, "Contact.LookupName")
        {
            _applicationPresenter = applicationPresenter;
            _contact = contact;
        }

        public Contact Contact
        {
            get { return _contact; }
        }

        public void SelectImage()
        {
            string imagePath = view.AskUserForImagePath();

            ImageConverter imageConverter = new ImageConverter();
            Bitmap bitmap = new Bitmap(imagePath);
            byte[] imageBytes = (byte[])imageConverter.ConvertTo(bitmap, typeof(byte[]));
            Contact.Image = imageBytes;
        }
        public void Save()
        {
            Contact temp = Contact;
            Validation.validateContact(temp);
            
            _applicationPresenter.SaveContact(temp);
        }
        public void Delete()
        {
            _applicationPresenter.CloseTab(this);
            _applicationPresenter.DeleteContact(Contact);
        }
        public void Close()
        {
            _applicationPresenter.CloseTab(this);
        }
        public override bool Equals(object obj)
        {
            EditContactPresenter presenter = obj as EditContactPresenter;
            return presenter != null && presenter.Contact.Equals(Contact);
        }
    }
}