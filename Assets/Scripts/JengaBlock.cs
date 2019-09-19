using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JengaBlock : MonoBehaviour
{
    //변수 선언 여러가지
    public bool isFloor = false;//바닥 블럭 확인
    public bool isTop = false;//꼭대기 블럭 확인(사용안함)
    public bool isPicked = false;//선택된 블럭 확인(사용안함)
    private GameObject t;
    

    [SerializeField] private GameObject Board;

    [SerializeField]private GameObject gameManager;
    private Manager manager;

    // Start is called before the first frame update
    void Start()
    {
        Board = GameObject.FindGameObjectWithTag("Board");
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        manager = gameManager.gameObject.GetComponent<Manager>();//게임매니저 호출
        Debug.Log(manager.GetCurrentCamera(0));
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0))//젠가 블럭을 클릭하면 그 블럭 제거
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
        if (manager.turnPre==manager.isPlayer1Turn)//턴 조건
        {
            Destroy(gameObject);
            manager.isPlayer1Turn = !manager.isPlayer1Turn;//제거할때 턴넘기기
            Debug.Log(manager.isPlayer1Turn);
            return true;
        }
        return false;
    }
    private GameObject ClickedObject()//클릭한 오브젝트 반환
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
    

    private void OnCollisionEnter(Collision collision)//충돌 이벤트
    {
        if (collision.gameObject == Board)//바닥이랑 충돌
        {
            if (!(isFloor))//바닥 블럭이 아니면 게임오버
            {
                Debug.Log("Game Over");
                Board.gameObject.GetComponent<JengaBoard>().GameOver(manager.isPlayer1Turn);
            }
        }
        else if (collision.gameObject.tag == this.gameObject.tag)//자기들끼리 충돌했을 때 정지(그냥 넣어본거)
        {
            this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
