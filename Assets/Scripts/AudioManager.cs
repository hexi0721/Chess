using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static AudioManager _instance;

    public AudioClip RestartAudio, ReturnAudio, QuitAudio , MoveAudio , KillAudio , CheckMateAudio;

    

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
