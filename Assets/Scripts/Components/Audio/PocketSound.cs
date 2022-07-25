using Pool.Balls;
using UnityEngine;

namespace Pool.Audio
{
    public class PocketSound : Sound
    {
        [Header("References")] 
        [SerializeField] private PocketComponent _pocket;

        private void Awake()
        {
            _pocket.Object.OnBallFallIntoPocket += () =>
            {
                if (Physics2D.simulationMode != SimulationMode2D.Script)
                {
                    PlaySound();
                }
            };
        }
    }
}