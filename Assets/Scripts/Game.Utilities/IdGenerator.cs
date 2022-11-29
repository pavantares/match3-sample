using System;

namespace Game.Utilities
{
    public static class IdGenerator
    {
        public static string Get()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
