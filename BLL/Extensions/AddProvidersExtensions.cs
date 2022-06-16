using DAL.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BLL.Services;
using BLL.DTO;
using DAL.Repositories;
using Microsoft.AspNetCore.Identity;

namespace BLL.Extensions
{
    public static class AddProvidersExtensions
    {
        public static void AddLeadSubDbContext(this IServiceCollection services,string connectionStr)
        {
            services.AddDbContext<LeadSubContext>(options =>
            {
                options.UseSqlServer(connectionStr);
            });

            services.AddIdentityCore<User>().AddEntityFrameworkStores<LeadSubContext>()
                .AddEntityFrameworkStores<LeadSubContext>();
                //.AddDefaultTokenProviders();

        }
        public static void AddLeadSubDataTransients(this IServiceCollection services)
        {
            services.AddTransient<IService<SubPage,SubPageDTO>, SubPagesService>();
            services.AddTransient<SubPagesService, SubPagesService>();
            services.AddTransient<IRepository<SubPage>, SubPageRepository>();

            services.AddTransient<IService<SubPageStat, SubPageStatDTO>, SubPagesStatService>();
            services.AddTransient<SubPagesStatService, SubPagesStatService>();
            services.AddTransient<IRepository<SubPageStat>, SubPageStatRepository>();

            services.AddTransient<IService<Billing, BillingDTO>, BillingService>();
            services.AddTransient<BillingService, BillingService>();
            services.AddTransient<IRepository<Billing>, BillingRepository>();

            services.AddTransient<IService<BilledPages, BilledPagesDTO>, BilledPagesService>();
            services.AddTransient<BilledPagesService, BilledPagesService>();
            services.AddTransient<IRepository<BilledPages>, BilledPagesRepository>();
          

            services.AddTransient<DbContext, LeadSubContext>();
        }
    }
}
