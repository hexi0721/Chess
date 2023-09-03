using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action : MonoBehaviour
{

    // UI btn
    public static GameObject menuplain;


    // Start is called before the first frame update
    void Start()
    {
        menuplain = GameObject.Find("menuplain");
        menuplain.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menuplain.SetActive(!menuplain.activeSelf);
            switch (menuplain.activeSelf)
            {
                case true:
                    GameController.G.enabled = false;
                    break;

                case false:
                    GameController.G.enabled = true;
                    break;
            }
        }
    }
}
