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
        AudioManager.Instance.PlayAuido(AudioManager.Instance.Audio1);
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
        ForeGroundTransition.instance.StartTransition("Home");
        AudioManager.Instance.PlayAuido(AudioManager.Instance.QuitAudio);
        
    }

    public void ResetBtn()
    {
        ForeGroundTransition.instance.StartTransition("GameScene");
        AudioManager.Instance.PlayAuido(AudioManager.Instance.RestartAudio);

    }

    public void ReplayBtn()
    {

        ForeGroundTransition.instance.StartTransition();
        AudioManager.Instance.PlayAuido(AudioManager.Instance.Audio1);

    }


    
}

