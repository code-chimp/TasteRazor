using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(TasteRazor.Areas.Identity.IdentityHostingStartup))]
namespace TasteRazor.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}