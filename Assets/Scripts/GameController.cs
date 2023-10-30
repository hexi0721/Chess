using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using Unity.Burst.CompilerServices;

public class GameController : MonoBehaviour
{
    Transform dragging ; // 棋子位置
    public static GameController G; // GameController本身

    public static bool turn; // false:紅方 true:黑方
    
    Vector3 TargetPos; // 目標點擊位置
    string target; //  target tag
    bool Movable; // 能否移動
    

    public GameObject Arrow_left , Arrow_right;
    public GameObject Focus ; // 瞄準
    GameObject FocusTmp;

    Transform r, b; // 帥 將
    public static bool Isend ; // 判斷帥或將消失
    public static bool JudgeCheckMateTurnIsChange ; // 判斷將軍的回合不能重複

    // 起點
    const int OriginalX = -8;
    const int OriginalY = -9;

    const int x = -9; // 最左
    const int y = -10; // 最下
    const int x1 = 2; // x間隔
    const int y1 = 2; // y間隔

    int round; // 回合

    ChessMovement chessmovement ;

    bool HackMode = false; // 作弊
    
    void Start()
    {
        round = 1;
        Isend = false;
        dragging = null;
        turn = false;
        FocusTmp = null;
        Movable = false;
        JudgeCheckMateTurnIsChange = false;

        G = GetComponent<GameController>();
        chessmovement = new ChessMovement();
    }

    // 要確保Chess將帥射線正確  [執行順序 Chess -> GameController] 因此Chess 將帥射線能指向
    void LateUpdate() 
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

        if (Input.GetKeyDown(KeyCode.W))
        {
            HackMode = !HackMode;

            if (HackMode)
                Debug.Log("作弊開始");
            else
                Debug.Log("作弊關閉");

        }

        JudgeIsEnd(); // 判斷是否結束
        

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

                    Movable = chessmovement.WhichTargetMove(target , TargetPos , dragging);
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
                        Isend = true;
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

                    turn = !turn; // 改變回合
                    Movable = false;
                    if (Isend != true)
                    {
                        
                        switch (turn)
                        {
                            case false:
                                Action.Instance.RoundText.text = "第" + round + "回合 - 紅";

                                
                                break;

                            case true:
                                Action.Instance.RoundText.text = "第" + round + "回合 - 黑";
                                round += 1;
                                
                                break;
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

    private void JudgeIsEnd()
    {
        if (Isend)
        {

            switch (turn)
            {
                case true:
                    Action.Instance.WhoWinText.text = "紅方勝 !";
                    break;

                case false:
                    Action.Instance.WhoWinText.text = "黑方勝 !";
                    break;
            }

            Arrow_left.transform.SetParent(Action.Instance.reset_btn.transform);
            Arrow_left.GetComponent<RectTransform>().localPosition = new Vector3(Action.Instance.reset_btn.transform.localPosition.x + 225, 0, 0);

            Arrow_right.transform.SetParent(Action.Instance.reset_btn.transform);
            Arrow_right.GetComponent<RectTransform>().localPosition = new Vector3(Action.Instance.reset_btn.transform.localPosition.x - 225, 0, 0);

            Action.Instance.gamereturn_btn.SetActive(false);
            Action.Instance.setting_btn.SetActive(false);
            Action.Instance.replay_btn.SetActive(true);
            Action.Instance.menuplain.SetActive(true);

            G.enabled = false;

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


}
