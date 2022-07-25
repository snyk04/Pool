using System.Collections.Generic;
using UnityEngine;

namespace Pool.Balls
{
    public class BallsContainer : IBallsContainer
    {
        public Rigidbody2D PlayerBall { get; }
        public List<Rigidbody2D> FieldBalls { get; }
        
        public BallsContainer(Rigidbody2D playerBall, List<Rigidbody2D> fieldBalls)
        {
            PlayerBall = playerBall;
            FieldBalls = fieldBalls;
            
            FieldBalls.ForEach(rb => { rb.GetComponent<BallComponent>().Object.OnDestroy += () => FieldBalls.Remove(rb); });
        }   
    }
}