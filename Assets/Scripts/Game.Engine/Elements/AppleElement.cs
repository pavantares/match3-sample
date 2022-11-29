using Game.Engine.Core;
using Game.Engine.Core.Elements;

namespace Game.Engine.Elements
{
    public class AppleElement : IAppleElement
    {
        public string Id { get; }
        public Point Point { get; set; }

        public AppleElement(string id, Point point)
        {
            Id = id;
            Point = point;
        }

        public IElement Copy()
        {
            return new AppleElement(Id, Point);
        }
    }
}
