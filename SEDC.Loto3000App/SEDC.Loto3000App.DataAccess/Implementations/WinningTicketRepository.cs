using SEDC.Loto3000App.DataAccess.Interfaces;
using SEDC.Loto3000App.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.Loto3000App.DataAccess.Implementations
{
    public class WinningTicketRepository : IWinningTicketRepository
    {
        private Loto3000DbContext _context;
        public WinningTicketRepository(Loto3000DbContext context)
        {
            _context = context;
        }
        public void Add(WinningTicket entity)
        {
            _context.WinningTickets.Add(entity);
            _context.SaveChanges();
        }

        public WinningTicket AddAndReturn(WinningTicket entity)
        {
            _context.WinningTickets.Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public void Delete(WinningTicket entity)
        {
            _context.WinningTickets.Remove(entity);
            _context.SaveChanges();
        }

        public List<WinningTicket> GetAll()
        {
            return _context.WinningTickets.ToList();
        }

        public WinningTicket GetById(int id)
        {
            return _context.WinningTickets.FirstOrDefault(x => x.Id == id);
        }

        public void Update(WinningTicket entity)
        {
            _context.WinningTickets.Update(entity);
            _context.SaveChanges();
        }
    }
}
