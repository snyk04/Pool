using System;
using UnityEngine;

namespace Pool.Balls
{
    public interface IBallHitter
    {
        event Action OnHit;
        
        void Hit(Rigidbody2D ball, Vector2 direction, float force);
    }
}