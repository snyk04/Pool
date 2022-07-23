using Pool.Common;
using UnityEngine;

namespace Pool.GameRules
{
    public class GameCycleComponent : Component<GameCycle>
    {
        [SerializeField] private BallTrackerComponent _ballTracker;
        
        protected override GameCycle CreateObject()
        {
            return new GameCycle(_ballTracker.Object);
        }
    }
}