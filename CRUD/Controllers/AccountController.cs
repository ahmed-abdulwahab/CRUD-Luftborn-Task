using BAL.DTOs;
using BAL.Repository;
using BAL.UnitOfWork;
using DAL.Context;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<AppUser> _userRepository;
        private readonly IConfiguration _config;

        public AccountController(
            IUnitOfWork unitOfWork,
            IRepository<AppUser> userRepository,
            IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _config = config;
        }
        [HttpOptions]
        [AllowAnonymous]
        public IActionResult HandlePreflight()
        {
            return NoContent();
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = new AppUser
            {
                UserName = registerDto.Username.ToLower(),
                KnownAs = registerDto.Username,
                Gender = "NotSpecified",
                City = "Unknown",
                Country = "Unknown",
                DateOfBirth = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-18)),
                Created = DateTime.UtcNow,
                LastActive = DateTime.UtcNow
            };
            using var hmac = new HMACSHA512();
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));
            user.PasswordSalt = hmac.Key;


            await _userRepository.AddAsync(user);
            await _unitOfWork.CommitAsync();

            var userDto = new UserDto
            {
                Id = user.Id,
                Username = user.UserName,
                Token = CreateToken(user)
            };
            
            return userDto;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userRepository.FindAsync(u => u.UserName == loginDto.Username);
            if (user == null) return Unauthorized("Invalid username");

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid password");
            }
            var userDto = new UserDto
            {
                Id = user.Id,
                Username = user.UserName,
                Token = CreateToken(user)
            };
            if (user.UserName == "admin")
                userDto.Role = user.UserName;
            return userDto;
        }



        public string CreateToken(AppUser user)
        {
            var tokenKey = _config["TokenKey"] ?? throw new Exception("Cannot access tokenKey from appsettings");
            if (tokenKey.Length < 64) throw new Exception("Your tokenKey needs to be longer");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey));


            var claims = new List<Claim>
            {
                new (ClaimTypes.NameIdentifier, user.UserName)
            };

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };

            var tokenHnadler = new JwtSecurityTokenHandler();
            var token = tokenHnadler.CreateToken(tokenDescriptor);


            return tokenHnadler.WriteToken(token);
        }

    }
}
