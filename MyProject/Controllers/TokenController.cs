//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.IdentityModel.Tokens;
//using MyProject.Context;
//using MyProject.ViewModel;
//using System.IdentityModel.Tokens.Jwt;
//using System.Security.Claims;
//using System.Text;

//namespace MyProject.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class TokenController : ControllerBase
//    {
//        public IConfiguration _configuration;
//        private readonly MyContext _context;

//        public TokenController(IConfiguration config, MyContext context)
//        {
//            _configuration = config;
//            _context = context;
//        }

//        [HttpPost]
//        public async Task<IActionResult> Post(LoginVM loginVM)
//        {

//            if (loginVM != null && loginVM.Email != null && loginVM.Password != null)
//            {
//                var user = await loginreposiotry.login(_userData.Email, _userData.Password);

//                if (user != null)
//                {
//                    //create claims details based on the user information
//                    var claims = new[] {
//                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
//                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
//                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
//                    new Claim("Id", user.UserId.ToString()),
//                    new Claim("FirstName", user.FirstName),
//                    new Claim("LastName", user.LastName),
//                    new Claim("UserName", user.UserName),
//                    new Claim("Email", user.Email)
//                   };

//                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

//                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//                    var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims, expires: DateTime.UtcNow.AddDays(1), signingCredentials: signIn);

//                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
//                }
//                else
//                {
//                    return BadRequest("Invalid credentials");
//                }
//            }
//            else
//            {
//                return BadRequest();
//            }
//        }

//        private async Task<UserInfo> GetUser(string email, string password)
//        {
//            return await _context.UserInfo.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
//        }
//    }
//}
