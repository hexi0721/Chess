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

        // �O�_��Pblack1�X�� ?
        //Debug.Log(GameController.turn + "red11");
        if (GameController.turn == true) // �I���˴� ���O�¤�N�}�a
        {
            if (collision.gameObject.tag == "black")
                
                Destroy(collision.gameObject);
                
        }


    }
}
