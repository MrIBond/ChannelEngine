using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace ChannelEngine.Infrastructure.Models;

public class OrdersResponse : ResponseBase
{
    public OrdersResponse(
        bool success,
        string message,
        string logId,
        int statusCode,
        List<Order> orders, 
        int count,
        int totalCount,
        int itemsPerPage
        ) : base(success, message, logId, statusCode)
    {
        Orders = orders;
        Count = count;
        TotalCount = totalCount;
        ItemsPerPage = itemsPerPage;
    }

    [JsonProperty("Content")] 
    public List<Order> Orders { get; private set; }

    [JsonPropertyName("Count")]
    public int Count { get; private set; }

    [JsonPropertyName("TotalCount")]
    public int TotalCount { get; private set; }

    [JsonPropertyName("ItemsPerPage")]
    public int ItemsPerPage { get; private set; }

    public bool HasNext => TotalCount > ItemsPerPage && Count == ItemsPerPage;
}

public class Order
{
    public Order(string status, IEnumerable<Line> lines)
    {
        Status = status;
        Lines = lines;
    }

    [JsonProperty("Status")] 
    public string Status { get; private set; }

    [JsonProperty("Lines")] 
    public IEnumerable<Line> Lines { get; private set; }
}

public class Line
{
    public Line(string gtin, int quantity, string description, string merchantProductNo)
    {
        Gtin = gtin;
        Quantity = quantity;
        Description = description;
        MerchantProductNo = merchantProductNo;
    }

    [JsonProperty("Gtin")]
    public string Gtin { get; private set; }

    [JsonProperty("Quantity")]
    public int Quantity { get; private set; }

    [JsonProperty("Description")] 
    public string Description { get; private set; }

    [JsonProperty("MerchantProductNo")]
    public string MerchantProductNo { get; private set; }
}