namespace ChannelEngine.Application.Exceptions;

public class ChannelEngineApiException : Exception
{
    public ChannelEngineApiException()
    {
    }

    public ChannelEngineApiException(string message)
        : base(message)
    {
    }

    public ChannelEngineApiException(string message, Exception inner)
        : base(message, inner)
    {
    }
}