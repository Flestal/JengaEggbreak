﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    private static Manager m_Instance;

    [SerializeField] private Text text;
    public Camera[] m_AllCameras;
    int cameraNum = 0;
    private bool GameOverCalled = false;

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

    private void Awake()
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
        text.text = "";
    }
    public static Manager GetManager()
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
        if (GetCurrentCamera(1))
        {
            text.gameObject.SetActive(true);
        }
        else
        {
            text.gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            cameraNum=(cameraNum < m_AllCameras.Length-1 ? cameraNum+1 : 0);
            for (int i = 0; i < m_AllCameras.Length; i++)
            {
                m_AllCameras[i].gameObject.active = false;
            }
            m_AllCameras[cameraNum].gameObject.active = true;
        }
        if (!GameOverCalled)
        {
            if (chipRed == 0)
            {
                text.text = "Blue Wins!";
                GameOverCalled = true;
            }
            if (chipBlue == 0)
            {
                text.text = "Red Wins!";
                GameOverCalled = true;
            }
        }
        
    }
}
