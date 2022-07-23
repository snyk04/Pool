using UnityEngine;

namespace Pool.Balls
{
    public interface IBallHitter
    {
        void Hit(Rigidbody2D ball, Vector2 direction, float force);
    }
}