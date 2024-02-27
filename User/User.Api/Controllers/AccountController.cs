using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using User.Application.Dtos;
using User.Application.Interfaces;

namespace User.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private const string Jwt_Security_Key = "averysecretkeygeneratedformicroserviceauth";
        private const int Jwt_Token_Validity_Mins = 20;
        public AccountController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public ActionResult<AuthenticationResponse> Authenticate(AuthenticationRequest request)
        {
            var response = GenerateJwtToken(request);
            if (response == null) return Unauthorized();
            return response;
        }

        private AuthenticationResponse GenerateJwtToken(AuthenticationRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.userName) || string.IsNullOrWhiteSpace(request.password))
                return null;

            // Validation
            var userAccount = _userService.Get(request);
            if (userAccount == null) return null;

            var tokenExpiryTimeStamp = DateTime.Now.AddMinutes(Jwt_Token_Validity_Mins);
            var tokenKey = Encoding.ASCII.GetBytes(Jwt_Security_Key);

            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name, request.userName),
                new Claim(ClaimTypes.Role, userAccount.Role)
                //new Claim("Role", userAccount.Role)
            });

            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256Signature);

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = signingCredentials
            };

            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);

            return new AuthenticationResponse(userAccount.UserName, token, (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds);
        }
    }
}
