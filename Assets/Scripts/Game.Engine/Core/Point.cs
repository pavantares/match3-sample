namespace Game.Engine.Core
{
    public readonly struct Point
    {
        public readonly int Row;
        public readonly int Column;

        public static Point Undefined => new(-1, -1);

        public bool IsUndefined => Equal(Undefined);

        public Point(int row, int column)
        {
            Row = row;
            Column = column;
        }

        public bool Equal(Point other)
        {
            return Row == other.Row && Column == other.Column;
        }

        public override string ToString()
        {
            return $"Row:{Row}, Column:{Column}";
        }
    }
}
