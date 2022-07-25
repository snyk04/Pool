using System;
using System.Collections.Generic;
using Pool.Common;
using Pool.GameRules;
using UnityEngine;

namespace Pool.Balls
{
    public class BallVelocityTrackerComponent : Component<BallVelocityTracker>
    {
        [Header("References")] 
        [SerializeField] private GameCycleComponent _gameCycle;
        
        [Header("Objects")]
        [SerializeField] private List<Rigidbody2D> _balls;

        [Header("Settings")]
        [SerializeField] private float _velocityToStop;
        [SerializeField] private float _angularVelocityToStop;
        
        protected override BallVelocityTracker CreateObject()
        {
            return new BallVelocityTracker(_gameCycle.Object, _balls, _velocityToStop, _angularVelocityToStop);
        }

        private void Update()
        {
            Object.Track();
        }
    }
}