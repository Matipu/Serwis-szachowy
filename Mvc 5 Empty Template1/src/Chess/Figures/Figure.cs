using Chess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.src.Chess.Figures
{
    abstract public class Figure
    {
        public const int PAWN_VALUE = 1;
        public const int BISHOP_VALUE = 3;
        public const int ROOK_VALUE = 5;
        public const int KNIGHT_VALUE = 3;
        public const int QUEEN_VALUE = 8;
        public const int KING_VALUE = 9001;

        public float value { get; set; } //material and position value
        public String color { get; set; }
        public Coordinate position { get; set; }
        public Figure(String color, int x, int y)
        {
            position = new Coordinate(x, y);
            this.color = color;
        }


        abstract public int gerMaterialValue();
        abstract public String getName();


        abstract public List<Coordinate> getAllPossibleMoves(int line, int collumn, Figure[][] figures);
    }
}