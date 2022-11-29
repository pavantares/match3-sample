﻿using Game.Engine.Core;
using Game.Engine.Core.Elements;

namespace Game.Engine.Elements
{
    public class JellyBearElement : IJellyBearElement
    {
        public string Id { get; }
        public Point Point { get; set; }

        public JellyBearElement(string id, Point point)
        {
            Id = id;
            Point = point;
        }

        public IElement Copy()
        {
            return new JellyBearElement(Id, Point);
        }
    }
}
