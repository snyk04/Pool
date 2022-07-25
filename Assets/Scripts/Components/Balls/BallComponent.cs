using System;
using Pool.Common;
using UnityEngine;

namespace Pool.Balls
{
    public class BallComponent : Component<Ball>
    {
        protected override Ball CreateObject()
        {
            return new Ball(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            Object.OnCollisionEnter2D(col);
        }
    }
}