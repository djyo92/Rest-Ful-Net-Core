using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using MiPrimerWebApiM3.Data;
using MiPrimerWebApiM3.DataContext;
using MiPrimerWebApiM3.Entities;
using MiPrimerWebApiM3.Helpers;
using MiPrimerWebApiM3.Models;
using MiPrimerWebApiM3.Services;

namespace MiPrimerWebApiM3
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
                options.AddPolicy("PermitirApiRequest",
                    builder => builder.WithOrigins("http://www.apirequest.io").WithMethods("GET", "POST").AllowAnyHeader());
            });
            services.AddScoped<HashService>();
            services.AddDataProtection();

            services.AddDbContext<AplicationDbContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("AutoresDbConection")));
            services.AddIdentity<AplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<AplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAutoMapper(configuration => configuration.CreateMap<Autor,AutorDTO>(), typeof(Startup));
            services.AddAutoMapper(configuration => configuration.CreateMap<AutorCreacionDTO,Autor>().ReverseMap(), typeof(Startup));

            services.AddTransient<IHostedService,WriteToFileHostedServices>();
            services.AddTransient<IHostedService, ConsumeScopeService>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = false,
                     ValidateAudience = false,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(Configuration["jwt:key"])),
                     ClockSkew = TimeSpan.Zero
                 });


            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddControllers();
            services.AddMvc(options => options.Filters.Add(new MiFiltroDeExcepcion()));
            services.AddTransient<IClaseB, ClaseB>();
            services.AddScoped<MiFiltroDeAccion>();
            services.AddScoped<AutorRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors();
            //app.UseCors(builder => builder.WithOrigins("http://www.apirequest.io").WithMethods("GET", "POST").AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
