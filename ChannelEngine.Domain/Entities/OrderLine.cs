namespace ChannelEngine.Domain.Entities;

public class OrderLine
{
    public int Quantity { get; init; }
    public Product Product { get; init; }
}