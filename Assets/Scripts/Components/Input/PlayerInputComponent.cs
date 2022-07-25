using Pool.Balls;
using Pool.Common;
using Pool.GameRules;
using UnityEngine;

namespace Pool.Input
{
    public class PlayerInputComponent : Component<PlayerInput>
    {
        [Header("References")]
        [SerializeField] private BallHitterComponent _ballHitter;
        [SerializeField] private GameCycleComponent _gameCycleComponent;
        [SerializeField] private BallVelocityTrackerComponent _ballVelocityTracker;
        
        [Header("Objects")]
        [SerializeField] private Rigidbody2D _playerBall;
        
        [Header("Settings")]
        [SerializeField] private float _minHitPower;
        [SerializeField] private float _maxHitPower;
        [SerializeField] private float _deltaToHitPowerRatio;
        
        protected override PlayerInput CreateObject()
        {
            return new PlayerInput(_ballHitter.Object, _gameCycleComponent.Object, _ballVelocityTracker.Object, 
                _playerBall, _minHitPower, _maxHitPower, _deltaToHitPowerRatio);
        }
    }
}