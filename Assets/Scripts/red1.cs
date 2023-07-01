using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class red1 : MonoBehaviour
{
    private Vector3 tmp;
    // Start is called before the first frame update
    void Start()
    {
        tmp = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        




        if (transform.position != tmp)
        {
            GameController.turn = true;
            tmp = transform.position;
            
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        // 是否能與black1合併 ?
        //Debug.Log(GameController.turn + "red11");
        if (GameController.turn == true) // 碰撞檢測 對方是黑方就破壞
        {
            if (collision.gameObject.tag == "black")
                
                Destroy(collision.gameObject);
                
        }


    }
}
