using Data.Context;
using Data.Repositories;
using Domain.HttpService.Interfaces;
using Domain.HttpService;
using Domain.OrganizationNS.Interfaces;
using Domain.OrganizationNS.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.ProjectNS.Interfaces;
using Domain.ProjectNS.Services;

namespace IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<OrganizationDBContext>(option => option.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            services.AddScoped<IOrganizationService, OrganizationService>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IProjectService, ProjectServices>();
            services.AddTransient<IMatchHttpService, MatchHttpService>();
            return services;
        }
    }
}
