using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;


public class Chess : MonoBehaviour
{


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Replay.Instance.Isplay)
        {
            if (GameController.turn == false)
            {
                if (Regex.IsMatch(collision.transform.tag, "red")) // 碰撞檢測 對方是紅方就破壞
                {
                    
                    Destroy(collision.gameObject);
                    


                }
                
            }
            else
            {
                if (Regex.IsMatch(collision.transform.tag, "black")) // 碰撞檢測 對方是黑方就破壞
                {
                    Destroy(collision.gameObject);
                    

                }
                
            }

            
        }
        else
        {
            if (Replay.Instance.turn == false)
            {
                if (Regex.IsMatch(collision.transform.tag, "red")) // 碰撞檢測 對方是紅方就隱藏
                {
                    Replay.Instance.Revive_Chess.Add(collision.gameObject);
                    collision.gameObject.SetActive(false);


                }

            }
            else
            {
                if (Regex.IsMatch(collision.transform.tag, "black")) // 碰撞檢測 對方是黑方就隱藏
                {
                    Replay.Instance.Revive_Chess.Add(collision.gameObject);
                    collision.gameObject.SetActive(false);
                    
                    
                }

            }

            

            
        }
        

    }
            


}

    


