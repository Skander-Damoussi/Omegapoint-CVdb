using System;
using AspNetCore.Identity.MongoDbCore.Infrastructure;
using cv_api.Areas.Identity.Data;
using cv_api.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(cv_api.Areas.Identity.IdentityHostingStartup))]
namespace cv_api.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {

				services.AddIdentity<ApplicationUser, ApplicationRole>()
				.AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>
				(
					"mongodb+srv://Admin:Test123@cvdb.wonwu.mongodb.net/myFirstDatabase?retryWrites=true&w=majority",
					"MongoDbTests"
				)
				.AddDefaultTokenProviders();
                
                services.Configure<IdentityOptions>(options =>
                {
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = true;
                    options.Password.RequiredLength = 6;
                    options.Password.RequiredUniqueChars = 0;

                    options.User.RequireUniqueEmail = false;
                });

            });
        }
    }
}