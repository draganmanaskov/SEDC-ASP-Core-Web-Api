using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Class2_Homework1.Models;
using Class2_Homework1.Mappers;
using Class2_Homework1.DTOs;

namespace Class2_Homework1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            try
            {
                var userDb = StaticDb.Users;
                return Ok(userDb.Select(user => user.ToUserDto()));

            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

        [HttpGet("{id}")] //api/user/id
        public ActionResult<UserDto> Get(int id)
        {
            if(id <= 0)
            {
                return StatusCode(StatusCodes.Status400BadRequest, "Only a positive id is valid!");
            }

            var userDb = StaticDb.Users.FirstOrDefault(user => user.Id == id);
            if(userDb == null)
            {
                return NotFound("User does not exist");
            }

            return Ok(userDb.ToUserDto());
        }

        [HttpPost("createUser")]
        public IActionResult CreateUser(AddUserDto userDto)
        {

            try
            {
                if (string.IsNullOrEmpty(userDto.FirstName))
                {
                    return BadRequest("First Name must not be empty");
                }

                if (string.IsNullOrEmpty(userDto.LastName))
                {
                    return BadRequest("Last Name must not be empty");
                }

                User user = new User()
                {
                    FirstName = userDto.FirstName,
                    LastName = userDto.LastName,
                };
                user.Id = ++StaticDb.UserId;
                StaticDb.Users.Add(user);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception e)
            {
                //log error
                return StatusCode(StatusCodes.Status500InternalServerError, "Server error occured");
            }
        }

    }
}
