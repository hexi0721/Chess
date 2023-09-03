using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    Transform dragging ; // 棋子位置
    public static GameController G;

    public static bool turn; // false:紅方 true:黑方
    int layermask; // 層

    Vector3 TargetPos; // 目標點擊位置
    string target; //  target tag
    bool Movable = false; // 能否移動

    Transform r, b; // 帥 將
    bool Isend = false; // 判斷帥或將消失

    // 起點
    int OriginalX = -8;
    int OriginalY = -9;

    int x = -9; // 最左
    int y = -10; // 最下
    int x1 = 2; // x間隔
    int y1 = 2; // y間隔

    // Text
    Text WhoWinText;
    Text RoundText;
    Text StatText;


    // UI btn
    GameObject gamereturn_btn;

    int round = 1; // 回合

    // 狀態
    GameObject child;
    public Transform stat;

    // Audio
    

    

    // Start is called before the first frame update
    void Start()
    {
        dragging = null;
        turn = false;
        
        WhoWinText = GameObject.Find("whowin").GetComponent<Text>(); // 誰勝誰負文字
        WhoWinText.text = "";

        RoundText = GameObject.Find("round").GetComponent<Text>(); // 回合文字
        RoundText.text = "第" + round + "回合 - 紅";

        StatText = GameObject.Find("stat_txt").GetComponent<Text>(); // 狀態文字
        StatText.text = "";

        

        G = GetComponent<GameController>();

        gamereturn_btn = GameObject.Find("gamereturn_btn"); // 重新開始按鈕

        r = GameObject.FindWithTag("red2").transform;
        b = GameObject.FindWithTag("black2").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Destroy(GameObject.FindWithTag("red2"));
            Isend = true;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            Destroy(GameObject.FindWithTag("black2"));
            turn = !turn;
            Isend = true;
        }

        // 勝負
        if (Isend) 
        {
            
            switch(turn)
            {
                case true:
                    WhoWinText.text = "紅方勝 !";
                    break;

                case false:
                    WhoWinText.text = "黑方勝 !";
                    break;
            }

            gamereturn_btn.SetActive(false);
            Action.menuplain.SetActive(true);
            
            G.enabled = false;

        }
        
       

        if (Input.GetMouseButtonDown(0)) // 點擊
        {

            RaycastHit2D hit ;
            RaycastHit2D hit2 ;

            RaycastHit2D redhit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.zero,
                Mathf.Infinity, 1 << LayerMask.NameToLayer("red"));
            RaycastHit2D blackhit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.zero,
                Mathf.Infinity, 1 << LayerMask.NameToLayer("black"));

            if (turn == false) // 紅方回合
            {
                hit = redhit;
                hit2 = blackhit;
            }
            else // 黑方回合
            {
                hit = blackhit;
                hit2 = redhit;
            }

            if (hit)
            {

                 if (Match(hit,turn)) // 判斷誰的回合誰動
                     dragging = hit.transform;
                 target = hit.transform.tag;
                
            }
            else if (dragging != null)
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0;
                int tmp = 0;
                TargetPos = new Vector3(0,0,0);
                for (int i = x; i < OriginalX + x1 * 8 ; i += x1)
                {
                    float xtmp = i + x1;
                    
                    if((pos.x > i) && (pos.x < xtmp))
                    {
                        
                        TargetPos.x = OriginalX + tmp * x1;
                        //Debug.Log("x " + TargetPos.x);
                        break;
                        
                    }

                    tmp += 1;
                }
                tmp = 0;
                for (int j = y; j < OriginalY + y1 * 9 ; j += y1)
                {
                    int ytmp = j + y1;
                    
                    if ((pos.y > j) && (pos.y < ytmp))
                    {
                        
                        TargetPos.y = OriginalY + tmp * y1;
                        //Debug.Log("y " + TargetPos.y);
                        break;
                    }

                    tmp += 1;
                }
                

                switch (target)
                {
                    // 紅兵走法
                    case "red1": 

                        if (dragging.position.y < 0)
                        {
                            
                            if ((TargetPos.y == dragging.position.y + 2) && (TargetPos.x == dragging.position.x))
                            {
                                Movable = true;
                            }
                        }
                        else if (dragging.position.y > 0)
                        {
                            
                            if (((TargetPos.y == dragging.position.y + 2) && (TargetPos.x == dragging.position.x)) || 
                                ((TargetPos.x == dragging.position.x + 2) && (TargetPos.y == dragging.position.y)) ||
                                ((TargetPos.x == dragging.position.x - 2) && (TargetPos.y == dragging.position.y) ))
                            {
                                Movable = true;
                            }
                        }

                        break;

                    // 黑卒走法
                    case "black1": 

                        if (dragging.position.y > 0)
                        {

                            if ((TargetPos.y == dragging.position.y - 2) && (TargetPos.x == dragging.position.x))
                            {
                                Movable = true;
                            }
                        }
                        else if (dragging.position.y < 0)
                        {

                            if (((TargetPos.y == dragging.position.y - 2) && (TargetPos.x == dragging.position.x)) || 
                                ((TargetPos.x == dragging.position.x + 2) && (TargetPos.y == dragging.position.y)) ||
                                ((TargetPos.x == dragging.position.x - 2) && (TargetPos.y == dragging.position.y)))
                            {
                                Movable = true;
                            }
                        }

                        break;

                    // 紅帥走法
                    case "red2": 

                        hit = Physics2D.Raycast(dragging.position+new Vector3(0,1,0) , Vector2.up,
                                            Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));


                        if ((TargetPos.x <= 2) && (TargetPos.x >= -2) && (TargetPos.y >= -9) && (TargetPos.y <= -5))
                        {
                            if (((TargetPos.y == dragging.position.y + 2) && (TargetPos.x == dragging.position.x)) ||
                                ((TargetPos.y == dragging.position.y - 2) && (TargetPos.x == dragging.position.x)) ||
                                ((TargetPos.x == dragging.position.x + 2) && (TargetPos.y == dragging.position.y)) ||
                                ((TargetPos.x == dragging.position.x - 2) && (TargetPos.y == dragging.position.y)))
                            {
                                Movable = true;
                            }
                        }
                        else if(hit.transform.tag == "black2")
                        {
                            
                            Movable = true;
                        }


                        break;

                    // 黑將走法
                    case "black2": 

                        
                        hit = Physics2D.Raycast(dragging.position - new Vector3(0, 1, 0), Vector2.down,
                                            Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));

                        

                        if ((TargetPos.x <= 2) && (TargetPos.x >= -2) && (TargetPos.y <= 9) && (TargetPos.y >= 5))
                        {
                            if (((TargetPos.y == dragging.position.y + 2) && (TargetPos.x == dragging.position.x)) ||
                                ((TargetPos.y == dragging.position.y - 2) && (TargetPos.x == dragging.position.x)) ||
                                ((TargetPos.x == dragging.position.x + 2) && (TargetPos.y == dragging.position.y)) ||
                                ((TargetPos.x == dragging.position.x - 2) && (TargetPos.y == dragging.position.y)))
                            {
                                Movable = true;
                            }
                        }
                        else if (hit.transform.tag == "red2")
                        {

                            Movable = true;
                        }

                        break;

                    // 紅相走法
                    case "red3": 


                        if (TargetPos.y < 0)
                        {
                            Movable = Chess3CanMove(TargetPos, dragging.position);
                            
                        }


                        break;

                    // 黑象走法
                    case "black3": 

                        if (TargetPos.y > 0)
                        {
                            Movable = Chess3CanMove(TargetPos, dragging.position);
                            
                        }

                        break;


                    case "red4": // 馬
                    case "black4":

                        Movable = Chess4CanMove(TargetPos , dragging.position);


                        break;

                    case "red5": // 炮
                    case "black5":

                        Movable = Chess5CanMove(TargetPos, dragging.position);

                        break;

                    // 車走法
                    case "red6": 
                    case "black6":

                        if ((TargetPos.x == dragging.position.x) || (TargetPos.y == dragging.position.y))
                        {
                            Movable = true;
                        }


                        break;

                    // 紅仕走法
                    case "red7": 

                        if ((TargetPos.x <= 2) && (TargetPos.x >= -2) && (TargetPos.y >= -9) && (TargetPos.y <= -5))
                        {
                            if (((TargetPos.y == dragging.position.y + 2) && (TargetPos.x == dragging.position.x + 2)) ||
                                ((TargetPos.y == dragging.position.y - 2) && (TargetPos.x == dragging.position.x - 2)) ||
                                ((TargetPos.x == dragging.position.x + 2) && (TargetPos.y == dragging.position.y - 2)) ||
                                ((TargetPos.x == dragging.position.x - 2) && (TargetPos.y == dragging.position.y + 2)))
                            {
                                Movable = true;
                            }
                        }

                        break;

                    // 黑士走法
                    case "black7": 

                        if ((TargetPos.x <= 2) && (TargetPos.x >= -2) && (TargetPos.y <= 9) && (TargetPos.y >= 5))
                        {
                            if (((TargetPos.y == dragging.position.y + 2) && (TargetPos.x == dragging.position.x + 2)) ||
                                ((TargetPos.y == dragging.position.y - 2) && (TargetPos.x == dragging.position.x - 2)) ||
                                ((TargetPos.x == dragging.position.x + 2) && (TargetPos.y == dragging.position.y - 2)) ||
                                ((TargetPos.x == dragging.position.x - 2) && (TargetPos.y == dragging.position.y + 2)))
                            {
                                Movable = true;
                            }
                        }

                        break;

                }

                // 判斷帥或將是否被吃
                if (TargetPos == b.transform.position || TargetPos == r.transform.position)
                {
                    Isend = true;
                }

                //判斷是否超出棋盤
                if ((pos.x > -9) && (pos.y > -10) && (pos.x <  9 ) && (pos.y < 10 ) && Movable && (dragging.position != TargetPos)) 
                {


                    dragging.position = TargetPos;

                    if(hit2)
                    {
                        if (hit2.transform.position == TargetPos)
                        {
                            AudioManager.Instance.PlayAuido(AudioManager.Instance.KillAudio);
                        }
                    }
                    else
                    {
                        AudioManager.Instance.PlayAuido(AudioManager.Instance.MoveAudio);
                    }

                    turn = !turn; // 改變回合
                    Movable = false;
                    if (Isend != true)
                    {
                        
                        switch (turn)
                        {
                            case false:
                                RoundText.text = "第" + round + "回合 - 紅";
                                break;

                            case true:
                                RoundText.text = "第" + round + "回合 - 黑";
                                round += 1;
                                break;
                        }

                        
                    }

                    int index = stat.childCount;
                    //Debug.Log(index + "\n");
                    for (int i = index - 1; i >= 0; i--)
                    {
                        //Debug.Log(stat.GetChild(i).gameObject);
                        Destroy(stat.GetChild(i).gameObject);
                    }

                    child = GameObject.Instantiate(GameObject.FindWithTag(target), new Vector3(-14,6,0), Quaternion.identity) as GameObject;
                    child.transform.SetParent(stat);
                    StatText.text = "Move";


                }

                
                dragging = null;
                

            }

        }

    }

    //  匹配TAG
    private bool Match(RaycastHit2D hit ,bool turn)  
    {

        if (turn == false)
            return  Regex.IsMatch(hit.transform.tag, "red");
        else
            return  Regex.IsMatch(hit.transform.tag, "black");

    }

    // 相象走法函數
    private bool Chess3CanMove(Vector3 Tpos , Vector3 pos) 
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

    // 相象碰撞檢測機制
    private bool Chess3RayDirection(Vector3 pos , Vector3 d , string s) 
    {
        
        RaycastHit2D hit = Physics2D.Raycast(pos + d, d,
                                            Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));
        
        switch (s)
        { 
            case "LeftUp":
                
                if (hit && (hit.transform.position.x == pos.x - 2) && (hit.transform.position.y == pos.y + 2))
                {
                    
                    return false;
                }

                break;

            case "RightUp":
                if (hit && (hit.transform.position.x == pos.x + 2) && (hit.transform.position.y == pos.y + 2))
                {
                    
                    return false;
                }

                break;

            case "LeftDown":
                if (hit && (hit.transform.position.x == pos.x - 2) && (hit.transform.position.y == pos.y - 2))
                {
                    
                    return false;
                }

                break;

            case "RightDown":
                if (hit && (hit.transform.position.x == pos.x + 2) && (hit.transform.position.y == pos.y - 2))
                {
                    
                    return false;
                }

                break;

                
        }


        return true;

    }

    // 馬走法函數
    private bool Chess4CanMove(Vector3 Tpos, Vector3 pos)
    {

        if ((Mathf.Abs(Tpos.x - pos.x) + Mathf.Abs(Tpos.y - pos.y) == 6)
                && (Mathf.Abs(Tpos.x - pos.x) != 6)
                && (Mathf.Abs(Tpos.y - pos.y) != 6))
            {

                if  (((Tpos.x == pos.x + 4) && (Chess4RayDirection(pos , Vector3.right , "Right"))) ||
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

    // 馬碰撞檢測機制
    private bool Chess4RayDirection(Vector3 d , Vector3 v , string s)
    {

        RaycastHit2D hit = Physics2D.Raycast(d + v, v,
                                            Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));
        
        switch (s) // 判斷障礙物
        {
            case "Right":
                
                if(hit && (hit.transform.position.x == d.x + 2))
                {
                    
                    
                    return true;
                    
                }
                    

                break;

            case "Left":

                if(hit && (hit.transform.position.x == d.x - 2))
                {
                    
                    
                    return true;
                    
                }
                

                break;

            case "Up":

                if(hit && (hit.transform.position.y == d.y + 2))
                {
                    
                    
                    return true;
                    
                }
                

                break;

            case "Down":

                if(hit && (hit.transform.position.y == d.y - 2))
                {
                    
                    
                    return true;
                    
                }
                

                break;
        }
            
        return false;
    }

    // 炮砲走法函數
    private bool Chess5CanMove(Vector3 Tpos , Vector3 pos)
    {
        if ((Tpos.x == pos.x) || (Tpos.y == pos.y))
        {
            if ((Tpos.x <= pos.x && Tpos.y == pos.y && Chess5RayDirection(Tpos, pos, Vector3.left, Vector3.right, "Left")) ||
                (Tpos.x >= pos.x && Tpos.y == pos.y && Chess5RayDirection(Tpos, pos, Vector3.right, Vector3.left, "Right")) ||
                (Tpos.y >= pos.y && Tpos.x == pos.x && Chess5RayDirection(Tpos, pos, Vector3.up, Vector3.down, "Up")) ||
                (Tpos.y <= pos.y && Tpos.x == pos.x && Chess5RayDirection(Tpos, pos, Vector3.down, Vector3.up, "Down")))
            {

                 return true;
                
            }

        }

        return false;
    }

    // 炮砲碰撞檢測機制
    private bool Chess5RayDirection(Vector3 pos, Vector3 d , Vector3 v1 , Vector3 v2 , string s)
    {   // 原點
        RaycastHit2D hit = Physics2D.Raycast(d + v1, v1 ,
                                            Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));
        // 點擊目標
        RaycastHit2D hit2 = Physics2D.Raycast(pos + v2, v2,
                        Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));
        
        // 點擊位置
        RaycastHit2D hit3 = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.zero,
                        Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));


        switch (s)
        {

            case "Left":

                if (hit && hit.transform.position.x >= pos.x)
                {

                    if (hit2 && hit.transform.position == hit2.transform.position && hit3 && TargetPos == hit3.transform.position)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }

                break;

            case "Right":

                if (hit && hit.transform.position.x <= pos.x)
                {

                    if (hit2 && hit.transform.position == hit2.transform.position && hit3 && TargetPos == hit3.transform.position)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }


                break;


            case "Up":

                if (hit && hit.transform.position.y <= pos.y)
                {

                    if (hit2 && hit.transform.position == hit2.transform.position && hit3 && TargetPos == hit3.transform.position)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                

                break;

            case "Down":

                if (hit && hit.transform.position.y >= pos.y)
                {

                    if (hit2 && hit.transform.position == hit2.transform.position && hit3 && TargetPos == hit3.transform.position)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }

                break;

        }


        return true;

    }




}
