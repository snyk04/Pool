using System;

namespace Pool.Balls
{
    public interface IBallVelocityTracker
    {
        event Action OnBallsStartedMoving;
        event Action OnBallsStoppedMoving;
    }
}