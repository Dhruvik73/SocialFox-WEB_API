using Microsoft.Extensions.DependencyInjection;
using Repositories.Implementation;
using Repositories.Interfaces;
using Services.Implementation;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace socialFox.AddRepositories
{
    public static class AddRepositories
    {
        public static IServiceCollection AddProjectRepositories(this IServiceCollection services)
        {
            return services.AddTransient<IAuthentication,Authentication>()
                           .AddTransient(typeof(IRepository<>),typeof(Repository<>))
                           .AddTransient(typeof(IService<>), typeof(Service<>))
                           .AddTransient<IAuthenticationService,AuthenticationService>()
                           .AddTransient<IUserRepository,UserRepository>()
                           .AddTransient<IUserServices,UserServices>();
        }
    }
}
