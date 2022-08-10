using System.Net.Http.Headers;
using ChannelEngine.Application.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ChannelEngine.Infrastructure.Services
{
    public abstract class ApiServiceBase
    {
        protected readonly ApiSettings ApiSettings;
        protected readonly HttpClient HttpClient;
        protected readonly ILogger<OrderApiService> Logger;

        protected ApiServiceBase(
            HttpClient httpClient,
            IOptions<ApiSettings> options,
            ILogger<OrderApiService> logger
        )
        {
            Logger = logger;
            ApiSettings = options.Value;
            HttpClient = httpClient;
            HttpClient.BaseAddress = new Uri(ApiSettings.BaseUrl);
            HttpClient.DefaultRequestHeaders.Clear();
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
