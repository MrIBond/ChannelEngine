namespace ChannelEngine.Application.Models;

public class OrderLineDto
{
    public string Gtin { get; init; }
    public int Quantity { get; init; }
    public string Description { get; init; }
    public string MerchantProductNo { get; init; }
}