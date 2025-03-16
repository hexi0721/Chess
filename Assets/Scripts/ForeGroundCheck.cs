using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForeGroundCheck : MonoBehaviour
{
    [SerializeField] GameObject foreGroundObj;
    GameObject foreGroundCanvas = null;

    void Start()
    {

        foreGroundCanvas = GameObject.FindWithTag("ForeGround");
        if (foreGroundCanvas == null)
        {
            Instantiate(foreGroundObj);
        }

    }
}
