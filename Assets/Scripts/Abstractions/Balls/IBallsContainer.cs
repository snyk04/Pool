using System.Collections.Generic;
using UnityEngine;

namespace Pool.Balls
{
    public interface IBallsContainer
    {
        Rigidbody2D PlayerBall { get; }
        List<Rigidbody2D> FieldBalls { get; }
    }
}