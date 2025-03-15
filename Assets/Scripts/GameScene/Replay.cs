using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Replay : MonoBehaviour
{

    public static Replay Instance { get; private set;}

    public bool IsPlaying = false ;

    public List<Transform> Chess_Tran ; // �n���ʪ��Ѥl
    public List<Vector3> OriginalLocation ; // �Ѥl�쥻��m
    public List<Vector3> Destination ; // ���ʴѤl���ʦ�m

    public List<GameObject> Revive_Chess ; // �n�_�����Ѥl
    public List<bool> isCollision ; // �T�{�O�_�I��

    public bool turn;
    public int index;

    [SerializeField] MapController mapController;
    [SerializeField] Action action;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        index = 0;
        turn = false;


        transform.GetChild(1).gameObject.GetComponent<Button>().onClick.AddListener(Last);
        transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(Next);
    }

    private void Update()
    {
        if (!IsPlaying)
        {
            return;
        }

        action.RoundText.text = "��" + (index / 2 + 1) + "�^�X";

    }

    public void PlayReWatch()
    {
        IsPlaying = true;
        mapController.DestroyAll();
        mapController.InitMap();

        transform.GetChild(0).gameObject.SetActive(false); // replayBtn
        transform.parent.GetChild(2).gameObject.SetActive(true); // ResetBtn
        transform.parent.GetChild(3).gameObject.SetActive(true); // HomeBtn

        
        transform.GetChild(1).gameObject.SetActive(true); // last
        transform.GetChild(2).gameObject.SetActive(true); // next


        action.WhoWinText.text = "";

        Destroy(GameObject.FindWithTag("Focus"));



    }

    public void Last() 
    {
        if (index > 0)
        {
            turn = !turn ;
            index -= 1;

            RaycastHit2D hit = Physics2D.Raycast(Destination[index], Vector3.zero, Mathf.Infinity);
            

            if (hit)
            {
                hit.transform.position = OriginalLocation[index];

                if (isCollision[index] == true)
                {
                    Revive_Chess[^1].SetActive(true); // [^1] ���o�̫�@�Ӥ���

                    Revive_Chess.RemoveAt(Revive_Chess.Count - 1);
                }
            }

        }
    }

    public void Next()
    {
        if (index < Chess_Tran.Count)
        {


            RaycastHit2D hit = Physics2D.Raycast(OriginalLocation[index], Vector3.zero, Mathf.Infinity);

            RaycastHit2D hit2 = Physics2D.Raycast(Destination[index], Vector3.zero, Mathf.Infinity);

            if (hit.collider != null)
            {
                hit.transform.position = Destination[index];


                index += 1;


            }

            turn = !turn ;

        }
    }


}


