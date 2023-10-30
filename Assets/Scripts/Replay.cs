using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Replay : MonoBehaviour
{
    static Replay _instance;

    public static Replay Instance
    {

        get
        {
            return _instance;
        }
    }


    public bool Isplay = false ;

    public List<Transform> Chess_Tran ; // 要移動的棋子
    public List<Vector3> OriginalLocation ; // 棋子原本位置
    public List<Vector3> Destination ; // 移動棋子移動位置

    public List<GameObject> Revive_Chess ; // 要復活的棋子
    public List<bool> isCollision ; // 確認是否碰撞

    public GameObject Focus; // 瞄準
    

    public bool turn;
    public int index;

    

    void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        
        turn = false;

        
    }

    public void PlayReWatch()
    {
        Isplay = true;
        MapController.Instance.DestroyAll();
        MapController.Instance.InitMap();

        Action.Instance.menuplain.SetActive(false);
        Action.Instance.replay_btn.SetActive(false);
        Action.Instance.setting_btn.SetActive(true);
        Action.Instance.replay.SetActive(true);
        Action.Instance.StatScrollView.SetActive(true);

        Action.Instance.WhoWinText.text = "";
        Action.Instance.RoundText.text = "第" + (index / 2 + 1) + "回合 - 紅";
        


        Focus = GameObject.FindWithTag("Focus");
        Focus.SetActive(false);
        

    }

    public void Last() 
    {
        if (index > 0)
        {
            turn = !turn ;
            index -= 1;

            WhoseTurn();

            Action.Instance.StatText.text = "";

            RaycastHit2D hit = Physics2D.Raycast(Destination[index], Vector3.zero, Mathf.Infinity);
            

            if (hit)
            {
                hit.transform.position = OriginalLocation[index];
                Focus.SetActive(true);
                Focus.transform.position = OriginalLocation[index];


                if (isCollision[index] == true)
                {
                    Revive_Chess[^1].SetActive(true);
                    
                    Revive_Chess.RemoveAt(Revive_Chess.Count - 1);
                }
            }

        }
    }

    public void Next()
    {
        if (index < Chess_Tran.Count)
        {
            WhoseTurn();

            RaycastHit2D hit = Physics2D.Raycast(OriginalLocation[index], Vector3.zero, Mathf.Infinity);

            RaycastHit2D hit2 = Physics2D.Raycast(Destination[index], Vector3.zero, Mathf.Infinity);

            if (hit)
            {
                hit.transform.position = Destination[index];
                Focus.SetActive(true);
                Focus.transform.position = Destination[index];

                index += 1;
            }

            if (hit2)
            {
               
               Action.Instance.StatText.text += hit.transform.name[0] + "  吃  " + hit2.transform.name[0] + "\n";
            }
            else
            {
                Action.Instance.StatText.text += hit.transform.name[0] + "  移動 \n";
            }

            if(Action.Instance.StatText.rectTransform.sizeDelta.y > 30)
            {
                Action.Instance.StatText.transform.localPosition = new Vector3(Action.Instance.StatText.transform.localPosition.x, Action.Instance.StatText.rectTransform.sizeDelta.y , 0 );
            }

            turn = !turn ;

        }
    }

    void WhoseTurn()
    {
        switch (turn)
        {
            case false:
                Action.Instance.RoundText.text = "第" + (index / 2 + 1) + "回合 - 紅";
                break;

            case true:
                Action.Instance.RoundText.text = "第" + (index / 2 + 1) + "回合 - 黑";

                break;
        }
    }

}
