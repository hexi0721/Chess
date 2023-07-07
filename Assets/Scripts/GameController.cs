using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions; 

public class GameController : MonoBehaviour
{
    private Transform dragging ; // �Ѥl��m

    public static bool turn; // false:���� true:�¤�
    private int layermask; // �h

    private static Vector3 TargetPos; // �ؼ��I����m
    private string target; //  target tag
    private bool Movable = false; // ��_����

    // �_�I
    private int OriginalX = -8;
    private int OriginalY = -9;

    private int x = -9; // �̥�
    private int y = -10; // �̤U
    private int x1 = 2; // x���j
    private int y1 = 2; // y���j

    

    // Start is called before the first frame update
    void Start()
    {
        dragging = null;
        turn = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0)) // �I��
        {

            //MapController.location.ForEach(num => Debug.Log(num + ", "));
            if (turn == false) // ����^�X
            {
                layermask = LayerMask.NameToLayer("red");
                layermask = 1 << layermask;
            }
            else // �¤�^�X
            {
                layermask = LayerMask.NameToLayer("black");
                layermask = 1 << layermask;
            }

            // �Ѥl
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.zero ,
                Mathf.Infinity ,layermask);

            if (hit)
            {

                 if (Match(hit,turn)) // �P�_�֪��^�X�ְ�
                     dragging = hit.transform;
                 target = hit.transform.tag;
                //Debug.Log(hit.transform.name);
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

                        hit = Physics2D.Raycast(dragging.position + Vector3.up, Vector3.up,
                                            Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));

                        if (hit)
                        {
                            Debug.Log(hit.transform.name + hit.transform.position + TargetPos);
                            
                            
                        }

                        if ((TargetPos.x == dragging.position.x) || (TargetPos.y == dragging.position.y))
                        {
                            if(hit.transform.position == TargetPos && )
                            {
                                
                                Debug.Log("1");
                                Movable = false;
                            }



                            else
                            {
                                Debug.Log("2");
                                Movable = true;
                            }
                            
                        }

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


                //�P�_�O�_�W�X�ѽL
                if ((pos.x > -9) && (pos.y > -10) && (pos.x <  9 ) && (pos.y < 10 ) && Movable) 
                {
                    dragging.position = TargetPos;
                    Movable = false;
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
                
                if (hit)
                {
                    if ((hit.transform.position.x == pos.x - 2) && (hit.transform.position.y == pos.y + 2))
                        return false;
                }

                break;

            case "RightUp":
                if (hit)
                {
                    if ((hit.transform.position.x == pos.x + 2) && (hit.transform.position.y == pos.y + 2))
                        return false;
                }

                break;

            case "LeftDown":
                if (hit)
                {
                    if ((hit.transform.position.x == pos.x - 2) && (hit.transform.position.y == pos.y - 2))
                        return false;
                }

                break;

            case "RightDown":
                if (hit)
                {
                    if ((hit.transform.position.x == pos.x + 2) && (hit.transform.position.y == pos.y - 2))
                        return false;
                }

                break;

                
        }


        return true;

    }

    // �����k���
    private bool Chess4CanMove(Vector3 TPos, Vector3 pos)
    {

        if ((Mathf.Abs(TPos.x - dragging.position.x) + Mathf.Abs(TPos.y - dragging.position.y) == 6)
                && (Mathf.Abs(TPos.x - dragging.position.x) != 6)
                && (Mathf.Abs(TPos.y - dragging.position.y) != 6))
            {

                if  (((TPos.x == pos.x + 4) && (Chess4RayDirection(TPos , pos , Vector3.right , "Right"))) ||
                    ((TPos.x == pos.x - 4) && (Chess4RayDirection(TPos, pos, Vector3.left, "Left"))) ||
                    ((TPos.y == pos.y + 4) && (Chess4RayDirection(TPos, pos, Vector3.up, "Up"))) ||
                    ((TPos.y == pos.y - 4) && (Chess4RayDirection(TPos, pos, Vector3.down, "Down"))))
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
    private bool Chess4RayDirection(Vector3 pos, Vector3 d , Vector3 v , string s)
    {

        RaycastHit2D hit = Physics2D.Raycast(d + v, v,
                                            Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));

        switch (s) // �P�_��ê��
        {
            case "Right":

                if (hit.transform.position.x == d.x + 2)
                {
                    return true;
                }

                break;

            case "Left":

                if (hit.transform.position.x == d.x - 2)
                {
                    return true;
                }

                break;

            case "Up":

                if (hit.transform.position.y == d.y + 2)
                {
                    return true;
                }

                break;

            case "Down":

                if (hit.transform.position.y == d.y - 2)
                {
                    return true;
                }

                break;
        }
            
        return false;
    }





}
