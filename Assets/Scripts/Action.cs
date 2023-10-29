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
        

        gamereturn_btn = GameObject.Find("gamereturn_btn"); // ���s�}�l���s
        setting_btn = GameObject.Find("Setting_btn"); // �]�w���s
        reset_btn = GameObject.Find("reset_btn"); // �������s
        replay = GameObject.Find("Replay"); // ���s����W�@��
        replay_btn = GameObject.Find("replay_btn");

        menuplain.SetActive(false); // ���Ǥ��i���W����
        replay.SetActive(false);

        


        WhoWinText = GameObject.Find("whowin").GetComponent<Text>(); // �ֳӽ֭t��r
        WhoWinText.text = "";

        RoundText = GameObject.Find("round").GetComponent<Text>(); // �^�X��r
        

        StatText = GameObject.Find("StatContent").GetComponent<Text>(); // ���A��r
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
