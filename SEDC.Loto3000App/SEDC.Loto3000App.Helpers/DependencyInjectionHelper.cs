using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SEDC.Loto3000App.DataAccess;
using SEDC.Loto3000App.DataAccess.Implementations;
using SEDC.Loto3000App.DataAccess.Interfaces;
using SEDC.Loto3000App.Services.Implementations;
using SEDC.Loto3000App.Services.Interfaces;

namespace SEDC.Loto3000App.Helpers
{
    public class DependencyInjectionHelper
    {
        public static void InjectDbContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<Loto3000DbContext>(options => 
                options.UseSqlServer(connectionString)
            );
        }

        public static void InjectRepositories(IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ITicketRepository, TicketRepository>();
            services.AddTransient<ISessionRepository, SessionRepository>();
            services.AddTransient<IWinningTicketRepository, WinningTicketRepository>();
        }

        public static void InjectServices(IServiceCollection services)
        {
            services.AddTransient<IWinningTicketService, WinningTicketService>();
            services.AddTransient<ISessionService, SessionService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITicketService, TicketService>();
        }
    }
}
