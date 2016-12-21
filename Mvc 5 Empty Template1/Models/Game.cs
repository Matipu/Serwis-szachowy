using Chess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SerwisSzachowy.Models
{
    public class Game
    {
        public const String STATUS_BLACK_WIN = "b";
        public const String STATUS_WHITE_WIN = "w";
        public const String STATUS_DRAW = "d";

        public int id { get; set; }
        public Chessboard chessboard { get; set; }
        public DateTime startDate { get; set; }
        public String playerColor { get; set; }
        public string difficult { get; set; }
        public List<Move> history { get; set; }
        public int numberOfMovements { get; set; }
        public int idWhitePlayer { get; set; }
        public int idBlackPlayer { get; set; }

        public String finishStatus { get; set; }

        public Game()
        {
            numberOfMovements = 0;
            idWhitePlayer = -1;
            idBlackPlayer = -1;
            history = new List<Move>();
            finishStatus = null;
        }
    }
}