using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SEDC.Loto3000App.Domain.Models;
using SEDC.Loto3000App.Dtos.Tickets;
using SEDC.Loto3000App.Services.Interfaces;
using SEDC.Loto3000App.Shared.CustomExceptions;
using System.Linq;
using System.Security.Claims;

namespace SEDC.Loto3000App.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private ITicketService _ticketService;
        private ISessionService _sessionService;
        private IUserService _userService;

        public TicketController(ITicketService ticketService, ISessionService sessionService, IUserService userService)
        {
            _ticketService = ticketService;
            _sessionService = sessionService;
            _userService = userService;
        }


        [HttpPost("createTicket")]
        public ActionResult CreateTicket([FromBody] CreateTicketDto createTicketDto)
        {
            try
            {
                var userName = User.FindFirstValue(ClaimTypes.Name);

                var user = _userService.GetUserByUsername(userName);

                var session = _sessionService.GetById(createTicketDto.SessionId);

                _ticketService.AddTicket(createTicketDto.Numbers, user.Id, session.Id);

                return StatusCode(StatusCodes.Status201Created, "Ticket Created");
            }
            catch (UserNotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (SessionNotFoundException e)
            {
                return StatusCode(StatusCodes.Status404NotFound, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred, contact the admin!");
            }
          
        }
    }
}
