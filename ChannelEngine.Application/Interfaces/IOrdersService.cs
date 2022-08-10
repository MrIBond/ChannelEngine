using ChannelEngine.Application.Models;

namespace ChannelEngine.Application.Interfaces;

public interface IOrdersService
{
    Task<IEnumerable<ProductSalesDto>> GetTopSoldProductsAndChangeStockForRandomOneAsync(CancellationToken cancellationToken = default);
}