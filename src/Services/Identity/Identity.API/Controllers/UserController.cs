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

        // token service for returning token
        public UserController(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        // register a user in db
        [HttpPost("register")]
        public async Task<ActionResult<ResponseUserWithToken>> Register(RequestRegister requestRegister)
        {
            // if entry exists then return
            if (await _userRepository.DoesUserExist(requestRegister.Email))
                return BadRequest("User with same email already exists");

            // for password creation
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

        // returns an auth token 
        [HttpPost("login")]
        public async Task<ActionResult<ResponseUserWithToken>> Login(RequestLogin requestLogin)
        {
            // if entry doesnt exist then return
            if (!await _userRepository.DoesUserExist(requestLogin.Email)) return Unauthorized("Invalid Credentials");

            var user = await _userRepository.GetUserAsync(requestLogin.Email);

            // get the password salt to compare password
            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(requestLogin.Password));

            // compare password and return
            if (computedHash.SequenceEqual(user.PasswordHash)) return CreateUserDtoWithToken(user);
            return Unauthorized("Invalid Credentials");
        }

        // change password
        [Authorize]
        [HttpPut("change-password")]
        public async Task<ActionResult<ResponseUserWithToken>> ChangePassword(
            RequestChangePassword requestChangePassword)
        {
            // it takes email from token
            // as token is already specified, don't compare old password as auth is provided
            var email = User.FindFirstValue(ClaimTypes.NameIdentifier);
            // if entry with specified email doesnt exist then return
            if (!await _userRepository.DoesUserExist(email)) return Unauthorized("Invalid Credentials");
            var user = await _userRepository.GetUserAsync(email);


            await _userRepository.ChangePassword(user, requestChangePassword.NewPassword);
            return CreateUserDtoWithToken(user);
        }


        // deletes the entry of user
        // this end point isn't accessed directly it goes through aggregator
        // the reason is when a user is deleted from identity 
        // the counter parts in other db with same email should also be deleted
        [Authorize]
        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteUser(RequestDelete requestDelete)
        {
            await _userRepository.DeleteUser(requestDelete.email);
            return Ok();
        }

        // creates a token using jwt with token service for auth purposes
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