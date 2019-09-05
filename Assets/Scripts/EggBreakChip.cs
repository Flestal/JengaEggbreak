using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggBreakChip : MonoBehaviour
{
    [SerializeField] private GameObject Board;
    [SerializeField] private bool isRed;

    [SerializeField] private GameObject gameManager;
    private Manager manager;

    private GameObject t;
    private bool isClicked = false;
    private float velocity = 10;

    // Start is called before the first frame update
    void Start()
    {
        Board = GameObject.FindGameObjectWithTag("Board");
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        manager = gameManager.gameObject.GetComponent<Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            t = ClickedObject();
            if (t != null)
            {
                if (t.Equals(gameObject))
                {
                    Debug.Log(this.gameObject.transform.position.x+"/"+this.gameObject.transform.position.z);
                    this.isClicked = true;
                }
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            t = ClickedObject();
            if (t != null)
            {
                if (t.Equals(Board))
                {
                    if (isClicked)
                    {
                        RaycastHit hit;
                        GameObject target = null;

                        if (manager.GetCurrentCamera(1))
                        {
                            Ray ray = manager.GetCamera(1).ScreenPointToRay(Input.mousePosition);
                            if ((Physics.Raycast(ray.origin, ray.direction * 10, out hit)) == true)
                            {
                                float xx = hit.point.x;
                                float zz = hit.point.z;
                                //Debug.Log(xx + "|" + zz);
                                Vector3 direction = new Vector3((this.gameObject.transform.position.x-xx)*velocity,this.gameObject.transform.position.y+0.01f,(this.gameObject.transform.position.z-zz)*velocity);
                                this.gameObject.GetComponent<Rigidbody>().AddForce(direction, ForceMode.Impulse);
                            }
                        }
                        isClicked = false;
                    }
                }
            }

        }
    }
    private GameObject ClickedObject()
    {
        RaycastHit hit;
        GameObject target = null;

        if (manager.GetCurrentCamera(1))
        {
            Ray ray = manager.GetCamera(1).ScreenPointToRay(Input.mousePosition);
            if ((Physics.Raycast(ray.origin, ray.direction * 10, out hit)) == true)
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
            Debug.Log("Collision eggbreak");
            
            Destroy(this.gameObject);
        }
    }
    private void OnDestroy()
    {
        if (isRed)
        {
            manager.chipRed--;
        }else if (!isRed)
        {
            manager.chipBlue--;
        }
    }
}
