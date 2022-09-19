using SEDC.Loto3000App.Domain.Models;
using SEDC.Loto3000App.Dtos.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.Loto3000App.Mappers
{
    public static class TicketMapper
    {
        public static TicketDto ToTicketDto (this Ticket ticket)
        {
            return new TicketDto
            {
                Id = ticket.Id,
                Number1 = ticket.Number1,
                Number2 = ticket.Number2,
                Number3 = ticket.Number3,
                Number4 = ticket.Number4,
                Number5 = ticket.Number5,
                Number6 = ticket.Number6,
                Number7 = ticket.Number7,
                Prize = ticket.Prize,
                SessionId = ticket.SessionId,
                UserDataDto = ticket.User.ToUserDataDto()
            };
        }
    }
}
