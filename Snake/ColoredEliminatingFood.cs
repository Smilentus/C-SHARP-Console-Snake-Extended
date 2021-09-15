using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    // исчезание еды
    public interface IEliminatable
    {
        bool IsLifetimeElapsed { get; }
    }
    public class ColoredEliminatingFood: FieldElement, IEliminatable
    {
        static ConsoleColor[] colorsToSelect = new ConsoleColor[]
        {
            ConsoleColor.Blue, ConsoleColor.Yellow, ConsoleColor.Green
        };
        private Random rnd = new Random();
        ConsoleColor selectedColor;
        const int maxLifetime = 25;
        const int minLifetime = 15;
        int lifetime;
        int lifetimeElapsed;

        public bool IsLifetimeElapsed { get { return lifetimeElapsed >= lifetime; } } 

        public ColoredEliminatingFood(int x, int y):base(x, y)
        {
            var colorIndex = rnd.Next(0, colorsToSelect.Length);
            selectedColor = colorsToSelect[colorIndex];
            lifetime = rnd.Next(minLifetime, maxLifetime);
        }
        public override void Draw()
        {
            lifetimeElapsed++;
            var prevColor = Console.ForegroundColor;
            Console.ForegroundColor = selectedColor;
            base.Draw();
            Console.ForegroundColor = prevColor;
        }

        
    }
}
