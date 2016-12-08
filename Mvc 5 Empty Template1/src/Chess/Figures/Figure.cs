using Chess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.src.Chess.Figures
{
    abstract public class Figure
    {
        public String color { get; set; }
        public Coordinate position { get; set; }
        public Figure(String color, int x, int y)
        {
            position = new Coordinate(x, y);
            this.color = color;
        }

        abstract public int getValue();
        abstract public String getName();


        abstract public List<Coordinate> getAllPossibleMoves(int line, int collumn, Figure[][] figures);
    }
}