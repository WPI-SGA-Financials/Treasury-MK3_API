using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using Treasury.Data;

namespace Treasury
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        // builder.WithOrigins("http://localhost:4200");
                        builder.AllowAnyOrigin();
                    });
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Treasury", Version = "v1" });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.EnableAnnotations();
                c.OrderActionsBy((apiDesc) => $"{apiDesc.ActionDescriptor.RouteValues["controller"]}_{apiDesc.RelativePath}");
            });

            services.AddRouting(options => options.LowercaseUrls = true);

            StringBuilder builder = new StringBuilder();

            string server = Environment.GetEnvironmentVariable("TREASURY_SERVER");
            builder.Append("server=").Append(server).Append(';');

            string user = Environment.GetEnvironmentVariable("TREASURY_USER");
            builder.Append("uid=").Append(user).Append(';');

            string password = Environment.GetEnvironmentVariable("TREASURY_PASSWORD");
            builder.Append("pwd=").Append(password).Append(';');

            string database = Environment.GetEnvironmentVariable("TREASURY_DATABASE");
            builder.Append("database=").Append(database);//.Append(";SslMode=none");

            services.AddDbContext<ApiDbContext>(option => option.UseMySQL(builder.ToString()));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApiDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Treasury v1"));

            dbContext.Database.EnsureCreated();

            app.UseHttpsRedirection();

            app.UseRouting();
            
            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
