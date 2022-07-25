using System;
using System.Collections.Generic;
using System.Linq;
using Pool.GameRules;
using UnityEngine;

namespace Pool.Balls
{
    public class BallVelocityTracker : IBallVelocityTracker
    {
        private readonly List<Rigidbody2D> _balls;

        private readonly float _velocityToStop;
        private readonly float _angularVelocityToStop;

        public event Action OnBallsStartedMoving;
        public event Action OnBallsStoppedMoving;

        private bool _areBallsMoving;
        private bool _isGameRunning;
        
        public BallVelocityTracker(IGameCycle gameCycle, List<Rigidbody2D> balls, float velocityToStop,
            float angularVelocityToStop)
        {
            _balls = balls;
            _velocityToStop = velocityToStop;
            _angularVelocityToStop = angularVelocityToStop;
            
            gameCycle.OnGameEnd += _ => _isGameRunning = false;
            _balls.ForEach(ball => ball.GetComponent<BallComponent>().Object.OnDestroy += () => _balls.Remove(ball));

            _isGameRunning = true;
        }

        public void Track()
        {
            if (!_isGameRunning)
            {
                return;
            }
            
            if (_balls.Any(IsBallMoving))
            {
                if (!_areBallsMoving)
                {
                    _areBallsMoving = true;
                    OnBallsStartedMoving?.Invoke();
                }
            }
            else
            {
                if (_areBallsMoving)
                {
                    _areBallsMoving = false;
                    OnBallsStoppedMoving?.Invoke();
                }
            }
        }
        
        private bool IsBallMoving(Rigidbody2D ball)
        {
            return ball.velocity.magnitude > _velocityToStop || ball.angularVelocity > _angularVelocityToStop;
        }
    }
}