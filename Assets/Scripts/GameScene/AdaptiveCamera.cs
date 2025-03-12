using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]  // 在編輯模式下也更新
public class AdaptiveCamera : MonoBehaviour
{
    [SerializeField] private float targetWidth = 2160f;  // 設定基準寬度
    [SerializeField] private float targetHeight = 1080f; // 設定基準高度
    [SerializeField] private float targetOrthographicSize = 5f; // 預設正交大小

    private Camera cam;

    [SerializeField] GameController gameController;


    void Start()
    {

        cam = GetComponent<Camera>();

        UpdateCameraSize();
    }

    void Update()
    {
        if (gameController.IsEnd)
            return;


        UpdateCameraSize();

    }

    private void UpdateCameraSize()
    {
        

        float screenRatio = (float)Screen.width / Screen.height;
        float targetRatio = targetWidth / targetHeight;

        // 根據比例自動調整正交大小
        if (screenRatio >= targetRatio)
        {
            cam.orthographicSize = targetOrthographicSize;
        }
        else
        {
            float differenceInSize = targetRatio / screenRatio;
            cam.orthographicSize = targetOrthographicSize * differenceInSize;
        }
    }
}
