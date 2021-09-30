using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace WebStore
{
    public class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(host => host
                   .UseStartup<Startup>()
                //.ConfigureLogging((host, log) => log
                //    .ClearProviders()
                //    .AddDebug()
                //    .AddConsole(opt => opt.IncludeScopes = true)
                //    .AddEventLog()
                //    .AddFilter<ConsoleLoggerProvider>("Microsoft", LogLevel.Warning))
                )
            ;
    }
}