using System;
using Pool.Balls;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pool.Input
{
    public class PlayerInput : IPlayerInput
    {
        public float HitPower
        {
            get
            {
                float delta = GetBallToTouchVector().magnitude;
                return Math.Clamp(delta * _deltaToHitPowerRatio, _minHitPower, MaxHitPower);
            }
        }
        public float MaxHitPower { get; }

        // TODO : Disable input when game is over
        private readonly IBallHitter _ballHitter;
        private readonly Rigidbody2D _playerBall;
        private readonly InputAction _inputAction;

        private readonly float _minHitPower;
        private readonly float _deltaToHitPowerRatio;
        
        public event Action OnAimStart;
        public event Action OnAimEnd;
        
        public PlayerInput(IBallHitter ballHitter, Rigidbody2D playerBall, float minHitPower, float maxHitPower,
            float deltaToHitPowerRatio)
        {
            _ballHitter = ballHitter;
            _playerBall = playerBall;
            
            _inputAction = new Controls().Player.Hit;

            _minHitPower = minHitPower;
            MaxHitPower = maxHitPower;
            _deltaToHitPowerRatio = deltaToHitPowerRatio;

            _inputAction.started += HandleHitStarted;
            _inputAction.canceled += HandleHitCanceled;
            _inputAction.performed += HandleHitPerformed;
            
            Enable();
        }

        private void HandleHitStarted(InputAction.CallbackContext ctx)
        {
            OnAimStart?.Invoke();
        }
        private void HandleHitCanceled(InputAction.CallbackContext ctx)
        {
            OnAimEnd?.Invoke();
        }
        private void HandleHitPerformed(InputAction.CallbackContext ctx)
        {
            OnAimEnd?.Invoke();

            _ballHitter.Hit(_playerBall, GetBallToTouchVector().normalized, HitPower);
        }
        private Vector2 GetBallToTouchVector()
        {
            // TODO : Handle null
            Vector2 touchPosition = Touchscreen.current.position.ReadValue();
            Vector3 worldTouchPosition = Camera.main.ScreenToWorldPoint(touchPosition);
            return worldTouchPosition - _playerBall.transform.position;
        }
        
        public void Enable()
        {
            _inputAction.Enable();
        }
        public void Disable()
        {
            _inputAction.Disable();
        }
    }
}