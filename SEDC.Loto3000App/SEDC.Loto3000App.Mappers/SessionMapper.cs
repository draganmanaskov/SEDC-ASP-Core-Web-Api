using SEDC.Loto3000App.Domain.Models;
using SEDC.Loto3000App.Dtos.Sessions;
using SEDC.Loto3000App.Dtos.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.Loto3000App.Mappers
{
    public static class SessionMapper
    {
        public static SessionDto ToSessionDto (this Session session)
        {
            List <TicketDto> ticketsDto = new List<TicketDto>();

            List<Ticket> list = session.Tickets.ToList();
            list.ForEach(ticket =>
            {
                ticketsDto.Add(ticket.ToTicketDto());
            });

            return new SessionDto
            {
                Id = session.Id,
                Active = session.Active,
                Start = session.Start,
                End = session.End,
                WinningTicket = session.WinningTicket,
                Tickets = ticketsDto,
            };
        }

    }
}
