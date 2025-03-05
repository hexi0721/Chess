using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    [SerializeField] GameObject menuOption;
    [SerializeField] GameObject rePlay;
    [SerializeField] GameObject statScrollView;

    [SerializeField] Text whoWin , statText;

    public bool turn;
    public int index;

    [SerializeField] MapController mapController;
    [SerializeField] Action action;

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
        mapController.DestroyAll();
        mapController.InitMap();

        menuOption.transform.GetChild(0).gameObject.SetActive(true); // settingBtn
        menuOption.transform.GetChild(2).gameObject.SetActive(false); // replayBtn
        
        rePlay.SetActive(true);
        statScrollView.SetActive(true);

        whoWin.text = "";
        action.RoundText.text = "第" + (index / 2 + 1) + "回合 - 紅";
        


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

            statText.text = "";

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
               
               statText.text += hit.transform.name[0] + "  吃  " + hit2.transform.name[0] + "\n";
            }
            else
            {
                statText.text += hit.transform.name[0] + "  移動 \n";
            }

            if(statText.rectTransform.sizeDelta.y > 30)
            {
                statText.transform.localPosition = new Vector3(statText.transform.localPosition.x, statText.rectTransform.sizeDelta.y , 0 );
            }

            turn = !turn ;

        }
    }

    void WhoseTurn()
    {
        switch (turn)
        {
            case false:
                action.RoundText.text = "第" + (index / 2 + 1) + "回合 - 紅";
                break;

            case true:
                action.RoundText.text = "第" + (index / 2 + 1) + "回合 - 黑";

                break;
        }
    }

}
