using Pool.Balls;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pool.Input
{
    public class PlayerInput : IPlayerInput
    {
        private const float HitForce = 2.5f;
        
        private readonly IBallHitter _ballHitter;
        private readonly Rigidbody2D _ball;
        private readonly InputAction _inputAction;

        private Vector2 _hitStartPoint;
        
        public PlayerInput(IBallHitter ballHitter, Rigidbody2D ball)
        {
            _ballHitter = ballHitter;
            _ball = ball;
            
            _inputAction = new Controls().Player.Hit;

            _inputAction.started += HandleHitStart;
            _inputAction.performed += HandleHitPerformed;
            
            Enable();
        }

        private void HandleHitStart(InputAction.CallbackContext ctx)
        {
            _hitStartPoint = Touchscreen.current.position.ReadValue();
        }
        private void HandleHitPerformed(InputAction.CallbackContext ctx)
        {
            Vector2 hitPerformedPoint = Touchscreen.current.position.ReadValue();
            Vector2 directionVector = (_hitStartPoint - hitPerformedPoint).normalized;
            
            _ballHitter.Hit(_ball, directionVector, HitForce);
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