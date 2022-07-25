using Pool.Balls;
using Pool.Common;
using UnityEngine;

namespace Pool.Input
{
    public class PlayerInputComponent : Component<PlayerInput>
    {
        [Header("References")]
        [SerializeField] private BallHitterComponent _ballHitter;
        
        [Header("Objects")]
        [SerializeField] private Rigidbody2D _playerBall;
        
        [Header("Settings")]
        [SerializeField] private float _minHitPower;
        [SerializeField] private float _maxHitPower;
        [SerializeField] private float _deltaToHitPowerRatio;
        
        protected override PlayerInput CreateObject()
        {
            return new PlayerInput(_ballHitter.Object, _playerBall, _minHitPower, _maxHitPower, _deltaToHitPowerRatio);
        }
    }
}