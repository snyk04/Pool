using System;
using UnityEngine;

namespace Pool.Balls
{
    public class BallHitter : IBallHitter
    {
        public event Action OnHit;
        
        public void Hit(Rigidbody2D ball, Vector2 direction, float force)
        {
            OnHit?.Invoke();
            ball.velocity = direction.normalized * force;
        }
    }
}