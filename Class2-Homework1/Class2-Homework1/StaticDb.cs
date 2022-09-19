using Class2_Homework1.Models;

namespace Class2_Homework1
{
    public class StaticDb
    {
        public static int UserId = 3;
        public static List<User> Users = new List<User>(){
            new User{
                Id = 1,
                FirstName = "John",
                LastName = "Smith"
            },
            new User{
                Id = 2,
                FirstName = "Alex",
                LastName = "Park"
            },
            new User{
                Id = 3,
                FirstName = "Anna",
                LastName = "Walker"
            },
        };
    }
}
