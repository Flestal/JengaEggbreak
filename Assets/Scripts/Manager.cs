using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    //변수 여러가지 선언
    public static Manager m_Instance;

    [SerializeField] private Text text;
    [SerializeField] private Text turn;
    public Camera[] m_AllCameras;
    int cameraNum = 0;
    private bool GameOverCalled = false;
    double timeSpan = 0f;
    double cooltime = 0.5f;
    public bool isTurn1_Jenga=false;//젠가에서 선 플레이어 구분
    public bool isTurn1_EggBreak = false;//알까기에서 선 플레이어 구분
    public bool isPlayer1Turn = true;

    public int chipRed
    {
        get;
        set;
    }
    public int chipBlue
    {
        get;
        set;
    }

    
    public bool EggBreak_RedTurn
    {
        get;
        set;
    }
    public bool turnPre
    {
        get;
        set;
    }
    public bool clickAble
    {
        get;
        set;
    }

    private void Awake()//게임 시작 시 초기화
    {
        m_Instance = new Manager();
        for(int i = 0; i < m_AllCameras.Length; i++)
        {
            m_AllCameras[i].gameObject.SetActive(false);
        }
        m_AllCameras[cameraNum].gameObject.SetActive(true);
        //Debug.Log(m_AllCameras[0].gameObject.active);
        chipRed = 2;
        chipBlue = 2;
        isPlayer1Turn = true;
        EggBreak_RedTurn = true;
        text.text = "";
        turnPre = false;
        clickAble = true;
    }
    private void Update()
    {
        
    }
    public static Manager GetManager()//싱글톤
    {
        if (m_Instance==null)
            m_Instance = new Manager();
        return m_Instance;
    }
    private void FixedUpdate()
    {
        turn.text = "";
        if (isPlayer1Turn)
        {
            turn.text += "\n당신은 플레이어 1입니다.";
        }
        else
        {
            turn.text += "\n당신은 플레이어 2입니다.";
        }
    }
}
