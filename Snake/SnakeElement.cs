using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class SnakeElement
        : FieldElement
    {
        public Vector2 CurrentDirection { get; protected set; }
        protected static Queue<Vector2>
            track = new 
             Queue<Vector2>();


        public SnakeElement()            
        {
            Image = 'o';
        }

        public SnakeElement(
            int x, int y)
        {
            this.x = x;
            this.y = y;
        }
                

        public virtual void Move(Vector2 direction,bool isLast)
        {
            CurrentDirection = track.Dequeue();
            x += CurrentDirection.X;
            y += CurrentDirection.Y;
            if (!isLast) track.Enqueue(CurrentDirection);
        }

        public static void CutTailMovement()
        {
            track.Dequeue();
        }
    }
}
