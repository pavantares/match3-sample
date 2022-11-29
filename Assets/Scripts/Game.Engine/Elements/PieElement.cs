using Game.Engine.Core;
using Game.Engine.Core.Elements;

namespace Game.Engine.Elements
{
    public class PieElement : IPieElement
    {
        public string Id { get; }
        public Point Point { get; set; }

        public PieElement(string id, Point point)
        {
            Id = id;
            Point = point;
        }

        public IElement Copy()
        {
            return new PieElement(Id, Point);
        }
    }
}
