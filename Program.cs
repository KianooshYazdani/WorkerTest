using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using TaxService.Data;
using TaxService.Settings;
using TaxService.Worker;
using TaxService.Worker.ServiceFactory;

namespace TaxService;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = CreateHostBuilder(args);

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Verbose()
            .WriteTo.Logger(l => l.Filter.ByIncludingOnly("@l = 'Verbose'")
                .WriteTo.File("./Log/Tax-Verbos-.txt", rollingInterval: RollingInterval.Day))
            .WriteTo.Logger(l => l.Filter.ByIncludingOnly("@l = 'Information'")
                .WriteTo.File("./Log/Tax-Info-.txt", rollingInterval: RollingInterval.Day))
            .WriteTo.Logger(l => l.Filter.ByIncludingOnly("@l = 'Error' or @l = 'Fatal'")
                .WriteTo.File("./Log/Tax-Error-.txt", rollingInterval: RollingInterval.Day))
            .WriteTo.Logger(l => l.Filter.ByIncludingOnly("@l = 'Debug'")
                .WriteTo.File("./Log/Tax-Debug-.txt", rollingInterval: RollingInterval.Day))
            .CreateLogger();

        Log.Logger.Information("Tax services started at: {time}", DateTime.Now);

        var host = builder.Build();

        var configuration = host.Services.GetService<IConfiguration>();
        var settings = host.Services.GetService<Settings.ServiceSettings>();
        SetConfig(configuration, settings);


        host.Run();
    }
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .UseWindowsService()
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                if (hostingContext.HostingEnvironment.IsDevelopment())
                    config.AddJsonFile("appsettings.Development.json");
                else
                    config.AddJsonFile("appsettings.json");
            })
            .ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<IServiceCollection>(services); 
                services.AddSingleton<Settings.ServiceSettings>();
                services.AddScoped<ISendInvoiceServiceFactory, SendInvoiceServiceFactory>();
                services.AddScoped<IInquiryServiceFactory, InquiryServiceFactory>();

                services.AddScoped<ITaxDbContextFactory, TaxDbContextFactory>();

                services.AddHostedService<WorkerManager>();
                services.AddHostedService<FileHistoryService>();

                services.Configure<HostOptions>(hostOptions =>
                {
                    hostOptions.BackgroundServiceExceptionBehavior = BackgroundServiceExceptionBehavior.Ignore;
                });
                services.AddDbContextFactory<TaxDbContext>(options =>

                        options.UseSqlServer(hostContext.Configuration.GetConnectionString("Tax"), optionsBuilder =>
                        {
                            // Configure the retry logic here
                            optionsBuilder.EnableRetryOnFailure(3, TimeSpan.FromSeconds(2), null);
                        }));

                services.AddDbContext<TaxDbContext>(options =>
                        options.UseSqlServer(hostContext.Configuration.GetConnectionString("Tax")));

                
            });

    private static void SetConfig(IConfiguration configuration, Settings.ServiceSettings settings)
    {
        settings.WorkerSettings = configuration.GetSection("WorkerSettings").Get<WorkerSettings>();
        settings.TaxApiSettings = configuration.GetSection("TaxApiSettings").Get<TaxApiSettings>();
    }

}