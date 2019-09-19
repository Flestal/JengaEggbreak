using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    //변수 여러가지 선언
    private static Manager m_Instance;

    [SerializeField] private Text text;
    public Camera[] m_AllCameras;
    int cameraNum = 0;
    private bool GameOverCalled = false;
    double timeSpan = 0f;
    double cooltime = 0.5f;

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

    public bool isPlayer1Turn
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
            m_AllCameras[i].gameObject.active = false;
        }
        m_AllCameras[cameraNum].gameObject.active = true;
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
        if (turnPre != isPlayer1Turn)
        {
            timeSpan += Time.deltaTime;  // 경과 시간을 계속 등록
            if (timeSpan > cooltime)  // 경과 시간이 특정 시간이 보다 커졋을 경우
            {
                turnPre = isPlayer1Turn;
                timeSpan = 0;
            }
        }
    }
    public static Manager GetManager()//싱글톤
    {
        if (m_Instance==null)
        m_Instance = new Manager();
        return m_Instance;
    }
    public Camera GetCamera(int number)//젠가 0번 알까기 1번
    {
        if (null == m_AllCameras)
        {
            m_AllCameras = Camera.allCameras;           
        }
        return m_AllCameras[number];

    }

    public bool GetCurrentCamera(int number)
    {

        return m_AllCameras[number].gameObject.active;
    }
    private void FixedUpdate()
    {
        if (GetCurrentCamera(1))//알까기 게임오버 텍스트
        {
            text.gameObject.SetActive(true);
        }
        else
        {
            text.gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Space))//스페이스 바를 통해 카메라 전환(테스트용)
        {
            cameraNum=(cameraNum < m_AllCameras.Length-1 ? cameraNum+1 : 0);
            for (int i = 0; i < m_AllCameras.Length; i++)
            {
                m_AllCameras[i].gameObject.active = false;
            }
            m_AllCameras[cameraNum].gameObject.active = true;
        }
        if (!GameOverCalled)//게임 진행 도중 알까기 칩이 사라질 때
        {
            if (chipRed == 0)
            {
                text.text = "Blue Wins!";//게임 오버 선언
                GameOverCalled = true;
            }
            if (chipBlue == 0)
            {
                text.text = "Red Wins!";//게임 오버 선언
                GameOverCalled = true;
            }
        }
        
    }
}
