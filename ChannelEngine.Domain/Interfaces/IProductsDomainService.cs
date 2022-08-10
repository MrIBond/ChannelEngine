using ChannelEngine.Domain.Entities;

namespace ChannelEngine.Domain.Interfaces;

public interface IProductsDomainService
{
    Product GetRandomProduct(IReadOnlyList<Product> products);
}