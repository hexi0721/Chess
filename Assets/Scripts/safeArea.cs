using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SafeArea : MonoBehaviour 
{
    RectTransform rectTransform;
    Rect safeArea;
    [SerializeField] Vector2 minAnchor , maxAnchor;

    private void Awake()
    {
        RunSafeArea();

    }
    
    private void Update()
    {
        if (safeArea != Screen.safeArea)
        {
            RunSafeArea();
        }
    }
    
    public void RunSafeArea()
    {
        rectTransform = GetComponent<RectTransform>();
        safeArea = Screen.safeArea;

        minAnchor = safeArea.position;
        maxAnchor = minAnchor + safeArea.size;

        minAnchor.x /= Screen.width;
        minAnchor.y /= Screen.height;
        maxAnchor.x /= Screen.width;
        maxAnchor.y /= Screen.height;

        rectTransform.anchorMin = minAnchor;
        rectTransform.anchorMax = maxAnchor;
    }


}
