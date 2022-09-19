using SEDC.Loto3000App.Domain.Models;
using SEDC.Loto3000App.Dtos.Users;

namespace SEDC.Loto3000App.Mappers
{
    public static class UserMapper
    {
        public static UserDataDto ToUserDataDto(this User user)
        {
            return new UserDataDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Username = user.Username,
            };
        }
    }
}
