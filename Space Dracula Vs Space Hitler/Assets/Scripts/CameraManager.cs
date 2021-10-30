using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.LookAt(target);
    }

    private void LateUpdate()
    {
        if(target.transform.position.x > -5.52 && target.transform.position.x < 19.53)
        {
            this.transform.position = new Vector3(target.transform.position.x, 1.23f, -30);
        }
        else if(target.transform.position.x < -5.52)
        {
            this.transform.position = new Vector3(-5.52f, 1.23f, -30);
        }else if(target.transform.position.x > 19.53) this.transform.position = new Vector3(19.532f, 1.23f, -30);

    }
}
