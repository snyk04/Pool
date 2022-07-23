using System;

namespace Pool.Balls
{
    public interface IBall
    {
        event Action OnDestroy;

        void Destroy();
    }
}