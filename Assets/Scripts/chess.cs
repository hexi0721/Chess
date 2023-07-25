using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class chess : MonoBehaviour
{
    
    

    // Start is called before the first frame update
    void Start()
    {



    }

    // Update is called once per frame
    void Update()
    {
        



    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (GameController.turn == false) // 碰撞檢測 對方是紅方就破壞
        {
            if (Regex.IsMatch(collision.transform.tag, "red"))
            {
                Destroy(collision.gameObject);
                
                    
            }
                
        }
        else
        {
            if (Regex.IsMatch(collision.transform.tag, "black"))
            {
                Destroy(collision.gameObject);
                
                    
            }
                
        }
            
                
    }

    


            


}

    


