using Chess;
using SerwisSzachowy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.src.Chess.Figures;
using WebApplication1.src.Response;

namespace SerwisSzachowy.src.Chess.Serializers
{
    public class ChessboardSerializer
    {
        public GameResponse toGameResponse(Game game)
        {
            
            String[][] names;
            names = new String[8][];
            for (int i = 0; i < 8; i++)
            {
                names[i] = new String[8];
                for (int j = 0; j < 8; j++)
                    if (game.chessboard.getFigure(i,j) != null)
                        names[i][j] = game.chessboard.getFigure(i, j).getName();
            }
            GameResponse gameResponse = new GameResponse(names, game.playerColor, game.startDate, game.id, game.difficult, game.history);
            return gameResponse;
        }

        public Game toGame(GameResponse gameResponse)
        {
            Game game = new Models.Game();

            Figure[][] figures;
            figures = new Figure[8][];
            for (int i = 0; i < 8; i++)
            {
                figures[i] = new Figure[8];
                for (int j = 0; j < 8; j++)
                        figures[i][j] = getFigureByName(gameResponse.figures[i][j], i ,j);
            }
            game.chessboard = new Chessboard(figures);
            game.playerColor = gameResponse.playerColor;
            game.startDate = gameResponse.startDate;
            game.difficult = gameResponse.difficult;
            game.history = gameResponse.history;
            return game;
        }

        public Figure getFigureByName(string name, int x, int y)
        {
            if (name == null)
            {
                return null;
            }

            if (name == "bPawn")
            {
                return new Pawn("b",x,y);
            }
            else if (name == "wPawn")
            {
                return new Pawn("w", x, y);
            }


            else if (name == "bRook")
            {
                return new Rook("b", x, y);
            }
            else if (name == "wRook")
            {
                return new Rook("w", x, y);
            }


            if (name == "bKnight")
            {
                return new Knight("b", x, y);
            }
            else if (name == "wKnight")
            {
                return new Knight("w", x, y);
            }


            else if (name == "bBishop")
            {
                return new Bishop("b", x, y);
            }
            else if (name == "wBishop")
            {
                return new Bishop("w", x, y);
            }


            if (name == "bQueen")
            {
                return new Queen("b", x, y);
            }
            else if (name == "wQueen")
            {
                return new Queen("w", x, y);
            }


            else if (name == "bKing")
            {
                return new King("b", x, y);
            }
            else if (name == "wKing")
            {
                return new King("w", x, y);
            }

            return null;
        }
    }
}