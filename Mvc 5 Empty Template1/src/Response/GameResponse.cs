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
        public int numberOfMovements;
        public List<Move> history;
        public String finishStatus { get; set; }
        public GameResponse(String[][] figures, String playerColor, DateTime startDate, int id, string difficult, List<Move> history, int numberOfMovements, String finishStatus)
        {
            this.figures = figures;
            this.playerColor = playerColor;
            this.startDate = startDate;
            this.id = id;
            this.difficult = difficult;
            this.history = history;
            this.numberOfMovements = numberOfMovements;
            this.finishStatus = finishStatus;
        }

    }
}