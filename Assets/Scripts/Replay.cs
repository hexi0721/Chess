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

    public List<Transform> Chess_Tran = new List<Transform>(); // �n���ʪ��Ѥl
    public List<Vector3> OriginalLocation = new List<Vector3>(); // �Ѥl�쥻��m
    public List<Vector3> Destination = new List<Vector3>(); // ���ʴѤl���ʦ�m

    public List<GameObject> Revive_Chess = new List<GameObject>(); // �n�_�����Ѥl
    public List<bool> isCollision = new List<bool>(); // �T�{�O�_�I��


    public GameObject stat;
    public GameObject Focus; // �˷�
    

    public bool turn;
    public int index;

    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        
        turn = false;

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayReWatch()
    {
        Isplay = true;
        MapController.Instance.DestroyAll();
        MapController.Instance.initMap();

        Action.Instance.menuplain.SetActive(false);
        Action.Instance.replay_btn.SetActive(false);
        Action.Instance.setting_btn.SetActive(true);
        Action.Instance.replay.SetActive(true);

        Action.Instance.WhoWinText.text = "";
        Action.Instance.RoundText.text = "";
        Action.Instance.StatText.text = "";

        Focus = GameObject.FindWithTag("Focus");
        Destroy(stat);

    }

    public void Last() 
    {
        if (index > 0)
        {
            turn = !turn ;
            index -= 1;

            whoseturn();

            RaycastHit2D hit = Physics2D.Raycast(Destination[index], Vector3.zero, Mathf.Infinity);

            if (hit)
            {
                hit.transform.position = OriginalLocation[index];
                Focus.transform.position = OriginalLocation[index];


                if (isCollision[index] == true)
                {
                    Revive_Chess[Revive_Chess.Count - 1].SetActive(true);
                    
                    Revive_Chess.RemoveAt(Revive_Chess.Count - 1);
                }
            }

        }
    }

    public void Next()
    {
        if (index < Chess_Tran.Count)
        {
            whoseturn();

            RaycastHit2D hit = Physics2D.Raycast(OriginalLocation[index], Vector3.zero, Mathf.Infinity);

            if (hit)
            {
                hit.transform.position = Destination[index];
                Focus.transform.position = Destination[index];

                index += 1;
            }

            turn = !turn ;

        }
    }

    void whoseturn()
    {
        switch (turn)
        {
            case false:
                Action.Instance.RoundText.text = "��" + (index / 2 + 1) + "�^�X - ��";
                break;

            case true:
                Action.Instance.RoundText.text = "��" + (index / 2 + 1) + "�^�X - ��";

                break;
        }
    }

}
