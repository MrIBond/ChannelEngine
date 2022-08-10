using ChannelEngine.Domain.Entities;
using ChannelEngine.Domain.Interfaces;

namespace ChannelEngine.Domain.Services;

public class OrdersDomainService : IOrdersDomainService
{
    public IEnumerable<ProductSales> GetTopSoldProducts(IEnumerable<Order> orders, int top)
    {
        if (orders == null) throw new ArgumentNullException(nameof(orders));
        if(top < 0) throw new ArgumentOutOfRangeException(nameof(top));

        return orders
            .SelectMany(x => x.Lines)
            .GroupBy(x => x.Product.MerchantProductNo)
            .Select(g => new ProductSales
            {
                TotalSoldQuantity = g.Sum(s => s.Quantity),
                Product = g.First().Product
            })
            .OrderByDescending(x => x.TotalSoldQuantity)
            .Take(top);
    }
}