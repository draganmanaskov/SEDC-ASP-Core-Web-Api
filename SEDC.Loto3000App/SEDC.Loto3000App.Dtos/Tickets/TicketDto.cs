using SEDC.Loto3000App.Domain.Models;
using SEDC.Loto3000App.Dtos.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.Loto3000App.Dtos.Tickets
{
    public class TicketDto
    {
        public int Id { get; set; }
        public int Number1 { get; set; }
        public int Number2 { get; set; }
        public int Number3 { get; set; }
        public int Number4 { get; set; }
        public int Number5 { get; set; }
        public int Number6 { get; set; }
        public int Number7 { get; set; }
        public string Prize { get; set; }
        public UserDataDto UserDataDto { get; set; }
        public int SessionId { get; set; }

    }
}
