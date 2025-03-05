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
        // Ωb¿Y±€¬‡
        Arrow_left.transform.Rotate(0.1f,0,0);
        Arrow_right.transform.Rotate(0.1f, 0, 0);
        
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
        
        
        Arrow_left.transform.SetParent(go.transform);
        Arrow_left.GetComponent<RectTransform>().localPosition = new Vector3(go.transform.localPosition.x + 225, 0, 0);

        Arrow_right.transform.SetParent(go.transform);
        Arrow_right.GetComponent<RectTransform>().localPosition = new Vector3(go.transform.localPosition.x - 225, 0, 0);

    }


}
