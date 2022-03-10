using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Identity.BusinessLogic.Interfaces;
using Identity.Infrastructure.Models;
using Identity.Infrastructure.RequestResponseModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers
{
    [ApiController]
    [Route("/api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public UserController(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ResponseUserWithToken>> Register(RequestRegister requestRegister)
        {
            if (await _userRepository.DoesUserExist(requestRegister.Email))
                return BadRequest("User with same email already exists");
            using var hmac = new HMACSHA512();

            var user = new User
            {
                UserName = requestRegister.UserName,
                Email = requestRegister.Email,
                FullName = requestRegister.FullName,
                Phone = requestRegister.Phone,
                UserType = (UserType) requestRegister.UserType,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(requestRegister.Password)),
                PasswordSalt = hmac.Key
            };

            await _userRepository.Register(user);


            return CreateUserDtoWithToken(user);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ResponseUserWithToken>> Login(RequestLogin requestLogin)
        {
            if (!await _userRepository.DoesUserExist(requestLogin.Email)) return Unauthorized("Invalid Credentials");
            var user = await _userRepository.GetUserAsync(requestLogin.Email);
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(requestLogin.Password));
            if (computedHash.SequenceEqual(user.PasswordHash)) return CreateUserDtoWithToken(user);
            return Unauthorized("Invalid Credentials");
        }

        [Authorize]
        [HttpPut("change-password")]
        public async Task<ActionResult<ResponseUserWithToken>> ChangePassword(RequestChangePassword requestChangePassword)
        {
            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!await _userRepository.DoesUserExist(email)) return Unauthorized("Invalid Credentials");
            var user = await _userRepository.GetUserAsync(email);

            // using var hmac = new HMACSHA512(user.PasswordSalt);
            // var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(changePasswordDto.NewPassword));

            // if (computedHash.SequenceEqual(user.PasswordHash))
            // {
            //     return BadRequest("Please Enter New password from the last time");
            // }

            await _userRepository.ChangePassword(user, requestChangePassword.NewPassword);
            return CreateUserDtoWithToken(user);
        }


        // [Authorize]
        // [HttpGet("get")]
        // public async Task<ActionResult<>> GetRole()
        // {
        //     // job seeker is 0 and employer is 1
        //     var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //     // HttpContext.User. gotta check if i can use claims
        //     if (!await _userRepository.DoesUserExist(email)) return Unauthorized("Invalid Credentials");
        //     var user = await _userRepository.GetUserAsync(email);
        //     return (int) user.UserType;
        // }

        [ApiExplorerSettings(IgnoreApi = true)]
        public ResponseUserWithToken CreateUserDtoWithToken(User user)
        {
            return new ResponseUserWithToken
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            };
        }
    }
}