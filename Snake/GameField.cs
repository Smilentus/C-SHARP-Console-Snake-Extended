using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Snake
{
    public class GameField
    {
        const int StepDelay = 200;
        //int fieldWidth, fieldHeight;          
        protected Vector2 fieldBottomRight, fieldTopLeft = new Vector2();
        protected Snake snake = new Snake();
        SnakeDirectionBase direction = new SnakeDirection(Direction.Right);
        protected List<FieldElement> foodList = new List<FieldElement>();
        List<FieldElement> walls = new List<FieldElement>();

        public GameField()
        {
            fieldBottomRight = new Vector2(Console.WindowWidth,Console.WindowHeight);
        }
        
        public void Play()
        {
            var inputThread =new Thread(GetInput);
            inputThread.Start();

            Console.CursorVisible = false;

            while (true)
            {
                Thread.Sleep(StepDelay);
                Console.Clear();
                DrawFood();
                WordManager.ColorLetters();
                GenerateLetters();
                GenerateFood();
                GenerateWalls();
                var nextStepDirection = direction.GetDirectionByCurrent();
                Eat(nextStepDirection);
                if (!CanUpdate(nextStepDirection))
                {
                    inputThread.Abort();
                    break;
                }               
            }
        }
        protected virtual void DrawFood()
        {
            foreach (var item in foodList)
            {
                item.Draw();
            }
        }

        private void GenerateNewWord()
        {
            var rnd = new Random();

            WordManager.currentWord.Clear();
            WordManager.leftLetters.Clear();
            WordManager.fullWord = "";
            foreach (char ch in WordManager.wordsList[rnd.Next(0, WordManager.wordsList.Length)])
            {
                WordManager.leftLetters.Add(ch);
                WordManager.fullWord += ch;
            }
            WordManager.template = new char[WordManager.fullWord.Length];
            for(int i = 0; i < WordManager.template.Length; i++)
            {
                WordManager.template[i] = '.';
            }
            WordManager.isGenerated = true;
        }

        public void GenerateLetters()
        {
            var rnd = new Random();

            if (WordManager.wordsPos.Count > 0)
            {
                return;
            }
            else
            {
                WordManager.isSpawned = false;
            }
            if (WordManager.isSpawned)
            {
                return;
            }
            if (!WordManager.isGenerated)
            {
                GenerateNewWord();
            }
            if (WordManager.leftLetters.Count == 0)
            {
                WordManager.guessedWords++;
                WordManager.isGenerated = false;
                GenerateNewWord();
            }

            Vector2 letterElementPos;
            do
            {
                int x = rnd.Next(fieldTopLeft.X, fieldBottomRight.X);
                int y = rnd.Next(fieldTopLeft.Y, fieldBottomRight.Y);

                letterElementPos = new Vector2(x, y);
            } while (FallsOverExistingPoint(letterElementPos));

            var letterPiece = new FieldElement(letterElementPos.X, letterElementPos.Y);
            letterPiece.Image = WordManager.leftLetters[0];
            WordManager.wordsPos.Add(letterPiece);
            foodList.Add(letterPiece);
            WordManager.leftLetters.RemoveAt(0);
        }

        void Eat(Vector2 nextStepDirection)
        {
            var head = snake.Head;
            if (head.CurrentDirection is null) return;
            var foodPiece = GetFood(head + nextStepDirection);
            if (foodPiece == null)
            {
                return;
            }

            foodList.Remove(foodPiece);
            snake.Grow();            
        }

        bool BiteOneself()
        {
            foreach (var segment in snake.Body)
            {
                if (snake.HasIntersection(segment, segment)) return true;
            }
            return false;
        }
        protected bool HitWall(Vector2 moveDirection)
        {
            var nextHeadPosition = snake.Head + moveDirection;
            return nextHeadPosition.X == fieldBottomRight.X || nextHeadPosition.X < fieldTopLeft.X||
                nextHeadPosition.Y == fieldBottomRight.Y || nextHeadPosition.Y < fieldTopLeft.Y;
        }
        protected virtual bool CanUpdate(Vector2 moveDirection)
        {
            if (BiteOneself()|| HitWall(moveDirection) || HitRndWall(moveDirection))
            {
                var fieldSize = fieldBottomRight - fieldTopLeft;
                Console.SetCursorPosition(fieldTopLeft.X+fieldSize.X / 2 - 5, fieldTopLeft.Y+fieldSize.Y / 2);
                Console.BackgroundColor = ConsoleColor.Red;
                Console.Write("GAME OVER!");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(fieldTopLeft.X + fieldSize.X / 2 - 14, fieldTopLeft.Y + fieldSize.Y / 2 + 1);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("[Press Enter Key To Exit > ... ]");
                Console.ForegroundColor = ConsoleColor.White;
                Console.ReadKey();
                return false;
            }
            if(HitLetter(moveDirection))
            {
                WordManager.AddLetter(moveDirection);
            }
            snake.Move(moveDirection);
            return true;
        }

        private bool HitRndWall(Vector2 moveDirection)
        {
            var nextHeadPosition = snake.Head + moveDirection;
            for(int i = 0; i < walls.Count; i++)
            {
                if(walls[i].X == nextHeadPosition.X && walls[i].Y == nextHeadPosition.Y)
                {
                    return true;
                }
            }
            return false;
        }

        private bool HitLetter(Vector2 moveDirection)
        {
            var nextHeadPosition = snake.Head + moveDirection;
            for (int i = 0; i < WordManager.wordsPos.Count; i++)
            {
                if (WordManager.wordsPos[i].X == nextHeadPosition.X && WordManager.wordsPos[i].Y == nextHeadPosition.Y)
                {
                    return true;
                }
            }
            return false;
        }

        void GenerateFood()
        {
            var rnd = new Random();
            int probability = rnd.Next(0, 2);
            if (probability != 0) return;

            Vector2 foodPiecePosition;
            for (int i = 0; i < 2; i++)
            {
                do
                {
                    int x = rnd.Next(fieldTopLeft.X, fieldBottomRight.X);
                    int y = rnd.Next(fieldTopLeft.Y, fieldBottomRight.Y);

                    foodPiecePosition = new Vector2(x, y);
                } while (FallsOverExistingPoint(foodPiecePosition));

                var foodPiece = new ColoredEliminatingFood(foodPiecePosition.X, foodPiecePosition.Y);
                foodPiece.Image = '*';
                foodList.Add(foodPiece);
            }

        }

        void GenerateWalls()
        {
            var rnd = new Random();
            int probability = rnd.Next(0, 5);
            if (probability != 0) return;

            Vector2 position;
            do
            {
                int x = rnd.Next(fieldTopLeft.X, fieldBottomRight.X);
                int y = rnd.Next(fieldTopLeft.Y, fieldBottomRight.Y);

                position = new Vector2(x, y);
            } while (FallsOverExistingPoint(position));

            var wallPiece = new FieldElement(position.X, position.Y);
            wallPiece.Image = '#';
            foodList.Add(wallPiece);
            walls.Add(wallPiece);
        }

        public bool FallsOverExistingPoint(Vector2 point)
        {
            var foodPice = GetFood(point);
            var fallsOverSnake = snake.HasIntersection(point);
            return foodPice != null || fallsOverSnake;
        }

        FieldElement GetFood(Vector2 point)
        {
            foreach (var foodPice in foodList)
            {
                if (foodPice.IsCoincident(point)) return foodPice;
            }
            return null;
        }

        void GetInput()
        {
            while (true)
            {
                while (!Console.KeyAvailable)
                {
                    Thread.Sleep(5);
                    var key = Console.ReadKey().Key;

                    direction.SetVectorByInput(key);
                }
            }
        }
    }
}
