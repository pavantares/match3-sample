using System;
using System.Collections.Generic;
using Game.Engine.Core;
using Game.Engine.Core.Elements;
using Game.Engine.Elements;
using Game.Utilities;

namespace Game.Engine
{
    public class ElementsFactory
    {
        public IAppleElement CreateAppleElement(Point point)
        {
            return new AppleElement(IdGenerator.Get(), point);
        }

        public IFishElement CreateFishElement(Point point)
        {
            return new FishElement(IdGenerator.Get(), point);
        }

        public IIceCreamElement CreateIceCreamElement(Point point)
        {
            return new IceCreamElement(IdGenerator.Get(), point);
        }

        public IJellyBearElement CreateJellyBearElement(Point point)
        {
            return new JellyBearElement(IdGenerator.Get(), point);
        }

        public IPieElement CreatePieElement(Point point)
        {
            return new PieElement(IdGenerator.Get(), point);
        }

        public List<Func<Point, IElement>> GetCreateElementMethods()
        {
            return new List<Func<Point, IElement>>
            {
                CreateAppleElement,
                CreateFishElement,
                CreateIceCreamElement,
                CreateJellyBearElement,
                CreatePieElement
            };
        }
    }
}
