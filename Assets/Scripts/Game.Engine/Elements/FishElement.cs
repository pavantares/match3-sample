using Game.Engine.Core;
using Game.Engine.Core.Elements;

namespace Game.Engine.Elements
{
    public class FishElement : IFishElement
    {
        public string Id { get; }
        public Point Point { get; set; }

        public FishElement(string id, Point point)
        {
            Id = id;
            Point = point;
        }

        public IElement Copy()
        {
            return new FishElement(Id, Point);
        }
    }
}
