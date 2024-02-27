using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Application.Dtos;
using User.Application.Interfaces;
using User.Domain.Entities;

namespace User.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public UserList Get(AuthenticationRequest request)
        {
            return _userRepository.GetUser(request);
        }
    }
}
