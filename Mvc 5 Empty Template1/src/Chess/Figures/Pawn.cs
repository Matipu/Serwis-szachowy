using Chess;
using SerwisSzachowy.src.Chess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.src.Chess.Figures
{
    public class Pawn : Figure
    {
        public Pawn(String color, int x, int y) : base(color, x, y)
        {

        }
        public override string getName()
        {
            return color + "Pawn";
        }
        public override int gerMaterialValue()
        {
            return Figure.PAWN_VALUE;
        }
        public override List<Coordinate> getAllPossibleMoves(int line, int collumn, Figure[][] figures)
        {
            List<Coordinate> coordinates = new List<Coordinate>();
            if (color == "b")
            {
                if (position.line == 1)
                {
                    if (figures[position.line + 1][position.collumn] == null)
                    {
                        coordinates.Add(new Coordinate(position.line + 1, position.collumn));
                        if (figures[position.line + 2][position.collumn] == null)
                        {
                            coordinates.Add(new Coordinate(position.line + 2, position.collumn));
                        }
                    }

                }
                else
                {
                    if (figures[position.line + 1][position.collumn] == null)
                    {
                        coordinates.Add(new Coordinate(position.line + 1, position.collumn));
                    }
                }

                if (position.collumn + 1 < 8 && figures[position.line + 1][position.collumn + 1] != null && figures[position.line + 1][position.collumn + 1].color != this.color)
                {
                    coordinates.Add(new Coordinate(position.line + 1, position.collumn + 1));
                }
                if (position.collumn - 1 >= 0 && figures[position.line + 1][position.collumn - 1] != null && figures[position.line + 1][position.collumn - 1].color != this.color)
                {
                    coordinates.Add(new Coordinate(position.line + 1, position.collumn - 1));
                }
            }

            if (color == "w")
            {
                if(position.line == 6)
                {
                     if (figures[position.line-1][position.collumn] == null)
                    {
                        coordinates.Add(new Coordinate(position.line - 1, position.collumn));
                        if (figures[position.line-2][position.collumn] == null)
                        {
                            coordinates.Add(new Coordinate(position.line-2, position.collumn));
                        }
                    }

                }
                else
                {
                    if (figures[position.line - 1][position.collumn] == null)
                    {
                        coordinates.Add(new Coordinate(position.line - 1, position.collumn));
                    }
                }

                if (position.collumn + 1 < 8 && figures[position.line - 1][position.collumn + 1] != null && figures[position.line - 1][position.collumn + 1].color != this.color)
                {
                    coordinates.Add(new Coordinate(position.line - 1, position.collumn + 1));
                }
                if (position.collumn - 1 >= 0 && figures[position.line - 1][position.collumn - 1] != null && figures[position.line - 1][position.collumn - 1].color != this.color)
                {
                    coordinates.Add(new Coordinate(position.line - 1, position.collumn - 1));
                }


            }

            return coordinates;

        }
    }
}