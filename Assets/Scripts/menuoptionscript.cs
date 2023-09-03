using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;

public class Menuoptionscript : MonoBehaviour
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

    // Update is called once per frame
    void Update()
    {
        

        
    }

    public void Click_reset()
    {
        AudioManager.Instance.PlayAuido(AudioManager.Instance.RestartAudio);
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    public void Click_gamereturn()
    {
        AudioManager.Instance.PlayAuido(AudioManager.Instance.ReturnAudio);
        Action.menuplain.SetActive(!Action.menuplain.activeSelf);

        

        switch (Action.menuplain.activeSelf)
        {
            case true:
                GameController.G.enabled = false;
                break;

            case false:
                GameController.G.enabled = true;
                break;
        }
    }

    public void Click_homereturn()
    {
        AudioManager.Instance.PlayAuido(AudioManager.Instance.QuitAudio);
        SceneManager.LoadScene("Home", LoadSceneMode.Single);
    }

    public void Click_entergame()
    {
        AudioManager.Instance.PlayAuido(AudioManager.Instance.RestartAudio);
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    public void Click_exitgame()
    {
        Application.Quit();
        
    }


}
