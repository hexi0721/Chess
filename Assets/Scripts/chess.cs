using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;


public class Chess : MonoBehaviour
{

    Transform stat;
    GameObject child;

    

    public static bool redcheckmate, blackcheckmate;

    // Start is called before the first frame update
    void Start()
    {
        
        
        redcheckmate = false;
        blackcheckmate = false;
        stat = GameObject.Find("stat").transform;

    }

    
    void Update()
    {
        // 判斷將軍
        if (GameController.JudgeCheckMateTurnIsChange)
        {
            switch (transform.tag)
            {
                case "red2":



                    if (JudgeChess1IsCheckMate("black1", transform.position) || JudgeChess6IsCheckMate("black6", transform.position))
                    {
                        blackcheckmate = true;
                        
                    }
                    


                    break;

                case "black2":


                    if (JudgeChess1IsCheckMate("red1", transform.position) || JudgeChess6IsCheckMate("red6", transform.position))
                    {
                        redcheckmate = true;
                    }
                    


                    break;

            }
            
            PlayCheckMateAudio();
        }

        


    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Replay.Instance.Isplay)
        {
            if (GameController.turn == false)
            {
                if (Regex.IsMatch(collision.transform.tag, "red")) // 碰撞檢測 對方是紅方就破壞
                {
                    
                    Destroy(collision.gameObject);
                    child = GameObject.Instantiate(collision.gameObject, new Vector3(-14, 2, 0), Quaternion.identity) as GameObject;
                    child.transform.SetParent(stat);


                }
                
            }
            else
            {
                if (Regex.IsMatch(collision.transform.tag, "black")) // 碰撞檢測 對方是黑方就破壞
                {
                    Destroy(collision.gameObject);
                    child = GameObject.Instantiate(collision.gameObject, new Vector3(-14, 2, 0), Quaternion.identity) as GameObject;
                    child.transform.SetParent(stat);

                }
                
            }

            Action.Instance.StatText.text = "slain";
        }
        else
        {
            if (Replay.Instance.turn == false)
            {
                if (Regex.IsMatch(collision.transform.tag, "red")) // 碰撞檢測 對方是紅方就破壞
                {
                    Replay.Instance.Revive_Chess.Add(collision.gameObject);
                    collision.gameObject.SetActive(false);


                }

            }
            else
            {
                if (Regex.IsMatch(collision.transform.tag, "black")) // 碰撞檢測 對方是黑方就破壞
                {
                    Replay.Instance.Revive_Chess.Add(collision.gameObject);
                    collision.gameObject.SetActive(false);
                    
                    
                }

            }

            

            
        }
        

    }

    // 判斷兵卒是否將軍
    private bool JudgeChess1IsCheckMate(string s , Vector3 pos)
    {
        RaycastHit2D hit_up = Physics2D.Raycast(pos + Vector3.up, Vector3.up, 1.0f, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));
        RaycastHit2D hit_down = Physics2D.Raycast(pos + Vector3.down, Vector3.down, 1.0f, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));
        RaycastHit2D hit_left = Physics2D.Raycast(pos + Vector3.left, Vector3.left, 1.0f, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));
        RaycastHit2D hit_right = Physics2D.Raycast(pos + Vector3.right, Vector3.right, 1.0f, 1 << LayerMask.NameToLayer("black") | 1 << LayerMask.NameToLayer("red"));


        if (hit_up &&  hit_up.transform.CompareTag(s) && transform.CompareTag("red2"))
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
        else
        {
            return false;
        }


    }

    private bool JudgeChess4IsCheckMate(string s, Vector3 pos)
    {


        return false;
    }

    private bool JudgeChess5IsCheckMate(string s, Vector3 pos)
    {
        return false;
    }

    //判斷車是否將軍
    private bool JudgeChess6IsCheckMate(string s ,Vector3 pos)
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
        else
        {
            return false;
            
        }
    }

    private void PlayCheckMateAudio()
    {
        
        if (redcheckmate && GameController.turn)
        {
            
            //AudioManager.Instance.PlayAuido(AudioManager.Instance.CheckMateAudio);
            GameController.JudgeCheckMateTurnIsChange = false;
            //redcheckmate = false;
        }
        else if (blackcheckmate  && !GameController.turn)
        {
            
            //AudioManager.Instance.PlayAuido(AudioManager.Instance.CheckMateAudio);
            GameController.JudgeCheckMateTurnIsChange = false;
            //blackcheckmate = false;
        }
            
            
            
        

    }
            


}

    


