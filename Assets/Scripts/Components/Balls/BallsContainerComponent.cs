using System.Collections.Generic;
using Pool.Common;
using UnityEngine;

namespace Pool.Balls
{
    public class BallsContainerComponent : Component<BallsContainer>
    {
        [SerializeField] private Rigidbody2D _playerBall;
        [SerializeField] private List<Rigidbody2D> _fieldBalls;
        
        protected override BallsContainer CreateObject()
        {
            return new BallsContainer(_playerBall, _fieldBalls);
        }
    }
}