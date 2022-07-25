using System;
using Pool.Balls;
using UnityEngine;
using UnityEngine.UI;

namespace Pool.UI
{
    public class BallVelocityTrackerUserInterface : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private BallVelocityTrackerComponent _ballVelocityTracker;

        [Header("Objects")]
        [SerializeField] private Text _ballsStatusText;

        [Header("Settings")] 
        [SerializeField] private string _ballsAreMovingText;
        [SerializeField] private string _ballsStoppedText;
        
        private void Awake()
        {
            SetBallStatusText(_ballsStoppedText);
            
            _ballVelocityTracker.Object.OnBallsStartedMoving += () => SetBallStatusText(_ballsAreMovingText);
            _ballVelocityTracker.Object.OnBallsStoppedMoving += () => SetBallStatusText(_ballsStoppedText);
        }

        private void SetBallStatusText(string text)
        {
            _ballsStatusText.text = text;
        }
    }
}