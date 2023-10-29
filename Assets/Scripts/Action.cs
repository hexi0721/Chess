using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Action : MonoBehaviour
{

    static Action _instance; // ctrl + r + e

    public static Action Instance { get => _instance; }


    public GameObject replay;

    // UI btn
    public GameObject menuplain;
    public GameObject gamereturn_btn;
    public GameObject setting_btn;
    public GameObject reset_btn;
    public GameObject replay_btn;


    // Text
    public Text WhoWinText;
    public Text RoundText;
    public Text StatText;
    
    // Scroll

    public GameObject StatScrollView;



    private void Awake()
    {
        _instance = this ;
    }


    // Start is called before the first frame update
    void Start()
    {
        menuplain = GameObject.Find("menuplain");
        

        gamereturn_btn = GameObject.Find("gamereturn_btn"); // 重新開始按鈕
        setting_btn = GameObject.Find("Setting_btn"); // 設定按鈕
        reset_btn = GameObject.Find("reset_btn"); // 重玩按鈕
        replay = GameObject.Find("Replay"); // 重新播放上一局
        replay_btn = GameObject.Find("replay_btn");

        menuplain.SetActive(false); // 順序不可往上移動
        replay.SetActive(false);

        


        WhoWinText = GameObject.Find("whowin").GetComponent<Text>(); // 誰勝誰負文字
        WhoWinText.text = "";

        RoundText = GameObject.Find("round").GetComponent<Text>(); // 回合文字
        

        StatText = GameObject.Find("StatContent").GetComponent<Text>(); // 狀態文字
        StatText.text = "";

        StatScrollView = GameObject.Find("Stat");
        StatScrollView.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && GameController.Isend != true)
        {
            menuplain.SetActive(!menuplain.activeSelf);
            replay.SetActive(false);
            replay_btn.SetActive(false);
            switch (menuplain.activeSelf)
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
}
