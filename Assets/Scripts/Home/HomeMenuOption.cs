using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using static UnityEngine.GraphicsBuffer;


public class HomeMenuOption : MonoBehaviour 
{

    public GameObject Arrow_left , Arrow_right;
    [SerializeField] GameObject exitGameBtn, enterGameBtn;
    [SerializeField] float speed = 5f;

    private void Start()
    {
        exitGameBtn.GetComponent<Button>().onClick.AddListener(() => { Application.Quit(); });
        enterGameBtn.GetComponent<Button>().onClick.AddListener(() => {
            AudioManager.Instance.PlayAuido(AudioManager.Instance.RestartAudio);
            SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
        });



        CreateEventEntry(exitGameBtn);
        CreateEventEntry(enterGameBtn);

    }

    void Update()
    {
        // �b�Y����
        Arrow_left.transform.Rotate(speed * Time.deltaTime,0,0);
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
        Arrow_left.GetComponent<RectTransform>().localPosition = new Vector3(Arrow_left.GetComponent<RectTransform>().localPosition.x, go.GetComponent<RectTransform>().localPosition.y, 0);
        Arrow_right.GetComponent<RectTransform>().localPosition = new Vector3(Arrow_right.GetComponent<RectTransform>().localPosition.x, go.GetComponent<RectTransform>().localPosition.y, 0);
    }


}
