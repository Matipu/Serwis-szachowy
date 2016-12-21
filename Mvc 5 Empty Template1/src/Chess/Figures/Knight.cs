using Chess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.src.Chess.Figures
{
    public class Knight : Figure
    {
        public Knight(String color, int x, int y) : base(color, x, y)
        {

        }
        public override string getName()
        {
            return color + "Knight";
        }
        public override int gerMaterialValue()
        {
            return Figure.KNIGHT_VALUE;
        }

        public override List<Coordinate> getAllPossibleMoves(int line, int collumn, Figure[][] figures)
        {
            List<Coordinate> coordinates = new List<Coordinate>();
            string color = figures[line][collumn].color;

            if (line < 6)
            {
                if (collumn < 7 && !(figures[line + 2][collumn + 1] != null && figures[line + 2][collumn + 1].color == color))
                {
                    coordinates.Add(new Coordinate(line + 2, collumn + 1));
                }
                if (collumn > 0 && !(figures[line + 2][collumn - 1] != null && figures[line + 2][collumn - 1].color == color))
                {
                    coordinates.Add(new Coordinate(line + 2, collumn - 1));
                }
            }

            if (line < 7)
            {
                if (collumn > 1 && !(figures[line + 1][collumn - 2] != null && figures[line + 1][collumn - 2].color == color))
                {
                    coordinates.Add(new Coordinate(line + 1, collumn - 2));
                }
                if (collumn < 6 && !(figures[line + 1][collumn + 2] != null && figures[line + 1][collumn + 2].color == color))
                {
                    coordinates.Add(new Coordinate(line + 1, collumn + 2));
                }
            }

            if (line > 0)
            {
                if (collumn < 6 && !(figures[line - 1][collumn + 2] != null  && figures[line - 1][collumn + 2].color == color))
                {
                    coordinates.Add(new Coordinate(line - 1, collumn + 2));
                }
                if (collumn > 1 && !(figures[line - 1][collumn - 2] != null && figures[line - 1][collumn - 2].color == color))
                {
                    coordinates.Add(new Coordinate(line - 1, collumn - 2));
                }
            }

            if(line > 1)
            {
                if (collumn < 7 && !(figures[line - 2][collumn + 1] != null && figures[line - 2][collumn + 1].color == color))
                {
                    coordinates.Add(new Coordinate(line - 2, collumn + 1));
                }
                if (collumn > 0 && !(figures[line - 2][collumn - 1] != null && figures[line - 2][collumn - 1].color == color))
                {
                    coordinates.Add(new Coordinate(line - 2, collumn - 1));
                }
            }





            return coordinates;
        }
    }
}