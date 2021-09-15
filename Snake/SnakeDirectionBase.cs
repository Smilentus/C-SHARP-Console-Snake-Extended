using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public enum Direction
    {
        Left,Right,Up,Down
    }
    public abstract class SnakeDirectionBase
    {
        static Dictionary<ConsoleKey, Direction> directions = new Dictionary<ConsoleKey, Direction>()
        {
            {ConsoleKey.LeftArrow,Direction.Left },
            {ConsoleKey.RightArrow,Direction.Right },
            {ConsoleKey.UpArrow,Direction.Up },
            {ConsoleKey.DownArrow,Direction.Down },
        };
        public Direction CurrentDirection { get; protected set; }
        public SnakeDirectionBase(Direction initalDirection)
        {
            CurrentDirection = initalDirection;
        }
        public void SetVectorByInput(ConsoleKey key)
        {
            directions.TryGetValue(key, out Direction result);
            if (result == Direction.Left && CurrentDirection == Direction.Right ||
                result == Direction.Right && CurrentDirection == Direction.Left ||
                result == Direction.Up && CurrentDirection == Direction.Down ||
                result == Direction.Down && CurrentDirection == Direction.Up)
                return;
            CurrentDirection = result;
        }
        public Vector2 GetDirectionByCurrent()
        {
            return CalculateVector(CurrentDirection);
        }
        protected abstract Vector2 CalculateVector(Direction direction);
    }
}
