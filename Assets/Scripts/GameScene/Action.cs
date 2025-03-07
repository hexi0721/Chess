using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Action : MonoBehaviour
{

    [SerializeField] Text WhoWinText ;
    public Text RoundText;

    [SerializeField] GameObject StatScrollView;
    GameController gameController;
    [SerializeField] GameSceneMenuOption gameSceneMenuOption;

    

    void Start()
    {

        gameController = GetComponent<GameController>();
        
        gameSceneMenuOption.inerOption.SetActive(false); // 順序不可往上移動
        
        StatScrollView.SetActive(false);
    }

    void Update()
    {
        if (Replay.Instance.IsPlaying)
            return;

        if (gameController.IsEnd)
        {

            switch (gameController.Turn)
            {
                case true:
                    WhoWinText.text = "紅方勝 !";
                    break;

                case false:
                    WhoWinText.text = "黑方勝 !";
                    break;
            }


            gameSceneMenuOption.settingBtn.SetActive(false); // SettingBtn
            gameSceneMenuOption.inerOption.SetActive(true); // InerOption
            gameSceneMenuOption.inerOption.transform.GetChild(0).gameObject.SetActive(false); // GameReturnBtn

            var rePlay = gameSceneMenuOption.inerOption.transform.GetChild(1).gameObject; // Replay
            rePlay.SetActive(true);
            rePlay.transform.GetChild(0).gameObject.SetActive(true);
            rePlay.transform.GetChild(1).gameObject.SetActive(false);
            rePlay.transform.GetChild(2).gameObject.SetActive(false);

            gameSceneMenuOption.inerOption.transform.GetChild(2).gameObject.SetActive(true); // ResetBtn
            gameSceneMenuOption.inerOption.transform.GetChild(3).gameObject.SetActive(true); // HomeBtn

        }

        if (Input.GetKeyDown(KeyCode.Escape) && gameController.IsEnd != true)
        {
            gameSceneMenuOption.ClickGameReturnOrOpenMenu();
            
        }
    }

    
}
