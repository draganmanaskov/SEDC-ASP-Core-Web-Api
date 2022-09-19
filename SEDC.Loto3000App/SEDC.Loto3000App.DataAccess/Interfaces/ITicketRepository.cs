using SEDC.Loto3000App.Domain.Models;


namespace SEDC.Loto3000App.DataAccess.Interfaces
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        List<Ticket> GetBySessionId(int id);
    }
}
