using System.Collections.Generic;
using Game.Engine.Core.Elements;

namespace Game.Engine.Core
{
    public interface IField
    {
        IReadOnlyList<IElement> Elements { get; }
        bool HasElementAt(Point point);
        bool SwapElements(Point fromPoint, Point toPoint);
        IElement GetElementAt(Point point);
        List<IElement> DeleteAndGetElementByPoints(List<Point> points);
        void AddOrReplaceElementByPoint(IElement element);
        void RemoveElement(IElement element);
        IField Copy();
    }
}
