using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class JengaBoard : MonoBehaviour
{
    [SerializeField] private Text text;
    private bool GameOverCalled=false;

    [SerializeField] private GameObject gameManager;
    private Manager manager;

    // Start is called before the first frame update
    void Start()
    {
        text.text = "";
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        manager = gameManager.gameObject.GetComponent<Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (manager.GetCurrentCamera(0))
        {
            text.gameObject.SetActive(true);
        }
        else
        {
            text.gameObject.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void GameOver(bool Turn)
    {
        if (!GameOverCalled)
        {
            string tex = "Game Over\n";
            if (Turn)
            {
                tex += "Player 1 wins!";
            }
            else
            {
                tex += "Player 2 wins!";
            }
            tex += "\nPress R to restart";
            text.text = tex;
            GameOverCalled = true;
        }
        
    }
}
