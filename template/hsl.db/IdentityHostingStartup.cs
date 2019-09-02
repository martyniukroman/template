//using hsl.api.Models;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//
//[assembly: HostingStartup(typeof(hsl.api.Areas.Identity.IdentityHostingStartup))]
//
//namespace hsl.api.Areas.Identity
//{
//    public class IdentityHostingStartup : IHostingStartup
//    {
//        public void Configure(IWebHostBuilder builder)
//        {
//            builder.ConfigureServices((context, services) =>
//            {
//                services.AddDbContext<hslapiContext>(options =>
//                    options.UseSqlServer(
//                        context.Configuration.GetConnectionString("hslapiContextConnection")));
//
//                services.AddDefaultIdentity<IdentityUser>()
//                    .AddEntityFrameworkStores<hslapiContext>();
//            });
//        }
//    }
//}