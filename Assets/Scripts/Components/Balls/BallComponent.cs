using Pool.Common;

namespace Pool.Balls
{
    public class BallComponent : Component<Ball>
    {
        protected override Ball CreateObject()
        {
            return new Ball(gameObject);
        }
    }
}