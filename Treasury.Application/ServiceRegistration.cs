using System;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Treasury.Application.Accessor;
using Treasury.Application.Contexts;

namespace Treasury.Application
{
    public static class ServiceRegistration
    {
        public static void AddDatabase(this IServiceCollection services)
        {
            StringBuilder builder = new StringBuilder();

            string server = Environment.GetEnvironmentVariable("TREASURY_SERVER");
            builder.Append("server=").Append(server).Append(';');

            string user = Environment.GetEnvironmentVariable("TREASURY_USER");
            builder.Append("uid=").Append(user).Append(';');

            string password = Environment.GetEnvironmentVariable("TREASURY_PASSWORD");
            builder.Append("pwd=").Append(password).Append(';');

            string database = Environment.GetEnvironmentVariable("TREASURY_DATABASE");
            builder.Append("database=").Append(database);

            services.AddDbContext<sgadbContext>(option => option.UseMySQL(builder.ToString()));
        }

        public static void AddAccessors(this IServiceCollection services)
        {
            services.AddScoped<BudgetAccessor>();
            services.AddScoped<FundingRequestAccessor>();
            services.AddScoped<OrganizationAccessor>();
            services.AddScoped<ReallocationRequestAccessor>();
            services.AddScoped<StudentLifeFeeAccessor>();
            services.AddScoped<MetadataAccessor>();
        }
    }
}