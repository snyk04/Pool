using Pool.Balls;
using UnityEngine;

namespace Pool.Audio
{
    public class BallSound : Sound
    {
        [Header("References")] 
        [SerializeField] private BallComponent _ball;

        private void Awake()
        {
            _ball.Object.OnCollision += obj =>
            {
                if (Physics2D.simulationMode != SimulationMode2D.Script)
                {
                    if (obj.TryGetComponent(out BallComponent _))
                    {
                        PlaySound();
                        Debug.Log("playing");
                    }
                }
            };
        }
    }
}