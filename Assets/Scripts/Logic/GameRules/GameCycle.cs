using System;

namespace Pool.GameRules
{
    public class GameCycle : IGameCycle
    {
        public event Action<GameEndType> OnGameEnd;

        public GameCycle(IBallTracker ballTracker)
        {
            ballTracker.OnPlayerBallDestroyed += () => OnGameEnd?.Invoke(GameEndType.GameOver);
            ballTracker.OnAllFieldBallsDestroyed += () => OnGameEnd?.Invoke(GameEndType.Victory);
        }
    }
}