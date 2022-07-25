using System;

namespace Pool.Input
{
    public interface IPlayerInput
    {
        float HitPower { get; }
        float MaxHitPower { get; }
        
        event Action OnAimStart;
        event Action OnAimEnd;
        
        void Enable();
        void Disable();
    }
}