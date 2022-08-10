using ChannelEngine.Domain.Interfaces;

namespace ChannelEngine.Domain.Providers;

public class RandomGenerator : IRandomGenerator
{
    public int Generate(int maxValue)
    {
        var random = new Random();

        return random.Next(maxValue);
    }
}