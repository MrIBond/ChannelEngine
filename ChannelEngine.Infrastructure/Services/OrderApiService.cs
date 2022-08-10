using ChannelEngine.Application.Exceptions;
using ChannelEngine.Application.Interfaces;
using ChannelEngine.Application.Models;
using ChannelEngine.Infrastructure.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace ChannelEngine.Infrastructure.Services;

public class OrderApiService : ApiServiceBase, IOrderApiService
{
    public OrderApiService(
        HttpClient httpClient,
        IOptions<ApiSettings> options,
        ILogger<OrderApiService> logger
    ) : base(httpClient, options, logger)
    {
    }

    public async Task<IEnumerable<OrderDto>> GetOrdersAsync(string orderStatus, CancellationToken cancellationToken = default)
    {
        try
        {
            var orders = await GetAllOrdersAsync(orderStatus, cancellationToken);

            return orders
                .Select(x => new OrderDto
                    {
                        Lines = x.Lines.Select(l => new OrderLineDto
                        {
                            MerchantProductNo = l.MerchantProductNo,
                            Quantity = l.Quantity,
                            Description = l.Description,
                            Gtin = l.Gtin
                        })
                    }
                );
        }
        catch (HttpRequestException httpRequestException)
        {
            Logger.LogError(httpRequestException, httpRequestException.Message);
            throw new ChannelEngineApiException("Can not load orders.");
        }
    }

    private async Task<IEnumerable<Order>> GetAllOrdersAsync(
        string orderStatus,
        CancellationToken cancellationToken
        )
    {
        var orders = new List<Order>();
        var startingPage = 1;

        OrdersResponse ordersResponse;

        do
        {
            //should be done with Uri builder
            var uri = $"orders?statuses={orderStatus}&apikey={ApiSettings.ApiKey}&page={startingPage}";
            var response = await HttpClient.GetAsync(uri, cancellationToken).ConfigureAwait(false);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
            ordersResponse = JsonConvert.DeserializeObject<OrdersResponse>(responseString);
            ordersResponse.EnsureSuccessResponse();

            orders.AddRange(ordersResponse.Orders);
            startingPage++;
        }
        while (ordersResponse.HasNext);

        return orders;
    }
}