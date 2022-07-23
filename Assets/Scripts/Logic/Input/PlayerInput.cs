using Pool.Balls;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Pool.Input
{
    public class PlayerInput : IPlayerInput
    {
        private const float HitForce = 20;
        
        private readonly IBallHitter _ballHitter;
        private readonly Rigidbody2D _ball;
        private readonly InputAction _inputAction;
        
        public PlayerInput(IBallHitter ballHitter, Rigidbody2D ball)
        {
            _ballHitter = ballHitter;
            _ball = ball;
            
            _inputAction = new Controls().Player.Hit;

            _inputAction.performed += HandleHitPerformed;
            
            Enable();
        }
        
        private void HandleHitPerformed(InputAction.CallbackContext ctx)
        {
            Vector2 hitPerformedPoint = Camera.main.ScreenToWorldPoint(Touchscreen.current.position.ReadValue());
            Vector2 directionVector = ((Vector2) _ball.transform.position - hitPerformedPoint).normalized;
            
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