using Loto3000App.DataAccess;
using Loto3000App.DataAccess.Implementations;
using Loto3000App.DataAccess.Interfaces;
using Loto3000App.Domain.Models;
using Loto3000App.Models.Draw;
using Loto3000App.Models.Prize;
using Loto3000App.Models.Session;
using Loto3000App.Models.Ticket;
using Loto3000App.Models.UserEntity;
using Loto3000App.Services.Implementations;
using Loto3000App.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loto3000App.Helper
{
    public static class DependencyInjectionHelper
    {

        public static void InjectDbContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<Loto3000AppDbContext>(x =>
                x.UseSqlServer(connectionString));
        }

        public static void InjectRepository(IServiceCollection services)
        {
            
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IUserRoleRepository, UserRoleRepository>();

            services.AddTransient<ISessionRepository, SessionRepository>();

            services.AddTransient<ITicketRepository, TicketRepository>();
            services.AddTransient<ITicketDetailsRepository, TicketDetailsRepository>();

            services.AddTransient<IDrawRepository, DrawRepository>();
            services.AddTransient<IDrawDetailsRepository, DrawDetailsRepository>();

            services.AddTransient<IWinningRepository, WinningRepository>();
            services.AddTransient<IPrizeRepository, PrizeRepository>();




        }


        public static void InjectServices(IServiceCollection services)
        {
            services.AddTransient<IService<UserRegisterModel>, UserService>();
            services.AddTransient<ILoginService, LoginService>();
            services.AddTransient<IRegisterService, RegisterService>();

            services.AddTransient<ISessionService, SessionService>();
            services.AddTransient<ITicketService, TicketService>();
            services.AddTransient<IDrawService, DrawService>();
            
            services.AddTransient<IWinningService, WinningService>();
            services.AddTransient<IService<PrizeModel>, PrizeService>();




        }






    }
}
