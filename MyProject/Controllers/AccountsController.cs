using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyProject.Models;
using MyProject.Repository;
using MyProject.ViewModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace MyProject.Controllers
{
    [Route("api/[controller]"), Authorize]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly AccountRepository accountRepository;
        private readonly IConfiguration _configuration;

        public AccountsController(AccountRepository accountRepository, IConfiguration configuration)
        {
            this.accountRepository = accountRepository;
            _configuration = configuration;
        }

        [HttpGet, Authorize]
        public ActionResult Get()
        {
            var get = accountRepository.Get();

            if (get.Count() == 0)
            {
                return StatusCode(204, new { status = HttpStatusCode.NoContent, message = "Data Not Found!", Data = get });
            }
            return StatusCode(200, new { status = HttpStatusCode.OK, message = "Data Found!", Data = get });
        }
        //[HttpGet("Login")]
        //public ActionResult Login(string email, string password)
        //{
        //    var getDataAcc = accountRepository.Login(email);

        //    if (getDataAcc == null)
        //    {
        //        return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Email is not valid!", Data = getDataAcc });
        //    } else if (!BCrypt.Net.BCrypt.Verify(password, getDataAcc.Password))
        //    {
        //        return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Password is not valid!", Data = getDataAcc });
        //    }
        //    return StatusCode(200, new { status = HttpStatusCode.OK, message = "Login Success!", Data = getDataAcc });
        //}

        [HttpPost("/Login"), AllowAnonymous]
        public ActionResult Login(LoginVM loginVM)
        {

            var account = accountRepository.Login(loginVM);

            //if (account != null && BCrypt.Net.BCrypt.Verify(loginVM.Password, account.Password))
            //{
            //    return account;
            //}

            if (account == null)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = $"Email:'{loginVM.Email}' is not valid!" });
            }
            else if (!BCrypt.Net.BCrypt.Verify(loginVM.Password, account.Password))
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Password is not valid!" });
            }
            var token = CreateToken(loginVM);
            return StatusCode(200, new { status = HttpStatusCode.OK, message = "Login Success!", Token = token });
        }
        //jwt create token
        private string CreateToken(LoginVM loginVM)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, loginVM.Email),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("JwtConfig:Key").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: cred);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
    }
}
