using AutoMapper;
using hsl.api.Helpers;
using hsl.api.Interfaces;
using hsl.api.Models;
using hsl.api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            //enable CORS
            services.AddCors();

            // setup mapper
            var mappingConfig = new MapperConfiguration(mConfig =>
            {
                mConfig.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
#pragma warning disable CS0618 // Тип или член устарел
            services.AddAutoMapper();
#pragma warning restore CS0618 // Тип или член устарел

            // setup entity
            services.AddEntityFrameworkSqlServer().AddDbContext<hslapiContext>(options =>
                 options.UseSqlServer(Configuration.GetConnectionString("hslapiContextConnection"),
                 b => b.MigrationsAssembly("hsl.api")));

            var builder = services.AddIdentityCore<User>(o =>
            {
                // configure identity options
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
            });
            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
            builder.AddEntityFrameworkStores<hslapiContext>().AddDefaultTokenProviders();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);

            // Initialization dependency injection
           // services.AddScoped<IRegistrationInterface, RegistrationService>();


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

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseCors(options => options.AllowAnyOrigin()
                                          .AllowAnyMethod()
                                          .AllowAnyHeader()
                                          .AllowCredentials());
        }
    }
}
