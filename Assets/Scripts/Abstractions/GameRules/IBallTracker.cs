using System;

namespace Pool.GameRules
{
    public interface IBallTracker
    {
        event Action OnPlayerBallDestroyed;
        event Action OnAllFieldBallsDestroyed;
    }
}