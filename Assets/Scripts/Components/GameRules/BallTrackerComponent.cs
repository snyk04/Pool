using Pool.Balls;
using Pool.Common;
using UnityEngine;

namespace Pool.GameRules
{
    public class BallTrackerComponent : Component<BallTracker>
    {
        [SerializeField] private BallsContainerComponent _ballsContainer;
        
        protected override BallTracker CreateObject()
        {
            return new BallTracker(_ballsContainer.Object);
        }
    }
}