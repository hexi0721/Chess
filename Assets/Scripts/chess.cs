using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class chess : MonoBehaviour
{

    private Transform stat;
    private GameObject child;

    private Text StatText;

    // Start is called before the first frame update
    void Start()
    {
        StatText = GameObject.Find("stat_txt").GetComponent<Text>();
        stat = GameObject.Find("stat").transform;

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

                child = GameObject.Instantiate(collision.gameObject, new Vector3(-14, 2, 0), Quaternion.identity) as GameObject;
                child.transform.SetParent(stat);
                StatText.text = "slain";

            }
                
        }
        else
        {
            if (Regex.IsMatch(collision.transform.tag, "black"))
            {
                Destroy(collision.gameObject);

                child = GameObject.Instantiate(collision.gameObject, new Vector3(-14, 2, 0), Quaternion.identity) as GameObject;
                child.transform.SetParent(stat);
                StatText.text = "slain";
            }
                
        }
            
                
    }

    


            


}

    


