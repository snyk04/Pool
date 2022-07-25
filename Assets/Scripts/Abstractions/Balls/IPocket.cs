using System;

namespace Pool.Balls
{
    public interface IPocket
    {
        event Action OnBallFallIntoPocket;
    }
}