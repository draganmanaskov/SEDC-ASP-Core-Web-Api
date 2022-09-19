using SEDC.Loto3000App.DataAccess.Interfaces;
using SEDC.Loto3000App.Domain.Models;
using SEDC.Loto3000App.Dtos.WinningTickets;

namespace SEDC.Loto3000App.DataAccess.Implementations
{
    public class TicketRepository : ITicketRepository
    {

        private Loto3000DbContext _context;

        public TicketRepository(Loto3000DbContext context)
        {
            _context = context;
        }

        public void Add(Ticket entity)
        {
            _context.Tickets.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(Ticket entity)
        {
            _context.Tickets.Remove(entity);
            _context.SaveChanges();
        }

        public List<Ticket> GetAll()
        {
            return _context.Tickets.ToList();
        }

        public Ticket GetById(int id)
        {
            return _context.Tickets.FirstOrDefault(ticket => ticket.Id == id);
        }

        public List<Ticket> GetBySessionId(int id)
        {
            return _context.Tickets.Where(ticket => ticket.SessionId == id).ToList();
        }

        public void Update(Ticket entity)
        {
            _context.Tickets.Update(entity);
            _context.SaveChanges();
        }

        
    }
}
