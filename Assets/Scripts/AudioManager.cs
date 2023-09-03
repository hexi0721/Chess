using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static AudioManager _instance;

    public static AudioManager Instance
    {
        get
        {
            return _instance;
        }
    }


    public AudioSource efxsource;

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAuido(AudioClip clip)
    {
        efxsource.clip = clip;

        switch (clip.name)
        {
            case "KillAudio":
                efxsource.volume = 0.3f;
                break;

            case "MoveAudio":
                efxsource.volume = 1.0f;
                break;


        }

        
        efxsource.Play();
    }


}
