using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class black1 : MonoBehaviour
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
        //Debug.Log(GameController.turn);
        if (transform.position != tmp) // �˴��O�_������ �����ʫh�洫�^�X �ýᤩ�s��m
        {
            GameController.turn = !GameController.turn;
            
            tmp = transform.position;
        }

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (GameController.turn == false) // �I���˴� ���O����N�}�a
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

    


