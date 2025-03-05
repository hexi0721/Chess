using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCheck : MonoBehaviour
{

    public GameObject TitleBGM;
    GameObject BGM = null;

    // Start is called before the first frame update
    void Start()
    {

        // ¿À¨d¶≥µLsound tag
        BGM = GameObject.FindGameObjectWithTag("sound");
        if (BGM == null)
        {
            Instantiate(TitleBGM);
        }

    }


}
