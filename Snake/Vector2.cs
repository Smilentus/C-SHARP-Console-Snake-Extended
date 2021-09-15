using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class Vector2
    {
        protected int x, y;
        public virtual int X
        {
            get => x;
        }
        public virtual int Y
        {
            get => y;
        }
        public Vector2(int x,int y)
        {
            this.x = x;
            this.y = y;
        }
        public Vector2()
        {

        }

        public static Vector2 operator +(Vector2 a,Vector2 b)
        {
            var result = new Vector2();
            result.x = a.x + b.x;
            result.y = a.y + b.y;
            return result;
        }
        //------------------------------------------------------------------------------------------//
        public bool IsCoincident(Vector2 a)
        {
            return a.x == x && a.y == y;
        }

        public static Vector2 Razn(Vector2 a, Vector2 b)
        {
            Vector2 res = new Vector2(a.x,a.y);
            return res;
        }
        public static Vector2 operator -(Vector2 a, Vector2 b)
        {
            return Vector2.Razn(a, b);
        }
    }
}
