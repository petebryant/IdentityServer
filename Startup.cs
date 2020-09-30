using IdentityServer.Common;
using IdentityServer.Data;
using IdentityServer.Models;
using IdentityServer.Services;
using IdentityServer.Stores;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IdentityServer
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
            services.AddOptions();

            // var section = Configuration.GetSection("Email");
            // services.Configure<Common.EmailConfiguration>(section);

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
                {
                    // Password settings as we are using windows logins
                    options.Password.RequireDigit = false;
                    options.Password.RequiredLength = 0;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireLowercase = false;
                    options.User.AllowedUserNameCharacters = null;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddUserManager<IdentityServerUserManager<ApplicationUser>>()
                .AddDefaultTokenProviders();

            services.AddDataProtection()
                .SetApplicationName("IdentityServer")
                .PersistKeysToDbContext<ApplicationDbContext>()
                .ProtectKeysWithDpapi();

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IPersistedGrantStore, PersistedGrantStore>();
            services.AddTransient<IAccessTokenIssuer, AccessTokenIssuer>();
            services.AddTransient<IHttpClientService, HttpClientService>();
            services.AddTransient<IClientRepository, ClientRepository>();

            services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, AppClaimsPrincipalFactory>();
            services.AddScoped<ICorsPolicyService, CorsPolicyService>();

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddLocalApiAuthentication();
            SwaggerHelper.Configuration = Configuration;
            services.AddSwaggerGen(SwaggerHelper.ConfigureSwaggerGen);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseAuthentication();

            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(SwaggerHelper.ConfigureSwaggerUI);

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
