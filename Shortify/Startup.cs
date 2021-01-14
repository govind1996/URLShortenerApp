using DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using shortify.Code;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace shortify
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

            services.AddControllersWithViews();
            services.AddHttpClient();
            
            services.AddDbContext<UrlShortnerDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("UrlShortenerContext")));
            services.AddTransient<DbDataSeeder>();
            //disabling auto 400 response for invalid modelstate
            services.Configure<ApiBehaviorOptions>(options =>
            {
                //options.SuppressConsumesConstraintForFormFileParameters = true;
                //options.SuppressInferBindingSourcesForParameters = true;
                options.SuppressModelStateInvalidFilter = true;
            });

            services.AddScoped<IShortifyService, ShortifyService>();
            services.AddHttpContextAccessor();
            //Identity
            services.AddIdentity<IdentityUser, IdentityRole>().
                AddEntityFrameworkStores<UrlShortnerDbContext>().
                AddDefaultTokenProviders();

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/build";
            });

            //Read from config
            var SecretKey = Configuration["SecretKey"];
            //create SignIn key using the secret key which will be used to create signature
            var SignInkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));

            var ValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = SignInkey
            };
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = ValidationParameters;
                    options.SaveToken = true;
                    options.Events = new JwtBearerEvents();

                    //add below lines to accept tokens from cookies
                    //options.Events.OnMessageReceived = context => {

                    //    if (context.Request.Cookies.ContainsKey("X-Access-Token"))
                    //    {
                    //        context.Token = context.Request.Cookies["X-Access-Token"];
                    //    }
                    //    return Task.CompletedTask;
                    //};
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DbDataSeeder seeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            seeder.SeedAdminUser();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                
            endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });
        }
    }
}
