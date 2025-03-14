using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSceneMenuOption : MonoBehaviour
{

    public GameObject Arrow_left, Arrow_right;
    public GameObject settingBtn , inerOption;
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

        CreateEventEntry(iner.GetChild(0).gameObject);
        CreateEventEntry(iner.GetChild(1).GetChild(0).gameObject);
        CreateEventEntry(iner.GetChild(2).gameObject);
        CreateEventEntry(iner.GetChild(3).gameObject);

        HoverButton(iner.GetChild(0).gameObject);
    }

    void Update()
    {
        // Ωb¿Y±€¬‡
        Arrow_left.transform.Rotate(speed * Time.deltaTime, 0, 0);
        Arrow_right.transform.Rotate(speed * Time.deltaTime, 0, 0);


    }

    public void CreateEventEntry(GameObject go)
    {
        if (go.GetComponent<EventTrigger>() == null)
        {
            go.AddComponent<EventTrigger>();
        }


        EventTrigger.Entry entry = new EventTrigger.Entry { eventID = EventTriggerType.PointerEnter };

        entry.callback.AddListener((data) => { HoverButton(go); });
        go.GetComponent<EventTrigger>().triggers.Add(entry);
    }

    public void HoverButton(GameObject go)
    {
        float range = 250f;
        Arrow_left.GetComponent<RectTransform>().localPosition = new Vector3(go.GetComponent<RectTransform>().localPosition.x + range, go.GetComponent<RectTransform>().localPosition.y, 0);
        Arrow_right.GetComponent<RectTransform>().localPosition = new Vector3(go.GetComponent<RectTransform>().localPosition.x - range, go.GetComponent<RectTransform>().localPosition.y, 0);
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
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        inerOption.transform.GetChild(2).GetComponent<RectTransform>().localPosition = new Vector2(-750, -180);
        inerOption.transform.GetChild(3).GetComponent<RectTransform>().localPosition = new Vector2(-750, -400);
        Replay.Instance.PlayReWatch();
    }

}
