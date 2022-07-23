using UnityEngine;

namespace Pool.Balls
{
    public class Pocket
    {
        public void OnCollisionEnter2D(Collision2D col)
        {
            GameObject collisionObject = col.gameObject;
            if (collisionObject.TryGetComponent(out BallComponent ball))
            {
                ball.Object.Destroy();
            }
        }
    }
}