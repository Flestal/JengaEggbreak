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

    [SerializeField]private GameObject gameManager;
    private Manager manager;

    // Start is called before the first frame update
    void Start()
    {
        Board = GameObject.FindGameObjectWithTag("Board");
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        manager = gameManager.gameObject.GetComponent<Manager>();
        Debug.Log(manager.GetCurrentCamera(0));
        
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
                    BlockDestroy();
                }
            }
            
        }
    }
    public bool BlockDestroy()
    {
        if (true)//턴 조건
        {
            Destroy(gameObject);
            manager.isPlayer1Turn = !manager.isPlayer1Turn;
            Debug.Log(manager.isPlayer1Turn);
            return true;
        }
        return false;
    }
    private GameObject ClickedObject()
    {
        RaycastHit hit;
        GameObject target = null;

        if (manager.GetCurrentCamera(0))
        {
            Ray ray = manager.GetCamera(0).ScreenPointToRay(Input.mousePosition);
            if((Physics.Raycast(ray.origin,ray.direction*10,out hit)) == true)
            {
                target = hit.collider.gameObject;
            }
            return target;
        }
        return null;
    }
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == Board)
        {
            if (!(isFloor))
            {
                Debug.Log("Game Over");
                Board.gameObject.GetComponent<JengaBoard>().GameOver(manager.isPlayer1Turn);
            }
        }
        else if (collision.gameObject.tag == this.gameObject.tag)
        {
            this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
