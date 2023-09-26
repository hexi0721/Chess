using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReWatch : MonoBehaviour
{
    static ReWatch _instance;

    public static ReWatch Instance
    {
        get
        {
            return _instance;
        }
    }

    bool Isplay = false;

    public List<Transform> Chess_Tran = new List<Transform>(); // 要移動的棋子
    public List<Vector3> OriginalLocation = new List<Vector3>(); // 棋子原本位置
    public List<Vector3> Destination = new List<Vector3>(); // 移動棋子移動位置

    


    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RePlay();
    }

    public void PlayReWatch()
    {

        MapController.Instance.DestroyAll();
        MapController.Instance.initMap();

        Action.Instance.menuplain.SetActive(false);

        Action.Instance.WhoWinText.text = "";

        Isplay = true;

        
    }

    private void RePlay()
    {
        if(Isplay == true)
        {
            for (int i = 0; i < Chess_Tran.Count; i++)
            {
                RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(OriginalLocation[i]), Vector3.zero,
                Mathf.Infinity );

                if (hit)
                {
                    hit.transform.position = Destination[i];
                }
                


            }
        }
    }


}
