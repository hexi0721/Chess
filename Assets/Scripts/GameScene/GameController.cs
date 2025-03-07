using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;
using Unity.Burst.CompilerServices;

public class GameController : MonoBehaviour
{
    Transform dragging ; // �Ѥl��m

    public bool Turn { get; set; } // false:���� true:�¤�
    
    Vector3 TargetPos; // �ؼ��I����m
    string target; //  target tag
    bool Movable; // ��_����
    
    public bool IsEnd { get; set; }


    public GameObject Focus ; // �˷�
    GameObject FocusTmp;

    Transform r, b; // �� �N
    
    public bool JudgeCheckMateTurnIsChange { get; set; } // �P�_�N�x���^�X���୫��

    
    int OriginalX = -8 ,  OriginalY = -9;// �_�I
    int x = -9 ,  y = -10; // �̤U
    int x1 = 2 , y1 = 2; // ���j

    int round;

    ChessMovement chessMovement ;
    Action action;

    bool HackMode = false; // �@��
    
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


    // �n�T�OChess�N�Ӯg�u���T  [���涶�� Chess -> GameController] �]��Chess �N�Ӯg�u����V
    void LateUpdate() 
    {
        if (IsEnd)
            return;

        CheatMode();

        if (Input.GetMouseButtonDown(0)) // �I��
        {
            
            RaycastHit2D hit ;
            RaycastHit2D hit2 ;

            RaycastHit2D redhit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.zero,
                Mathf.Infinity, 1 << LayerMask.NameToLayer("red"));
            RaycastHit2D blackhit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.zero,
                Mathf.Infinity, 1 << LayerMask.NameToLayer("black"));

            if (Turn == false) // ����^�X
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

                 if (Match(hit)) // �P�_�֪��^�X�ְ�
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
                    Movable = chessMovement.WhichTargetMove(target , TargetPos , dragging);
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
                        IsEnd = true;
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

                    Turn = !Turn; // ���ܦ^�X
                    Movable = false;
                    if (IsEnd != true)
                    {



                        if (!Turn)
                        {

                            action.RoundText.text = "��" + round + "�^�X - ��";
                        }
                        else
                        {
                            action.RoundText.text = "��" + round + "�^�X - ��";
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
                Debug.Log("�@���}�l");
            else
                Debug.Log("�@������");

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



    //  �ǰtTAG
    private bool Match(RaycastHit2D hit)  
    {

        if (Turn == false)
            return  Regex.IsMatch(hit.transform.tag, "red");
        else
            return  Regex.IsMatch(hit.transform.tag, "black");

    }


}
