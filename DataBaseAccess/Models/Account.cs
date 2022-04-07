using System;
using System.Collections.Generic;

namespace DataBaseAccess.Models
{
    public partial class Account
    {
        public int IdAccount { get; set; }
        public string EMail { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime? AccountCreationDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }

        public virtual Client Client { get; set; }
        public virtual Manager Manager { get; set; }
    }
}
