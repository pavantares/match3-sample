using System.Collections.Generic;
using Game.Engine.Core;
using Game.Engine.Core.Elements;

namespace Game.Engine
{
    public class Field : IField
    {
        private readonly List<IElement> elements;

        public IReadOnlyList<IElement> Elements => elements;

        public Field(List<IElement> elements)
        {
            this.elements = elements;
        }

        public bool HasElementAt(Point point)
        {
            return elements.Exists(x => x.Point.Equal(point));
        }

        public bool SwapElements(Point fromPoint, Point toPoint)
        {
            var fromElement = GetElementAt(fromPoint);
            var toElement = GetElementAt(toPoint);

            if (fromElement == null || toElement == null)
            {
                return false;
            }

            fromElement.Point = toPoint;
            toElement.Point = fromPoint;

            return true;
        }

        public IElement GetElementAt(Point point)
        {
            return elements.Find(x => x.Point.Equal(point));
        }

        public List<IElement> DeleteAndGetElementByPoints(List<Point> points)
        {
            var deletedElements = new List<IElement>(points.Count);

            for (var i = 0; i < points.Count; i++)
            {
                var element = GetElementAt(points[i]);

                if (element == null)
                {
                    continue;
                }

                elements.Remove(element);
                deletedElements.Add(element);
            }

            return deletedElements;
        }

        public void AddOrReplaceElementByPoint(IElement element)
        {
            elements.RemoveAll(x => x.Point.Equal(element.Point));
            elements.Add(element);
        }

        public void RemoveElement(IElement element)
        {
            elements.RemoveAll(x => x == element);
        }

        public IField Copy()
        {
            var copyElements = new List<IElement>(elements.Count);

            for (var i = 0; i < elements.Count; i++)
            {
                copyElements.Add(elements[i].Copy());
            }

            return new Field(copyElements);
        }
    }
}
