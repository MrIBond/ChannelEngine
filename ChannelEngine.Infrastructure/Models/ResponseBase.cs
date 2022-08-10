using System.Text.Json.Serialization;
using ChannelEngine.Application.Exceptions;

namespace ChannelEngine.Infrastructure.Models;

public abstract class ResponseBase
{
    protected ResponseBase(
        bool success,
        string message,
        string logId,
        int statusCode
        )
    {
        Success = success;
        Message = message;
        LogId = logId;
        StatusCode = statusCode;
    }

    [JsonPropertyName("Success")]
    public bool Success { get; private set; }

    [JsonPropertyName("Message")]
    public string Message { get; private set; }

    [JsonPropertyName("LogId")]
    public string LogId { get; private set; }

    [JsonPropertyName("StatusCode")]
    public int StatusCode { get; private set; }

    public void EnsureSuccessResponse()
    {
        if (!Success)
        {
            throw 
                new ChannelEngineApiException(
                    $"LogId = {LogId}, Message = {Message}, StatusCode = {StatusCode}"
                    );
        }
    }
}