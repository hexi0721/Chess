using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    Transform dragging ; // �Ѥl��m
    public static GameController G;

    public static bool turn; // false:���� true:�¤�
    int layermask; // �h

    Vector3 TargetPos; // �ؼ��I����m
    string target; //  target tag
    bool Movable = false; // ��_����

    Transform r, b; // �� �N
    bool Isend = false; // �P�_�өαN����

    // �_�I
    int OriginalX = -8;
    int OriginalY = -9;

    int x = -9; // �̥�
    int y = -10; // �̤U
    int x1 = 2; // x���j
    int y1 = 2; // y���j

    // Text
    Text WhoWinText;
    Text RoundText;
    Text StatText;


    // UI btn
    GameObject gamereturn_btn;

    int round = 1; // �^�X

    // ���A
    GameObject child;
    public Transform stat;

    // Audio
    

    

    // Start is called before the first frame update
    void Start()
    {
        dragging = null;
        turn = false;
        
        WhoWinText = GameObject.Find("whowin").GetComponent<Text>(); // �ֳӽ֭t��r
        WhoWinText.text = "";

        RoundText = GameObject.Find("round").GetComponent<Text>(); // �^�X��r
        RoundText.text = "��" + round + "�^�X - ��";

        StatText = GameObject.Find("stat_txt").GetComponent<Text>(); // ���A��r
        StatText.text = "";

        

        G = GetComponent<GameController>();

        gamereturn_btn = GameObject.Find("gamereturn_btn"); // ���s�}�l���s

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
                    WhoWinText.text = "����� !";
                    break;

                case false:
                    WhoWinText.text = "�¤�� !";
                    break;
            }

            gamereturn_btn.SetActive(false);
            Action.menuplain.SetActive(true);
            
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
                    // ���L���k
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

                    // �¨򨫪k
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

                    // ���Ө��k
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
                                Movable = true;
                            }
                        }
                        else if (hit.transform.tag == "red2")
                        {

                            Movable = true;
                        }

                        break;

                    // ���ۨ��k
                    case "red3": 


                        if (TargetPos.y < 0)
                        {
                            Movable = Chess3CanMove(TargetPos, dragging.position);
                            
                        }


                        break;

                    // �¶H���k
                    case "black3": 

                        if (TargetPos.y > 0)
                        {
                            Movable = Chess3CanMove(TargetPos, dragging.position);
                            
                        }

                        break;


                    case "red4": // ��
                    case "black4":

                        Movable = Chess4CanMove(TargetPos , dragging.position);


                        break;

                    case "red5": // ��
                    case "black5":

                        Movable = Chess5CanMove(TargetPos, dragging.position);

                        break;

                    // �����k
                    case "red6": 
                    case "black6":

                        if ((TargetPos.x == dragging.position.x) || (TargetPos.y == dragging.position.y))
                        {
                            Movable = true;
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
                                Movable = true;
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
                                Movable = true;
                            }
                        }

                        break;

                }

                // �P�_�өαN�O�_�Q�Y
                if (TargetPos == b.transform.position || TargetPos == r.transform.position)
                {
                    Isend = true;
                }

                //�P�_�O�_�W�X�ѽL
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

                    turn = !turn; // ���ܦ^�X
                    Movable = false;
                    if (Isend != true)
                    {
                        
                        switch (turn)
                        {
                            case false:
                                RoundText.text = "��" + round + "�^�X - ��";
                                break;

                            case true:
                                RoundText.text = "��" + round + "�^�X - ��";
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

    // �����I���˴�����
    private bool Chess5RayDirection(Vector3 pos, Vector3 d , Vector3 v1 , Vector3 v2 , string s)
    {   // ���I
        RaycastHit2D hit = Physics2D.Raycast(d + v1, v1 ,
                                            Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));
        // �I���ؼ�
        RaycastHit2D hit2 = Physics2D.Raycast(pos + v2, v2,
                        Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));
        
        // �I����m
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
