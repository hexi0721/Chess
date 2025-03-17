using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioClip Audio1, RestartAudio,  QuitAudio , MoveAudio , KillAudio , CheckMateAudio;

    public static AudioManager Instance
    {
        get; private set;
    }


    public AudioSource efxsource;

    private void Awake()
    {
        Instance = this;

    }

    void Start()
    {

        DontDestroyOnLoad(this.gameObject);

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

            case "QuitAudio":
            case "ResumeAudio":
            case "RestartAudio":
                efxsource.volume = 0.7f;
                break;

            case "CheckMateAudio":
                efxsource.volume = 0.7f;
                break;


        }

        
        efxsource.PlayOneShot(clip);
    }


}
