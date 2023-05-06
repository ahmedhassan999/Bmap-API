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
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Stack.API.AutoMapperConfig;
using Stack.API.Extensions;
using Stack.Core;
using Stack.Core.Managers;
using Stack.DAL;
using Stack.Entities.Models;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using System.IO;
using Hangfire;

namespace Stack.API
{
    public class Startup
    {
        readonly string AllowSpecificOrigins = "_AllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //Remote server connection string example . 
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("Server=162.241.156.19; Database=BMAP;User ID=sa;Password=P@ssw0rd@./;"));
            services.AddHangfire(x => x.UseSqlServerStorage("Server=162.241.156.19; Database=BMAP;User ID=sa;Password=P@ssw0rd@./;"));

            //Local server connection string. 
            //services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("Server=B-YASMIN-GHAZY\\SQLEXPRESS; Database=BMAP;User ID=sa;Password=P@ssw0rd;"));
            //services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("Server=BOLT-NADER\\SQLEXPRESS; Database=BMAP;User ID=sa;Password=P@ssw0rd;"));
            //services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer("Server=B-AMR-MOUSHTAHA\\SQLEXPRESS; Database=BEMAPS;User ID=sa;Password=P@ssw0rd;"));

            //HangFire strings
            //services.AddHangfire(x => x.UseSqlServerStorage("Server=BOLT-NADER\\SQLEXPRESS; Database=BMAP;User ID=sa;Password=P@ssw0rd;"));
            services.AddHangfireServer();

            //Add Identity framework.
            services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddUserManager<ApplicationUserManager>();

            //Configure which clients could send a request to the API .
            services.AddCors(options =>
            {
                options.AddPolicy(name: AllowSpecificOrigins,
                             builder =>
                             {
                                 builder.SetIsOriginAllowed(origin => true) // allow any origin
                                 //"https://admin.bmap.ae", "https://bmap.ae", "http://172.20.10.3:8100"
                                    .AllowAnyMethod()
                                    .AllowAnyHeader()
                                    .AllowCredentials();
                             });
            });

            //services.AddCors(options =>
            //{
            //    options.AddPolicy(name: AllowSpecificOrigins,
            //                 builder =>
            //                 {
            //                     builder.WithOrigins("http://localhost:4200", "http://localhost:4201")
            //                        .AllowAnyMethod()
            //                        .AllowAnyHeader()
            //                        .AllowCredentials();
            //                 });
            //});


            //Configure Auto Mapper .
            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddScoped<UnitOfWork>();

            services.AddBusinessServices();

            //Add and configure JWT Bearer Token Authentication . 
            services.AddAuthentication(options => options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration.GetSection("Token:Key").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        return Task.CompletedTask;
                    }
                };

                services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                });
            });

            ///Use Swagger .
            ConfigureSwagger(services);

            var defaultApp = FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "key.json")),
            });

            services.AddControllers();
        }

        //Configure Swagger 
        private static void ConfigureSwagger(IServiceCollection services)
        {

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API Stack",
                    Version = "v1"
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                           {
                             new OpenApiSecurityScheme
                             {
                               Reference = new OpenApiReference
                               {
                                 Type = ReferenceType.SecurityScheme,
                                 Id = "Bearer"
                               }
                              },
                              new string[] { }
                            }
                  });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");

            });


            //Use CORS 
            app.UseCors(AllowSpecificOrigins);

            app.UseRouting();

            // using authentication middleware

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseHangfireDashboard("/mydashboard");

        }


    }
}
