using Nancy.Simple.Models;

namespace Nancy.Simple
{
    public interface IStrategy
    {
        int CalculateBet(GameState state);
    }
}
