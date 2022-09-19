using SEDC.Loto3000App.Domain.Models;


namespace SEDC.Loto3000App.DataAccess.Interfaces
{
    public interface IWinningTicketRepository : IRepository<WinningTicket>
    {
        WinningTicket AddAndReturn(WinningTicket entity);
    }
}
