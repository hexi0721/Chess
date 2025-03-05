using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.RegularExpressions;

public class MapController : MonoBehaviour
{
    public GameObject board;
    
    public Transform RedChess;
    public Transform BlackChess;

    // �L �� �� �� �� �� �h
    public GameObject black1 , black2, black3, black4, black5, black6, black7;
    public GameObject red1 , red2, red3, red4, red5, red6, red7;

    const int x = -8; // x ���I
    const int y = -9; // y ���I
    const int xInterval = 2; // x ���j
    const int yInterval = 2; // y ���j

    GameObject child;
    Vector3 pos;


    void Start()
    {
        
        pos = new Vector3(0, 0, 0);
        Instantiation(board, pos); // �ͦ��a��
        InitMap();
    }

    
    public void InitMap()
    {
        // �ͦ��L
        for (int i = 0; i < 5; i++)
        {
            pos = new Vector3(x + i * 2 * xInterval, y + 3 * yInterval, 0);
            Instantiation(red1, pos);
            pos = new Vector3(x + i * 2 * xInterval, y + 6 * yInterval, 0);
            Instantiation(black1, pos);

        }

        // �ͦ���
        pos = new Vector3(x + 4 * xInterval, y, 0);
        Instantiation(red2, pos);
        pos = new Vector3(x + 4 * xInterval, y + 9 * yInterval, 0);
        Instantiation(black2, pos);

        
        for (int i = 0; i < 2; i += 1)
        {
            // �ͦ���
            pos = new Vector3((x + 2 * xInterval) + i * 4 * xInterval, y, 0);
            Instantiation(red3, pos);
            pos = new Vector3((x + 2 * xInterval) + i * 4 * xInterval, y + 9 * yInterval, 0);
            Instantiation(black3, pos);

            //  �ͦ���
            pos = new Vector3((x + xInterval) + i * 6 * xInterval, y, 0);
            Instantiation(red4, pos);
            pos = new Vector3((x + xInterval) + i * 6 * xInterval, y + 9 * yInterval, 0);
            Instantiation(black4, pos);

            // �ͦ���
            pos = new Vector3((x + xInterval) + i * 6 * xInterval, y + 2 * yInterval, 0);
            Instantiation(red5, pos);
            pos = new Vector3((x + xInterval) + i * 6 * xInterval, y + 7 * yInterval, 0);
            Instantiation(black5, pos);

            //  �ͦ���
            pos = new Vector3(x + i * 8 * xInterval, y, 0);
            Instantiation(red6, pos);
            pos = new Vector3(x + i * 8 * xInterval, y + 9 * yInterval, 0);
            Instantiation(black6, pos);

            // �ͦ��h
            pos = new Vector3((x + 3 * xInterval) + i * 2 * xInterval, y, 0);
            Instantiation(red7, pos);
            pos = new Vector3((x + 3 * xInterval) + i * 2 * xInterval, y + 9 * yInterval, 0);
            Instantiation(black7, pos);
        }


    }


    public void ChangeScale(GameObject G , string S ,bool Flip ) // ���ܤj�p �]�m�h
    {
        int layer;
        G.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        
        if (S == "black")
        {
            /* ��V
            if (Flip)
            {
                G.transform.Rotate(new Vector3(0, 0, 180));
            }
            */
            layer = LayerMask.NameToLayer("black");
            G.layer = layer;
            
        }
        else if (S == "red")
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
            ChangeScale(child, "red" ,false);
            child.transform.SetParent(RedChess);
        }
        else if (Regex.IsMatch(G.transform.tag, "black"))
        {
            if(GameObject.Find("AIManager"))
            {
                ChangeScale(child, "black" , false);
            }
            else
            {
                ChangeScale(child, "black", true);
            }
            
            
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
