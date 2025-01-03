using ExcelToAppleCalendar.App;
using ExcelToAppleCalendar.Library.Interfaces;
using ExcelToAppleCalendar.Library.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder()
    .ConfigureServices((_, serviceCollection) =>
    {
        serviceCollection.AddTransient<ICalendarService, CalendarService>();
        serviceCollection.AddTransient<IExcelDataReader, ExcelDataReader>();
        serviceCollection.AddSingleton<App>();
    })
    .ConfigureAppConfiguration(configBuilder =>
    {
        configBuilder.AddJsonFile("appsettings.json");
    })
    .Build();

using var scope = host.Services.CreateScope();
var services = scope.ServiceProvider;

try
{
    services.GetRequiredService<App>().Run();
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}