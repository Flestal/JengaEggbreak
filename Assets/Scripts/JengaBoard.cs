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

    [SerializeField] private GameObject[] Blocks;

    // Start is called before the first frame update
    void Start()
    {
        text.text = "";
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        manager = gameManager.gameObject.GetComponent<Manager>();
        text.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("GamePlay");
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
    /*public void Board_Shuffle()
    {
        Debug.Log("Shuffle");
        double cooltime = 0.7f;
        double timeSpan = 0;
        timeSpan += Time.deltaTime;
        double r = Random.value;
        if (timeSpan > cooltime)  // 경과 시간이 특정 시간이 보다 커졋을 경우
        {
            timeSpan = 0;
            return;
        }
        else
        {
            if (r < 0.5)
                this.gameObject.transform.Rotate(Vector3.down * Time.deltaTime);
            else
                this.gameObject.transform.Rotate(Vector3.up*Time.deltaTime);
        }
    }*/

    public IEnumerator Board_Shuffle()
    {
        Debug.Log("shuffle");
        double setTime = 0.15f;
        double timeSpan = 0;
        double r = Random.value;
        while (timeSpan < setTime)
        {
            if(r<0.5)
                this.gameObject.transform.Rotate(Vector3.forward * Random.Range(-10, 10) * Time.deltaTime);
            else
                this.gameObject.transform.Rotate(Vector3.left * Random.Range(-10, 10) * Time.deltaTime);

            timeSpan += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
        this.gameObject.transform.eulerAngles = new Vector3(0,0,0);
    }
    public void BlockDestroy_AI()
    {
        if (manager.isPlayer1Turn==false)
        {
            int r = Random.Range(0, Blocks.Length);
            while (Blocks[r] == null)
            {
                r = Random.Range(0, Blocks.Length);
            }
            Destroy(Blocks[r],Random.Range(1.5f,5.5f));
            StartCoroutine("Board_Shuffle");
            manager.isPlayer1Turn = true;
        }
    }
}
