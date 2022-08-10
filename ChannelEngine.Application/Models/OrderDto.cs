namespace ChannelEngine.Application.Models;

public class OrderDto
{
    public IEnumerable<OrderLineDto> Lines { get; init; } = new List<OrderLineDto>();
}