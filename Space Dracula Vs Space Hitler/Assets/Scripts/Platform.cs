using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public Rigidbody2D player;
    private PlatformEffector2D effector;
    public float waitTime;

    // Start is called before the first frame update
    void Start()
    {

        effector = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Vertical") == 0f)
        {
            waitTime = 0.05f;
        }
        if (Input.GetAxis("Vertical") < 0f)
        {
            if (waitTime <= 0)
            {
                effector.rotationalOffset = 180f;
                waitTime = 0.05f;
            } else waitTime -= Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            effector.rotationalOffset = 0f;
        }

    }
}
