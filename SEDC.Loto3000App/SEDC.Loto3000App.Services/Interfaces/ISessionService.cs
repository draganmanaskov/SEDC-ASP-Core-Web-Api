using SEDC.Loto3000App.Domain.Models;
using SEDC.Loto3000App.Dtos.Sessions;
using SEDC.Loto3000App.Dtos.WinningTickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.Loto3000App.Services.Interfaces
{
    public interface ISessionService
    {
        List<SessionDto> GetAllSessions();
        Session GetById(int id);
        Session StartSession(WinningTicket entity);
        void EndSession(int id);
        WinningTicketDto EndSessionRigged(int id);
        void DeleteSession(int id);
        void CheckForActiveSession();
        Session GetActiveSession();
        Session StartSessionRigged();
    }
}
