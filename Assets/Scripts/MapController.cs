using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class MapController : MonoBehaviour
{
    static MapController _instance;

    public static MapController Instance
    {
        get
        {
            return _instance;
        }
    }


    public GameObject board;
    
    public Transform RedChess;
    public Transform BlackChess;

    // �L �� �� �� �� �� �h
    public GameObject black1 , black2, black3, black4, black5, black6, black7;
    public GameObject red1 , red2, red3, red4, red5, red6, red7;

    int x = -8; // x ���I
    int y = -9; // y ���I
    int x1 = 2; // x ���j
    int y1 = 2; // y ���j

    GameObject child;
    Vector3 pos;

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        pos = new Vector3(0, 0, 0);
        Instantiation(board, pos); // �ͦ��a��

        initMap();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void initMap()
    {

        if (GameObject.Find("GameManager"))
        {
            // �ͦ��L
            for (int i = 0; i < 5; i++)
            {

                ChangeScale(red1, false);
                pos = new Vector3(x + i * 2 * x1, y + 3 * y1, 0);
                Instantiation(red1, pos);

                ChangeScale(black1, true);
                pos = new Vector3(x + i * 2 * x1, y + 6 * y1, 0);
                Instantiation(black1, pos);

            }

            // �ͦ���
            ChangeScale(red2, false);
            pos = new Vector3(x + 4 * x1, y, 0);
            Instantiation(red2, pos);

            ChangeScale(black2, true);
            pos = new Vector3(x + 4 * x1, y + 9 * y1, 0);
            Instantiation(black2, pos);

            //  �ͦ���
            for (int i = 0; i < 2; i += 1)
            {

                ChangeScale(red3, false);
                pos = new Vector3((x + 2 * x1) + i * 4 * x1, y, 0);
                Instantiation(red3, pos);

                ChangeScale(black3, true);
                pos = new Vector3((x + 2 * x1) + i * 4 * x1, y + 9 * y1, 0);
                Instantiation(black3, pos);

            }

            //  �ͦ���
            for (int i = 0; i < 2; i += 1)
            {

                ChangeScale(red4, false);
                pos = new Vector3((x + x1) + i * 6 * x1, y, 0);
                Instantiation(red4, pos);

                ChangeScale(black4, true);
                pos = new Vector3((x + x1) + i * 6 * x1, y + 9 * y1, 0);
                Instantiation(black4, pos);

            }

            // �ͦ���
            for (int i = 0; i < 2; i += 1)
            {

                ChangeScale(red5, false);
                pos = new Vector3((x + x1) + i * 6 * x1, y + 2 * y1, 0);
                Instantiation(red5, pos);

                ChangeScale(black5, true);
                pos = new Vector3((x + x1) + i * 6 * x1, y + 7 * y1, 0);
                Instantiation(black5, pos);
            }

            //  �ͦ���
            for (int i = 0; i < 2; i += 1)
            {

                ChangeScale(red6, false);
                pos = new Vector3(x + i * 8 * x1, y, 0);
                Instantiation(red6, pos);

                ChangeScale(black6, true);
                pos = new Vector3(x + i * 8 * x1, y + 9 * y1, 0);
                Instantiation(black6, pos);

            }

            // �ͦ��h
            for (int i = 0; i < 2; i += 1)
            {

                ChangeScale(red7, false);
                pos = new Vector3((x + 3 * x1) + i * 2 * x1, y, 0);
                Instantiation(red7, pos);

                ChangeScale(black7, true);
                pos = new Vector3((x + 3 * x1) + i * 2 * x1, y + 9 * y1, 0);
                Instantiation(black7, pos);
            }
        }
        

    }


    public void ChangeScale(GameObject G , bool Flip) // ���ܤj�p ��V �]�m�h
    {
        int layer;
        G.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        
        if (Flip)
        {
            G.GetComponent<SpriteRenderer>().flipY = false;
            layer = LayerMask.NameToLayer("black");
            G.layer = layer;
            
        }
        else
        {
            layer = LayerMask.NameToLayer("red");
            G.layer = layer;
            
        }
                
    }

    public void Instantiation(GameObject G, Vector3 pos) // �ͦ��Ѥl
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
            

    }

    public void DestroyAll()
    {
        for (int i = RedChess.childCount - 1; i >= 0; i--)
        {
            Destroy(RedChess.GetChild(i).gameObject);
            
        }

        for (int i = BlackChess.childCount - 1; i >= 0; i--)
        {
            Destroy(BlackChess.GetChild(i).gameObject);

        }
    }


}
