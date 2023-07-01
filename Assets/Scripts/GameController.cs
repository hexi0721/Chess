using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions; 

public class GameController : MonoBehaviour
{
    private Transform dragging ;

    public static bool turn; // false:���� true:�¤�
    private int layermask;

    public static Vector3 TargetPos; // �ؼ��I����m
    public static string target;

    // �_�I
    private float OriginalX = -2.865f;
    private float OriginalY = -3.115f;

    private float x = -3.21875f; // �̥�
    private float y = -3.47f; // �̤U
    private float x1 = 0.7075f; // x���j
    private float y1 = 0.71f; // y���j

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
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.zero ,Mathf.Infinity ,layermask);

            if (hit)
            {

                 if (Match(hit,turn))
                     dragging = hit.transform;
                 target = hit.transform.tag;
            }
            else if (dragging != null)
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0;
                float tmp = 0.0f;
                TargetPos = new Vector3(0,0,0);
                for (float i = x; i < OriginalX + x1 * 8 + 0.35375; i += x1)
                {
                    float itmp = i + x1;
                    
                    if((pos.x > i) && (pos.x < itmp))
                    {
                        
                        TargetPos.x = OriginalX + tmp * x1;
                        //Debug.Log("x " + TargetPos.x);
                        break;
                        
                    }

                    tmp += 1.0f;
                }
                tmp = 0.0f;
                for (float j = y; j < OriginalY + y1 * 9 + 0.355; j += y1)
                {
                    float jtmp = j + y1;
                    
                    if ((pos.y > j) && (pos.y < jtmp))
                    {
                        
                        TargetPos.y = OriginalY + tmp * y1;
                        //Debug.Log("y " + TargetPos.y);
                        break;
                    }

                    tmp += 1.0f;
                }

                if ((pos.x > -3.21875f) && (pos.y > -3.47f) && (pos.x < OriginalX + x1 * 8 + 0.35375) && (pos.y < OriginalY + y1 * 9 + 0.355))
                    dragging.position = TargetPos;
                

                dragging = null;
                

            }

            

        }
        
        

        

    }

    private bool Match(RaycastHit2D hit ,bool turn)  //  �ǰtTAG
    {
        bool flag;
        if (turn == false)
            flag =  Regex.IsMatch(hit.transform.tag, "red");
        else
            flag =  Regex.IsMatch(hit.transform.tag, "black");

        return flag;
    }

    
}
