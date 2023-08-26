using DL.Domain.Configuration;
using DL.Domain.Request;
using DL.Service;
using DL.Service.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace DL.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IConfiguration Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            var host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<IWarriorsService, WarriorsService>();
                    services.AddTransient<IFightService, FightService>();
                    services.Configure<WarriorConfig>(Configuration.GetSection(WarriorConfig.ConfigurationName));
                    services.Configure<NameConfig>(Configuration.GetSection(NameConfig.ConfigurationName));
                })
                .UseSerilog()
                .Build();

            var svc = ActivatorUtilities.CreateInstance<FightService>(host.Services);

            FightServiceRequest request = new FightServiceRequest()
            {
                WarriorCount = Convert.ToInt32(Environment.GetCommandLineArgs()[1])
            };

            svc.Start(request);
        }        
    }
}