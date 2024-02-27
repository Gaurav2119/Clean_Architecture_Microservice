using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#nullable disable

namespace User.Domain.Entities
{
    public sealed class UserList
    {
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public string Role { get; private set; }

        public UserList(string userName, string password, string role)
        {
            UserName = userName; 
            Password = password; 
            Role = role;
        }
    }
}
