using Game.Engine.Core;

namespace Game.Engine
{
    public class StepInputFactory
    {
        public IStepInput Create(Point fromPoint, Point toPoint)
        {
            return new StepInput(fromPoint, toPoint);
        }
    }
}
