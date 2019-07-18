using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JengaBlock : MonoBehaviour
{
    public bool isFloor = false;
    public bool isTop = false;
    public bool isPicked = false;
    private GameObject t;

    [SerializeField] private GameObject Board;
    [SerializeField] private GameObject text;

    // Start is called before the first frame update
    void Start()
    {
        Board = GameObject.FindGameObjectWithTag("Board");
        text = GameObject.FindGameObjectWithTag("GameOverText");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            t = ClickedObject();
            if (t != null)
            {
                if (t.Equals(gameObject))
                {
                    Destroy(gameObject);
                }
            }
            
        }
    }
    private GameObject ClickedObject()
    {
        RaycastHit hit;
        GameObject target = null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if((Physics.Raycast(ray.origin,ray.direction*10,out hit)) == true)
        {
            target = hit.collider.gameObject;
        }
        return target;
    }
    private void GameOver()
    {
        text.active = true;
        Time.timeScale = 0.0f;
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == Board)
        {
            Debug.Log("Collision");
            if (!(isFloor && isPicked))
            {
                Debug.Log("Game Over");
                GameOver();
            }
        }
    }
}
