using SEDC.Loto3000App.DataAccess.Interfaces;
using SEDC.Loto3000App.Domain.Models;
using SEDC.Loto3000App.Dtos.WinningTickets;
using SEDC.Loto3000App.Mappers;
using SEDC.Loto3000App.Services.Interfaces;
using SEDC.Loto3000App.Shared.CustomExceptions;

namespace SEDC.Loto3000App.Services.Implementations
{
    public class WinningTicketService : IWinningTicketService
    {
        private IWinningTicketRepository _repo;
        public WinningTicketService(IWinningTicketRepository repo)
        {
            _repo = repo;
        }
        public WinningTicket AddWinningTicket()
        {
            WinningTicket wTicket = new WinningTicket();

            return _repo.AddAndReturn(wTicket);
        }

        public void DeleteWinningTicket(int id)
        {
            throw new NotImplementedException();
        }

        public List<int> GenerateWinningTicket()
        {
            Random rnd = new Random();
            List<int> list = new List<int>();

            for (int j = 0; j < 8; j++)
            {
                int random = rnd.Next(1, 38);
                if (!list.Contains(random))
                {
                    list.Add(random);
                }
                else
                {
                    j--;
                }
            }

            return list;
        }

        public List<WinningTicket> GetAllWinningTickets()
        {
            throw new NotImplementedException();
        }

        public WinningTicket GetById(int id)
        {
            var wTicket = _repo.GetById(id);
            if(wTicket == null)
            {
                throw new WinningTicketNotFoundException($"No winning ticket with id {id}");
            }

            return wTicket;
        }

        public WinningTicket UpdateWinningTicket(int id)
        {
            WinningTicket ticket = _repo.GetById(id);

            List<int> updatetTicket = GenerateWinningTicket();

            ticket.Number1 = updatetTicket[0];
            ticket.Number2 = updatetTicket[1];
            ticket.Number3 = updatetTicket[2];
            ticket.Number4 = updatetTicket[3];
            ticket.Number5 = updatetTicket[4];
            ticket.Number6 = updatetTicket[5]; 
            ticket.Number7 = updatetTicket[6];
            ticket.Number8 = updatetTicket[7];

            _repo.Update(ticket);

            return ticket;

        }

    }
}
