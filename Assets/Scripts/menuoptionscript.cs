using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;

public class menuoptionscript : MonoBehaviour
{
    private GameObject menuplain;

    // Start is called before the first frame update
    void Start()
    {

        menuplain = GameObject.Find("menuplain");
        

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
        menuplain.SetActive(!menuplain.activeSelf);
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
