using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public static class WordManager
    {
        public static string[] wordsList = { "FOOD", "BLOCK", "FAST", "SNAKE", "WORD", "COOL", "HELLO" };
        public static List<char> leftLetters = new List<char>();
        public static List<char> currentWord = new List<char>();
        public static List<FieldElement> wordsPos = new List<FieldElement>();
        public static string fullWord = "";
        public static char[] template;
        public static bool isSpawned = false;
        public static bool isGenerated = false;
        public static int guessedWords = 0;

        private static void CorrectLetters(char letter)
        {
            for(int i = 0; i < template.Length; i++)
            {
                if(template[i] == '.')
                {
                    template[i] = letter;
                    break;
                }
            }
        }
            
        private static int lPos(Vector2 pos)
        {
            int ret = 0;
            for(int i = 0; i < wordsPos.Count; i++)
            {
                if(wordsPos[i].X == pos.X && wordsPos[i].Y == pos.Y)
                {
                    ret = i;
                }
            }
            return ret;
        }
            
        public static void AddLetter(Vector2 position)
        {
            char let = wordsPos[lPos(position)].Image;
            currentWord.Add(let);
            CorrectLetters(let);
            wordsPos.RemoveAt(lPos(position));
        }

        public static string getWord()
        {
            if (template == null)
                return "?";

            string str = "";
            foreach(var ch in template)
            {
                str += ch;
            }
            return str;
        }

        public static void ColorLetters()
        {
            foreach(var l in wordsPos)
            {
                Console.SetCursorPosition(l.X, l.Y);
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(l.Image);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
