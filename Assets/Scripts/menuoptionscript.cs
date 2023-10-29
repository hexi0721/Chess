using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;

public class Menuoptionscript : MonoBehaviour
{

    public GameObject Arrow_left , Arrow_right;


    // Update is called once per frame
    void Update()
    {
        // Ωb¿Y±€¬‡
        Arrow_left.transform.Rotate(0.1f,0,0);
        Arrow_right.transform.Rotate(0.1f, 0, 0);

    }

    public void Click_gamereturn()
    {
        AudioManager.Instance.PlayAuido(AudioManager.Instance.ReturnAudio);
        Action.Instance.menuplain.SetActive(!Action.Instance.menuplain.activeSelf);

        if (!Replay.Instance.Isplay)
        {
            Action.Instance.replay_btn.SetActive(false);
            switch (Action.Instance.menuplain.activeSelf)
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

    public void Click_enterAIgame()
    {
        AudioManager.Instance.PlayAuido(AudioManager.Instance.RestartAudio);
        SceneManager.LoadScene("GameScene2", LoadSceneMode.Single);
    }

    public void Click_exitgame()
    {
        Application.Quit();
        
    }

    public void Click_PlayReWatch()
    {
        Replay.Instance.PlayReWatch();
    }


    public void Hover_button(GameObject G)
    {

        Arrow_left.transform.SetParent(G.transform);
        Arrow_left.GetComponent<RectTransform>().localPosition = new Vector3(G.transform.localPosition.x + 225, 0, 0);

        Arrow_right.transform.SetParent(G.transform);
        Arrow_right.GetComponent<RectTransform>().localPosition = new Vector3(G.transform.localPosition.x - 225, 0, 0);

    }
}
