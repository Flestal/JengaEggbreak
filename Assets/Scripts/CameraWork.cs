using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWork : MonoBehaviour
{
    /*---------원형 이동---------*/
    [SerializeField]
    float fRange = 7;
    float maxRad = 360;
    float minRad = 0;
    [SerializeField]
    float fRad = 0;
    [SerializeField]
    float fSpeed = 30;
    [SerializeField]
    Vector3 v3MovePos = Vector3.zero;
    Transform transme = null;
    /*------------------------*/
    /*---------카메라 회전----------*/
    [SerializeField] Vector3 targetPos=Vector3.zero;
    /*--------------------------*/

    // Start is called before the first frame update
    void Start()
    {
        if (transme == null)
            transme = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (fRad > maxRad)
            fRad = minRad;
        else if (fRad < minRad)
            fRad = maxRad;
        if (Input.GetKey(KeyCode.A))//왼쪽 이동
        {
            fRad -= Time.deltaTime * fSpeed;
        }
        if (Input.GetKey(KeyCode.D))//오른쪽 이동
        {
            fRad += Time.deltaTime * fSpeed;
        }
        float deRad = fRad * Mathf.Deg2Rad;
        float sinVal = Mathf.Sin(deRad);
        float cosVal = Mathf.Cos(deRad);
        float x = sinVal * fRange;
        float z = cosVal * fRange;
        transform.localPosition = new Vector3(x, 0, z) + v3MovePos;
        /*------카메라 회전------*/
        Vector3 vec = targetPos - transform.position;
        vec.Normalize();
        Quaternion q = Quaternion.LookRotation(vec);
        this.gameObject.transform.rotation = q;
    }
}
