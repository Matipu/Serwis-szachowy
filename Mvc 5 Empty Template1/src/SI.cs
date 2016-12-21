using Chess;
using SerwisSzachowy.Models;
using SerwisSzachowy.Models.Repository;
using SerwisSzachowy.src.Chess.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using WebApplication1.src.Chess.Figures;

namespace Mvc_5_Empty_Template1.src
{
    public class SI
    {
        const int maxGlebokosc = 4;
        string colorSI;
        int userId;
        Game game;

        public Move getNextMove(Chess.Chessboard chessboard, String color)
        {
            colorSI = color;
            Move move = new SerwisSzachowy.Models.Move();
            int maxAlfa = -90000000;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (chessboard.figures[i][j] != null && chessboard.figures[i][j].color == color)
                    {
                        List<Chess.Coordinate> possiblesMoves = chessboard.figures[i][j].getAllPossibleMoves(i, j, chessboard.figures);

                        for (int k = 0; k < possiblesMoves.Count; k++)
                        {
                            Figure zbitaFigura = chessboard.figures[possiblesMoves[k].line][possiblesMoves[k].collumn];
                            chessboard.makeAMove(i, j, possiblesMoves[k].line, possiblesMoves[k].collumn);

                            String kolorPotomka = color == "w" ? "b" : "w";
                            int alfaPotomka = alfaBeta(chessboard, kolorPotomka, 2, -900000, 900000);


                            if (alfaPotomka > maxAlfa)
                            {
                                maxAlfa = alfaPotomka;
                                if (zbitaFigura != null)
                                {
                                    move.compactFigure = zbitaFigura.getName();
                                }
                                move.init = new Coordinate(i, j);
                                move.target = new Coordinate(possiblesMoves[k].line, possiblesMoves[k].collumn);

                            }


                            chessboard.makeAMove(possiblesMoves[k].line, possiblesMoves[k].collumn, i, j);
                            if (zbitaFigura != null)
                            {
                                chessboard.addFigure(zbitaFigura);
                            }
                            //chessboard.figures[possiblesMoves[k].line][possiblesMoves[k].collumn] = zbitaFigura;
                        }
                    }
                }
            }



            return move;
        }
        public int minimax(Chess.Chessboard wezel, int glebokosc, String color)
        {
            return alfaBeta(wezel, color, 0, -90000000, 90000000);
        }
        public int alfaBeta(Chess.Chessboard wezel, string color, int glebokosc, int alfa, int beta)
        {
            int wartosc = (int)(ChessboardAnalizer.getWhiteAdvantage(wezel) * 100000) - 50000;
            if (colorSI == "b")
            {
                wartosc = -wartosc;
            }

            if (maxGlebokosc < glebokosc)
            {
                return wartosc;
            }

            if (color != colorSI) //jeżeli przeciwnik ma zagrać w węźle
            {


                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (wezel.figures[i][j] != null && wezel.figures[i][j].color == color)
                        {
                            List<Chess.Coordinate> possiblesMoves = wezel.figures[i][j].getAllPossibleMoves(i, j, wezel.figures);

                            for (int k = 0; k < possiblesMoves.Count; k++)
                            {
                                Figure zbitaFigura = wezel.figures[possiblesMoves[k].line][possiblesMoves[k].collumn];
                                wezel.makeAMove(i, j, possiblesMoves[k].line, possiblesMoves[k].collumn);

                                String kolorPotomka = color == "w" ? "b" : "w";
                                int betaPotomka = alfaBeta(wezel, kolorPotomka, glebokosc + 1, alfa, beta);


                                if (betaPotomka < beta)
                                {
                                    beta = betaPotomka;
                                }
                                //        jeżeli α≥β
                                //            przerwij przeszukiwanie  {odcinamy gałąź Alfa}
                                wezel.makeAMove(possiblesMoves[k].line, possiblesMoves[k].collumn, i, j);
                                if (zbitaFigura != null)
                                {
                                    wezel.addFigure(zbitaFigura);
                                }
                                //wezel.figures[possiblesMoves[k].line][possiblesMoves[k].collumn] = zbitaFigura;
                            }
                        }
                    }
                }


                return beta;
            }
            else//my mamy zagrać w węźle
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (wezel.figures[i][j] != null && wezel.figures[i][j].color == color)
                        {
                            List<Chess.Coordinate> possiblesMoves = wezel.figures[i][j].getAllPossibleMoves(i, j, wezel.figures);

                            for (int k = 0; k < possiblesMoves.Count; k++)
                            {
                                Figure zbitaFigura = wezel.figures[possiblesMoves[k].line][possiblesMoves[k].collumn];
                                wezel.makeAMove(i, j, possiblesMoves[k].line, possiblesMoves[k].collumn);

                                String kolorPotomka = color == "w" ? "b" : "w";
                                int alfaPotomka = alfaBeta(wezel, kolorPotomka, glebokosc + 1, alfa, beta);


                                if (alfaPotomka > alfa)
                                {
                                    alfa = alfaPotomka;
                                }
                                //        jeżeli α≥β
                                //            przerwij przeszukiwanie  {odcinamy gałąź Beta}
                                wezel.makeAMove(possiblesMoves[k].line, possiblesMoves[k].collumn, i, j);
                                if (zbitaFigura != null)
                                {
                                    wezel.addFigure(zbitaFigura);
                                }
                                //wezel.figures[possiblesMoves[k].line][possiblesMoves[k].collumn] = zbitaFigura;
                            }
                        }
                    }
                }

                return alfa;
            }


            return 0;
        }

        public void makeASIMove(Game game, int userId)
        {
            this.game = game;
            this.userId = userId;
            Thread thr = new Thread(makeASIMoveThread);
            thr.Start();
        }

        public void makeASIMoveThread()
        {
            int start, stop;//czas pracy
            start = Environment.TickCount & Int32.MaxValue;


            GameRepository gameRepository = new GameRepository();
            ChessboardSerializer chessboardSerializer = new ChessboardSerializer();
            Move siMove;
            if (game.idBlackPlayer == -1)
            {
                siMove = this.getNextMove(game.chessboard, "b");
            }
            else
            {
                siMove = this.getNextMove(game.chessboard, "w");
            }

            game.numberOfMovements++;
            game.chessboard.makeAMove(siMove.init.line, siMove.init.collumn, siMove.target.line, siMove.target.collumn);
            game.history.Add(siMove);
            ChessboardAnalizer.calculateFinishStatus(game.chessboard);
            gameRepository.save(userId, game.id, chessboardSerializer.toGameResponse(game));


            stop = Environment.TickCount & Int32.MaxValue;
            gameRepository.logData("Czas pracy: " + (stop - start) + "ms dla " + maxGlebokosc + " głębokości\n");
        }


    }
}