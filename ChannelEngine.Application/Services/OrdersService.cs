using ChannelEngine.Application.Exceptions;
using ChannelEngine.Application.Interfaces;
using ChannelEngine.Application.Models;
using ChannelEngine.Domain.Entities;
using ChannelEngine.Domain.Exceptions;
using ChannelEngine.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace ChannelEngine.Application.Services;

public class OrdersService : IOrdersService
{
    // should be replaced with an enum. Adds additional flexibility for the system.
    private readonly string _inProgressOrderStatusName = "IN_PROGRESS";
    // should be taken form the configuration of as a parameter from a client. skip for test project
    private readonly int _topSoldProductsNumber = 5;
    private readonly int _newStockQuantity = 25;
    private readonly IOrderApiService _orderApiService;
    private readonly IProductApiService _productApiService;
    private readonly ILogger<OrdersService> _logger;
    private readonly IOrdersDomainService _ordersDomainService;
    private readonly IProductsDomainService _productsDomainService;

    public OrdersService(
        IOrderApiService orderApiService,
        IOrdersDomainService ordersDomainService,
        IProductsDomainService productsDomainService,
        IProductApiService productApiService,
        ILogger<OrdersService> logger
        )
    {
        _orderApiService = orderApiService;
        _ordersDomainService = ordersDomainService;
        _productsDomainService = productsDomainService;
        _productApiService = productApiService;
        _logger = logger;
    }

    public async Task<IEnumerable<ProductSalesDto>> GetTopSoldProductsAndChangeStockForRandomOneAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            var ordersDto = await _orderApiService.GetOrdersAsync(_inProgressOrderStatusName, cancellationToken).ConfigureAwait(false);
            var orders = MapOrderDtosToDomainOrders(ordersDto);

            var topSoldProducts = _ordersDomainService.GetTopSoldProducts(
                orders,
                _topSoldProductsNumber
            ).ToList();

            var products = topSoldProducts.Select(x => x.Product).ToList();
            await UpdateRandomProductStockAsync(products, cancellationToken).ConfigureAwait(false);

            return topSoldProducts.Select(x => new ProductSalesDto
            {
                TotalSoldQuantity = x.TotalSoldQuantity,
                Name = x.Product.Description,
                GTIN = x.Product.Gtin
            });
        }
        //Only for the demonstration purposes.
        catch (NegativeStockValueException negativeStockValueException)
        {
            _logger.LogError(negativeStockValueException, negativeStockValueException.Message);
            throw;
        }
        catch (ChannelEngineApiException apiCallException)
        {
            _logger.LogError(apiCallException, apiCallException.Message);
            throw;
        }
    }


    private static List<Order> MapOrderDtosToDomainOrders(IEnumerable<OrderDto> orderDtos)
    {
        return orderDtos
            .Select(x => new Order
            {
                Lines = x.Lines.Select(l => new OrderLine
                {
                    Product = new Product
                    {
                        MerchantProductNo = l.MerchantProductNo,
                        Description = l.Description,
                        Gtin = l.Gtin
                    },
                    Quantity = l.Quantity
                })
            })
            .ToList();
    }

    private async Task UpdateRandomProductStockAsync(IReadOnlyList<Product> products,
        CancellationToken cancellationToken = default)
    {
        var product = _productsDomainService.GetRandomProduct(products);
        product.SetStock(_newStockQuantity);

        await _productApiService.UpdateProductStockAsync(new ProductDto
            {
                MerchantProductNo = product.MerchantProductNo,
                Stock = product.Stock
            },
            cancellationToken).ConfigureAwait(false);
    }
}