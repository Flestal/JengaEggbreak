using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggBreakChip : MonoBehaviour
{
    //변수 선언 여러가지
    [SerializeField] private GameObject Board;
    [SerializeField] private bool isRed;//팀 구분

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
        manager = gameManager.gameObject.GetComponent<Manager>();//게임매니저 호출
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))//마우스 누를 때 클릭한 오브젝트 기록
        {
            t = ClickedObject();
            if (t != null)
            {
                if (t.Equals(gameObject) && (manager.EggBreak_RedTurn == this.isRed))
                {
                    Debug.Log(this.gameObject.transform.position.x+"/"+this.gameObject.transform.position.z);
                    this.isClicked = true;
                }
            }

        }
        if (Input.GetMouseButtonUp(0))//마우스 뗄 때 이동
        {
            t = ClickedObject();
            if (t != null)
            {
                if (t.Equals(Board))
                {
                    if (isClicked && (manager.EggBreak_RedTurn == this.isRed))
                    {//그 오브젝트가 클릭한 오브젝트인지 확인, 턴 확인
                        RaycastHit hit;
                        GameObject target = null;

                        if (manager.GetCurrentCamera(1))
                        {
                            Ray ray = manager.GetCamera(1).ScreenPointToRay(Input.mousePosition);
                            if ((Physics.Raycast(ray.origin, ray.direction * 10, out hit)) == true)
                            {
                                //클릭 위치에 따른 방향과 세기 설정
                                float xx = hit.point.x;
                                float zz = hit.point.z;
                                Vector3 direction = new Vector3((this.gameObject.transform.position.x-xx)*velocity,this.gameObject.transform.position.y+0.01f,(this.gameObject.transform.position.z-zz)*velocity);
                                this.gameObject.GetComponent<Rigidbody>().AddForce(direction, ForceMode.Impulse);
                            }
                        }
                        manager.EggBreak_RedTurn = !manager.EggBreak_RedTurn;//턴넘기기
                    }
                }
            }
            isClicked = false;//모든 알까기 칩 클릭 상태 초기화
        }
    }
    private GameObject ClickedObject()//클릭한 오브젝트 반환
    {
        RaycastHit hit;
        GameObject target = null;

        if (manager.GetCurrentCamera(1))//카메라가 알까기 화면을 비추고 있을 때
        {
            Ray ray = manager.GetCamera(1).ScreenPointToRay(Input.mousePosition);
            if ((Physics.Raycast(ray.origin, ray.direction * 10, out hit)) == true)
            {
                target = hit.collider.gameObject;
            }
            return target;//대충 레이캐스트 쏴서 알까기 칩 반환한다는 뜻
        }
        return null;
    }

    private void OnCollisionEnter(Collision collision)//칩이 바닥에 닿을 때
    {
        if (collision.gameObject == Board)
        {
            Debug.Log("Collision eggbreak");
            
            Destroy(this.gameObject);//그 칩 제거
        }
    }
    private void OnDestroy()//칩이 사라질 때
    {
        if (isRed)
        {
            manager.chipRed--;
        }else if (!isRed)
        {
            manager.chipBlue--;
        }//매니저에 칩 제거 전송
    }
}
