using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class SnakeHead :
        SnakeElement
    {        
        public SnakeHead()
        {
            x = 10;
            y = 10;
        }
        public SnakeHead(int x, int y)
        {
            this.x = x;
            this.y = y;
        }


        public override void Move(Vector2 direction,bool isLast)
        {
            x += direction.X;
            y += direction.Y;
            CurrentDirection = direction;
            track.Enqueue(direction);
        }
    }
}
