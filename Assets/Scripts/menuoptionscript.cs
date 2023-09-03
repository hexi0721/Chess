using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;

public class Menuoptionscript : MonoBehaviour
{
    
    

    // Start is called before the first frame update
    void Start()
    {

        
        

    }

    // Update is called once per frame
    void Update()
    {
        

        
    }

    public void Click_reset()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    public void Click_gamereturn()
    {
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
        SceneManager.LoadScene("Home", LoadSceneMode.Single);
    }

    public void Click_entergame()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    public void Click_exitgame()
    {
        Application.Quit();
        
    }


}
