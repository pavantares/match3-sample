using System.Collections.Generic;
using Game.Engine.Core;

namespace Game.Engine.Utilities
{
    public static class MatchEngineUtility
    {
        public static bool HasMatch(IField field, Point point)
        {
            var templates = GetTemplates(point);

            return templates.Exists(x => HasMatch(field, x));
        }

        public static List<Point> GetMatchPoints(IField field, Point point)
        {
            var templates = GetTemplates(point);
            var matchPoints = new List<Point>();

            for (var i = 0; i < templates.Count; i++)
            {
                var template = templates[i];

                if (HasMatch(field, template))
                {
                    matchPoints.AddRange(template);
                }
            }

            return matchPoints;
        }

        private static bool HasMatch(IField field, List<Point> points)
        {
            for (var i = 0; i < points.Count - 1; i++)
            {
                var element0 = field.GetElementAt(points[i]);
                var element1 = field.GetElementAt(points[i + 1]);

                if (element0 == null || element1 == null || element0.GetType() != element1.GetType())
                {
                    return false;
                }
            }

            return true;
        }

        private static List<List<Point>> GetTemplates(Point point)
        {
            var row = point.Row;
            var column = point.Column;

            var template1 = new List<Point> { point, new(row + 1, column), new(row - 1, column) };
            var template2 = new List<Point> { point, new(row + 1, column), new(row + 2, column) };
            var template3 = new List<Point> { point, new(row - 1, column), new(row - 2, column) };

            var template4 = new List<Point> { point, new(row, column + 1), new(row, column - 1) };
            var template5 = new List<Point> { point, new(row, column + 1), new(row, column + 2) };
            var template6 = new List<Point> { point, new(row, column - 1), new(row, column - 2) };

            return new List<List<Point>>
            {
                template1,
                template2,
                template3,
                template4,
                template5,
                template6
            };
        }
    }
}
