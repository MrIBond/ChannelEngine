namespace ChannelEngine.Domain.Exceptions;

public class NegativeStockValueException : System.Exception
{
    public NegativeStockValueException()
    {
    }

    public NegativeStockValueException(string message)
        : base(message)
    {
    }

    public NegativeStockValueException(string message, System.Exception inner)
        : base(message, inner)
    {
    }
}