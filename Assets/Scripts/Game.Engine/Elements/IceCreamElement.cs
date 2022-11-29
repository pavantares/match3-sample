using Game.Engine.Core;
using Game.Engine.Core.Elements;

namespace Game.Engine.Elements
{
    public class IceCreamElement : IIceCreamElement
    {
        public string Id { get; }
        public Point Point { get; set; }

        public IceCreamElement(string id, Point point)
        {
            Id = id;
            Point = point;
        }

        public IElement Copy()
        {
            return new IceCreamElement(Id, Point);
        }
    }
}
