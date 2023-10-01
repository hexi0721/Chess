using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;

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
    Transform r, b; // �� �N
    public static bool Isend ; // �P�_�өαN����

    // �_�I
    int OriginalX = -8;
    int OriginalY = -9;

    int x = -9; // �̥�
    int y = -10; // �̤U
    int x1 = 2; // x���j
    int y1 = 2; // y���j

    int round; // �^�X

    // ���A
    GameObject child;
    public Transform stat;

    GameObject FocusTmp ;

    // Start is called before the first frame update
    void Start()
    {
        round = 1;
        Isend = false;
        dragging = null;
        turn = false;
        FocusTmp = null;
        Movable = false;

        G = GetComponent<GameController>();

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

        // �ӭt
        if (Isend) 
        {
            
            switch(turn)
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

                // �T�{���L�W�X�ѽL�d��
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

                Movable = IsTargetCanMove(target , dragging, TargetPos);


                //�P�_�O�_�W�X�ѽL
                if ((pos.x > -9) && (pos.y > -10) && (pos.x <  9 ) && (pos.y < 10 ) && Movable && (dragging.position != TargetPos)) 
                {

                    // �P�_�өαN�O�_�Q�Y
                    if (TargetPos == b.transform.position || TargetPos == r.transform.position)
                    {
                        Isend = true;
                    }

                    Replay.Instance.Chess_Tran.Add(dragging);
                    Replay.Instance.OriginalLocation.Add(dragging.position);
                    Replay.Instance.Destination.Add(TargetPos);

                    dragging.position = TargetPos;
                    FocusTmp.transform.position = TargetPos;

                    bool ischeckmate = false;
                    if(turn == false)
                    {
                        if (IsTargetCanMove(target, dragging, b.transform.position))
                        {
                            Debug.Log("����N�x");
                            ischeckmate = true;
                        }
                    }
                    else
                    {
                        if (IsTargetCanMove(target, dragging, r.transform.position))
                        {
                            Debug.Log("�¤�N�x");
                            ischeckmate = true;
                        }
                    }

                    if (!ischeckmate)
                    {
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
                    }
                    else
                    {
                        AudioManager.Instance.PlayAuido(AudioManager.Instance.CheckMateAudio);
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

                    }

                    // ���A���l
                    int index = stat.childCount;
                    
                    for (int i = index - 1; i >= 0; i--)
                    {
                        Destroy(stat.GetChild(i).gameObject);
                    }

                    child = GameObject.Instantiate(GameObject.FindWithTag(target), new Vector3(-14,6,0), Quaternion.identity) as GameObject;
                    child.transform.SetParent(stat);
                    Action.Instance.StatText.text = "Move";

                }
                else
                {
                    Destroy(FocusTmp);
                }

                

                dragging = null;
                


            }

            

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

    // �۶H���k���
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

    // �۶H�I���˴�����
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

    // �����k���
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

    // ���I���˴�����
    private bool Chess4RayDirection(Vector3 d , Vector3 v , string s)
    {

        RaycastHit2D hit = Physics2D.Raycast(d + v, v,
                                            Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));
        
        switch (s) // �P�_��ê��
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

    // �������k���
    private bool Chess5CanMove(Vector3 Tpos , Vector3 pos)
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

    // �����I���˴�����
    private bool Chess5RayDirection(Vector3 pos, Vector3 d , Vector3 v1 , Vector3 v2 , string s)
    {   // ���I
        RaycastHit2D hit = Physics2D.Raycast(d + v1, v1 ,
                                            Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));

        // �ؼЦV����o�X�g�u
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
            // �I����m
            RaycastHit2D hit3 = Physics2D.Raycast(pos, Vector3.zero,
                            Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));

            Debug.Log(hit.transform.name);
            Debug.Log(hit2.transform.name);
            Debug.Log(hit3.transform.name);
            if (hit3 && hit2 && hit.transform.position == hit2.transform.position && hit.transform.position != hit3.transform.position && hit2.transform.position != hit3.transform.position )
            {
                Debug.Log(2);
                return true;
            }
            else
            {
                Debug.Log(3);
                return false;
            }
        }

    }


    private bool IsTargetCanMove(string target , Transform dragging , Vector3 TargetPos)
    {
        RaycastHit2D hit ; 
        switch (target)
        {
            // ���L���k
            case "red1":

                if (dragging.position.y < 0)
                {

                    if ((TargetPos.y == dragging.position.y + 2) && (TargetPos.x == dragging.position.x))
                    {
                        return true;
                    }
                }
                else if (dragging.position.y > 0)
                {

                    if (((TargetPos.y == dragging.position.y + 2) && (TargetPos.x == dragging.position.x)) ||
                        ((TargetPos.x == dragging.position.x + 2) && (TargetPos.y == dragging.position.y)) ||
                        ((TargetPos.x == dragging.position.x - 2) && (TargetPos.y == dragging.position.y)))
                    {
                        return true;
                    }
                }

                break;

            // �¨򨫪k
            case "black1":

                if (dragging.position.y > 0)
                {

                    if ((TargetPos.y == dragging.position.y - 2) && (TargetPos.x == dragging.position.x))
                    {
                        return true;
                    }
                }
                else if (dragging.position.y < 0)
                {

                    if (((TargetPos.y == dragging.position.y - 2) && (TargetPos.x == dragging.position.x)) ||
                        ((TargetPos.x == dragging.position.x + 2) && (TargetPos.y == dragging.position.y)) ||
                        ((TargetPos.x == dragging.position.x - 2) && (TargetPos.y == dragging.position.y)))
                    {
                        return true;
                    }
                }

                break;

            // ���Ө��k
            case "red2":

                hit = Physics2D.Raycast(dragging.position + new Vector3(0, 1, 0), Vector2.up,
                                    Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));


                if ((TargetPos.x <= 2) && (TargetPos.x >= -2) && (TargetPos.y >= -9) && (TargetPos.y <= -5))
                {
                    if (((TargetPos.y == dragging.position.y + 2) && (TargetPos.x == dragging.position.x)) ||
                        ((TargetPos.y == dragging.position.y - 2) && (TargetPos.x == dragging.position.x)) ||
                        ((TargetPos.x == dragging.position.x + 2) && (TargetPos.y == dragging.position.y)) ||
                        ((TargetPos.x == dragging.position.x - 2) && (TargetPos.y == dragging.position.y)))
                    {
                        return true;
                    }
                }
                else if (hit.transform.tag == "black2")
                {

                    return true;
                }


                break;

            // �±N���k
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
                        return true;
                    }
                }
                else if (hit.transform.tag == "red2")
                {

                    return true;
                }

                break;

            // ���ۨ��k
            case "red3":


                if (TargetPos.y < 0)
                {
                    Movable = Chess3CanMove(TargetPos, dragging.position);
                    return Movable;
                }

                break;


            // �¶H���k
            case "black3":

                if (TargetPos.y > 0)
                {
                    Movable = Chess3CanMove(TargetPos, dragging.position);
                    return Movable;
                }

                break;


            case "red4": // ��
            case "black4":

                Movable = Chess4CanMove(TargetPos, dragging.position);
                return Movable;

                

            case "red5": // ��
            case "black5":

                Movable = Chess5CanMove(TargetPos, dragging.position);
                
                return Movable;

                

            // �����k
            case "red6":
            case "black6":

                if ((TargetPos.x == dragging.position.x) || (TargetPos.y == dragging.position.y))
                {
                    return true;
                }


                break;

            // ���K���k
            case "red7":

                if ((TargetPos.x <= 2) && (TargetPos.x >= -2) && (TargetPos.y >= -9) && (TargetPos.y <= -5))
                {
                    if (((TargetPos.y == dragging.position.y + 2) && (TargetPos.x == dragging.position.x + 2)) ||
                        ((TargetPos.y == dragging.position.y - 2) && (TargetPos.x == dragging.position.x - 2)) ||
                        ((TargetPos.x == dragging.position.x + 2) && (TargetPos.y == dragging.position.y - 2)) ||
                        ((TargetPos.x == dragging.position.x - 2) && (TargetPos.y == dragging.position.y + 2)))
                    {
                        return true;
                    }
                }

                break;

            // �¤h���k
            case "black7":

                if ((TargetPos.x <= 2) && (TargetPos.x >= -2) && (TargetPos.y <= 9) && (TargetPos.y >= 5))
                {
                    if (((TargetPos.y == dragging.position.y + 2) && (TargetPos.x == dragging.position.x + 2)) ||
                        ((TargetPos.y == dragging.position.y - 2) && (TargetPos.x == dragging.position.x - 2)) ||
                        ((TargetPos.x == dragging.position.x + 2) && (TargetPos.y == dragging.position.y - 2)) ||
                        ((TargetPos.x == dragging.position.x - 2) && (TargetPos.y == dragging.position.y + 2)))
                    {
                        return true;
                    }
                }

                break;

        }

        return false;
    }

    private bool IsCheckMate(Transform r)
    {
        RaycastHit2D hit_up , hit_down , hit_left , hit_right;
        hit_up = Physics2D.Raycast(r.transform.position + Vector3.up, Vector3.up , Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));
        hit_down = Physics2D.Raycast(r.transform.position + Vector3.down, Vector3.down, Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));
        hit_left = Physics2D.Raycast(r.transform.position + Vector3.left, Vector3.left, Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));
        hit_right = Physics2D.Raycast(r.transform.position + Vector3.right, Vector3.right, Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));

        if (hit_up)
        {
            Debug.Log(hit_up.transform.name);
            if (hit_up.transform.tag == "black6")
            {
                
                Debug.Log("�¤�N�x");

                return true;
            }
        }


        

        return false;
    }

}
