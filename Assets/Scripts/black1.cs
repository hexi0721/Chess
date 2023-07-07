using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class black1 : MonoBehaviour
{
    
    private Vector3 pos;
    
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (transform.position != pos) // 檢測是否未移動 有移動則交換回合 並賦予新位置
        {
            GameController.turn = !GameController.turn;
            //GameController.turn = false; //測試
            pos = transform.position;
        }

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (GameController.turn == false) // 碰撞檢測 對方是紅方就破壞
        {
            if (Regex.IsMatch(collision.transform.tag, "red"))
                Destroy(collision.gameObject);
        }
        else
        {
            if (Regex.IsMatch(collision.transform.tag, "black"))
                Destroy(collision.gameObject);
        }
            
                
     }
            


}

    


