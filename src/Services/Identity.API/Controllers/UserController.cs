

using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Identity.API.DTOs;
using Identity.API.Entities;
using Identity.API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Identity.API.Data
{
    [ApiController]
    [Route("/api/user")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;

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


            return new UserDto
            {
                UserName = user.UserName,
                Token = "still to create"
            };

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
                return new UserDto
                {
                    UserName = user.UserName,
                    Token = "Create token later"
                };
            }
            return Unauthorized("Invalid Credentials");
        }
        [HttpPost("change-password")]
        public async Task<ActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            var user = await _userRepository.GetUserAsync(changePasswordDto.Email);
            await _userRepository.ChangePassword(user, changePasswordDto.NewPassword);
            return Ok("Password changed");
        }
    }
}