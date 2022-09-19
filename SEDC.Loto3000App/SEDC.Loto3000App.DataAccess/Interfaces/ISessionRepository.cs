using SEDC.Loto3000App.Domain.Models;

namespace SEDC.Loto3000App.DataAccess.Interfaces
{
    public interface ISessionRepository : IRepository<Session>
    {
        Session AddAndReturn(Session entity);

        Session GetByStatus();

        Session UpdateAndReturn(Session entity);
    }
}
