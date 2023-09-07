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
    
    

    public GameObject Arrow_left , Arrow_right;

    // Start is called before the first frame update
    void Start()
    {
        
        // 檢查有無sound tag
        BGM = GameObject.FindGameObjectWithTag("sound"); 
        if (BGM == null)
        {
            Instantiate(TitleBGM);
        }
        
        

        
        
    }

    // Update is called once per frame
    void Update()
    {
        // 箭頭旋轉
        Arrow_left.transform.Rotate(0.1f,0,0);
        Arrow_right.transform.Rotate(0.1f, 0, 0);

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


    public void Hover_button(GameObject G)
    {

        Arrow_left.transform.SetParent(G.transform);
        Arrow_left.GetComponent<RectTransform>().localPosition = new Vector3(G.transform.localPosition.x + 225, 0, 0);

        Arrow_right.transform.SetParent(G.transform);
        Arrow_right.GetComponent<RectTransform>().localPosition = new Vector3(G.transform.localPosition.x - 225, 0, 0);

    }
}
