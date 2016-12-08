using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chess
{
    public class ChessboardAnalizer
    {
        static public float getWhiteAdvantage(Chessboard chessboard)
        {
            int whiteValue = 0;
            int blackValue = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (chessboard.figures[i][j] != null)
                    {
                        if(chessboard.figures[i][j].color == "w")
                        {
                            whiteValue += chessboard.figures[i][j].getValue();
                        }else
                        {
                            blackValue += chessboard.figures[i][j].getValue();
                        } 
                    }
                }
            }
            whiteValue -= 9001;
            blackValue -= 9001;
            return ((float)(whiteValue)) / ((float)(blackValue + whiteValue));
        }
    }
}