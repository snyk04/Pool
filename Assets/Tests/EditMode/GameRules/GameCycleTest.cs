using System;
using NUnit.Framework;
using Pool.GameRules;

namespace Pool.Tests.EditMode.GameRules
{
    public class BallTrackerMock : IBallTracker
    {
        public event Action OnPlayerBallDestroyed;
        public event Action OnAllFieldBallsDestroyed;

        public void DestroyPlayerBall()
        {
            OnPlayerBallDestroyed?.Invoke();
        }
        public void DestroyAllFieldBalls()
        {
            OnAllFieldBallsDestroyed?.Invoke();
        }
    }

    public class GameCycleTest
    {
        [Test]
        public void GameOverTest()
        {
            var ballTracker = new BallTrackerMock();
            var gameCycle = new GameCycle(ballTracker);

            bool isGameOverInvoked = false;
            gameCycle.OnGameEnd += type => isGameOverInvoked = type == GameEndType.GameOver;
            
            ballTracker.DestroyPlayerBall();
            
            Assert.IsTrue(isGameOverInvoked);
        }
        [Test]
        public void VictoryTest()
        {
            var ballTracker = new BallTrackerMock();
            var gameCycle = new GameCycle(ballTracker);

            bool isVictoryInvoked = false;
            gameCycle.OnGameEnd += type => isVictoryInvoked = type == GameEndType.Victory;
            
            ballTracker.DestroyAllFieldBalls();
            
            Assert.IsTrue(isVictoryInvoked);
        }
    }
}