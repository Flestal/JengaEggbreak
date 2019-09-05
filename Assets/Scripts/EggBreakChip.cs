using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggBreakChip : MonoBehaviour
{
    [SerializeField] private GameObject Board;
    [SerializeField] private bool isRed;

    [SerializeField] private GameObject gameManager;
    private Manager manager;
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
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == Board)
        {
            Debug.Log("Collision eggbreak");
            
            Destroy(this.gameObject);
        }
    }
}
