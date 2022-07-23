using System.Collections;
using NUnit.Framework;
using Pool.Balls;
using UnityEngine;
using UnityEngine.TestTools;

namespace Pool.Tests.PlayMode
{
    public class BallTest
    {
        [UnityTest]
        public IEnumerator BallDestroyTest()
        {
            var ballObject = new GameObject();
            IBall ball = ballObject.AddComponent<BallComponent>().Object;
            
            ball.Destroy();
            yield return null;
            
            Assert.IsTrue(ballObject == null);
        }
        [UnityTest]
        public IEnumerator EventInvokedOnDestroyTest()
        {
            IBall ball = new GameObject().AddComponent<BallComponent>().Object;
            bool isDestroyEventInvoked = false;
            ball.OnDestroy += () => isDestroyEventInvoked = true;
            
            ball.Destroy();
            yield return null;
            
            Assert.IsTrue(isDestroyEventInvoked);
        }
    }
}