using ChannelEngine.Domain.Entities;
using ChannelEngine.Domain.Interfaces;

namespace ChannelEngine.Domain.Services;

public class ProductsDomainService : IProductsDomainService
{
    private readonly IRandomGenerator _randomGenerator;

    public ProductsDomainService(IRandomGenerator randomGenerator)
    {
        _randomGenerator = randomGenerator;
    }

    public Product GetRandomProduct(IReadOnlyList<Product> products)
    {
        if (products == null) throw new ArgumentNullException(nameof(products));

        var randomIndex = _randomGenerator.Generate(products.Count);

        return products[randomIndex];
    }
}