using SEDC.Loto3000App.Domain.Models;
using SEDC.Loto3000App.Dtos.WinningTickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDC.Loto3000App.Mappers
{ 
    public static class WinningTicketMapper
    {
        public static WinningTicket ToWinningTicket(List<int> list)
        {
            return new WinningTicket
            {
                Number1 = list[0],
                Number2 = list[1],
                Number3 = list[2],
                Number4 = list[3],
                Number5 = list[4],
                Number6 = list[5],
                Number7 = list[6],
                Number8 = list[7],
            };
        }


        public static WinningTicketDto ToWinningTicketDto(WinningTicket winningTicket)
        {
            List<int> numbers = new List<int>();
            numbers.Add(winningTicket.Number1);
            numbers.Add(winningTicket.Number2);
            numbers.Add(winningTicket.Number3);
            numbers.Add(winningTicket.Number4);
            numbers.Add(winningTicket.Number5);
            numbers.Add(winningTicket.Number6);
            numbers.Add(winningTicket.Number7);
            numbers.Add(winningTicket.Number8);


            return new WinningTicketDto
            {
                Id = winningTicket.Id,
                NumbersList = numbers

            };
        }
    }
}
