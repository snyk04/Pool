using System;

namespace Pool.GameRules
{
    public interface IGameCycle
    {
        event Action<GameEndType> OnGameEnd;
    }
}