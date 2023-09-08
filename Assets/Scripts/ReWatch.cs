using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public List<Transform> Chess_Tran = new List<Transform>(); // 要移動的棋子
    public List<Vector3> OriginalLocation = new List<Vector3>(); // 移動棋子原本位置
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

    }

    public void PlayReWatch()
    {
        Debug.Log(Chess_Tran.Count);

        foreach (Transform element in Chess_Tran)
        {

        }
    }
}
