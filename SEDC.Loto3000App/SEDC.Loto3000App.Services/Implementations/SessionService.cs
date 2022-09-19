using SEDC.Loto3000App.DataAccess.Interfaces;
using SEDC.Loto3000App.Domain.Models;
using SEDC.Loto3000App.Dtos.Sessions;
using SEDC.Loto3000App.Dtos.WinningTickets;
using SEDC.Loto3000App.Mappers;
using SEDC.Loto3000App.Services.Interfaces;
using SEDC.Loto3000App.Shared.CustomExceptions;

namespace SEDC.Loto3000App.Services.Implementations
{
    public class SessionService : ISessionService
    {
        private ISessionRepository _sessionRepo;
        private IWinningTicketService _winningTicketService;
        public SessionService(ISessionRepository sessionRepo, IWinningTicketService winningTicketService)
        {
            _sessionRepo = sessionRepo;
            _winningTicketService = winningTicketService;
        }

        public void CheckForActiveSession()
        {
            var session = _sessionRepo.GetByStatus();
            if (session != null)
            {
                throw new SessionDataException("Only one Session can be Active at a time!");
            }
        }

        public Session GetActiveSession()
        {
            return _sessionRepo.GetByStatus();
        }

        public void DeleteSession(int id)
        {
            throw new NotImplementedException();
        }


        public void EndSession(int id)
        {
            Session session = GetById(id);
            if (session.Active == false)
            {
                throw new SessionDataException("Session is already over!");
            }

            session.End = DateTime.Now;
            session.Active = false;

            WinningTicket wTicket = _winningTicketService.UpdateWinningTicket(session.WinningTicketId);

            _sessionRepo.Update(session);

        }

        public WinningTicketDto EndSessionRigged(int id)
        {

            Session session = GetById(id);
            if (session.Active == false)
            {
                throw new SessionDataException("Session is already over!");
            }

            session.End = DateTime.Now;
            session.Active = false;

            var  sessionUpdated = _sessionRepo.UpdateAndReturn(session);

            return WinningTicketMapper.ToWinningTicketDto(_winningTicketService.GetById(sessionUpdated.WinningTicketId));
        }

        public List<SessionDto> GetAllSessions()
        {
            List<Session> sesions = _sessionRepo.GetAll();

            List<SessionDto> sessionDtos = new List<SessionDto>();

            sesions.ForEach(session =>
            {
                sessionDtos.Add(session.ToSessionDto()); 
            });

            return sessionDtos;
        }


        public Session GetById(int id)
        {
            Session session = _sessionRepo.GetById(id);
            if(session == null)
            {
                throw new SessionNotFoundException("Session does not exist!");
            }
            return session;
        }

        public Session StartSession(WinningTicket entity)
        {
            CheckForActiveSession();

            Session newSession = new Session()
            {
                Start = DateTime.Now,
                End = null,
                Active = true,
                WinningTicketId = entity.Id
            };

            return _sessionRepo.AddAndReturn(newSession);
        }

        public Session StartSessionRigged()
        {
            CheckForActiveSession();

            WinningTicket ticket = _winningTicketService.AddWinningTicket();
            WinningTicket wTicket = _winningTicketService.UpdateWinningTicket(ticket.Id);

            Session newSession = new Session()
            {
                Start = DateTime.Now,
                End = null,
                Active = true,
                WinningTicketId = ticket.Id
            };

            return _sessionRepo.AddAndReturn(newSession);
        }



    }
}
