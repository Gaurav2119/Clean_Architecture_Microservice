using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Application.Dtos
{
    public record AuthenticationResponse(string userName, string jwtToken, int expiresInMins);
}
