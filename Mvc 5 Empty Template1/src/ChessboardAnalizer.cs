using SerwisSzachowy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.src.Chess.Figures;

namespace Chess
{
    public class ChessboardAnalizer
    {
        static public void calculateAdvantage(Chessboard chessboard)
        {
            float whiteValue = 0;
            float blackValue = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (chessboard.figures[i][j] != null)
                    {
                        chessboard.figures[i][j].value = getFigureAdvantage(chessboard, chessboard.figures[i][j]);

                        if (chessboard.figures[i][j].color == "w")
                        {
                            whiteValue += chessboard.figures[i][j].value;
                        }
                        else
                        {
                            blackValue += chessboard.figures[i][j].value;
                        }
                    }
                }
            }
            chessboard.whiteValue = whiteValue - 9001;
            chessboard.blackValue = blackValue - 9001;
        }
        static public float getWhiteAdvantage(Chessboard chessboard)
        {
            return ((float)(chessboard.whiteValue)) / ((float)(chessboard.blackValue + chessboard.whiteValue));
        }

        public static bool isCheck(Chessboard chessboard, String color)
        {
            Coordinate kingCoordinate = getKingPosition(chessboard, color);
            

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (chessboard.figures[i][j] != null && chessboard.figures[i][j].color != color)
                    {
                        List<Chess.Coordinate> possiblesMoves = chessboard.figures[i][j].getAllPossibleMoves(i, j, chessboard.figures);

                        for (int k = 0; k < possiblesMoves.Count; k++)
                        {
                            if (possiblesMoves[k].collumn == kingCoordinate.collumn && possiblesMoves[k].line == kingCoordinate.line)
                            {
                                return true;
                            }
                        }
                    }
                }
            }


            return false;
        }

        public static string calculateFinishStatus(Chessboard chessboard)
        {
            if (!haveAnyMove(chessboard, "w"))
            {
                if(isCheck( chessboard,  "w"))
                {
                    return Game.STATUS_BLACK_WIN;
                }
                return Game.STATUS_DRAW;
            }
            if (!haveAnyMove(chessboard, "b"))
            {
                if (isCheck(chessboard, "b"))
                {
                    return Game.STATUS_WHITE_WIN;
                }
                return Game.STATUS_DRAW;
            }
            return null;
        }
        public static Boolean haveAnyMove(Chessboard chessboard, String color)
        {
            Coordinate kingCoordinate = getKingPosition(chessboard, color);
            Boolean haveAnyMove = false;

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (chessboard.figures[i][j] != null && chessboard.figures[i][j].color == color)
                    {
                        List<Chess.Coordinate> possiblesMoves = getPosibleMovesWitchCheck(chessboard, i, j);

                        if(possiblesMoves.Count > 0)
                        {
                            haveAnyMove = true;
                            break;
                        }
                    }
                }
                if(haveAnyMove == true)
                {
                    break;
                }
            }

            return haveAnyMove;
        }

        static public Coordinate getKingPosition(Chessboard chessboard, String color)
        {
            Coordinate kingCoordinate = null;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (chessboard.figures[i][j] != null && chessboard.figures[i][j].color == color && chessboard.figures[i][j].getName() == color + "King")
                    {
                        kingCoordinate = new Coordinate(i, j);
                        break;
                    }
                }
                if (kingCoordinate != null)
                {
                    break;
                }
            }
            return kingCoordinate;
        }

        static public List<Chess.Coordinate> getPosibleMovesWitchCheck(Chessboard chessboard, int line, int collumn)
        {
            List<Chess.Coordinate> possiblesMoves = chessboard.getAllPossibleMoves(line, collumn);
            String color = chessboard.figures[line][collumn].color;
            Boolean isPossibleMove;
            for (int k = 0; k < possiblesMoves.Count; k++)
            {
                isPossibleMove = true;
                Figure zbitaFigura = chessboard.figures[possiblesMoves[k].line][possiblesMoves[k].collumn];
                chessboard.makeAMove(line, collumn, possiblesMoves[k].line, possiblesMoves[k].collumn);
                if (ChessboardAnalizer.isCheck(chessboard, color))
                {
                    isPossibleMove = false;
                }
                chessboard.makeAMove(possiblesMoves[k].line, possiblesMoves[k].collumn, line, collumn);
                if (zbitaFigura != null)
                {
                    chessboard.addFigure(zbitaFigura);
                }
                if (isPossibleMove == false)
                {
                    possiblesMoves.Remove(possiblesMoves[k]);
                    k--;
                }
            }
            return possiblesMoves;
        }
        static public float distanceAdvantage(int line, int collumn)
        {
            if (line >= 2 && line <= 5 && collumn >= 2 && collumn <= 5)
            {
                if (line >= 3 && line <= 4 && collumn >= 3 && collumn <= 4)
                {
                    return 1.2f;
                }
                return 1.1f;
            }

            return 1.0f;
        }

        static public float getFigureAdvantage(Chessboard chessboard, Figure figure)
        {
            if (chessboard.figures[figure.position.line][figure.position.collumn].gerMaterialValue() != 9001)
            {
                return chessboard.figures[figure.position.line][figure.position.collumn].gerMaterialValue() * distanceAdvantage(figure.position.line,figure.position.collumn);
            }
            else
            {
                return chessboard.figures[figure.position.line][figure.position.collumn].gerMaterialValue();
            }
        }
    }
}