using System.Linq;
using Pool.Balls;
using Pool.Common;
using UnityEngine;

namespace Pool.GameRules
{
    public class BallTrackerComponent : Component<BallTracker>
    {
        [SerializeField] private BallComponent _playerBall;
        [SerializeField] private BallComponent[] _fieldBalls;
        
        protected override BallTracker CreateObject()
        {
            return new BallTracker(
                _playerBall.Object,
                _fieldBalls.Select(ballComponent => ballComponent.Object).ToList()
            );
        }
    }
}