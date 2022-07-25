using System;
using UnityEngine;

namespace Pool.Balls
{
    public class Pocket : IPocket
    {
        public event Action OnBallFallIntoPocket;

        public void OnCollisionEnter2D(Collision2D col)
        {
            GameObject collisionObject = col.gameObject;
            if (collisionObject.TryGetComponent(out BallComponent ball))
            {
                OnBallFallIntoPocket?.Invoke();
                ball.Object.Destroy();
            }
        }
    }
}