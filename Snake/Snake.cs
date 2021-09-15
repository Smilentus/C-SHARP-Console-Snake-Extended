using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Snake
    {
        bool toGrow = false;

        Queue<SnakeElement> snakeQueue = new Queue<SnakeElement>();

        SnakeElement tail =
            new SnakeElement();
        SnakeHead head =
            new SnakeHead();

        public SnakeHead Head
        {
            get => head;
        }


        public Queue<SnakeElement> Body
        {
            get => snakeQueue;
        }

        public SnakeElement Tail
        {
            get => tail;
        }

        public Snake(int x= 5,int y = 5)
        {
            head = new SnakeHead(x, y);
            tail = head;
            Head.Image = 'O';           
            Body.Enqueue(head);
        }
        

        public void Grow()
        {
            toGrow = true;
        }

        public void Move(Vector2 direction)
        {
            var tailClone =new SnakeElement( tail.X,tail.Y);

            foreach (var element in Body)
            {
                element.Move(direction,element.Equals(tail)&&!toGrow);
                Console.Write(element);
            }

            if (toGrow)AddElement(tailClone,direction);
            if (Body.Count == 1) SnakeElement.CutTailMovement();
        }

        void AddElement(SnakeElement tailClone,Vector2 direction)
        {
            tail =
                tailClone;
            Body.Enqueue(
                tail);
            toGrow = false;
            Console.Write(tail);
        }
        public bool HasIntersection(Vector2 point, Vector2 elementToSkip = null)
        {
            foreach (var segment in Body)
            {
                if (elementToSkip != null && segment.Equals(elementToSkip))
                    continue;
                if (segment.IsCoincident(point)) return true;
            }
            return false;
        }

    }
}
