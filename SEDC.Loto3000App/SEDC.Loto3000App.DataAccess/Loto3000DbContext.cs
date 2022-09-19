using Microsoft.EntityFrameworkCore;
using SEDC.Loto3000App.Domain.Enums;
using SEDC.Loto3000App.Domain.Models;
using System.Text;
using XSystem.Security.Cryptography;

namespace SEDC.Loto3000App.DataAccess
{
    public class Loto3000DbContext : DbContext
    {
        public Loto3000DbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<WinningTicket> WinningTickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
            byte[] passwordBytes = Encoding.ASCII.GetBytes("admin");
            byte[] hashBytes = mD5CryptoServiceProvider.ComputeHash(passwordBytes);
            string hash = Encoding.ASCII.GetString(hashBytes);
            //SEED...

            modelBuilder.Entity<User>()
               .HasData(
               new User()
                   {
                       Id = 1,
                       FirstName = "John",
                       LastName = "Doe",
                       Username = "Admin",
                       Password = hash,
                       Role = RoleEnum.Admin
                   }
               );
        }
    }
}
