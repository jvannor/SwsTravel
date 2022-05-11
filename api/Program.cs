using System;
using System.Threading.Tasks;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Azure.Functions.Worker.Configuration;
using api.entities;

namespace api
{
    public class Program
    {
        public static void Main()
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()                
                .ConfigureServices(container => 
                {
                    container.AddDbContext<SwsTravelContext>(
                        options => options.UseSqlServer(
                            Environment.GetEnvironmentVariable("SwsTravelConnection"),
                            providerOptions => providerOptions.EnableRetryOnFailure()
                        )
                    );
                })
                .Build();

            host.Run();
        }
    }
}