using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class move : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        

        switch (GameController.target)
        {
            case "red1":
            case "black1":

                Debug.Log("1");
                break;

            case "red2":
            case "black2":

                Debug.Log("2");
                break;

            case "red6":
            case "black6":



                break;

        }
            
            


    }
}
