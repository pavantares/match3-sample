using System.Collections.Generic;

namespace Game.Utilities
{
    public static class List
    {
        public static List<T> Repeat<T>(int count) where T : struct
        {
            var list = new List<T>(count);

            for (var i = 0; i < count; i++)
            {
                list.Add(new T());
            }

            return list;
        }

        public static List<int> Range(int from, int to)
        {
            if (from >= to)
            {
                return new List<int>();
            }

            var count = to - from;
            var list = new List<int>(count);

            for (var i = 0; i < count; i++)
            {
                list.Add(i + from);
            }

            return list;
        }

        public static List<int> Range(int to)
        {
            return Range(0, to);
        }
    }
}
