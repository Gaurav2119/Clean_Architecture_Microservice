using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using User.Application.Dtos;
using User.Domain.Entities;

namespace User.Application.Interfaces
{
    public interface IUserService
    {
        UserList Get(AuthenticationRequest request);
    }
}
