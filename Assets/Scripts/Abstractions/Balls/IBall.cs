using System;
using UnityEngine;

namespace Pool.Balls
{
    public interface IBall
    {
        event Action OnDestroy;
        event Action<GameObject> OnCollision;

        void Destroy();
    }
}