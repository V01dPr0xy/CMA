using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManagerLib.Models
{
    public class UserData
    {
        private Guid userId { get; set; }
        private string userName { get; set; }
        private string password { get; set; }
        private string phoneNumber { get; set; }
        public string email { get; set; }
    }
}
