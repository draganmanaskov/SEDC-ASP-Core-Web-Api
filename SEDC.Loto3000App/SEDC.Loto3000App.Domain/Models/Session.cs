using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEDC.Loto3000App.Domain.Models
{
    public class Session
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
        public bool Active { get; set; }
        public virtual IList<Ticket> Tickets { get; set; }

        [ForeignKey(nameof(WinningTicketId))]
        public int WinningTicketId { get; set; }
        public WinningTicket WinningTicket { get; set; }

    }
}
