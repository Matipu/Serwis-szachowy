using Chess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.src.Chess.Figures;

namespace SerwisSzachowy.Models
{
    public class Move
    {
        public Coordinate init;
        public Coordinate target;
        public string compactFigure;
    }
}