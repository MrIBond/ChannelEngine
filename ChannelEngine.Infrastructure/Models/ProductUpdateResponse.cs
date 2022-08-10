namespace ChannelEngine.Infrastructure.Models;

public class ProductUpdateResponse : ResponseBase
{
    public ProductUpdateResponse(
        bool success,
        string message, 
        string logId,
        int statusCode
        ) : base(success, message, logId, statusCode)
    {
    }
}