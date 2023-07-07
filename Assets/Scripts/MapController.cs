using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class MapController : MonoBehaviour
{
    
    public GameObject board;
    
    public Transform RedChess;
    public Transform BlackChess;

    // 兵 帥 相 馬 炮 車 士
    public GameObject black1 , black2, black3, black4, black5, black6, black7;
    public GameObject red1 , red2, red3, red4, red5, red6, red7;

    //public static List<Vector3> location = new List<Vector3>(); // 已存在的座標

    private int x = -8; // x 原點
    private int y = -9; // y 原點
    private int x1 = 2; // x 間隔
    private int y1 = 2; // y 間隔

    private GameObject child;
    private Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        initMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void initMap()
    {
        
        pos = new Vector3(0, 0 ,0);
        Instantiation(board , pos); // 生成地圖
        //location.Remove(new Vector3(0,0,0));

        for (int i=0;i < 5;i++) // 生成兵
        {
            
            ChangeScale(red1,false);
            pos = new Vector3(x + i * 2 * x1, y + 3 * y1 , 0);
            Instantiation(red1, pos);

            ChangeScale(black1,true);
            pos = new Vector3(x + i * 2 * x1, y + 6 * y1 , 0);
            Instantiation(black1, pos);

        }

        // 生成帥
        ChangeScale(red2, false);
        pos = new Vector3(x + 4 * x1, y , 0);
        Instantiation(red2 , pos);
        
        ChangeScale(black2, true);
        pos = new Vector3(x + 4 * x1, y + 9 * y1 , 0);
        Instantiation(black2, pos);

        //  生成相
        for (int i = 0; i < 2; i += 1) 
        {

            ChangeScale(red3, false);
            pos = new Vector3((x + 2 * x1) + i * 4 * x1, y , 0);
            Instantiation(red3, pos);

            ChangeScale(black3, true);
            pos = new Vector3((x + 2 * x1) + i * 4 * x1, y + 9 * y1 , 0);
            Instantiation(black3, pos);
            
        }

        //  生成馬
        for (int i = 0; i < 2; i += 1) 
        {

            ChangeScale(red4, false);
            pos = new Vector3((x + x1) + i * 6 * x1, y , 0);
            Instantiation(red4, pos);
            
            ChangeScale(black4, true);
            pos = new Vector3((x + x1) + i * 6 * x1, y + 9 * y1 , 0);
            Instantiation(black4, pos);
            
        }

        // 生成炮
        for (int i = 0; i < 2; i+=1) 
        {
            
            ChangeScale(red5,false);
            pos = new Vector3((x + x1) + i * 6 * x1, y + 2 * y1 , 0);
            Instantiation(red5, pos);
            
            ChangeScale(black5,true);
            pos = new Vector3((x + x1) + i * 6 * x1, y + 7 * y1 , 0);
            Instantiation(black5, pos);
        }

        //  生成車
        for (int i = 0; i < 2; i += 1) 
        {

            ChangeScale(red6, false);
            pos = new Vector3(x + i * 8 * x1, y, 0);
            Instantiation(red6, pos);

            ChangeScale(black6, true);
            pos = new Vector3(x + i * 8 * x1, y + 9 * y1 , 0);
            Instantiation(black6, pos);

        }

        // 生成士
        for (int i = 0; i < 2; i += 1) 
        {

            ChangeScale(red7, false);
            pos = new Vector3((x + 3 * x1) + i * 2 * x1, y , 0);
            Instantiation(red7, pos);

            ChangeScale(black7, true);
            pos = new Vector3((x + 3 * x1) + i * 2 * x1, y + 9 * y1 , 0);
            Instantiation(black7, pos);
        }

    }


    private void ChangeScale(GameObject G , bool Flip) // 改變大小 轉向 設置層
    {
        int layer;
        G.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        
        if (Flip)
        {
            G.GetComponent<SpriteRenderer>().flipY = true;
            layer = LayerMask.NameToLayer("black");
            G.layer = layer;
            
        }
        else
        {
            layer = LayerMask.NameToLayer("red");
            G.layer = layer;
            
        }
                
    }

    private void Instantiation(GameObject G, Vector3 pos) // 生成棋子
    {
        child = GameObject.Instantiate(G, pos, Quaternion.identity) as GameObject;

        

        if (Regex.IsMatch(G.transform.tag, "red"))
        {
            
            child.transform.SetParent(RedChess);
        }
        else if (Regex.IsMatch(G.transform.tag, "black"))
        {
            child.transform.SetParent(BlackChess);
        }
            

        //location.Add(pos);
    }


}
