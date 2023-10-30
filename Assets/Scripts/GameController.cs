using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using Unity.Burst.CompilerServices;

public class GameController : MonoBehaviour
{
    Transform dragging ; // �Ѥl��m
    public static GameController G; // GameController����

    public static bool turn; // false:���� true:�¤�
    
    Vector3 TargetPos; // �ؼ��I����m
    string target; //  target tag
    bool Movable; // ��_����
    

    public GameObject Arrow_left , Arrow_right;
    public GameObject Focus ; // �˷�
    GameObject FocusTmp;

    Transform r, b; // �� �N
    public static bool Isend ; // �P�_�өαN����
    public static bool JudgeCheckMateTurnIsChange ; // �P�_�N�x���^�X���୫��

    // �_�I
    const int OriginalX = -8;
    const int OriginalY = -9;

    const int x = -9; // �̥�
    const int y = -10; // �̤U
    const int x1 = 2; // x���j
    const int y1 = 2; // y���j

    int round; // �^�X

    ChessMovement chessmovement ;

    bool HackMode = false; // �@��
    
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

    // �n�T�OChess�N�Ӯg�u���T  [���涶�� Chess -> GameController] �]��Chess �N�Ӯg�u����V
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
                Debug.Log("�@���}�l");
            else
                Debug.Log("�@������");

        }

        JudgeIsEnd(); // �P�_�O�_����
        

        if (Input.GetMouseButtonDown(0)) // �I��
        {
            
            RaycastHit2D hit ;
            RaycastHit2D hit2 ;

            RaycastHit2D redhit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.zero,
                Mathf.Infinity, 1 << LayerMask.NameToLayer("red"));
            RaycastHit2D blackhit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.zero,
                Mathf.Infinity, 1 << LayerMask.NameToLayer("black"));

            if (turn == false) // ����^�X
            {
                hit = redhit;
                hit2 = blackhit;
            }
            else // �¤�^�X
            {
                hit = blackhit;
                hit2 = redhit;
            }

            if (hit)
            {

                 if (Match(hit,turn)) // �P�_�֪��^�X�ְ�
                     dragging = hit.transform;
                 target = hit.transform.tag;

                 
                 if (GameObject.FindWithTag("Focus") == null) // �˷�target�Ϯ�
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

                // �T�{���L�W�X��l�d��
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
                //�P�_�O�_�W�X�ѽL
                if ((pos.x > -9) && (pos.y > -10) && (pos.x <  9 ) && (pos.y < 10 ) && Movable && (dragging.position != TargetPos)) 
                {

                    // �P�_�өαN�O�_�Q�Y
                    if (TargetPos == b.transform.position || TargetPos == r.transform.position)
                    {
                        Isend = true;
                    }
                    
                    // �`��Replay �Ѥl �Ѥl�y�� ���ʮy��
                    Replay.Instance.Chess_Tran.Add(dragging);
                    Replay.Instance.OriginalLocation.Add(dragging.position);
                    Replay.Instance.Destination.Add(TargetPos);


                    dragging.position = TargetPos;
                    FocusTmp.transform.position = TargetPos;

                    /*RaycastHit2D h = Physics2D.Raycast(b.transform.position + Vector3.down, Vector3.down, Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));
                    Debug.Log(1 + " " + h.transform.name);*/

                    if (hit2 && hit2.transform.position == TargetPos) // �����n��
                    {
                        
                        AudioManager.Instance.PlayAuido(AudioManager.Instance.KillAudio);
                        
                        Replay.Instance.isCollision.Add(true);

                        
                    }
                    else
                    {
                        AudioManager.Instance.PlayAuido(AudioManager.Instance.MoveAudio);
                        
                        Replay.Instance.isCollision.Add(false);
                    }

                    turn = !turn; // ���ܦ^�X
                    Movable = false;
                    if (Isend != true)
                    {
                        
                        switch (turn)
                        {
                            case false:
                                Action.Instance.RoundText.text = "��" + round + "�^�X - ��";

                                
                                break;

                            case true:
                                Action.Instance.RoundText.text = "��" + round + "�^�X - ��";
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
                    Action.Instance.WhoWinText.text = "����� !";
                    break;

                case false:
                    Action.Instance.WhoWinText.text = "�¤�� !";
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

    //  �ǰtTAG
    private bool Match(RaycastHit2D hit ,bool turn)  
    {

        if (turn == false)
            return  Regex.IsMatch(hit.transform.tag, "red");
        else
            return  Regex.IsMatch(hit.transform.tag, "black");

    }


}
