using SEDC.Loto3000App.Domain.Models;
using SEDC.Loto3000App.Dtos.Users;

namespace SEDC.Loto3000App.Services.Interfaces
{
    public interface IUserService
    {
        GetUserDto RegisterUser(RegisterUserDto registerUserDto);
        GetUserDto LoginUser(LoginUserDto loginDto);
        User GetUserByUsername(string username);
        
        }
}
