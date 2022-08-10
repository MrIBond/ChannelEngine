using ChannelEngine.Application.Interfaces;
using ChannelEngine.Application.Models;
using ChannelEngine.Application.Services;
using ChannelEngine.Domain.Interfaces;
using ChannelEngine.Domain.Providers;
using ChannelEngine.Domain.Services;
using ChannelEngine.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ChannelEngine.Console;

internal static class Program
{
    private static async Task Main(string[] args)
    {
        using var host = CreateHostBuilder(args).Build();
        var ordersService = host.Services.GetService<IOrdersService>();
        var topSoldProducts = await ordersService!.GetTopSoldProductsAndChangeStockForRandomOneAsync();
        PrintProducts(topSoldProducts);

        await host.RunAsync();
    }

    private static void PrintProducts(IEnumerable<ProductSalesDto> topSoldProducts)
    {
        System.Console.WriteLine("Top sold products");

        foreach (var product in topSoldProducts)
            System.Console.WriteLine($"{product.Name} - {product.GTIN} - {product.TotalSoldQuantity}");
    }

    private static IHostBuilder CreateHostBuilder(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        return Host.CreateDefaultBuilder(args)
            .ConfigureServices(services =>
            {
                //should be placed in shared place to not to repeat in web and console projects.
                services.AddHttpClient<IOrderApiService, OrderApiService>();
                services.AddHttpClient<IProductApiService, ProductApiService>();
                services.AddTransient<IOrdersDomainService, OrdersDomainService>();
                services.AddTransient<IProductsDomainService, ProductsDomainService>();
                services.AddTransient<IOrdersService, OrdersService>();
                services.AddTransient<IRandomGenerator, RandomGenerator>();
                services.Configure<ApiSettings>(configuration.GetSection("ApiSettings"));
            })
            .ConfigureLogging((_, logging) => logging.ClearProviders());
    }
}