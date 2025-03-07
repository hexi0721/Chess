using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using Unity.Burst.CompilerServices;

public class GameController : MonoBehaviour
{
    Transform dragging ; // 棋子位置

    public bool Turn { get; set; } // false:紅方 true:黑方
    
    Vector3 TargetPos; // 目標點擊位置
    string target; //  target tag
    bool Movable; // 能否移動
    
    public bool IsEnd { get; set; }


    public GameObject Focus ; // 瞄準
    GameObject FocusTmp;

    Transform r, b; // 帥 將
    
    public bool JudgeCheckMateTurnIsChange { get; set; } // 判斷將軍的回合不能重複

    
    int OriginalX = -8 ,  OriginalY = -9;// 起點
    int x = -9 ,  y = -10; // 最下
    int x1 = 2 , y1 = 2; // 間隔

    int round;

    ChessMovement chessMovement ;
    Action action;

    bool HackMode = false; // 作弊
    
    void Start()
    {
        round = 1;
        IsEnd = false;
        dragging = null;
        Turn = false;
        FocusTmp = null;
        Movable = false;
        JudgeCheckMateTurnIsChange = false;

        chessMovement = new ChessMovement();
        action = GetComponent<Action>();
    }


    // 要確保Chess將帥射線正確  [執行順序 Chess -> GameController] 因此Chess 將帥射線能指向
    void LateUpdate() 
    {
        if (IsEnd)
            return;

        CheatMode();

        if (Input.GetMouseButtonDown(0)) // 點擊
        {
            
            RaycastHit2D hit ;
            RaycastHit2D hit2 ;

            RaycastHit2D redhit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.zero,
                Mathf.Infinity, 1 << LayerMask.NameToLayer("red"));
            RaycastHit2D blackhit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.zero,
                Mathf.Infinity, 1 << LayerMask.NameToLayer("black"));

            if (Turn == false) // 紅方回合
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

                 if (Match(hit)) // 判斷誰的回合誰動
                     dragging = hit.transform;
                 target = hit.transform.tag;

                 
                 if (GameObject.FindWithTag("Focus") == null) // 瞄準target圖案
                 {
                    
                     Instantiate(Focus , dragging.position , Quaternion.identity);
                     FocusTmp = GameObject.FindGameObjectWithTag("Focus");
                     
                 }
                 else
                 {
                     FocusTmp.transform.position = dragging.position;
                 }
                  

            }
            else if (dragging != null)
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0;
                int tmp = 0;
                TargetPos = new Vector3(0 , 0 , 0);

                // 確認有無超出格子範圍
                for (int i = x; i < OriginalX + x1 * 8 ; i += x1)
                {
                    float xtmp = i + x1;
                    
                    if((pos.x > i) && (pos.x < xtmp))
                    {
                        TargetPos.x = OriginalX + tmp * x1;
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
                        break;
                    }

                    tmp += 1;
                }

                

                if (!HackMode)
                {
                    //Movable = WhichTargetMove(target);
                    Movable = chessMovement.WhichTargetMove(target , TargetPos , dragging);
                }
                else
                {
                    Movable = true;
                }


                r = GameObject.FindWithTag("red2").transform;
                b = GameObject.FindWithTag("black2").transform;
                //判斷是否超出棋盤
                if ((pos.x > -9) && (pos.y > -10) && (pos.x <  9 ) && (pos.y < 10 ) && Movable && (dragging.position != TargetPos)) 
                {

                    // 判斷帥或將是否被吃
                    if (TargetPos == b.transform.position || TargetPos == r.transform.position)
                    {
                        IsEnd = true;
                    }
                    
                    // 蒐集Replay 棋子 棋子座標 移動座標
                    Replay.Instance.Chess_Tran.Add(dragging);
                    Replay.Instance.OriginalLocation.Add(dragging.position);
                    Replay.Instance.Destination.Add(TargetPos);


                    dragging.position = TargetPos;
                    FocusTmp.transform.position = TargetPos;

                    /*RaycastHit2D h = Physics2D.Raycast(b.transform.position + Vector3.down, Vector3.down, Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));
                    Debug.Log(1 + " " + h.transform.name);*/

                    if (hit2 && hit2.transform.position == TargetPos) // 播放聲音
                    {
                        
                        AudioManager.Instance.PlayAuido(AudioManager.Instance.KillAudio);
                        
                        Replay.Instance.isCollision.Add(true);

                        
                    }
                    else
                    {
                        AudioManager.Instance.PlayAuido(AudioManager.Instance.MoveAudio);
                        
                        Replay.Instance.isCollision.Add(false);
                    }

                    Turn = !Turn; // 改變回合
                    Movable = false;
                    if (IsEnd != true)
                    {



                        if (!Turn)
                        {

                            action.RoundText.text = "第" + round + "回合 - 紅";
                        }
                        else
                        {
                            action.RoundText.text = "第" + round + "回合 - 黑";
                            round += 1;
                        }

                        JudgeCheckMateTurnIsChange = true;
 
                    }
                }
                else
                {
                    Destroy(FocusTmp);
                }


                dragging = null;

            }

        }

    }

    private void CheatMode()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            HackMode = !HackMode;

            if (HackMode)
                Debug.Log("作弊開始");
            else
                Debug.Log("作弊關閉");

        }

        if (Input.GetKeyDown(KeyCode.Q) && HackMode)
        {

            Destroy(GameObject.FindWithTag("red2"));
            IsEnd = true;
        }
        else if (Input.GetKeyDown(KeyCode.E) && HackMode)
        {
            Destroy(GameObject.FindWithTag("black2"));
            Turn = !Turn;
            IsEnd = true;
        }
    }



    //  匹配TAG
    private bool Match(RaycastHit2D hit)  
    {

        if (Turn == false)
            return  Regex.IsMatch(hit.transform.tag, "red");
        else
            return  Regex.IsMatch(hit.transform.tag, "black");

    }


}
