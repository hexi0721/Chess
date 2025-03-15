using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


public class AdaptiveCamera : MonoBehaviour
{
    [SerializeField] private float OrthographicSize;

    [SerializeField] SpriteRenderer spriteRenderer;

    private Camera cam;

    float screenRatio;

    void Start()
    {
        cam = GetComponent<Camera>();

        screenRatio = (float)Screen.width / (float)Screen.height;
    }

    void Update()
    {
        CheckScreenOrientation();
    }

    private void CheckScreenOrientation()
    {

        if (Screen.orientation == ScreenOrientation.Portrait)
        {
            cam.orthographicSize = spriteRenderer.bounds.size.x / 2 / screenRatio; 

        }
    }
}


