using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Action : MonoBehaviour
{

    public TextMeshProUGUI WhoWinText , RoundText;
    

    GameController gameController;
    [SerializeField] GameSceneMenuOption gameSceneMenuOption;

    void Start()
    {

        gameController = GetComponent<GameController>();
        
        gameSceneMenuOption.inerOption.SetActive(false); // ���Ǥ��i���W����
        

    }

    void Update()
    {
        if (Replay.Instance.IsPlaying)
            return;

        if (gameController.IsEnd)
        {
            WhoWinText.gameObject.SetActive(true);
            RectTransform rt = WhoWinText.GetComponent<RectTransform>();
            switch (gameController.Turn)
            {

                case true:
                    WhoWinText.text = "�����";
                    WhoWinText.color = Color.red;
                    rt.anchorMin = new Vector2(0.5f, 0f);
                    rt.anchorMax = new Vector2(0.5f, 1f);
                    rt.localRotation = Quaternion.Euler(0, 0, 0);



                    break;

                case false:
                    WhoWinText.text = "�¤��";
                    WhoWinText.color = Color.black;
                    rt.anchorMin = new Vector2(0.5f, 0f);
                    rt.anchorMax = new Vector2(0.5f, 1f);
                    rt.localRotation = Quaternion.Euler(0, 0, 180);
                 
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
