using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggBreakCameraWork : MonoBehaviour
{
    [SerializeField] private GameObject Red1;
    [SerializeField] private GameObject Red2;
    [SerializeField] private GameObject Blue1;
    [SerializeField] private GameObject Blue2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        short cnt = 0;
        float xx = 0, yy = 0, zz = 0;
        if (Red1)
        {
            xx += Red1.gameObject.transform.position.x;
            yy += Red1.gameObject.transform.position.y;
            zz += Red1.gameObject.transform.position.z;
            cnt++;
        }
        if (Red2)
        {
            xx += Red2.gameObject.transform.position.x;
            yy += Red2.gameObject.transform.position.y;
            zz += Red2.gameObject.transform.position.z;
            cnt++;
        }
        if (Blue1)
        {
            xx += Blue1.gameObject.transform.position.x;
            yy += Blue1.gameObject.transform.position.y;
            zz += Blue1.gameObject.transform.position.z;
            cnt++;
        }
        if (Blue2)
        {
            xx += Blue2.gameObject.transform.position.x;
            yy += Blue2.gameObject.transform.position.y;
            zz += Blue2.gameObject.transform.position.z;
            cnt++;
        }
        if (cnt <= 0)
        {
            xx = this.gameObject.transform.position.x;
            yy = this.gameObject.transform.position.y-5;
            zz = this.gameObject.transform.position.z;
        }
        else
        {
            xx /= cnt;
            yy /= cnt;
            zz /= cnt;
        }
        this.gameObject.transform.position = new Vector3(xx, yy + 5, zz);
    }
}
