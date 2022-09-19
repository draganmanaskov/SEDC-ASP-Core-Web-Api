using SEDC.Loto3000App.Domain.Models;
using SEDC.Loto3000App.Dtos.WinningTickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.Loto3000App.Services.Interfaces
{
    public interface ITicketService
    {
        List<Ticket> GetAllTickets();
        List<Ticket> GetBySession(int id);
        Ticket GetById(int id);
        void AddTicket(List<int> ticketList, int UserId, int SessionId);
        void UpdateTicket(int id);
        void DeleteTicket(int id);
        void CheckForWinners(int sessionId, WinningTicketDto winningTicketDto);
    }
}
