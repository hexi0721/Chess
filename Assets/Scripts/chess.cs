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
                if (Regex.IsMatch(collision.transform.tag, "red")) // �I���˴� ���O����N�}�a
                {
                    
                    Destroy(collision.gameObject);
                    


                }
                
            }
            else
            {
                if (Regex.IsMatch(collision.transform.tag, "black")) // �I���˴� ���O�¤�N�}�a
                {
                    Destroy(collision.gameObject);
                    

                }
                
            }

            
        }
        else
        {
            if (Replay.Instance.turn == false)
            {
                if (Regex.IsMatch(collision.transform.tag, "red")) // �I���˴� ���O����N����
                {
                    Replay.Instance.Revive_Chess.Add(collision.gameObject);
                    collision.gameObject.SetActive(false);


                }

            }
            else
            {
                if (Regex.IsMatch(collision.transform.tag, "black")) // �I���˴� ���O�¤�N����
                {
                    Replay.Instance.Revive_Chess.Add(collision.gameObject);
                    collision.gameObject.SetActive(false);
                    
                    
                }

            }

            

            
        }
        

    }
            


}

    


