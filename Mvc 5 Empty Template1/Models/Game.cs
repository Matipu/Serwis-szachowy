using Chess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SerwisSzachowy.Models
{
    public class Game
    {
        public int id { get; set; }
        public Chessboard chessboard { get; set; }
        public DateTime startDate { get; set; }
        public String playerColor { get; set; }
        public string difficult { get; set; }
        public List<Move> history { get; set; }

        public Game()
        {
            history = new List<Move>();
        }
    }
}