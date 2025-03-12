using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessMovement
{
    public bool WhichTargetMove(string target , Vector3 TargetPos , Transform dragging)
    {
        RaycastHit2D hit;
        int x = Mathf.RoundToInt(dragging.position.x);
        int y = Mathf.RoundToInt(dragging.position.y);
        Vector3 pos = new Vector3(x, y , 0);
        switch (target)
        {
            // 紅兵走法
            case "red1":

                if (pos.y < 0)
                {
                    if ((TargetPos.y == pos.y + 2) && (TargetPos.x == pos.x))
                    {
                        return true;
                    }
                }
                else if (pos.y > 0)
                {
                    if (((TargetPos.y == pos.y + 2) && (TargetPos.x == pos.x)) ||
                        ((TargetPos.x == pos.x + 2) && (TargetPos.y == pos.y)) ||
                        ((TargetPos.x == pos.x - 2) && (TargetPos.y == pos.y)))
                    {
                        return true;
                    }
                }

                break;

            // 黑卒走法
            case "black1":

                if (pos.y > 0)
                {
                    if ((TargetPos.y == pos.y - 2) && (TargetPos.x == pos.x))
                    {
                        return true;
                    }
                }
                else if (pos.y < 0)
                {
                    if (((TargetPos.y == pos.y - 2) && (TargetPos.x == pos.x)) ||
                        ((TargetPos.x == pos.x + 2) && (TargetPos.y == pos.y)) ||
                        ((TargetPos.x == pos.x - 2) && (TargetPos.y == pos.y)))
                    {
                        return true;
                    }
                }

                break;

            // 紅帥走法
            case "red2":

                hit = Physics2D.Raycast(pos + new Vector3(0, 1, 0), Vector2.up, Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));

                if ((TargetPos.x <= 2) && (TargetPos.x >= -2) && (TargetPos.y >= -9) && (TargetPos.y <= -5))
                {
                    if (((TargetPos.y == pos.y + 2) && (TargetPos.x == pos.x)) ||
                        ((TargetPos.y == pos.y - 2) && (TargetPos.x == pos.x)) ||
                        ((TargetPos.x == pos.x + 2) && (TargetPos.y == pos.y)) ||
                        ((TargetPos.x == pos.x - 2) && (TargetPos.y == pos.y)))
                    {
                        return true;
                    }
                }
                else if (hit.transform.CompareTag("black2"))
                {

                    if (TargetPos == hit.transform.position)
                    {
                        return true;
                    }

                }

                break;

            // 黑將走法
            case "black2":

                hit = Physics2D.Raycast(pos - new Vector3(0, 1, 0), Vector2.down, Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));

                if ((TargetPos.x <= 2) && (TargetPos.x >= -2) && (TargetPos.y <= 9) && (TargetPos.y >= 5))
                {
                    if (((TargetPos.y == pos.y + 2) && (TargetPos.x == pos.x)) ||
                        ((TargetPos.y == pos.y - 2) && (TargetPos.x == pos.x)) ||
                        ((TargetPos.x == pos.x + 2) && (TargetPos.y == pos.y)) ||
                        ((TargetPos.x == pos.x - 2) && (TargetPos.y == pos.y)))
                    {
                        return true;
                    }
                }
                else if (hit.transform.CompareTag("red2"))
                {

                    if (TargetPos == hit.transform.position)
                    {
                        return true;
                    }
                }

                break;

            // 紅相走法
            case "red3":

                if (TargetPos.y < 0)
                {
                    return Chess3CanMove(TargetPos, pos);
                }

                break;


            // 黑象走法
            case "black3":

                if (TargetPos.y > 0)
                {
                    return  Chess3CanMove(TargetPos, pos);
                }

                break;


            case "red4": // 馬
            case "black4":

                return  Chess4CanMove(TargetPos, pos);

            case "red5": // 炮
            case "black5":

                return  Chess5CanMove(TargetPos, pos);

            // 車走法
            case "red6":
            case "black6":

                return  Chess6CanMove(TargetPos, pos);


            // 紅仕走法
            case "red7":

                if ((TargetPos.x <= 2) && (TargetPos.x >= -2) && (TargetPos.y >= -9) && (TargetPos.y <= -5))
                {
                    if (((TargetPos.y == pos.y + 2) && (TargetPos.x == pos.x + 2)) ||
                        ((TargetPos.y == pos.y - 2) && (TargetPos.x == pos.x - 2)) ||
                        ((TargetPos.x == pos.x + 2) && (TargetPos.y == pos.y - 2)) ||
                        ((TargetPos.x == pos.x - 2) && (TargetPos.y == pos.y + 2)))
                    {
                        return true;
                    }
                }

                break;

            // 黑士走法
            case "black7":

                if ((TargetPos.x <= 2) && (TargetPos.x >= -2) && (TargetPos.y <= 9) && (TargetPos.y >= 5))
                {
                    if (((TargetPos.y == pos.y + 2) && (TargetPos.x == pos.x + 2)) ||
                        ((TargetPos.y == pos.y - 2) && (TargetPos.x == pos.x - 2)) ||
                        ((TargetPos.x == pos.x + 2) && (TargetPos.y == pos.y - 2)) ||
                        ((TargetPos.x == pos.x - 2) && (TargetPos.y == pos.y + 2)))
                    {
                        return true;
                    }
                }

                break;

        }
        return false;

    }

    // 相象走法
    private bool Chess3CanMove(Vector3 Tpos, Vector3 pos)
    {

        if ((Tpos.x == pos.x - 4) && (Tpos.y == pos.y + 4) && Chess3RayDirection(pos, new Vector3(-1, 1, 0), "LeftUp") ||
        (Tpos.x == pos.x + 4) && (Tpos.y == pos.y + 4) && Chess3RayDirection(pos, new Vector3(1, 1, 0), "RightUp") ||
        (Tpos.x == pos.x - 4) && (Tpos.y == pos.y - 4) && Chess3RayDirection(pos, new Vector3(-1, -1, 0), "LeftDown") ||
        (Tpos.x == pos.x + 4) && (Tpos.y == pos.y - 4) && Chess3RayDirection(pos, new Vector3(1, -1, 0), "RightDown"))
        {

            return true;
        }

        return false;
    }

    // 相象碰撞檢測
    private bool Chess3RayDirection(Vector3 pos, Vector3 d, string s)
    {

        RaycastHit2D hit = Physics2D.Raycast(pos + d, d, Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));

        if (hit)
        {
            int hitPosX = Mathf.RoundToInt(hit.transform.position.x);
            int hitPosY = Mathf.RoundToInt(hit.transform.position.y);

            switch (s)
            {
                case "LeftUp":

                    if (hitPosX == pos.x - 2 && hitPosY == pos.y + 2)
                    {
                        Debug.Log(hit.transform.name);
                        return false;
                    }

                    break;

                case "RightUp":
                    if (hitPosX == pos.x + 2 && hitPosY == pos.y + 2)
                    {
                        Debug.Log(hit.transform.name);
                        return false;
                    }

                    break;

                case "LeftDown":
                    if (hitPosX == pos.x - 2 && hitPosY == pos.y - 2)
                    {
                        Debug.Log(hit.transform.name);
                        return false;
                    }

                    break;

                case "RightDown":
                    if (hitPosX == pos.x + 2 && hitPosY == pos.y - 2)
                    {
                        Debug.Log(hit.transform.name);
                        return false;
                    }

                    break;

            }


        }

            

        return true;

    }

    // 馬走法
    private bool Chess4CanMove(Vector3 Tpos, Vector3 pos)
    {
        
        if ((Mathf.Abs(Tpos.x - pos.x) + Mathf.Abs(Tpos.y - pos.y) == 6)
                && (Mathf.Abs(Tpos.x - pos.x) != 6)
                && (Mathf.Abs(Tpos.y - pos.y) != 6))
        {

            if (((Tpos.x == pos.x + 4) && (Chess4RayDirection(pos, Vector3.right, "Right"))) ||
                ((Tpos.x == pos.x - 4) && (Chess4RayDirection(pos, Vector3.left, "Left"))) ||
                ((Tpos.y == pos.y + 4) && (Chess4RayDirection(pos, Vector3.up, "Up"))) ||
                ((Tpos.y == pos.y - 4) && (Chess4RayDirection(pos, Vector3.down, "Down"))))
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        return false;
    }

    // 馬碰撞檢測
    private bool Chess4RayDirection(Vector3 d, Vector3 v, string s)
    {
        RaycastHit2D hit = Physics2D.Raycast(d + v, v, Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));

        if(hit)
        {
            int hitPosX = Mathf.RoundToInt(hit.transform.position.x);
            int hitPosY = Mathf.RoundToInt(hit.transform.position.y);

            switch (s) // 判斷障礙物
            {
                case "Right":

                    if (hitPosX == d.x + 2)
                    {
                        return true;
                    }


                    break;

                case "Left":

                    if (hitPosX == d.x - 2)
                    {
                        return true;
                    }


                    break;

                case "Up":

                    if (hitPosY == d.y + 2)
                    {
                        return true;
                    }


                    break;

                case "Down":

                    if (hitPosY == d.y - 2)
                    {
                        return true;
                    }


                    break;
            }
        }

        

        return false;
    }

    // 炮砲走法
    private bool Chess5CanMove(Vector3 Tpos, Vector3 pos)
    {

        if ((Tpos.x < pos.x && Tpos.y == pos.y && Chess5RayDirection(Tpos, pos, Vector3.left, Vector3.right, "Left")) ||
            (Tpos.x > pos.x && Tpos.y == pos.y && Chess5RayDirection(Tpos, pos, Vector3.right, Vector3.left, "Right")) ||
            (Tpos.y > pos.y && Tpos.x == pos.x && Chess5RayDirection(Tpos, pos, Vector3.up, Vector3.down, "Up")) ||
            (Tpos.y < pos.y && Tpos.x == pos.x && Chess5RayDirection(Tpos, pos, Vector3.down, Vector3.up, "Down")))
        {
            return true;
        }

        return false;
    }

    // 炮砲碰撞檢測
    private bool Chess5RayDirection(Vector3 pos, Vector3 d, Vector3 v1, Vector3 v2, string s)
    {   // 原點
        RaycastHit2D hit = Physics2D.Raycast(d + v1, v1,
                                            Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));

        // 目標向哪方發出射線
        RaycastHit2D hit2 = Physics2D.Raycast(pos + v2, v2,
                        Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));

        switch (s)
        {

            case "Left":


                if (hit && hit.transform.position.x >= pos.x)
                {
                    return chess5iner(pos);

                }

                break;

            case "Right":

                if (hit && hit.transform.position.x <= pos.x)
                {
                    return chess5iner(pos);

                }


                break;


            case "Up":


                if (hit && hit.transform.position.y <= pos.y)
                {
                    return chess5iner(pos);

                }


                break;

            case "Down":

                if (hit && hit.transform.position.y >= pos.y)
                {
                    return chess5iner(pos);

                }

                break;

        }


        return true;


        bool chess5iner(Vector3 pos)
        {
            // 點擊位置
            RaycastHit2D hit3 = Physics2D.Raycast(pos, Vector3.zero,
                            Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));

            if (hit3 && hit2 && hit.transform.position == hit2.transform.position && hit.transform.position != hit3.transform.position && hit2.transform.position != hit3.transform.position)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }

    // 車走法
    private bool Chess6CanMove(Vector3 Tpos, Vector3 pos)
    {

        if ((Tpos.x > pos.x && Tpos.y == pos.y && (Chess6RayDirection(Tpos, pos, Vector3.right, "Right"))) ||
            (Tpos.x < pos.x && Tpos.y == pos.y && (Chess6RayDirection(Tpos, pos, Vector3.left, "Left"))) ||
            (Tpos.y > pos.y && Tpos.x == pos.x && (Chess6RayDirection(Tpos, pos, Vector3.up, "Up"))) ||
            (Tpos.y < pos.y && Tpos.x == pos.x && (Chess6RayDirection(Tpos, pos, Vector3.down, "Down"))))
        {
            return true;
        }

        return false;

    }

    // 車碰撞檢測
    private bool Chess6RayDirection(Vector3 Tpos, Vector3 pos, Vector3 v, string s)
    {
        RaycastHit2D hit = Physics2D.Raycast(pos + v, v, Mathf.Infinity, 1 << LayerMask.NameToLayer("red") | 1 << LayerMask.NameToLayer("black"));

        switch (s)
        {
            case "Right":


                if (hit && Tpos.x > hit.transform.position.x)
                {
                    return false;
                }

                break;

            case "Left":


                if (hit && Tpos.x < hit.transform.position.x)
                {
                    return false;
                }


                break;


            case "Up":


                if (hit && Tpos.y > hit.transform.position.y)
                {
                    return false;
                }


                break;

            case "Down":


                if (hit && Tpos.y < hit.transform.position.y)
                {
                    return false;
                }


                break;

        }

        return true;
    }


}
