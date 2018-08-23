using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagerLib.Models
{
    public class UserData
    {
        public Guid userId { get; set; }
        public string userName { get; set; }
        private string password;
        public string phoneNumber { get; set; }
        public string email { get; set; }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }
    }
}
