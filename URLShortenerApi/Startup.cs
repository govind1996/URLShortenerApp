using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using URLShortner.Code;
using AutoMapper;
using DAL;
using URLShortner.Filters;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using URLShortenerApi.Helpers;

namespace URLShortner
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
            services.AddControllers(x=>
            {
                x.Filters.Add(new Filters.ExceptionHandling());
            });
            services.AddHttpClient();
            services.AddMvc();
            services.AddScoped<IShortenerService, ShortenerService>();
            services.AddScoped<IURLHelper, URLHelper>();
            services.AddSwaggerGen(x =>
            {
                var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlFilePath = Path.Combine(AppContext.BaseDirectory, xmlFileName);
                x.IncludeXmlComments(xmlFilePath);
                //adding the options to take token in header
                x.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    
                    BearerFormat = "JWT",
                    Scheme = "bearer",
                    Description = "Specify the authorization token.",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http
                });
                //enabling the authentication functionality for swagger
                x.OperationFilter<SecurityRequirementsOperationFilter>();
            });
            var SecretKey = Configuration["SecretKey"];
            var SignInKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var ValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = SignInKey
            };
            services.AddDbContext<UrlShortnerDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("UrlShortenerContext")));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = ValidationParameters;
                    options.SaveToken = true;
                    options.Events = new JwtBearerEvents();
                });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            if (env.EnvironmentName == "Development")
            {
                app.UseSwagger();
                app.UseSwaggerUI(x =>
                {
                    x.SwaggerEndpoint("/swagger/v1/swagger.json", "UrlShortnerApi V1.0");
                });
            }
            
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
