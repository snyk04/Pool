using UnityEngine;

namespace Pool.Balls
{
    public class BallHitter : IBallHitter
    {
        public void Hit(Rigidbody2D ball, Vector2 direction, float force)
        {
            ball.velocity = direction.normalized * force;
        }
    }
}