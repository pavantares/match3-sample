using Game.Engine.Core;

namespace Game.Engine
{
    public class MatchEngineFactory
    {
        public IMatchEngine Create()
        {
            return new MatchEngine();
        }
    }
}
