using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ForeGroundTransition : MonoBehaviour
{
    
    public static ForeGroundTransition instance { get; private set; }

    [SerializeField] Text fpsText;
    [SerializeField] Image transitionForeGround;
    [SerializeField] float speed = 1.0f;
    [SerializeField] float scale = 0f;


    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        
        float fps = 1.0f / Time.deltaTime;

        if (fpsText != null)
        {
            fpsText.text = $"FPS : {Mathf.RoundToInt(fps)}";
        }
    }

    public void StartTransition(string sceneName = null)
    {
        
        StartCoroutine(TransitionOutAndLoadScene(sceneName));
    }

    public void EndTransition()
    {
        StartCoroutine(TransitionIn());
    }

    private IEnumerator TransitionOutAndLoadScene(string sceneName = null)
    {
 
        float targetScale = 1.0f;
        while (scale < targetScale)
        {

            scale += Time.deltaTime * speed;
            transitionForeGround.rectTransform.localScale = Vector3.one * scale;

            yield return null;
        }

        transitionForeGround.rectTransform.localScale = Vector3.one;

        if (sceneName != null)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
            yield return null; 
            GetComponent<Canvas>().worldCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
            EndTransition();
        }
        else
        {
            yield return null;
            Replay.Instance.PlayReWatch();
            EndTransition();
        }


    }

    private IEnumerator TransitionIn()
    {

        float targetScale = 0f;
        while (scale > targetScale)
        {

            scale -= Time.deltaTime * speed;
            transitionForeGround.rectTransform.localScale = Vector3.one * scale;

            yield return null;
        }


        transitionForeGround.rectTransform.localScale = Vector3.zero;

    }


}
