using Chess;
using SerwisSzachowy.Models;
using SerwisSzachowy.src.Chess.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.src.Response
{
    public class GameResponse
    {
        public String[][] figures;
        public String playerColor;
        public DateTime startDate;
        public string difficult;
        public int id;
        public List<Move> history;
        public GameResponse(String[][] figures, String playerColor, DateTime startDate, int id, string difficult, List<Move> history)
        {
            this.figures = figures;
            this.playerColor = playerColor;
            this.startDate = startDate;
            this.id = id;
            this.difficult = difficult;
            this.history = history;
        }

    }
}