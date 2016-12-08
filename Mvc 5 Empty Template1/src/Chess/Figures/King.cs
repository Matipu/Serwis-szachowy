using Chess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.src.Chess.Figures
{
    public class King : Figure
    {
        public King(String color, int x, int y) : base(color, x, y)
        {

        }
        public override string getName()
        {
            return color + "King";
        }
        public override int getValue()
        {
            return 9001;
        }
        public override List<Coordinate> getAllPossibleMoves(int line, int collumn, Figure[][] figures)
        {
            List<Coordinate> coordinates = new List<Coordinate>();

            int x = collumn-1;
            int y = line-2;
            if (x >= 0)
            {
                for (int i = 0; i < 3; i++)
                {
                    y++;
                    if (y >= 0 && y < 8)
                    {
                        if (figures[y][x] == null)
                        {
                            coordinates.Add(new Coordinate(y, x));
                        }
                        else
                        {
                            if (figures[y][x].color != figures[line][collumn].color)
                            {
                                coordinates.Add(new Coordinate(y, x));
                            }
                        }
                    }
                }
            }
            x++;
            y = line - 1;
            if (y >= 0)
            {
                if (figures[y][x] == null)
                {
                    coordinates.Add(new Coordinate(y, x));
                }
                else
                {
                    if (figures[y][x].color != figures[line][collumn].color)
                    {
                        coordinates.Add(new Coordinate(y, x));
                    }
                }
            }
            y = line + 1;
            if (y < 8)
            {
                if (figures[y][x] == null)
                {
                    coordinates.Add(new Coordinate(y, x));
                }
                else
                {
                    if (figures[y][x].color != figures[line][collumn].color)
                    {
                        coordinates.Add(new Coordinate(y, x));
                    }
                }
            }


            x++;
            y = line - 2;
            if (x < 8)
            {
                for (int i = 0; i < 3; i++)
                {
                    y++;
                    if (y >= 0 && y < 8)
                    {
                        if (figures[y][x] == null)
                        {
                            coordinates.Add(new Coordinate(y, x));
                        }
                        else
                        {
                            if (figures[y][x].color != figures[line][collumn].color)
                            {
                                coordinates.Add(new Coordinate(y, x));
                            }
                        }
                    }
                }
            }


            return coordinates;
        }
    }
}