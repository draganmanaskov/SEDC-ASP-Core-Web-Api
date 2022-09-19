using SEDC.Loto3000App.Domain.Models;
using SEDC.Loto3000App.Dtos.Tickets;

namespace SEDC.Loto3000App.Dtos.Sessions
{
    public class SessionDto
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public bool Active { get; set; }
        public List<TicketDto> Tickets { get; set; }
        public WinningTicket WinningTicket { get; set; }
    }
}
