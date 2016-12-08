using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.src.Chess.Figures;

namespace Chess
{
    public class Chessboard
    {
        public const string COLOR_WHITE = "w";
        public const string COLOR_BLACK = "b";
        public Figure[][] figures;
        public List<Coordinate>[][] posibleMoves;
        public Chessboard()
        {
            posibleMoves = new List <Coordinate>[8][];
            for(int i = 0; i < 8; i++)
            {
                posibleMoves[i] = new List<Coordinate>[8];
            }
            figures = new Figure[8][];
            figures[0] = new Figure[8] { new Rook("b", 0, 0), new Knight("b", 0, 1), new Bishop("b", 0, 2), new Queen("b", 0, 3), new King("b", 0, 4), new Bishop("b", 0, 5), new Knight("b", 0, 6), new Rook("b", 0, 7) };
            figures[1] = new Figure[8] { new Pawn("b", 1, 0), new Pawn("b", 1, 1), new Pawn("b", 1, 2), new Pawn("b", 1, 3), new Pawn("b", 1, 4), new Pawn("b", 1, 5), new Pawn("b", 1, 6), new Pawn("b", 1, 7) };
            figures[2] = new Figure[8] { null,null,null,null,null,null,null,null };
            figures[3] = new Figure[8] { null,null,null,null,null,null,null,null };
            figures[4] = new Figure[8] { null,null,null,null,null,null,null,null };
            figures[5] = new Figure[8] { null,null,null,null,null,null,null,null };
            figures[6] = new Figure[8] { new Pawn("w", 6, 0), new Pawn("w", 6, 1), new Pawn("w", 6, 2), new Pawn("w", 6, 3), new Pawn("w", 6, 4), new Pawn("w", 6, 5), new Pawn("w", 6, 6), new Pawn("w", 6, 7) };
            figures[7] = new Figure[8] { new Rook("w", 7, 0), new Knight("w", 7, 1), new Bishop("w", 7, 2), new Queen("w", 7, 3), new King("w", 7, 4), new Bishop("w", 7, 5), new Knight("w", 7, 6), new Rook("w", 7, 7) };

        }
        public Chessboard(Figure[][] figures)
        {
            posibleMoves = new List<Coordinate>[8][];
            for (int i = 0; i < 8; i++)
            {
                posibleMoves[i] = new List<Coordinate>[8];
            }
            this.figures = figures;
        }

        public Figure getFigure(int x, int y)
        {
            return figures[x][y];
        }

        public List<Coordinate> getAllPossibleMoves(int line, int collumn)
        {
            if(figures[line][collumn] == null)
            {
                return new List<Coordinate>();
            }
            return figures[line][collumn].getAllPossibleMoves(line, collumn, figures);
        }

    }
}