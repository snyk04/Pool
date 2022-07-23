using Pool.Common;

namespace Pool.Balls
{
    public class BallHitterComponent : Component<BallHitter>
    {
        protected override BallHitter CreateObject()
        {
            return new BallHitter();
        }
    }
}