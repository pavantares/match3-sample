using System.Collections.Generic;

namespace Game.Utilities
{
    public static class ListExtensions
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            for (var i = list.Count; i > 0; i--)
            {
                list.Swap(0, RandomUtil.Range(0, i));
            }
        }

        public static T RandomItem<T>(this IList<T> list)
        {
            if (list.Count == 0)
            {
                return default;
            }

            return list[RandomUtil.Range(list.Count)];
        }

        public static void Remove<T>(this IList<T> list, IList<T> toRemove)
        {
            for (var i = 0; i < toRemove.Count; i++)
            {
                list.Remove(toRemove[i]);
            }
        }

        public static List<T> Copy<T>(this IList<T> list)
        {
            var copy = new List<T>(list.Count);

            for (var i = 0; i < list.Count; i++)
            {
                copy.Add(list[i]);
            }

            return copy;
        }

        private static void Swap<T>(this IList<T> list, int i, int j)
        {
            (list[i], list[j]) = (list[j], list[i]);
        }
    }
}
