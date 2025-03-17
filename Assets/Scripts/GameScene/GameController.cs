using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using Unity.Burst.CompilerServices;
using Unity.Mathematics;
using JetBrains.Annotations;
using Unity.VisualScripting;

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
    public int Interval = 2; // 間隔

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
                TargetPos = pos;

                // 保持棋子在格子的正中央 
                for (int i = x; i < OriginalX + Interval * 8 ; i += Interval)
                {
                    float xtmp = i + Interval;
                    
                    if((pos.x > i) && (pos.x < xtmp))
                    {
                        TargetPos.x = Mathf.RoundToInt(OriginalX + tmp * Interval);
                        break;
                        
                    }

                    tmp += 1;
                }
                tmp = 0;
                for (int j = y; j < OriginalY + Interval * 9 ; j += Interval)
                {
                    int ytmp = j + Interval;
                    
                    if ((pos.y > j) && (pos.y < ytmp))
                    {
                        TargetPos.y = Mathf.RoundToInt(OriginalY + tmp * Interval);
                        break;
                    }

                    tmp += 1;
                }

                
                Movable = !HackMode ? chessMovement.WhichTargetMove(target, TargetPos, dragging) : true;


                if(r == null && b == null)
                {
                    r = GameObject.FindWithTag("red2").transform;
                    b = GameObject.FindWithTag("black2").transform;
                }

                //判斷是否超出棋盤
                // Debug.Log(Movable);
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

                        RectTransform rt = action.RoundText.GetComponent<RectTransform>();
                        float range = 20f;
                        float half = 2f;
                        if (!Turn)
                        {

                            action.RoundText.text = "第" + round + "回合 - 紅方";
                            action.RoundText.color = Color.red;
                            rt.anchorMin = new Vector2(0f, 1f);
                            rt.anchorMax = new Vector2(0f, 1f);
                            rt.localRotation = Quaternion.Euler(0, 0, 0);
                            rt.anchoredPosition = new Vector2((rt.rect.width / half) + range , -(rt.rect.height / half) - range);

                        }
                        else
                        {
                            action.RoundText.text = "第" + round + "回合 - 黑方";
                            action.RoundText.color = Color.black;
                            round += 1;

                            rt.anchorMin = new Vector2(1f, 0f);
                            rt.anchorMax = new Vector2(1f, 0f);
                            rt.localRotation = Quaternion.Euler(0, 0, 180);
                            rt.anchoredPosition = new Vector2(-(rt.rect.width / half) - range, (rt.rect.height / half) + range);
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
