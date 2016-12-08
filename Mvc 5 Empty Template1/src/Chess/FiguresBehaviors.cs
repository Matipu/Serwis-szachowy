using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.src.Chess.Figures;

namespace SerwisSzachowy.src.Chess
{
    static public class FiguresBehaviors
    {
        public static bool isPossibleToMove(Figure figure, int x, int y, Figure[][] figures)
        {
            if(figures[x][y] == null || figures[x][y].color != figure.color)
            {
                return true;
            }
            return false;
        }
    }
}