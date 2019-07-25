using EventOrganizer.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EventOrganizer.API
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
            services.AddCors();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            var key = Encoding.ASCII.GetBytes("hMlBevhXPdc9ucEZYcxyMmZ2p11RWGteFbH56YYRoAsvGKHkAS3-Tqg_nPNA9S9V_OZE1XqTLQRNWwGc1roEtd-NatZI6AJ1tHXfQnpMJZiUW8FQKF4il2Km9Im3raVnk5A9G1l6r51C-4YsCUGrRA1oamJFvrmTe3rh2Z0OoB6L2xS9hRnw9p3US939JY7LH_zh3NhwJ3o2D91TlxrLgaCLEy0pnHfL0PItTLAb1fFnVb6OBwp33ICTWfR617ozyb6Bgvr7jhqtY_OrvsnPmGFLuhrnzUqUJNbL37zyPtbvUxMM0S1rtSwVh700fGVhKSQYbIOkl23vrk4dR2DwlQ");
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            services.AddDbContext<Context>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("EventOrganizerContext"), b => b.MigrationsAssembly("EventOrganizer.API")));
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCors(x => x
                .WithOrigins(
                    "http://localhost:4200",
                    "https://hitw2019leaderboard.azurewebsites.net",
                    "https://hitw2019dashboard.azurewebsites.net",
                    "https://hitw2019photoBox.azurewebsites.net",
                    "https://hitw2019.azurewebsites.net")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
