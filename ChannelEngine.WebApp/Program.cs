using ChannelEngine.Application.Interfaces;
using ChannelEngine.Application.Models;
using ChannelEngine.Application.Services;
using ChannelEngine.Domain.Interfaces;
using ChannelEngine.Domain.Providers;
using ChannelEngine.Domain.Services;
using ChannelEngine.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
builder.Services.AddHttpClient<IOrderApiService, OrderApiService>();
builder.Services.AddHttpClient<IProductApiService, ProductApiService>();
builder.Services.AddTransient<IOrdersDomainService, OrdersDomainService>();
builder.Services.AddTransient<IProductsDomainService, ProductsDomainService>();
builder.Services.AddTransient<IOrdersService, OrdersService>();
builder.Services.AddTransient<IRandomGenerator, RandomGenerator>();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}");

app.Run();
