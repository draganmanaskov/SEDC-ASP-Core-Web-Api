using Class2_Homework1.Controllers;
using Class2_Homework1.Models;
using Class2_Homework1.DTOs;

namespace Class2_Homework1.Mappers
{
    public static class UserDtoMapper
    {
        public static UserDto ToUserDto (this User user)
        {
            return new UserDto
            {
                FullName = $"{user.FirstName} {user.LastName}",
            };
        }

    }
}
