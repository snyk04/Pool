using System;
using Pool.Balls;
using UnityEngine;

namespace Pool.Audio
{
    public class BallHitterSound : Sound
    {
        [Header("References")]
        [SerializeField] private BallHitterComponent _ballHitter;
        
        private void Awake()
        {
            _ballHitter.Object.OnHit += PlaySound;
        }
    }
}