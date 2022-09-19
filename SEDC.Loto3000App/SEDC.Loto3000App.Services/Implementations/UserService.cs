using Lucene.Net.Support;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SEDC.Loto3000App.DataAccess.Interfaces;
using SEDC.Loto3000App.Domain.Enums;
using SEDC.Loto3000App.Domain.Models;
using SEDC.Loto3000App.Dtos.Users;
using SEDC.Loto3000App.Services.Interfaces;
using SEDC.Loto3000App.Shared.CustomExceptions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using XSystem.Security.Cryptography;

namespace SEDC.Loto3000App.Services.Implementations
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;

        }

        public GetUserDto LoginUser(LoginUserDto loginDto)
        {
            if (string.IsNullOrEmpty(loginDto.Username) || string.IsNullOrEmpty(loginDto.Password))
            {
                throw new UserDataException("Username and password are required fields!");
            }

            string hash = HashPassword(loginDto.Password);

            User user = _userRepository.LoginUser(loginDto.Username, hash);
            if (user == null)
            {
                throw new UserNotFoundException("User not found");
            }

            GetUserDto getUserDto = new GetUserDto()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Role = user.Role,
                Token = GenerateJWT(user),
            };

            return getUserDto;
        }

        public GetUserDto RegisterUser(RegisterUserDto registerUserDto)
        {
            ValidateUser(registerUserDto);

            string hash = HashPassword(registerUserDto.Password);

            User user = new User
            {
                FirstName = registerUserDto.FirstName,
                LastName = registerUserDto.LastName,
                Username = registerUserDto.Username,
                Password = hash,
                Role = RoleEnum.User
            };
            _userRepository.Add(user);

            GetUserDto getUserDto = new GetUserDto()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
                Role = user.Role,
                Token = GenerateJWT(user),
            };

            return getUserDto;
        }

        private void ValidateUser(RegisterUserDto registerUserDto)
        {
            if (string.IsNullOrEmpty(registerUserDto.Username) || string.IsNullOrEmpty(registerUserDto.Password) || string.IsNullOrEmpty(registerUserDto.ConfirmedPassword))
            {
                throw new UserDataException("Username and password are required fields!");
            }
            if (registerUserDto.Username.Length > 30)
            {
                throw new UserDataException("Username: Maximum length for username is 30 characters");
            }
            if (!string.IsNullOrEmpty(registerUserDto.FirstName) && registerUserDto.FirstName.Length > 50)
            {
                throw new UserDataException("Maximum length for FirstName is 50 characters");
            }
            if (!string.IsNullOrEmpty(registerUserDto.LastName) && registerUserDto.LastName.Length > 50)
            {
                throw new UserDataException("Maximum length for LastName is 50 characters");
            }
            if (registerUserDto.Password != registerUserDto.ConfirmedPassword)
            {
                throw new UserDataException("Passwords must match!");
            }

            var userDb = _userRepository.GetUserByUsername(registerUserDto.Username);
            if (userDb != null)
            {
                throw new UserDataException($"The username {registerUserDto.Username} is already in use!");
            }
        }

        public User GetUserByUsername(string username)
        {
            var user = _userRepository.GetUserByUsername(username);
            if (user == null)
            {
                throw new UserNotFoundException($"A user with USername:{username} does not exist");
            }

            return user;
        }

        private string HashPassword(string password)
        {
            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            byte[] passwordBytes = Encoding.ASCII.GetBytes(password);
            byte[] hashBytes = mD5CryptoServiceProvider.ComputeHash(passwordBytes);
            return Encoding.ASCII.GetString(hashBytes);
        }

        private string GenerateJWT(User user)
        {
            JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            byte[] secretKeyBytes = Encoding.ASCII.GetBytes(_configuration["AppSettings:SecretKey"]);

          

            SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes),
                    SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(
                    new[]
                   {
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim("userFullName", $"{user.FirstName} {user.LastName}"),
                        new Claim(ClaimTypes.Role, user.Role == RoleEnum.User ? "User" : "Admin")
                    }
                )
            };

            SecurityToken token = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            return jwtSecurityTokenHandler.WriteToken(token);
        }


    }
}
