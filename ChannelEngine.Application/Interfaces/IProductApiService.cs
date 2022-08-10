using ChannelEngine.Application.Models;

namespace ChannelEngine.Application.Interfaces;

public interface IProductApiService
{
    Task UpdateProductStockAsync(ProductDto product, CancellationToken cancellationToken = default);
}