using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SEDC.Loto3000App.Domain.Models
{
    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Number1 { get; set; }
        public int Number2 { get; set; }
        public int Number3 { get; set; }
        public int Number4 { get; set; }
        public int Number5 { get; set; }
        public int Number6 { get; set; }
        public int Number7 { get; set; }
        public string Prize { get; set; }

        [ForeignKey(nameof(UserId))]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey(nameof(SessionId))]
        public int SessionId { get; set; }
        public virtual Session Session { get; set; }

    }
}
