﻿using System;
using hsl.api.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text;
using hsl.bl.Interfaces;
using hsl.bl.Services;
using hsl.db.Entities;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.Extensions.DependencyInjection.Extensions;


namespace hsl.api
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
            //allow to get user Id in BLL
            services.AddHttpContextAccessor();
            services.TryAddTransient<IHttpContextAccessor, HttpContextAccessor>();

            //enable CORS
            services.AddCors();
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    x => x
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials()
                        .Build()
                );
            });

            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new CorsAuthorizationFilterFactory("CorsPolicy"));
            });

            // setup mapper

            //setup jwtToken
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));
            services.Configure<JwtIssuerOptions>(jwtAppSettingOptions);
            var jwtSettings = jwtAppSettingOptions.Get<JwtIssuerOptions>();
            var secretKey = Encoding.ASCII.GetBytes(jwtSettings.Secret);
            
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = false, // should be validated on production

                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(secretKey),
            };
            services.AddAuthentication(op =>
            {
                op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(configureOptions =>
            {
                configureOptions.TokenValidationParameters = tokenValidationParameters;
            });
            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireLoggedIn",
                    policy => policy.RequireRole("Admin", "Customer", "Moderator").RequireAuthenticatedUser());
                options.AddPolicy("RequireAdministratorRole",
                    policy => policy.RequireRole("Admin").RequireAuthenticatedUser());
            });

            // setup entity
            services.AddEntityFrameworkSqlServer().AddDbContext<hslapiContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("hslapiContextConnection"),
                    b => b.MigrationsAssembly("hsl.api")));

            //setupIdentity
            var identityBuilder = services.AddIdentityCore<User>(o =>
                                          {
                                              // configure identity options
                                              o.Password.RequireDigit = false;
                                              o.Password.RequireLowercase = false;
                                              o.Password.RequireUppercase = false;
                                              o.Password.RequireNonAlphanumeric = false;
                                              o.Password.RequiredLength = 6;
                                              o.User.RequireUniqueEmail = true;
                                              o.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                                              o.Lockout.MaxFailedAccessAttempts = 3;
                                              o.Lockout.AllowedForNewUsers = true;
                                          }
                                      ).AddRoles<IdentityRole>().AddEntityFrameworkStores<hslapiContext>()
                                      .AddDefaultTokenProviders() ?? throw new ArgumentNullException(nameof(services));

            identityBuilder =
                new IdentityBuilder(identityBuilder.UserType, typeof(IdentityRole), identityBuilder.Services);
            identityBuilder.AddEntityFrameworkStores<hslapiContext>().AddDefaultTokenProviders();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);

            // Initialization dependency injection
            services.AddScoped<RefreshTokenModel>();
            services.AddScoped<IProfile, ProfileService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandler(
                builder =>
                {
                    builder.Run(
                        async context =>
                        {
                            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                            context.Response.Headers.Add("Access-Control-Allow-Origin", "*");

                            var error = context.Features.Get<IExceptionHandlerFeature>();
                            if (error != null)
                            {
                                await context.Response.WriteAsync(error.Error.Message).ConfigureAwait(false);
                            }
                        });
                });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseDefaultFiles();
            app.UseMvc();
            app.UseCors("CorsPolicy");
        }
    }
}