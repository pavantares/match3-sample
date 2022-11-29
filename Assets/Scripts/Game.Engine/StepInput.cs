using Game.Engine.Core;

namespace Game.Engine
{
    public class StepInput : IStepInput
    {
        public Point FromPoint { get; }
        public Point ToPoint { get; }

        public StepInput(Point fromPoint, Point toPoint)
        {
            FromPoint = fromPoint;
            ToPoint = toPoint;
        }
    }
}
