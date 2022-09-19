using SEDC.Loto3000App.Domain.Models;
using SEDC.Loto3000App.Dtos.WinningTickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.Loto3000App.Services.Interfaces
{
    public interface IWinningTicketService
    {
        List<WinningTicket> GetAllWinningTickets();
        WinningTicket GetById(int id);
        WinningTicket AddWinningTicket();
        WinningTicket UpdateWinningTicket(int id);
        void DeleteWinningTicket(int id);
        List<int> GenerateWinningTicket();
    }
}
