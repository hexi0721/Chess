using System.Collections;
using System.Collections.Generic;
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

    public GameObject Focus; // �˷�

    [SerializeField] Text whoWin , replayContent;

    public bool turn;
    public int index;

    [SerializeField] MapController mapController;
    [SerializeField] Action action;
    [SerializeField] GameSceneMenuOption gameSceneMenuOption;

    List<string> replayContentString;
    string allStrings;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        index = 0;
        turn = false;


        replayContentString = new List<string>();
        allStrings = "";

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


        replayContent.GetComponent<RectTransform>().localPosition = new Vector3(replayContent.transform.localPosition.x, replayContent.GetComponent<RectTransform>().sizeDelta.y, 0);
    }

    public void PlayReWatch()
    {
        IsPlaying = true;
        mapController.DestroyAll();
        mapController.InitMap();

        transform.GetChild(0).gameObject.SetActive(false); // replayBtn
        transform.parent.GetChild(2).gameObject.SetActive(true); // ResetBtn
        transform.parent.GetChild(3).gameObject.SetActive(true); // HomeBtn

        gameSceneMenuOption.HoverButton(transform.parent.GetChild(2).gameObject);
        transform.GetChild(1).gameObject.SetActive(true); // last
        transform.GetChild(2).gameObject.SetActive(true); // next
        transform.GetChild(3).gameObject.SetActive(true); // replayView

        whoWin.text = "";
        Focus = GameObject.FindWithTag("Focus");
        Focus.SetActive(false);

    }

    public void Last() 
    {
        if (index > 0)
        {
            turn = !turn ;
            index -= 1;

            replayContentString.RemoveAt(replayContentString.Count - 1);

            allStrings = "";
            foreach (string s in replayContentString)
            {
                allStrings = string.Join("\n", replayContentString);
            }

            replayContent.text = allStrings;

            RaycastHit2D hit = Physics2D.Raycast(Destination[index], Vector3.zero, Mathf.Infinity);
            

            if (hit)
            {
                hit.transform.position = OriginalLocation[index];
                Focus.SetActive(true);
                Focus.transform.position = OriginalLocation[index];


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
                Focus.SetActive(true);
                Focus.transform.position = Destination[index];

                index += 1;

                if (hit2.collider != null)
                {
                    replayContentString.Add(hit.transform.name[0] + "  �Y  " + hit2.transform.name[0]);

                }
                else
                {
                    replayContentString.Add(hit.transform.name[0] + "  ����");
                    
                }
            }

            allStrings = "";
            foreach (string s in replayContentString)
            {
                allStrings = string.Join("\n", replayContentString);
            }

            replayContent.text = allStrings;

            

            turn = !turn ;

        }
    }


}


