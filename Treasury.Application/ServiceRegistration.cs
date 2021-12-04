using System;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Treasury.Application.Accessor.Implementation;
using Treasury.Application.Accessor.Interface;
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
            services.AddScoped<IBudgetAccessor, BudgetAccessorImpl>();
            services.AddScoped<IFundingRequestAccessor, FundingRequestAccessorImpl>();
            services.AddScoped<IOrganizationAccessor, OrganizationAccessorImpl>();
            services.AddScoped<IReallocationRequestAccessor, ReallocationRequestAccessorImpl>();
            services.AddScoped<IStudentLifeFeeAccessor, StudentLifeFeeAccessorImpl>();
            services.AddScoped<IMetadataAccessor, MetadataAccessorImpl>();
        }
    }
}