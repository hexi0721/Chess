using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMate2
{
    float interval = 2;
    public bool CheckMateOrNot(Transform rKing , Transform bKing ,Transform chess)
    {
        Debug.Log($"{chess.tag}");
        Debug.Log($"{chess.position}");
        
        int rkingX = Mathf.RoundToInt(rKing.position.x);
        int rkingY = Mathf.RoundToInt(rKing.position.y);
        int bkingX = Mathf.RoundToInt(bKing.position.x);
        int bkingY = Mathf.RoundToInt(bKing.position.y);
        int chessX = Mathf.RoundToInt(chess.position.x);
        int chessY = Mathf.RoundToInt(chess.position.y);
        
        // Debug.Log($"{rkingX} {rkingY} {bkingX} {bkingY} {chessX} {chessY}");

        switch (chess.tag)
        {
            case "red1":

                // Debug.Log(1);
                if ((chessX - interval == bkingX && chessY == bkingY) || (chessX == bkingX && chessY + interval == bkingY) || (chessX + interval == bkingX && chessY == bkingY))
                    return true;


                break;
            case "red2":
                break;
            case "red3":
                break;
            case "red4":
                break;
            case "red5":
                break;
            case "red6":
                break;
            case "red7":
                break;

            case "black1":
                if ((chessX - interval == rkingX && chessY == rkingY) || (chessX == rkingX && chessY - interval == rkingY) || (chessX + interval == rkingX && chessY == rkingY))
                    return true;

                break;

        }


        return false;
    }



}
