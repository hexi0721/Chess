using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    // x一格0.7075 y一格0.71
    public GameObject board;
    
    public Transform mapholder;
    
    // 兵 帥 相 馬 炮 車 士
    public GameObject black1 , black2, black3, black4, black5, black6, black7;
    public GameObject red1 , red2, red3, red4, red5, red6, red7;

    private float x = -2.865f; // x 原點
    private float y = -3.115f; // y 原點
    private float x1 = 0.7075f; // x 間隔
    private float y1 = 0.71f; // y 間隔

    private GameObject child;
    private Vector2 pos;
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
        
        pos = new Vector2(0, 0);
        Instantiation(board , pos); // 生成地圖

        for (int i=0;i < 5;i++) // 生成兵
        {
            
            ChangeScale(red1,false);
            pos = new Vector2(x + i * 2 * x1, y + 3 * y1);
            Instantiation(red1, pos);

            ChangeScale(black1,true);
            pos = new Vector2(x + i * 2 * x1, y + 6 * y1);
            Instantiation(black1, pos);

        }

        // 生成帥
        
        ChangeScale(red2, false);
        pos = new Vector2(x + 4 * x1, y);
        Instantiation(red2 , pos);
        
        ChangeScale(black2, true);
        pos = new Vector2(x + 4 * x1, y + 9 * y1);
        Instantiation(black2, pos);
        
        for (int i = 0; i < 2; i += 1) //  生成相
        {

            ChangeScale(red3, false);
            pos = new Vector2((x + 2 * x1) + i * 4 * x1, y);
            Instantiation(red3, pos);

            ChangeScale(black3, true);
            pos = new Vector2((x + 2 * x1) + i * 4 * x1, y + 9 * y1);
            Instantiation(black3, pos);
            
        }

        for (int i = 0; i < 2; i += 1) //  生成馬
        {

            ChangeScale(red4, false);
            pos = new Vector2((x + x1) + i * 6 * x1, y);
            Instantiation(red4, pos);
            
            ChangeScale(black4, true);
            pos = new Vector2((x + x1) + i * 6 * x1, y + 9 * y1);
            Instantiation(black4, pos);
            
        }

        for (int i = 0; i < 2; i+=1) // 生成炮
        {
            
            ChangeScale(red5,false);
            pos = new Vector2((x + x1) + i * 6 * x1, y + 2 * y1);
            Instantiation(red5, pos);
            
            ChangeScale(black5,true);
            pos = new Vector2((x + x1) + i * 6 * x1, y + 7 * y1);
            Instantiation(black5, pos);
        }

        for (int i = 0; i < 2; i += 1) //  生成車
        {

            ChangeScale(red6, false);
            pos = new Vector2(x + i * 8 * x1, y);
            Instantiation(red6, pos);

            ChangeScale(black6, true);
            pos = new Vector2(x + i * 8 * x1, y + 9 * y1);
            Instantiation(black6, pos);

        }

        for (int i = 0; i < 2; i += 1) // 生成士
        {

            ChangeScale(red7, false);
            pos = new Vector2((x + 3 * x1) + i * 2 * x1, y);
            Instantiation(red7, pos);

            ChangeScale(black7, true);
            pos = new Vector2((x + 3 * x1) + i * 2 * x1, y + 9 * y1);
            Instantiation(black7, pos);
        }

    }


    private void ChangeScale(GameObject G , bool Flip) // 改變大小 轉向 設置層
    {
        G.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        
        if (Flip)
        {
            G.GetComponent<SpriteRenderer>().flipY = true;
            int layer = LayerMask.NameToLayer("black");
            G.layer = layer;
            
        }
        else
        {
            int layer = LayerMask.NameToLayer("red");
            G.layer = layer;
        }
                
    }

    private void Instantiation(GameObject G, Vector2 pos) // 生成棋子
    {
        child = GameObject.Instantiate(G, pos, Quaternion.identity) as GameObject;
        child.transform.SetParent(mapholder);
    }


}
