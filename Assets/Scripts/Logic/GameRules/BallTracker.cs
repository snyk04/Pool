using System;
using System.Collections.Generic;
using System.Linq;
using Pool.Balls;

namespace Pool.GameRules
{
    public class BallTracker : IBallTracker
    {
        public event Action OnPlayerBallDestroyed;
        public event Action OnAllFieldBallsDestroyed;

        private int _amountOfFieldBallsLeft;

        public BallTracker(IBallsContainer ballsContainer)
        {
            ballsContainer.PlayerBall.GetComponent<BallComponent>().Object.OnDestroy += () => OnPlayerBallDestroyed?.Invoke();
            
            _amountOfFieldBallsLeft = ballsContainer.FieldBalls.Count;
            foreach (IBall fieldBall in ballsContainer.FieldBalls.Select(rb => rb.GetComponent<BallComponent>().Object))
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