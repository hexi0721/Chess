using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]  // �b�s��Ҧ��U�]��s
public class AdaptiveCamera : MonoBehaviour
{
    [SerializeField] private float targetWidth = 2160f;  // �]�w��Ǽe��
    [SerializeField] private float targetHeight = 1080f; // �]�w��ǰ���
    [SerializeField] private float targetOrthographicSize = 5f; // �w�]����j�p

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

        // �ھڤ�Ҧ۰ʽվ㥿��j�p
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
