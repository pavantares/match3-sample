using Game.Engine.Core;

namespace Game.Engine.Extensions
{
    public static class MatchEngineExtensions
    {
        public static IState GetFirstState(this IState state)
        {
            var currentState = state;

            while (true)
            {
                var previousState = currentState.PreviousState;

                if (previousState == null)
                {
                    break;
                }

                currentState = previousState;
            }

            return currentState;
        }

        public static IState GetLastState(this IState state)
        {
            var currentState = state;

            while (true)
            {
                var nextState = currentState.NextState;

                if (nextState == null)
                {
                    break;
                }

                currentState = nextState;
            }

            return currentState;
        }

        public static bool IsTheSameRowOrColumn(this Point point, Point other)
        {
            return point.Row == other.Row || point.Column == other.Column;
        }
    }
}
