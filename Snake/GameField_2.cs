using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class GameField_2 : GameField
    {
        protected override void DrawFood()
        {
            for (int i = 0; i < foodList.Count; i++)
            {
                var foodPiece = foodList[i];
                if (foodPiece is IEliminatable eliminatable && eliminatable.IsLifetimeElapsed)
                    foodList.Remove(foodPiece);
            }
            base.DrawFood();
        }
        public GameField_2() : base()
        {
            fieldTopLeft = new Vector2(0, 4);
            for(int i = fieldTopLeft.X;i<fieldBottomRight.X;i++)
            {
                var borderBrick = new FieldElement(i, fieldTopLeft.Y - 1);
                borderBrick.Image = '#';
                foodList.Add(borderBrick);
            }
            
            foodList.Add(new SnakeCounterDisplay(5, 1, snake.Body));
        }

    }
}
