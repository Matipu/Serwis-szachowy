using Chess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.src.Chess.Figures
{
    public class Bishop : Figure
    {
        public Bishop(String color, int x, int y): base(color, x, y){

        }
        public override string getName(){
            return color + "Bishop";
        }

        public override int gerMaterialValue()
        {
            return Figure.BISHOP_VALUE;
        }
        public override List<Coordinate> getAllPossibleMoves(int line, int collumn, Figure[][] figures)
        {
            List<Coordinate> coordinates = new List<Coordinate>();
            
            int x = collumn;
            int y = line;
            while (x < 7  && y > 0)
            {
                x++;
                y--;
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
                    break;
                }
            }

            x = collumn;
            y = line;
            while (x < 7 && y < 7)
            {
                x++;
                y++;
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
                    break;
                }
            }

            x = collumn;
            y = line;
            while (x > 0 && y > 0)
            {
                x--;
                y--;
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
                    break;
                }
            }

            x = collumn;
            y = line;
            while ( y < 7 && x > 0 )
            {
                x--;
                y++;
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
                    break;
                }
            }
            return coordinates;
        }
    }
}