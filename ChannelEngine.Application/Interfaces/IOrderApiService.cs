using ChannelEngine.Application.Models;

namespace ChannelEngine.Application.Interfaces;

public interface IOrderApiService
{
    Task<IEnumerable<OrderDto>> GetOrdersAsync(string orderStatus, CancellationToken cancellationToken = default);
}