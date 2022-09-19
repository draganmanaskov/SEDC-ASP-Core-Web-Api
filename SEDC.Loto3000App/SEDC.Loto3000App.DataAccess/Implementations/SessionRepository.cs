using Microsoft.EntityFrameworkCore;
using SEDC.Loto3000App.DataAccess.Interfaces;
using SEDC.Loto3000App.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.Loto3000App.DataAccess.Implementations
{
    public class SessionRepository : ISessionRepository
    {

        private Loto3000DbContext _context;
        public SessionRepository(Loto3000DbContext context)
        {
            _context = context;
        }
        public void Add(Session entity)
        {
            _context.Sessions.Add(entity);
            _context.SaveChanges();
        }

        public Session AddAndReturn(Session entity)
        {
            _context.Sessions.Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public void Delete(Session entity)
        {
            _context.Sessions.Remove(entity);
            _context.SaveChanges();
        }

        public List<Session> GetAll()
        {
            return _context.Sessions.Include(x => x.Tickets.Where(ticket => ticket.Prize != "Nothing" && ticket.Prize != "Idle"))
                                    .ThenInclude(y => y.User)
                                    .Include(x => x.WinningTicket)
                                    .OrderByDescending(x => x.Id).ToList();
        }

        public Session GetById(int id)
        {
            return _context.Sessions.FirstOrDefault(session => session.Id == id);
        }

        public void Update(Session entity)
        {
            _context.Sessions.Update(entity);
            _context.SaveChanges();
        }

        public Session UpdateAndReturn(Session entity)
        {
            _context.Sessions.Update(entity);
            _context.SaveChanges();

            return _context.Sessions.FirstOrDefault(x => x.Id == entity.Id);
        }

        public Session GetByStatus()
        {
            List<Session> session = _context.Sessions.Where(session => session.Active).Include(x => x.WinningTicket).ToList();

            return session.FirstOrDefault();
        }

  
    }
}
