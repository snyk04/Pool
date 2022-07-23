using System;
using NUnit.Framework;
using Pool.Balls;
using Pool.GameRules;

namespace Pool.Tests.EditMode.GameRules
{
    public class BallMock : IBall
    {
        public event Action OnDestroy;
        
        public void Destroy()
        {
            OnDestroy?.Invoke();
        }
    }

    public class BallTrackerTest
    {
        private static readonly IBall PlayerBall = new BallMock();
        private static readonly IBall[] FieldBalls = { new BallMock(), new BallMock(), new BallMock() };

        [Test]
        public void PlayerBallDestroyTrackTest()
        {
            bool isPlayerBallDestroyTracked = false;
            
            var ballTracker = new BallTracker(PlayerBall, FieldBalls);
            ballTracker.OnPlayerBallDestroyed += () => isPlayerBallDestroyTracked = true;
            
            PlayerBall.Destroy();
            
            Assert.IsTrue(isPlayerBallDestroyTracked);
        }
        [Test]
        public void AllFieldBallsDestroyTrackTest()
        {
            bool isAllFieldBallsDestroyTracked = false;
            
            var ballTracker = new BallTracker(PlayerBall, FieldBalls);
            ballTracker.OnAllFieldBallsDestroyed += () => isAllFieldBallsDestroyTracked = true;

            foreach (IBall fieldBall in FieldBalls)
            {
                fieldBall.Destroy();
            }
            
            Assert.IsTrue(isAllFieldBallsDestroyTracked);
        }
    }
}