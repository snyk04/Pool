using System;
using Pool.Common;
using UnityEngine;

namespace Pool.Balls
{
    public class PocketComponent : Component<Pocket>
    {
        protected override Pocket CreateObject()
        {
            return new Pocket();
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            Object.OnCollisionEnter2D(col);
        }
    }
}