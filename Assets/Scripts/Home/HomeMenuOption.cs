using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class HomeMenuOption : MonoBehaviour 
{

    [SerializeField] GameObject exitGameBtn, enterGameBtn;

    private void Start()
    {

        exitGameBtn.GetComponent<Button>().onClick.AddListener(() => { Application.Quit(); });
        enterGameBtn.GetComponent<Button>().onClick.AddListener(() => {
            AudioManager.Instance.PlayAuido(AudioManager.Instance.RestartAudio);

            ForeGroundTransition.instance.StartTransition("GameScene");

        });

    }





}
