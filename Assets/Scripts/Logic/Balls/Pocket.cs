using UnityEngine;
using Object = UnityEngine.Object;

namespace Pool.Balls
{
    public class Pocket
    {
        public void OnCollisionEnter2D(Collision2D col)
        {
            GameObject collisionObject = col.gameObject;
            if (collisionObject.TryGetComponent(out BallComponent _))
            {
                Object.Destroy(collisionObject);
            }
        }
    }
}