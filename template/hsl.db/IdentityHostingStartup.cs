//using hsl.api.Models;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;

//[assembly: HostingStartup(typeof(hsl.api.Areas.Identity.IdentityHostingStartup))]

//namespace hsl.api.Areas.Identity
//{
//    public class IdentityHostingStartup : IHostingStartup
//    {
//        public void Configure(IWebHostBuilder builder)
//        {
//            builder.ConfigureServices((context, services) =>
//            {
//                services.AddDbContext<HslapiContext>(options =>
//                    options.UseSqlServer(
//                        context.Configuration.GetConnectionString("LocalHostConnection")));

//                services.AddDbContext<HslapiContext>(options =>
//                    options.UseSqlServer(config.GetConnectionString("optimumDB")));

//                services.AddDefaultIdentity<IdentityUser>()
//                    .AddEntityFrameworkStores<HslapiContext>();
//            });
//        }
//    }
//}