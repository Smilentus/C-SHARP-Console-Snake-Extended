using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class SnakeCounterDisplay : FieldElement
    {
        IEnumerable<FieldElement> Snake;
        public string MessageFormat { get; set; }
        public SnakeCounterDisplay(int x, int y, IEnumerable<FieldElement> Snake):base(x, y)
        {
            this.Snake = Snake;
        }
        public override void Draw()
        {
            Console.SetCursorPosition(x, y);
            var message = MessageFormat;
            if (string.IsNullOrEmpty(message))
            {
                message = "Длина змейки {0} | Собрано слов: {1} | Слово: " + WordManager.getWord();
            }
            Console.Write(message, Snake.Count(), WordManager.guessedWords);
        }
        
    }
}
