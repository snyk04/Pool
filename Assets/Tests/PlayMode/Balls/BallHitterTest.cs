using System.Collections;
using NUnit.Framework;
using Pool.Balls;
using UnityEngine;
using UnityEngine.TestTools;

namespace Pool.Tests.PlayMode.Balls
{
    public class BallHitterTest
    {
        private static Vector2[] _directions = { Vector2.up, Vector2.right, Vector2.down, Vector2.left };
        private static float[] _forces = { 1, 2.5f, -3, 0 };
        
        [UnityTest]
        public IEnumerator HitTest(
            [ValueSource(nameof(_directions))] Vector2 direction,
            [ValueSource(nameof(_forces))] float force)
        {
            var ballHitter = new GameObject().AddComponent<BallHitterComponent>();
            var ballRigidbody = new GameObject().AddComponent<Rigidbody2D>();
            ballRigidbody.gravityScale = 0;
            ballHitter.Object.Hit(ballRigidbody, direction, force);

            yield return null;
            
            Assert.AreEqual(direction * force, ballRigidbody.velocity);
        }
    }
}