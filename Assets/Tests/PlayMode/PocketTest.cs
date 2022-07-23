using System.Collections;
using NUnit.Framework;
using Pool.Balls;
using UnityEngine;
using UnityEngine.TestTools;

namespace Pool.Tests.PlayMode
{
    public class PocketTest
    {
        [UnityTest]
        public IEnumerator BallDestroyTest()
        {
            var pocketObject = new GameObject();
            pocketObject.AddComponent<Rigidbody2D>();
            pocketObject.AddComponent<BoxCollider2D>();
            pocketObject.AddComponent<PocketComponent>();

            var ballObject = new GameObject();
            ballObject.AddComponent<Rigidbody2D>();
            ballObject.AddComponent<CircleCollider2D>();
            ballObject.AddComponent<BallComponent>();

            yield return null;
            yield return null;
            
            Assert.IsTrue(ballObject == null);
        }
    }
}