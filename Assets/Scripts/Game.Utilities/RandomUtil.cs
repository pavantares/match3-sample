using UnityEngine;

namespace Game.Utilities
{
    public static class RandomUtil
    {
        public static int Range(int from, int to)
        {
            return Random.Range(from, to);
        }

        public static int Range(int to)
        {
            return Range(0, to);
        }
    }
}
