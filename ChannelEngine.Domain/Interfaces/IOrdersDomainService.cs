using ChannelEngine.Domain.Entities;

namespace ChannelEngine.Domain.Interfaces;

public interface IOrdersDomainService
{
    IEnumerable<ProductSales> GetTopSoldProducts(IEnumerable<Order> orders, int top);
}