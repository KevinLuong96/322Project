using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using _322Api.Models;


namespace _322Api
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
            var a = Configuration.GetConnectionString("DefaultConnection");
            string connectionString;
            if (Environment.GetEnvironmentVariable("db_env") == "production")
            {
                connectionString = Configuration.GetConnectionString("RemoteConnection");
            }
            else
            {
                connectionString = Configuration.GetConnectionString("LocalConnection");
            }
            services.AddDbContext<DatabaseContext>(opt =>
                opt.UseNpgsql(connectionString));

            //services.AddDbContext<DatabaseContext>(opt =>
            //opt.UseNpgsql(Environment.GetEnvironmentVariable("DB_CONNECTION_STRING")));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                };
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseAuthentication();


            app.UseMvc();
            //var services = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            //var context = services.ServiceProvider.GetService<DbContext>();
            //context.Database.Migrate();
            //context.EnsureSeedData();
        }
    }
}

