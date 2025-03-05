using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Action : MonoBehaviour
{

    [SerializeField] GameObject replay;

    [SerializeField] Text WhoWinText ;
    public Text RoundText;

    [SerializeField] GameObject StatScrollView;
    GameController gameController;
    [SerializeField] GameSceneMenuOption gameSceneMenuOption;

    public bool IsEnd { get; set; }

    void Start()
    {
        IsEnd = false;

        gameController = GetComponent<GameController>();
        gameSceneMenuOption.inerOption.SetActive(false); // 順序不可往上移動
        replay.SetActive(false);

        StatScrollView.SetActive(false);
    }

    private void LateUpdate()
    {
        if (IsEnd)
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
            gameSceneMenuOption.inerOption.transform.GetChild(0).gameObject.SetActive(false); // GameReturnBtn
            gameSceneMenuOption.inerOption.transform.GetChild(1).gameObject.SetActive(true); // ReplayBtn
            
            gameController.enabled = false;

        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && IsEnd != true)
        {
            gameSceneMenuOption.ClickGameReturnOrOpenMenu();
            
        }
    }

    
}
