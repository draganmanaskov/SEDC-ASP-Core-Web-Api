﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEDC.Loto3000App.Dtos.Users;
using SEDC.Loto3000App.Services.Interfaces;
using SEDC.Loto3000App.Shared.CustomExceptions;

namespace SEDC.Loto3000App.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        [AllowAnonymous] 
        [HttpPost("register")]
        public ActionResult<GetUserDto> Register([FromBody] RegisterUserDto registerUserDto)
        {
            try
            {            
                var userDto = _userService.RegisterUser(registerUserDto);

                return StatusCode(StatusCodes.Status201Created, userDto);
            }
            catch (UserDataException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }

        [AllowAnonymous] //no token needed (we can not be logged in before login)
        [HttpPost("login")]
        public ActionResult<GetUserDto> LoginUser([FromBody] LoginUserDto loginDto)
        {
            try
            {
                var userDto = _userService.LoginUser(loginDto);
                return Ok(userDto);
            }
            catch (UserDataException e)
            {
                return BadRequest(e.Message);
            }
            catch (UserNotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred!");
            }
        }

    }
}
