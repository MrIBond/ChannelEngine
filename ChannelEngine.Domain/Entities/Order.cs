namespace ChannelEngine.Domain.Entities;

public class Order
{
    public IEnumerable<OrderLine> Lines { get; set; } = new List<OrderLine>();
}