using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Identity.API.DTOs;
using Identity.API.Entities;
using Identity.API.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;

namespace Identity.API.Data
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
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await _userRepository.DoesUserExist(registerDto.Email))
            {
                return BadRequest("User with same email already exists");
            }
            using var hmac = new HMACSHA512();

            var user = new User
            {
                UserName = registerDto.UserName,
                Email = registerDto.Email,
                FullName = registerDto.FullName,
                Phone = registerDto.Phone,
                UserType = registerDto.UserType == 1 ? UserType.JobSeeker : UserType.Employer,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            await _userRepository.Register(user);


            return CreateUserDtoWithToken(user);
        }
        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            if (!(await _userRepository.DoesUserExist(loginDto.Email)))
            {
                return Unauthorized("Invalid Credentials");
            }
            var user = await _userRepository.GetUserAsync(loginDto.Email);
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            if (computedHash.SequenceEqual(user.PasswordHash))
            {
                return CreateUserDtoWithToken(user);
            }
            return Unauthorized("Invalid Credentials");
        }
        [Authorize]
        [HttpPut("change-password")]
        public async Task<ActionResult<UserDto>> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            string email = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!(await _userRepository.DoesUserExist(email)))
            {
                return Unauthorized("Invalid Credentials");
            }
            var user = await _userRepository.GetUserAsync(email);

            // using var hmac = new HMACSHA512(user.PasswordSalt);
            // var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(changePasswordDto.NewPassword));

            // if (computedHash.SequenceEqual(user.PasswordHash))
            // {
            //     return BadRequest("Please Enter New password from the last time");
            // }

            await _userRepository.ChangePassword(user, changePasswordDto.NewPassword);
            return CreateUserDtoWithToken(user);
        }
        [Authorize]
        [HttpGet("authenticate")]
        public ActionResult Authenticate()
        {
            return Ok();
        }

        [Authorize]
        [HttpGet("get-role")]
        public async Task<ActionResult<int>> GetRole()
        {
            string email = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // HttpContext.User. gotta check if i can use claims
            if (!(await _userRepository.DoesUserExist(email)))
            {
                return Unauthorized("Invalid Credentials");
            }
            var user = await _userRepository.GetUserAsync(email);
            return user.UserType == UserType.JobSeeker ? 1 : 2;
        }
        public UserDto CreateUserDtoWithToken(User user)
        {
            return new UserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user)
            };
        }
    }
}