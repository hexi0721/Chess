using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;


public class Chess : MonoBehaviour
{

    //Transform stat;
    //GameObject child;
  

    // Start is called before the first frame update
    void Start()
    {
        
        //stat = GameObject.Find("stat").transform;

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Replay.Instance.Isplay)
        {
            if (GameController.turn == false)
            {
                if (Regex.IsMatch(collision.transform.tag, "red")) // �I���˴� ���O����N�}�a
                {
                    
                    Destroy(collision.gameObject);
                    /*child = GameObject.Instantiate(collision.gameObject, new Vector3(-14, 2, 0), Quaternion.identity) as GameObject;
                    child.transform.SetParent(stat);*/


                }
                
            }
            else
            {
                if (Regex.IsMatch(collision.transform.tag, "black")) // �I���˴� ���O�¤�N�}�a
                {
                    Destroy(collision.gameObject);
                    /*child = GameObject.Instantiate(collision.gameObject, new Vector3(-14, 2, 0), Quaternion.identity) as GameObject;
                    child.transform.SetParent(stat);*/

                }
                
            }

            //Action.Instance.StatText.text = "slain";
        }
        else
        {
            if (Replay.Instance.turn == false)
            {
                if (Regex.IsMatch(collision.transform.tag, "red")) // �I���˴� ���O����N�}�a
                {
                    Replay.Instance.Revive_Chess.Add(collision.gameObject);
                    collision.gameObject.SetActive(false);


                }

            }
            else
            {
                if (Regex.IsMatch(collision.transform.tag, "black")) // �I���˴� ���O�¤�N�}�a
                {
                    Replay.Instance.Revive_Chess.Add(collision.gameObject);
                    collision.gameObject.SetActive(false);
                    
                    
                }

            }

            

            
        }
        

    }
            


}

    


