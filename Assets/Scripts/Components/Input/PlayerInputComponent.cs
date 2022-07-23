using Pool.Balls;
using Pool.Common;
using UnityEngine;

namespace Pool.Input
{
    public class PlayerInputComponent : Component<PlayerInput>
    {
        [SerializeField] private BallHitterComponent _ballHitter;
        [SerializeField] private Rigidbody2D _ball;
        
        protected override PlayerInput CreateObject()
        {
            return new PlayerInput(_ballHitter.Object, _ball);
        }
    }
}