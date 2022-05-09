using System;
using System.Threading.Tasks;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Azure.Functions.Worker.Configuration;
using api.entities;

// https://medium.com/@manuelspinto/create-a-complete-azure-function-project-in-net-6-and-af-v4-bd1cc714452c
// https://github.com/manuelspinto/azure-function-example-csharp
// https://dev.to/azure/using-entity-framework-with-azure-functions-50aa
// https://medium.com/geekculture/developing-net-isolated-process-azure-functions-5a1ff4acee46
// https://github.com/Azure/Azure-Functions/issues/717#:~:text=The%20connection%20string%20in%20the%20local.settings.json%20does%20indeed,name%20%29%20%7B%20string%20conStr%20%3D%20System.%20
// https://github.com/jeffhollan/functions-net5-entityframework/blob/main/HttpTrigger.cs

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
                        options => 
                            options.UseSqlServer(
                                Environment.GetEnvironmentVariable("SwsTravelConnection"),
                                sqlServerOptionsAction: sqlOptions =>
                                {
                                    sqlOptions.EnableRetryOnFailure(
                                        maxRetryCount: 10,
                                        maxRetryDelay: TimeSpan.FromSeconds(30),
                                        errorNumbersToAdd: null);
                                }
                            )
                    );
                })
                .Build();

            host.Run();
        }
    }
}