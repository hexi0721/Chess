using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCheck : MonoBehaviour
{

    public GameObject TitleBGM;
    GameObject BGM = null;

    void Start()
    {

        // �ˬd���Lsound tag
        BGM = GameObject.FindGameObjectWithTag("sound");
        if (BGM == null)
        {
            Instantiate(TitleBGM);
        }

    }


}
