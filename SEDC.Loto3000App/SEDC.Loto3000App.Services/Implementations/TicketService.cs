using SEDC.Loto3000App.DataAccess.Interfaces;
using SEDC.Loto3000App.Domain.Models;
using SEDC.Loto3000App.Dtos.WinningTickets;
using SEDC.Loto3000App.Services.Interfaces;
using SEDC.Loto3000App.Shared.CustomExceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.Loto3000App.Services.Implementations
{
    public class TicketService : ITicketService
    {

        private ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public void AddTicket(List<int> ticketList, int userId, int sessionId)
        {
            List<int> noDuplicatesList = ticketList.Distinct().ToList();

            if(noDuplicatesList.Count != 7)
            {
                throw new TicketDataException("All numbers must be different");
            }

            noDuplicatesList.ForEach(number =>
            {
                if (number < 1 || number > 37)
                {
                    throw new TicketDataException("All numbers must be between 1 and 37");
                }
            });

            Ticket ticket = new Ticket()
            {
                Number1 = ticketList[0],
                Number2 = ticketList[1],
                Number3 = ticketList[2],
                Number4 = ticketList[3],
                Number5 = ticketList[4],
                Number6 = ticketList[5],
                Number7 = ticketList[6],
                Prize = "Idle",
                UserId = userId,
                SessionId = sessionId
            };

            _ticketRepository.Add(ticket);
        }

        public void CheckForWinners(int sessionId, WinningTicketDto winningTicketDto)
        {
            List<Ticket> tickets = GetBySession(sessionId);

            tickets.ForEach(ticket =>
            {
                GradeTicket(ticket, winningTicketDto);
            });
        }

        public void DeleteTicket(int id)
        {
            Ticket ticket = _ticketRepository.GetById(id);
            if(ticket == null)
            {
                throw new TicketNotFoundException("Ticket does not exist");
            }

            _ticketRepository.Delete(ticket);
        }

        public List<Ticket> GetAllTickets()
        {
            throw new NotImplementedException();
        }

        public Ticket GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Ticket> GetBySession(int id)
        {
            return _ticketRepository.GetBySessionId(id);
        }

        public void GradeTicket(Ticket ticket, WinningTicketDto winningTicketDto)
        {
            List<int> ticketNumbers = new List<int>()
            {
                ticket.Number1,
                ticket.Number2,
                ticket.Number3,
                ticket.Number4,
                ticket.Number5,
                ticket.Number6,
                ticket.Number7,
            };

            int counter = 0;

            ticketNumbers.ForEach(number =>
            {
                if(winningTicketDto.NumbersList.Contains(number))
                {
                    counter++;
                }
            });

            switch(counter)
            {
                case 7:
                    ticket.Prize = "7 ( JackPot ) - Car";
                    break;
                case 6:
                    ticket.Prize = "6 - Vacation";
                    break;
                case 5:
                    ticket.Prize = "5 - TV";
                    break;
                case 4:
                    ticket.Prize = "4 - 100$ Gift Card";
                    break;
                case 3:
                    ticket.Prize = "3 - 50$ Gift Card";
                    break;
                default:
                    ticket.Prize = "Nothing";
                    break;
            }

            _ticketRepository.Update(ticket);
        }

        public void UpdateTicket(int id)
        {
            throw new NotImplementedException();
        }
    }
}
