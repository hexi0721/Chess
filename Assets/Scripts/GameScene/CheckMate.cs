using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMate : MonoBehaviour
{

    Vector3 King; // 帥與將位置

    bool redcheckmate , blackcheckmate ; // 哪方將軍
    GameController gameController;

    void Start()
    {
        redcheckmate = false;
        blackcheckmate = false;

        gameController = GetComponent<GameController>();
        
    }


    void Update()
    {
        // 判斷將軍
        if (gameController.JudgeCheckMateTurnIsChange)
        {
            
            King = GameObject.FindWithTag("red2").transform.position;

            if (gameController.Turn == false && JudgeChess1IsCheckMate("black1", King) || JudgeChess2IsCheckMate("black2" , King) || JudgeChess4IsCheckMate("black4" , King) || JudgeChess5IsCheckMate("black5", King) || JudgeChess6IsCheckMate("black6", King))
            {
                blackcheckmate = true;

            }
            else
            {
                blackcheckmate = false;
            }


            King = GameObject.FindWithTag("black2").transform.position;

            if (gameController.Turn == true && JudgeChess1IsCheckMate("red1", King) || JudgeChess2IsCheckMate("red2", King) || JudgeChess4IsCheckMate("red4", King) || JudgeChess5IsCheckMate("red5", King) || JudgeChess6IsCheckMate("red6", King))
            {
                redcheckmate = true;
                        
            }
            else
            {
                redcheckmate = false;
            }


            gameController.JudgeCheckMateTurnIsChange = false;

        }

        if (!gameController.IsEnd)
        {
            PlayCheckMateAudio();
        }
        

    }

    // 判斷兵卒是否將軍
    private bool JudgeChess1IsCheckMate(string s, Vector3 pos)
    {
        RaycastHit2D hit_up = Physics2D.Raycast(pos + Vector3.up, Vector3.up, 1.0f, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));
        RaycastHit2D hit_down = Physics2D.Raycast(pos + Vector3.down, Vector3.down, 1.0f, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));
        RaycastHit2D hit_left = Physics2D.Raycast(pos + Vector3.left, Vector3.left, 1.0f, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));
        RaycastHit2D hit_right = Physics2D.Raycast(pos + Vector3.right, Vector3.right, 1.0f, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));


        if (hit_up && hit_up.transform.CompareTag(s) && transform.CompareTag("red2"))
        {
            return true;

        }
        else if (hit_down && hit_down.transform.CompareTag(s) && transform.CompareTag("black2"))
        {
            return true;
        }
        else if (hit_left && hit_left.transform.CompareTag(s))
        {
            return true;
        }
        else if (hit_right && hit_right.transform.CompareTag(s))
        {
            return true;
        }

        return false;



    }

    // 判斷帥將是否將軍
    private bool JudgeChess2IsCheckMate(string s, Vector3 pos)
    {
        RaycastHit2D hit_up = Physics2D.Raycast(pos + Vector3.up, Vector3.up, Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));
        RaycastHit2D hit_down = Physics2D.Raycast(pos + Vector3.down, Vector3.down, Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));

        if(hit_up && hit_up.transform.CompareTag(s))
        {
            return true;
        }
        else if(hit_down && hit_down.transform.CompareTag(s))
        {
            return true;
        }


        return false;
    }

    // 判斷馬是否將軍
    private bool JudgeChess4IsCheckMate(string s, Vector3 pos)
    {

        RaycastHit2D hit_right_up = Physics2D.Raycast(pos + new Vector3(2, 4, 0), Vector3.zero, Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));
        RaycastHit2D hit_left_up = Physics2D.Raycast(pos + new Vector3(-2, 4, 0), Vector3.zero, Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));

        if (hit_right_up && hit_right_up.transform.CompareTag(s) && Inner(hit_right_up.transform.position, Vector3.down))
        {
            return true;
        }
        else if (hit_left_up && hit_left_up.transform.CompareTag(s) && Inner(hit_left_up.transform.position , Vector3.down))
        {
            return true;
        }

        RaycastHit2D hit_right_down = Physics2D.Raycast(pos + new Vector3(2, -4, 0), Vector3.zero, Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));
        RaycastHit2D hit_left_down = Physics2D.Raycast(pos + new Vector3(-2, -4, 0), Vector3.zero, Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));

        if (hit_right_down && hit_right_down.transform.CompareTag(s) && Inner(hit_right_down.transform.position, Vector3.up))
        {
            return true;
        }
        else if (hit_left_down && hit_left_down.transform.CompareTag(s) && Inner(hit_left_down.transform.position, Vector3.up))
        {
            return true;
        }

        RaycastHit2D hit_up_left = Physics2D.Raycast(pos + new Vector3(-4, 2, 0), Vector3.zero, Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));
        RaycastHit2D hit_down_left = Physics2D.Raycast(pos + new Vector3(-4, -2, 0), Vector3.zero, Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));

        if (hit_up_left && hit_up_left.transform.CompareTag(s) && Inner(hit_up_left.transform.position, Vector3.right))
        {
            return true;
        }
        else if (hit_down_left && hit_down_left.transform.CompareTag(s) && Inner(hit_down_left.transform.position, Vector3.right))
        {
            return true;
        }

        RaycastHit2D hit_up_right = Physics2D.Raycast(pos + new Vector3(4, 2, 0), Vector3.zero, Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));
        RaycastHit2D hit_down_right = Physics2D.Raycast(pos + new Vector3(4, -2, 0), Vector3.zero, Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));

        if (hit_up_right && hit_up_right.transform.CompareTag(s) && Inner(hit_up_right.transform.position, Vector3.left))
        {
            return true;
        }
        else if (hit_down_right && hit_down_right.transform.CompareTag(s) && Inner(hit_down_right.transform.position, Vector3.left))
        {
            return true;
        }

        return false;

        static bool Inner(Vector3 pos, Vector3 d)
        {
            RaycastHit2D hit = Physics2D.Raycast(pos + d, d, 1.0f, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));

            if (!hit)
            {
                return true;
            }


            return false;
        }

    }

    // 判斷包砲是否將軍
    private bool JudgeChess5IsCheckMate(string s, Vector3 pos)
    {
        RaycastHit2D hit_up = Physics2D.Raycast(pos + Vector3.up, Vector3.up, Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));
        RaycastHit2D hit_down = Physics2D.Raycast(pos + Vector3.down, Vector3.down, Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));
        RaycastHit2D hit_left = Physics2D.Raycast(pos + Vector3.left, Vector3.left, Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));
        RaycastHit2D hit_right = Physics2D.Raycast(pos + Vector3.right, Vector3.right, Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));



        if (hit_up)
        {
            RaycastHit2D hit_up_up = Physics2D.Raycast(hit_up.transform.position + Vector3.up, Vector3.up, Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));

            if (hit_up_up && hit_up_up.transform.CompareTag(s))
            {
                return true;
            }
        }

        if (hit_down)
        {
            RaycastHit2D hit_down_down = Physics2D.Raycast(hit_down.transform.position + Vector3.down, Vector3.down, Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));

            if (hit_down_down && hit_down_down.transform.CompareTag(s))
            {
                return true;
            }
        }

        if (hit_left)
        {
            RaycastHit2D hit_left_left = Physics2D.Raycast(hit_left.transform.position + Vector3.left, Vector3.left, Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));

            if (hit_left_left && hit_left_left.transform.CompareTag(s))
            {
                return true;
            }
        }

        if (hit_right)
        {
            RaycastHit2D hit_right_right = Physics2D.Raycast(hit_right.transform.position + Vector3.right, Vector3.right, Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));

            if (hit_right_right && hit_right_right.transform.CompareTag(s))
            {
                return true;
            }
        }

        return false;



    }

    //判斷車是否將軍
    private bool JudgeChess6IsCheckMate(string s, Vector3 pos)
    {
        RaycastHit2D hit_up = Physics2D.Raycast(pos + Vector3.up, Vector3.up, Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));
        RaycastHit2D hit_down = Physics2D.Raycast(pos + Vector3.down, Vector3.down, Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));
        RaycastHit2D hit_left = Physics2D.Raycast(pos + Vector3.left, Vector3.left, Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));
        RaycastHit2D hit_right = Physics2D.Raycast(pos + Vector3.right, Vector3.right, Mathf.Infinity, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));

        if (hit_up && hit_up.transform.CompareTag(s))
        {
            
            return true;

        }
        else if (hit_down && hit_down.transform.CompareTag(s))
        {
            
            return true;

        }
        else if (hit_left && hit_left.transform.CompareTag(s))
        {

            return true;

        }
        else if (hit_right && hit_right.transform.CompareTag(s))
        {

            return true;

        }

        return false;


    }

    private void PlayCheckMateAudio()
    {

        if (redcheckmate && gameController.Turn)
        {

            AudioManager.Instance.PlayAuido(AudioManager.Instance.CheckMateAudio);
            
            redcheckmate = false;
        }
        else if (blackcheckmate && !gameController.Turn)
        {

            AudioManager.Instance.PlayAuido(AudioManager.Instance.CheckMateAudio);
            
            blackcheckmate = false;
        }

        

    }
}
