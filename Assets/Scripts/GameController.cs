using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions; 

public class GameController : MonoBehaviour
{
    private Transform dragging ;

    public static bool turn; // false:紅方 true:黑方
    private int layermask;

    public static Vector3 TargetPos; // 目標點擊位置
    public static string target;

    // 起點
    private int OriginalX = -8;
    private int OriginalY = -9;

    private int x = -9; // 最左
    private int y = -10; // 最下
    private int x1 = 2; // x間隔
    private int y1 = 2; // y間隔

    // Start is called before the first frame update
    void Start()
    {
        dragging = null;
        turn = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0)) // 點擊
        {
            if (turn == false) // 紅方回合
            {
                layermask = LayerMask.NameToLayer("red");
                layermask = 1 << layermask;
            }
            else // 黑方回合
            {
                layermask = LayerMask.NameToLayer("black");
                layermask = 1 << layermask;
            }

            // 棋子
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.zero ,Mathf.Infinity ,layermask);

            if (hit)
            {

                 if (Match(hit,turn))
                     dragging = hit.transform;
                 target = hit.transform.tag;
                Debug.Log(hit.transform.name);
            }
            else if (dragging != null)
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.z = 0;
                int tmp = 0;
                TargetPos = new Vector3(0,0,0);
                for (int i = x; i < OriginalX + x1 * 8 ; i += x1)
                {
                    float itmp = i + x1;
                    
                    if((pos.x > i) && (pos.x < itmp))
                    {
                        
                        TargetPos.x = OriginalX + tmp * x1;
                        Debug.Log("x " + TargetPos.x);
                        break;
                        
                    }

                    tmp += 1;
                }
                tmp = 0;
                for (int j = y; j < OriginalY + y1 * 9 ; j += y1)
                {
                    int jtmp = j + y1;
                    
                    if ((pos.y > j) && (pos.y < jtmp))
                    {
                        
                        TargetPos.y = OriginalY + tmp * y1;
                        Debug.Log("y " + TargetPos.y);
                        break;
                    }

                    tmp += 1;
                }

                if ((pos.x > -9) && (pos.y > -10) && (pos.x <  9 ) && (pos.y < 10 ))
                    dragging.position = TargetPos;

                //dragging.position = TargetPos;
                dragging = null;
                

            }

            

        }
        
        

        

    }

    private bool Match(RaycastHit2D hit ,bool turn)  //  匹配TAG
    {
        bool flag;
        if (turn == false)
            flag =  Regex.IsMatch(hit.transform.tag, "red");
        else
            flag =  Regex.IsMatch(hit.transform.tag, "black");

        return flag;
    }

    
}
