using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class FieldElement: Vector2
    {
        public char Image
        { get; set; } = 'o';

        public FieldElement() { }

        public FieldElement(char image)
        {
            this.Image = image;
        }

        public FieldElement(int x, int y)
        {
            this.x = x;
            this.y = y;
        }       

        public override string ToString()
        {
            Console.SetCursorPosition(x, y);
            return Image.ToString();
        }
        public virtual void Draw()
        {
            Console.SetCursorPosition(x, y);
            Console.Write(Image);
        }

    }
}
