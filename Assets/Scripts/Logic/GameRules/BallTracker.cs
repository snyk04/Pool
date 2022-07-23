using System;
using System.Collections.Generic;
using Pool.Balls;

namespace Pool.GameRules
{
    public class BallTracker : IBallTracker
    {
        public event Action OnPlayerBallDestroyed;
        public event Action OnAllFieldBallsDestroyed;

        private int _amountOfFieldBallsLeft;

        public BallTracker(IBall playerBall, IReadOnlyCollection<IBall> fieldBalls)
        {
            playerBall.OnDestroy += () => OnPlayerBallDestroyed?.Invoke();
            
            _amountOfFieldBallsLeft = fieldBalls.Count;
            foreach (IBall fieldBall in fieldBalls)
            {
                fieldBall.OnDestroy += HandleFieldBallDestroyed;
            }
        }

        private void HandleFieldBallDestroyed()
        {
            _amountOfFieldBallsLeft -= 1;
            if (_amountOfFieldBallsLeft == 0)
            {
                OnAllFieldBallsDestroyed?.Invoke();
            }
        }
    }
}