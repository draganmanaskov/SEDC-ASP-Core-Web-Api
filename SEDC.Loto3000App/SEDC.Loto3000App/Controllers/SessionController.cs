using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEDC.Loto3000App.Domain.Models;
using SEDC.Loto3000App.Dtos.Sessions;
using SEDC.Loto3000App.Dtos.WinningTickets;
using SEDC.Loto3000App.Mappers;
using SEDC.Loto3000App.Services.Interfaces;
using SEDC.Loto3000App.Shared.CustomExceptions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SEDC.Loto3000App.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SessionController : ControllerBase
    {
        private IWinningTicketService _winningTicketService;
        private ISessionService _sessionService;
        private ITicketService _ticketService;

        public SessionController(IWinningTicketService winnigTicketService, ISessionService sessionService, ITicketService ticketService)
        {
            _winningTicketService = winnigTicketService;
            _sessionService = sessionService;
            _ticketService = ticketService;
        }


        [HttpPost("startSession"), Authorize(Roles = "Admin")]
        public ActionResult<Session> StartSession()
        {
            try
            {
                WinningTicket ticket = _winningTicketService.AddWinningTicket();
                var session = _sessionService.StartSession(ticket);

                return Ok(session);
            }
            catch(SessionDataException e)
            {
                return StatusCode(StatusCodes.Status403Forbidden, e.Message);
            }
            catch (Exception e)
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred, contact the admin!");
            }
        }

        [HttpPost("startSessionRigged"), Authorize(Roles = "Admin")]
        public ActionResult<Session> StartSessionRigged()
        {
            try
            {        
                var session = _sessionService.StartSessionRigged();

                return Ok(session);
            }
            catch (SessionDataException e)
            {
                return StatusCode(StatusCodes.Status403Forbidden, e.Message);
            }
            catch (Exception e)
            {
                //log
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred, contact the admin!");
            }
        }

        [HttpPut("endSession/{id}"), Authorize(Roles = "Admin")]
        public ActionResult<Session> EndSession(int id)
        {
            try
            {
                //End Session
                _sessionService.EndSession(id);

                //Automatically start next session
                WinningTicket ticket = _winningTicketService.AddWinningTicket();
              

                return Ok(_sessionService.StartSession(ticket));
            }
            catch(SessionDataException e)
            {
                return BadRequest(e.Message);
            }
            catch (SessionNotFoundException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred, contact the admin!");
            }
        }


        [HttpPut("endSessionRigged/{id}"), Authorize(Roles = "Admin")]
        public ActionResult<Session> EndSessionRigged(int id)
        {
            try
            {
                //End Session
                var winningTicketDto = _sessionService.EndSessionRigged(id);

                _ticketService.CheckForWinners(id, winningTicketDto);

                return Ok(_sessionService.StartSessionRigged());
            }
            catch (SessionDataException e)
            {
                return BadRequest(e.Message);
            }
            catch (SessionNotFoundException e)
            {
                return BadRequest(e.Message);
            }
            catch (WinningTicketNotFoundException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred, contact the admin!");
            }
        }

        [HttpGet("getActiveSession")]
        public ActionResult<Session> GetActiveSession()
        {
            try
            {
                return Ok(_sessionService.GetActiveSession());
            }
            catch (SessionNotFoundException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred, contact the admin!");
            }
        }

        [AllowAnonymous]
        [HttpGet("getAllSessions")]
        public ActionResult<List<SessionDto>> GetAllSessions()
        {
            try
            {
                return Ok(_sessionService.GetAllSessions());
            }
            catch (SessionNotFoundException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred, contact the admin!");
            }
        }

    }
}
