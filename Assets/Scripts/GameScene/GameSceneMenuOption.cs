using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneMenuOption : MonoBehaviour
{

    public GameObject settingBtn, inerOption;
    [SerializeField] GameController gameController;
    [SerializeField] float speed;


    private void Start()
    {

        Transform iner = inerOption.transform;
        iner.GetChild(0).GetComponent<Button>().onClick.AddListener(ClickGameReturnOrOpenMenu); // ReturnBtn
        iner.GetChild(1).GetChild(0).GetComponent<Button>().onClick.AddListener(ReplayBtn); // ReplayBtn
        iner.GetChild(2).GetComponent<Button>().onClick.AddListener(ResetBtn); // ResetBtn
        iner.GetChild(3).GetComponent<Button>().onClick.AddListener(HomeReturn); // HomeReturnBtn
        settingBtn.GetComponent<Button>().onClick.AddListener(ClickGameReturnOrOpenMenu); // SettingBtn

    }

    public void ClickGameReturnOrOpenMenu()
    {
        AudioManager.Instance.PlayAuido(AudioManager.Instance.ReturnAudio);
        inerOption.SetActive(!inerOption.activeSelf);

        if (!Replay.Instance.IsPlaying)
        {
            inerOption.transform.GetChild(1).gameObject.SetActive(false); // ReplayBtn
            switch (inerOption.activeSelf)
            {
                case true:
                    gameController.enabled = false;
                    break;

                case false:
                    gameController.enabled = true;
                    break;
            }
        }



    }

    public void HomeReturn()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        AudioManager.Instance.PlayAuido(AudioManager.Instance.QuitAudio);
        SceneManager.LoadScene("Home", LoadSceneMode.Single);
    }

    public void ResetBtn()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        AudioManager.Instance.PlayAuido(AudioManager.Instance.RestartAudio);
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);

    }

    public void ReplayBtn()
    {

        for (int i = 2; i <= 3; i++)
        {
            inerOption.transform.GetChild(i).GetComponent<RectTransform>().anchorMin = Vector2.zero;
            inerOption.transform.GetChild(i).GetComponent<RectTransform>().anchorMax = Vector2.zero;
            inerOption.transform.GetChild(i).GetComponent<RectTransform>().pivot = Vector2.zero;
        }

        inerOption.transform.GetChild(2).GetComponent<RectTransform>().anchoredPosition = new Vector2(50, 300);
        inerOption.transform.GetChild(3).GetComponent<RectTransform>().anchoredPosition = new Vector2(50, 50);

        Replay.Instance.PlayReWatch();

    }
}

