using ChannelEngine.Domain.Exceptions;

namespace ChannelEngine.Domain.Entities;

public class Product
{
    public string Gtin { get; init; }
    public string Description { get; init; }
    public string MerchantProductNo { get; init; }
    public int Stock { get; private set; }

    public void SetStock(int stock)
    {
        if (stock < 0)
        {
            throw new NegativeStockValueException(
                $"Can not assign negative stock to product. MerchantProductNo = {MerchantProductNo}");
        }

        Stock = stock;
    }
}