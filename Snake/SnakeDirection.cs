using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class SnakeDirection : SnakeDirectionBase
    {
        public SnakeDirection(Direction initialDirection): base(initialDirection)
        {

        }
        protected override Vector2 CalculateVector(Direction direction)
        {
            switch(direction)
            {
                case Direction.Left: return new Vector2(-1, 0);
                case Direction.Right: return new Vector2(1, 0);
                case Direction.Up: return new Vector2(0, -1);
                case Direction.Down: return new Vector2(0, 1);
                default: return null;
            }
        }
    }
}
