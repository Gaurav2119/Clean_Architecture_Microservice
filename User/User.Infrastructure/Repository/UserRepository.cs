using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Application.Dtos;
using User.Application.Interfaces;
using User.Domain.Entities;

namespace User.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private static readonly List<UserList> _users = new List<UserList>
        {
            new UserList("admin", "admin123", "Admin"),
            new UserList("user", "user123", "User")
        };

        public UserList GetUser(AuthenticationRequest request)
        {
            return _users.FirstOrDefault(user => user.UserName == request.userName && user.Password == request.password);
        }
    }
}
